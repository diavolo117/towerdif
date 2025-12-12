using UnityEngine;
using static UpgradeManager;

public class Tower : MonoBehaviour
{
    public GameObject hitbox; // Сюда перетаскиваешь Hitbox-объект
    public float tickInterval = 1f; // Время между "ударами"
    private float timer;
    private UpgradeManager upgradeManager;
    private UpgradeManager.UpgradeLevel upgrade;
    public AudioSource audioSource;
    public AudioClip soundClip;
    private float finalFireRate;
    void Start()
    {
        upgradeManager = Object.FindAnyObjectByType<UpgradeManager>();
        upgrade = UpgradeManager.Instance.GetSlowUpgrade();
        ApplyUpgrades();
    }


    public void ApplyUpgrades()
    {
        finalFireRate = tickInterval + upgrade.slowFireRate;
        Debug.Log(finalFireRate);

    }


    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= finalFireRate)
        {
            audioSource.PlayOneShot(soundClip);

            StartCoroutine(ActivateHitbox());
            timer = 0f;
        }
    }

    private System.Collections.IEnumerator ActivateHitbox()
    {
        hitbox.SetActive(true);
        yield return new WaitForSeconds(0.1f); // хитбокс работает долю секунды
        hitbox.SetActive(false);
    }
}