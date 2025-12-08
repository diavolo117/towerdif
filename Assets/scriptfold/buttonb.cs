using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    private UpgradeManager upgradeManager;

    public int levelNumber = 1;
    public Button button;
    public CanvasGroup lockedVisual;

    void Start()
    {
        upgradeManager = UpgradeManager.Instance;

        bool unlocked = upgradeManager.IsLevelUnlocked(levelNumber);

        button.interactable = unlocked;

        if (lockedVisual != null)
            lockedVisual.alpha = unlocked ? 0f : 0.6f;
    }

    public void OnClickLoadLevel()
    {
        upgradeManager.LoadLevel(levelNumber);
        Debug.Log("Loading level " + levelNumber);
    }
}

