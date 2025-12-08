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

        Debug.Log("Волна завершена!");
        waveInProgress = false;

        yield return new WaitForSeconds(wave.timeBeforeNextWave);

        // конец боя → снова Build
        GameStateManager.Instance.EndBattle();
    }

    
}