using UnityEngine;
using TMPro;

public class UpgradeUIController : MonoBehaviour
{
    [Header("Projectile UI")]
    public TextMeshProUGUI projLevelUI;
    public TextMeshProUGUI projCostUI;
    public TextMeshProUGUI projStatsUI;

    [Header("AOE UI")]
    public TextMeshProUGUI aoeLevelUI;
    public TextMeshProUGUI aoeCostUI;
    public TextMeshProUGUI aoeStatsUI;

    [Header("Slow UI")]
    public TextMeshProUGUI slowLevelUI;
    public TextMeshProUGUI slowCostUI;
    public TextMeshProUGUI slowStatsUI;

    [Header("Money UI")]
    public TextMeshProUGUI moneyUI;

    private void OnEnable()
    {
        RefreshUI();
    }
    public void OnBuyProjectile()
    {
        if (UpgradeManager.Instance.TryBuyProjectile())
            RefreshUI();
    }

    public void OnBuyAOE()
    {
        if (UpgradeManager.Instance.TryBuyAOE())
            RefreshUI();
    }

    public void OnBuySlow()
    {
        if (UpgradeManager.Instance.TryBuySlow())
            RefreshUI();
    }

    public void RefreshUI()
    {
        var u = UpgradeManager.Instance;

        // ==== Projectile ====
        projLevelUI.text = $"Level {u.projectileLevel + 1}/5";
        if (u.projectileLevel >= 4)
        {
            projCostUI.text = "MAX";
            projStatsUI.text = "No more upgrades";
        }
        else
        {
            var next = u.projectileLevels[u.projectileLevel];
            projCostUI.text = $"Cost: {next.cost}";
            projStatsUI.text = $"+Damage: {next.projDamage}\n+Rate: {next.projFireRate}";
        }

        // ==== AOE ====
        aoeLevelUI.text = $"Level {u.aoeLevel + 1}/5";
        if (u.aoeLevel >= 4)
        {
            aoeCostUI.text = "MAX";
            aoeStatsUI.text = "No more upgrades";
        }
        else
        {
            var next = u.aoeLevels[u.aoeLevel];
            aoeCostUI.text = $"Cost: {next.cost}";
            aoeStatsUI.text = $"+Damage: {next.aoeDamage}";
        }

        // ==== Slow ====
        slowLevelUI.text = $"Level {u.slowLevel + 1}/5";
        if (u.slowLevel >= 4)
        {
            slowCostUI.text = "MAX";
            slowStatsUI.text = "No more upgrades";
        }
        else
        {
            var next = u.slowLevels[u.slowLevel];
            slowCostUI.text = $"Cost: {next.cost}";
            slowStatsUI.text = $"+Duration: {next.slowDuration}\n+Rate: {next.slowFireRate}";
        }

        // ==== Money ====
        moneyUI.text = $"Money: {u.money}";
    }

    // ===== ймнойх онйсойх =====

    public void BuyProjectile()
    {
        if (UpgradeManager.Instance.TryBuyProjectile())
            RefreshUI();
    }

    public void BuyAOE()
    {
        if (UpgradeManager.Instance.TryBuyAOE())
            RefreshUI();
    }

    public void BuySlow()
    {
        if (UpgradeManager.Instance.TryBuySlow())
            RefreshUI();
    }
}
