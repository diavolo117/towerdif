using UnityEngine;

public class EnemyMoverSpline : MonoBehaviour
{
    public pathscript path;
    public float speed = 2f;
    public float rotateSpeed = 5f;

    private float t = 0f; // параметр движения по пути
    private int segment = 0; // текущий сегмент кривой
    
    public float maxHealth = 50f;

    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("тик урона");
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // Смерть врага
    private void Die()
    {
        Destroy(gameObject);
    }

    private float currentHealth;
    void Update()
    {
        if (path == null || path.points.Length < 2) return;

        // позиции для кривой
        Vector3 p0 = GetPoint(segment - 1);
        Vector3 p1 = GetPoint(segment);
        Vector3 p2 = GetPoint(segment + 1);
        Vector3 p3 = GetPoint(segment + 2);

        // движение вдоль сегмента
        t += speed * Time.deltaTime / Vector3.Distance(p1, p2);

        if (t > 1f)
        {
            t = 0f;
            segment++;
            if (segment >= path.points.Length - 2)
            {
                Destroy(gameObject); // конец пути
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

    Vector3 GetPoint(int index)
    {
        if (index < 0) return path.points[0].position;
        if (index >= path.points.Length) return path.points[path.points.Length - 1].position;
        return path.points[index].position;
    }

    // формула Catmull-Rom
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

