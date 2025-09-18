using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public GameState currentState = GameState.Build;

    [Header("UI")]
    public Button startBattleButton;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        startBattleButton.onClick.AddListener(StartBattle);
        UpdateUI();
    }

    void UpdateUI()
    {
        startBattleButton.gameObject.SetActive(currentState == GameState.Build);
    }

    public void StartBattle()
    {
        currentState = GameState.Battle;
        UpdateUI();
        WaveManager.Instance.StartNextWave();
    }

    public void EndBattle()
    {
        currentState = GameState.Build;
        UpdateUI();
        // выдаём награду
        NewMonoBehaviourScript.Instance.AddGold(100);
    }
}
