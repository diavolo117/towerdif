using UnityEngine;

public class TowerSlow : MonoBehaviour
{
    public float slowMultiplier = 0.5f; // �������� 50% ��������
    public float slowDuration = 2f;

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
        // ����� ��������� ���������� ������ N ������, ���� ����� ���������� ��������
        EnemyMoverSpline enemy = other.GetComponent<EnemyMoverSpline>();
        if (enemy != null)
        {
            enemy.ApplySlow(slowMultiplier, slowDuration);
            Debug.Log("it worked");
        }
    }
}