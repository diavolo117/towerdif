using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    public int rewardForWin = 500;   // сколько денег дать за победу
    
    public void GameOver(bool playerWon)
    {
        if (playerWon)
        {
            Debug.Log("Игрок победил!");

            var upgrades = UpgradeManager.Instance;
            int currentLevel = upgrades.currentLevel;
            var upgradeManager = UpgradeManager.Instance;
            if (upgradeManager != null)
            {
                upgradeManager.money += rewardForWin;
                upgradeManager.UnlockNextLevel(currentLevel++);
                Debug.Log($"Начислено {rewardForWin} денег. Теперь у игрока: {upgradeManager.money}");
            }
            else
            {
                Debug.LogError("UpgradeManager не найден, награда не выдана");
            }
        }
        LastGameResult.PlayerWon = playerWon;

        // Переход в меню
        SceneManager.LoadScene("lvlpickup");
    }

}
