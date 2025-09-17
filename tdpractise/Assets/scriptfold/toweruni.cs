using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject hitbox; // Сюда перетаскиваешь Hitbox-объект
    public float tickInterval = 1f; // Время между "ударами"
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= tickInterval)
        {
            // Включаем хитбокс на короткое время
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