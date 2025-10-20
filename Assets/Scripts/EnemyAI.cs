using UnityEngine;

/// <summary>
/// สคริปต์สำหรับควบคุมพฤติกรรมของศัตรู
/// </summary>
public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;    // ความเร็วของศัตรู
    public int damage = 10;         // พลังโจมตีของศัตรู
    public int health = 100;        // พลังชีวิตของศัตรู
    public GameObject xpGemPrefab;  // Prefab ของเม็ด XP ที่จะดรอป

    private Transform player;       // ตำแหน่งของผู้เล่น

    void Start()
    {
        // ค้นหา GameObject ที่มี Tag "Player" เพื่อใช้ในการติดตาม
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        // ถ้าหาผู้เล่นเจอ ให้เคลื่อนที่เข้าหา
        if (player != null)
        {
            // คำนวณทิศทางไปยังผู้เล่น
            Vector2 direction = (player.position - transform.position).normalized;
            // เคลื่อนที่ไปตามทิศทางนั้น
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    // ฟังก์ชันสำหรับรับความเสียหาย
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    // ฟังก์ชันเมื่อศัตรูตาย
    private void Die()
    {
        // สร้างเม็ด XP ขึ้นมา ณ ตำแหน่งที่ตาย
        if (xpGemPrefab != null)
        {
            Instantiate(xpGemPrefab, transform.position, Quaternion.identity);
        }
        // ทำลาย GameObject ของศัตรูทิ้ง
        Destroy(gameObject);
    }

    // ตรวจสอบการชนกับผู้เล่น
    private void OnCollisionStay2D(Collision2D collision)
    {
        // ถ้าชนกับ GameObject ที่มี Tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // ลองดึงสคริปต์ PlayerStats มาเพื่อสร้างความเสียหาย
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
            }
        }
    }
}
