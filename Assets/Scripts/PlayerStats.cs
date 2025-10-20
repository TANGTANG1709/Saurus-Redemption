using UnityEngine;
using UnityEngine.UI; // สำหรับเชื่อมต่อกับ UI

/// <summary>
/// จัดการค่าสถานะต่างๆ ของผู้เล่น เช่น HP, XP, Level
/// </summary>
public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public int level = 1;
    public float currentXp = 0;
    public float xpToNextLevel = 100;

    // public Slider healthSlider; // สร้างการเชื่อมต่อกับ UI Slider ใน Inspector
    // public Slider xpSlider;
    // public Text levelText;

    private float damageCooldown = 1f; // เวลาที่จะไม่ได้รับดาเมจหลังโดนโจมตี
    private float lastDamageTime;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    // ฟังก์ชันรับความเสียหาย
    public void TakeDamage(int damage)
    {
        // ตรวจสอบ Cooldown
        if (Time.time < lastDamageTime + damageCooldown)
        {
            return; // ยังอยู่ในช่วง Cooldown, ไม่รับดาเมจ
        }

        lastDamageTime = Time.time;
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Game Over!");
            // ใส่โค้ด Game Over ที่นี่
        }
        UpdateUI();
    }

    // ฟังก์ชันได้รับ XP
    public void GainXp(float amount)
    {
        currentXp += amount;
        if (currentXp >= xpToNextLevel)
        {
            LevelUp();
        }
        UpdateUI();
    }

    // ฟังก์ชันเมื่อเลเวลอัพ
    private void LevelUp()
    {
        level++;
        currentXp = currentXp - xpToNextLevel; // เอา XP ส่วนเกินไปใช้ต่อ
        xpToNextLevel *= 1.5f; // เพิ่ม XP ที่ต้องใช้ในเลเวลถัดไป

        Debug.Log("Level Up! Current Level: " + level);
        // ใส่โค้ดสำหรับหน้าต่างเลือกอัปเกรดอาวุธ/สกิลที่นี่
    }

    // อัปเดต UI
    private void UpdateUI()
    {
        // if (healthSlider != null) healthSlider.value = (float)currentHealth / maxHealth;
        // if (xpSlider != null) xpSlider.value = currentXp / xpToNextLevel;
        // if (levelText != null) levelText.text = "Lv: " + level;
    }
}
