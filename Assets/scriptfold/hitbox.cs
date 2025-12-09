using UnityEngine;

public class TowerHitbox : MonoBehaviour
{
    public float basedamage = 10;
    private float damage;
    private UpgradeManager upgradeManager;
    private UpgradeManager.UpgradeLevel upgrades;
    private void Start()
    {
        upgradeManager = Object.FindAnyObjectByType<UpgradeManager>();
        upgrades = UpgradeManager.Instance.GetAOEUpgrade();
        ApplyUpgrades();
    }
    private void ApplyUpgrades()
    {
        
        damage = basedamage + upgrades.aoeDamage;
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyMoverSpline enemy = other.GetComponent<EnemyMoverSpline>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}
