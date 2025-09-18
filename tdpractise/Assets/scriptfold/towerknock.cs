using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float rotationStep = 90f;    // ��� ��������
    public bool rotateOnClick = true;
    private float fireCooldown = 0f;

    void Update()
    {
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
    }

    void Shoot()
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        proj.GetComponent<Projectile>().Init(firePoint.forward);
    }
    private void OnMouseDown()
    {
        if (!rotateOnClick && GameStateManager.Instance.currentState == GameState.Build) return;

        // �����������: ����� �������� �������� ��������� ���� (��������, ������ � build ������)
        // if (GameStateManager.Instance.CurrentState != GameState.Build) return;

        Rotate90();
    }

    public void Rotate90()
    {
        transform.Rotate(Vector3.up, rotationStep, Space.World);
        // ����� �������� ��������� ������ ���� �� ��������� ������� rotationStep,
        // ����� �� ���� ���������� �����������
        SnapRotationToStep();
    }

    private void SnapRotationToStep()
    {
        Vector3 e = transform.eulerAngles;
        float snappedY = Mathf.Round(e.y / rotationStep) * rotationStep;
        transform.eulerAngles = new Vector3(e.x, snappedY, e.z);
    }

    // ������� ������: � UI ����� �������� RotateLeft/RotateRight
    public void RotateLeft() => Rotate90();            // �� ��������� +90 (������)
    public void RotateRight() => Rotate90();
}
