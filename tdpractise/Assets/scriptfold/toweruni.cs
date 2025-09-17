using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject hitbox; // ���� �������������� Hitbox-������
    public float tickInterval = 1f; // ����� ����� "�������"
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= tickInterval)
        {
            // �������� ������� �� �������� �����
            StartCoroutine(ActivateHitbox());
            timer = 0f;
        }
    }

    private System.Collections.IEnumerator ActivateHitbox()
    {
        hitbox.SetActive(true);
        yield return new WaitForSeconds(0.1f); // ������� �������� ���� �������
        hitbox.SetActive(false);
    }
}