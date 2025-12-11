using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;
    [SerializeField] private pathscript defaultPath; // назначь в инспекторе
    public Transform spawnPoint;
    public Wave[] waves;
    

    private int currentWaveIndex = -1;
    private bool waveInProgress = false;

    void Awake()
    {
        Instance = this;
    }
    private void SpawnEnemy(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

        EnemyMoverSpline enemy = obj.GetComponent<EnemyMoverSpline>();
        if (enemy != null)
        {
            enemy.path = defaultPath;
            EnemyMoverSpline.aliveEnemies++;   // <--- ДОБАВИЛИ
        }
    }

    public void StartNextWave()
    {
        if (waveInProgress) return;

        currentWaveIndex++;
        if (currentWaveIndex < waves.Length)
        {
            StartCoroutine(RunWave(waves[currentWaveIndex]));
        }
        else
        {
            Debug.Log("Все волны закончились!");
            GameStateManager.Instance.EndBattle();
        }
    }

    private IEnumerator RunWave(Wave wave)
    {
        waveInProgress = true;
        Debug.Log("Началась волна " + (currentWaveIndex + 1));

        foreach (var enemyGroup in wave.enemies)
        {
            for (int i = 0; i < enemyGroup.count; i++)
            {
                SpawnEnemy(enemyGroup.enemyPrefab);
                yield return new WaitForSeconds(enemyGroup.delayBetween);
            }
        }

        // --- ВСЕ ВРАГИ ПОРОЖДЕНЫ, ОЖИДАЕМ ИХ СМЕРТИ ---
        while (EnemyMoverSpline.aliveEnemies > 0)
        {
            yield return null; // ждём кадр
        }

        Debug.Log("Волна полностью зачищена!");

        // волна окончена
        waveInProgress = false;

        yield return new WaitForSeconds(wave.timeBeforeNextWave);

        // если это последняя волна – победа
        if (currentWaveIndex >= waves.Length - 1)
        {
            Debug.Log("Все волны пройдены! Победа!");
            GameManager.Instance.GameOver(true);
        }
        else
        {
            GameStateManager.Instance.EndBattle(); // переход в Build или что у тебя там
        }

    }


}