using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage;
    public float basedamage = 20f;
    public float slowMultiplier = 0.5f; // например 50% скорости
    public float slowDuration = 2f;
    private Vector3 direction;
    private UpgradeManager upgradeManager;
    private UpgradeManager.UpgradeLevel upgrade;
    private void Start()
    {
        upgradeManager = Object.FindAnyObjectByType<UpgradeManager>();
        upgrade = UpgradeManager.Instance.GetProjectileUpgrade();
        ApplyUpgrades();
    }
    private void ApplyUpgrades()
    {
        upgrade = UpgradeManager.Instance.GetProjectileUpgrade();
        damage = basedamage + upgrade.projFireRate;
    }
    public void Init(Vector3 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyMoverSpline enemy = other.GetComponent<EnemyMoverSpline>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            enemy.ApplySlow(slowMultiplier, slowDuration);
            Destroy(gameObject);
        }
    }
}