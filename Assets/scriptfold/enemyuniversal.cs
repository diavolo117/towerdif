using UnityEngine;
using System.Collections;

public class EnemyMoverSpline : MonoBehaviour
{
    [Header("Path Settings")]
    public pathscript path;
    public float speed = 2f;
    public float rotateSpeed = 5f;
    public static int aliveEnemies = 0;
    private float t = 0f; // параметр движения по пути
    private int segment = 0; // текущий сегмент кривой

    [Header("Stats")]
    public float maxHealth = 50f;
    private float currentHealth;
    public float damageToBuilding = 10f;
    [Header("Slow Settings")]
    public bool isImmuneToSlow = false;
    private float currentSpeed;       // текущая скорость
    private Coroutine slowCoroutine;  // чтобы не наложилось несколько замедлений

    void Start()
    {
        currentHealth = maxHealth;
        currentSpeed = speed;
    }

    void Update()
    {
        if (path == null || path.points.Length < 2) return;

        // позиции для кривой
        Vector3 p0 = GetPoint(segment - 1);
        Vector3 p1 = GetPoint(segment);
        Vector3 p2 = GetPoint(segment + 1);
        Vector3 p3 = GetPoint(segment + 2);

        // движение вдоль сегмента
        t += currentSpeed * Time.deltaTime / Vector3.Distance(p1, p2);

        if (t > 1f)
        {
            t = 0f;
            segment++;
            if (segment >= path.points.Length - 2)
            {
                // 1. Находим здание
                BuildingHealth building = FindAnyObjectByType<BuildingHealth>();
                if (building != null && !building.isDead)
                {
                    building.TakeDamage(damageToBuilding);
                }

                EnemyMoverSpline.aliveEnemies--;
                
                Destroy(gameObject);
                return;
            }

        }

        // вычисляем позицию по Catmull-Rom
        Vector3 newPos = CatmullRom(p0, p1, p2, p3, t);

        // направление движения
        Vector3 dir = (newPos - transform.position).normalized;

        // перемещаем врага
        transform.position = newPos;

        // поворачиваем плавно
        if (dir != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, rotateSpeed * Time.deltaTime);
        }
    }

    // ========== БОЕВАЯ ЛОГИКА ==========

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Враг получил {damage} урона (осталось {currentHealth})");
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        EnemyMoverSpline.aliveEnemies--;
        Destroy(gameObject);
    }

    private Coroutine knockbackCoroutine;

    public void ApplyPathKnockback(float duration, float multiplier = 1f)
    {
        // Если враг иммунен к контролю — не даём нокбэк
        if (isImmuneToSlow)
            return;

        if (knockbackCoroutine != null)
            StopCoroutine(knockbackCoroutine);

        knockbackCoroutine = StartCoroutine(KnockbackRoutine(duration, multiplier));
    }


    private IEnumerator KnockbackRoutine(float duration, float multiplier)
    {
        float originalSpeed = currentSpeed;

        // Двигаемся назад
        currentSpeed = -Mathf.Abs(originalSpeed) * multiplier;

        yield return new WaitForSeconds(duration);

        // Возвращаем нормальную скорость
        currentSpeed = originalSpeed;
    }

    public void ApplySlow(float multiplier, float duration)
    {
        if (isImmuneToSlow) return;

        if (slowCoroutine != null) StopCoroutine(slowCoroutine);
        slowCoroutine = StartCoroutine(SlowEffect(multiplier, duration));
    }

    private IEnumerator SlowEffect(float multiplier, float duration)
    {
        currentSpeed = speed * multiplier;
        yield return new WaitForSeconds(duration);
        currentSpeed = speed;
    }

    // ========== ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ==========

    Vector3 GetPoint(int index)
    {
        if (index < 0) return path.points[0].position;
        if (index >= path.points.Length) return path.points[path.points.Length - 1].position;
        return path.points[index].position;
    }

    Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return 0.5f * (
            (2 * p1) +
            (-p0 + p2) * t +
            (2 * p0 - 5 * p1 + 4 * p2 - p3) * (t * t) +
            (-p0 + 3 * p1 - 3 * p2 + p3) * (t * t * t)
        );
    }
}