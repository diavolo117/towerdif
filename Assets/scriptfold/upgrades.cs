using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // ==================== ÓÐÎÂÍÈ ÎÏÃÐÅÉÄÎÂ ====================

    [System.Serializable]
    public class UpgradeLevel
    {
        public int cost;

        public float projDamage;
        public float projFireRate;

        public float aoeDamage;
        public float aoefirerate;

        public float slowDuration;
        public float slowFireRate;
    }

    public UpgradeLevel[] projectileLevels = new UpgradeLevel[5];
    public UpgradeLevel[] aoeLevels = new UpgradeLevel[5];
    public UpgradeLevel[] slowLevels = new UpgradeLevel[5];

    public int projectileLevel = 0;
    public int aoeLevel = 0;
    public int slowLevel = 0;

    public int money = 9999;

    // ==================== ÏÐÎÃÐÅÑÑ ÓÐÎÂÍÅÉ ====================

    // 3 óðîâíÿ — 1 îòêðûò âñåãäà
    public bool level1Unlocked = true;
    public bool level2Unlocked = false;
    public bool level3Unlocked = false;

    public bool IsLevelUnlocked(int level)
    {
        if (level == 1) return level1Unlocked;
        if (level == 2) return level2Unlocked;
        if (level == 3) return level3Unlocked;

        return false;
    }

    public void UnlockNextLevel(int currentLevel)
    {
        if (currentLevel == 1)
            level2Unlocked = true;

        if (currentLevel == 2)
            level3Unlocked = true;
    }

    public void LoadLevel(int levelNumber)
    {
        if (!IsLevelUnlocked(levelNumber))
        {
            Debug.Log("Level locked");
            return;
        }

        string sceneName = "Level" + levelNumber;
        Debug.Log("Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    // ==================== ÄÎÑÒÓÏ Ê ÀÏÃÐÅÉÄÀÌ ====================

    public UpgradeLevel GetProjectileUpgrade() => projectileLevels[projectileLevel];
    public UpgradeLevel GetAOEUpgrade() => aoeLevels[aoeLevel];
    public UpgradeLevel GetSlowUpgrade() => slowLevels[slowLevel];

    // ==================== ÏÎÊÓÏÊÈ ====================

    public bool TryBuyProjectile()
    {
        if (projectileLevel >= 4) return false;
        var next = projectileLevels[projectileLevel];
        if (money < next.cost) return false;
        money -= next.cost;
        projectileLevel++;
        return true;
    }

    public bool TryBuyAOE()
    {
        if (aoeLevel >= 4) return false;
        var next = aoeLevels[aoeLevel];
        if (money < next.cost) return false;
        money -= next.cost;
        aoeLevel++;
        return true;
    }

    public bool TryBuySlow()
    {
        if (slowLevel >= 4) return false;
        var next = slowLevels[slowLevel];
        if (money < next.cost) return false;
        money -= next.cost;
        slowLevel++;
        return true;
    }
}