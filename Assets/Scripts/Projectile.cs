using UnityEngine;

/// <summary>
/// สคริปต์สำหรับโปรเจกไทล์ (กระสุน)
/// </summary>
public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 20;
    public float lifetime = 5f; // เวลาก่อนกระสุนจะหายไปเอง

    private Vector2 moveDirection;

    void Start()
    {
        // ทำลายตัวเองเมื่อเวลาหมด
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // เคลื่อนที่ไปตามทิศทางที่กำหนด
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    // กำหนดทิศทางจาก WeaponController
    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
        // หมุนกระสุนให้หันไปในทิศทางที่ถูกต้อง
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward); // -90 ถ้า sprite ของคุณหันขึ้น
    }

    // ตรวจสอบการชนแบบ Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ถ้าชนกับศัตรู
        if (collision.CompareTag("Enemy"))
        {
            // สร้างความเสียหาย
            EnemyAI enemy = collision.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            // ทำลายกระสุนทิ้ง
            Destroy(gameObject);
        }
    }
}
