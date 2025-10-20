using UnityEngine;

/// <summary>
/// ควบคุมการทำงานของอาวุธพื้นฐาน (ยิงโปรเจกไทล์หาศัตรูที่ใกล้ที่สุด)
/// </summary>
public class WeaponController : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab ของกระสุน
    public float fireRate = 1f;         // ยิงทุกๆ กี่วินาที
    public float weaponRange = 10f;     // ระยะทำการของอาวุธ

    private float fireTimer;            // ตัวนับเวลายิง

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
        // ค้นหาศัตรูที่ใกล้ที่สุด
        Transform nearestEnemy = FindNearestEnemy();

        // ถ้ายิงได้ (มีศัตรูในระยะ)
        if (nearestEnemy != null)
        {
            // สร้างกระสุนขึ้นมา
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // ส่งค่าเป้าหมายไปให้สคริปต์ของกระสุน
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
        float closestDistanceSqr = weaponRange * weaponRange; // ใช้ Sqr เพื่อประสิทธิภาพ
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
