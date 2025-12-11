using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildingHealth : MonoBehaviour
{
    public float maxHealth = 200f;
    private float currentHealth;
    public bool isDead = false;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"Здание получило {amount} урона. Осталось: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        // Сигнал GameOver — делаем ПЕРВЫМ
        GameManager.Instance.GameOver(false);

        // Не уничтожаем сразу здание!
        // Оно должно существовать пока сцена живёт.
        // Можно скрыть визуал, если нужно, но сам компонент оставляем.
    }
}
