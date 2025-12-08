using UnityEngine;

public class TowerSlow : MonoBehaviour
{
    public float slowMultiplier = 0.5f; // например 50% скорости
    public float baseslowDuration;
    public float slowDuration = 2f;
    private UpgradeManager upgradeManager;
    private UpgradeManager.UpgradeLevel upgrade;
    private void Start()
    {
        upgradeManager = Object.FindAnyObjectByType<UpgradeManager>();
        upgrade = UpgradeManager.Instance.GetSlowUpgrade();
        ApplyUpgrades();
    }
    private void ApplyUpgrades()
    {
        
        slowDuration = baseslowDuration + upgrade.slowDuration;
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyMoverSpline enemy = other.GetComponent<EnemyMoverSpline>();
        if (enemy != null)
        {
            enemy.ApplySlow(slowMultiplier, slowDuration);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Можно обновлять замедление каждые N секунд, если нужно постоянное действие
        EnemyMoverSpline enemy = other.GetComponent<EnemyMoverSpline>();
        if (enemy != null)
        {
            enemy.ApplySlow(slowMultiplier, slowDuration);
            Debug.Log("it worked");
        }
    }
}