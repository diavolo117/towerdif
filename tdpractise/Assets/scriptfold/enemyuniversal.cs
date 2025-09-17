using UnityEngine;

public class EnemyMoverSpline : MonoBehaviour
{
    public pathscript path;
    public float speed = 2f;
    public float rotateSpeed = 5f;

    private float t = 0f; // �������� �������� �� ����
    private int segment = 0; // ������� ������� ������
    
    public float maxHealth = 50f;

    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("��� �����");
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // ������ �����
    private void Die()
    {
        Destroy(gameObject);
    }

    private float currentHealth;
    void Update()
    {
        if (path == null || path.points.Length < 2) return;

        // ������� ��� ������
        Vector3 p0 = GetPoint(segment - 1);
        Vector3 p1 = GetPoint(segment);
        Vector3 p2 = GetPoint(segment + 1);
        Vector3 p3 = GetPoint(segment + 2);

        // �������� ����� ��������
        t += speed * Time.deltaTime / Vector3.Distance(p1, p2);

        if (t > 1f)
        {
            t = 0f;
            segment++;
            if (segment >= path.points.Length - 2)
            {
                Destroy(gameObject); // ����� ����
                return;
            }
        }
        
        
        // ��������� ������� �� Catmull-Rom
        Vector3 newPos = CatmullRom(p0, p1, p2, p3, t);

        // ����������� ��������
        Vector3 dir = (newPos - transform.position).normalized;

        // ���������� �����
        transform.position = newPos;

        // ������������ ������
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

    // ������� Catmull-Rom
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

