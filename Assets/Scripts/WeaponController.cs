using UnityEngine;

/// <summary>
/// �Ǻ�����÷ӧҹ�ͧ���ظ��鹰ҹ (�ԧ��ਡ������ѵ�ٷ��������ش)
/// </summary>
public class WeaponController : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab �ͧ����ع
    public float fireRate = 1f;         // �ԧ�ء� ����Թҷ�
    public float weaponRange = 10f;     // ���зӡ�âͧ���ظ

    private float fireTimer;            // ��ǹѺ�����ԧ

    void Update()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0)
        {
            fireTimer = fireRate;
            Fire();
        }
    }

    private void Fire()
    {
        // �����ѵ�ٷ��������ش
        Transform nearestEnemy = FindNearestEnemy();

        // ����ԧ�� (���ѵ�������)
        if (nearestEnemy != null)
        {
            // ���ҧ����ع�����
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // �觤�������������ʤ�Ի��ͧ����ع
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                Vector2 direction = (nearestEnemy.position - transform.position).normalized;
                projectileScript.SetDirection(direction);
            }
        }
    }

    private Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform bestTarget = null;
        float closestDistanceSqr = weaponRange * weaponRange; // �� Sqr ���ͻ���Է���Ҿ
        Vector3 currentPosition = transform.position;

        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }
        return bestTarget;
    }
}
