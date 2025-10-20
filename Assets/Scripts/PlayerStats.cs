using UnityEngine;
using UnityEngine.UI; // ����Ѻ�������͡Ѻ UI

/// <summary>
/// �Ѵ��ä��ʶҹе�ҧ� �ͧ������ �� HP, XP, Level
/// </summary>
public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public int level = 1;
    public float currentXp = 0;
    public float xpToNextLevel = 100;

    // public Slider healthSlider; // ���ҧ����������͡Ѻ UI Slider � Inspector
    // public Slider xpSlider;
    // public Text levelText;

    private float damageCooldown = 1f; // ���ҷ���������Ѻ�������ѧⴹ����
    private float lastDamageTime;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    // �ѧ��ѹ�Ѻ�����������
    public void TakeDamage(int damage)
    {
        // ��Ǩ�ͺ Cooldown
        if (Time.time < lastDamageTime + damageCooldown)
        {
            return; // �ѧ����㹪�ǧ Cooldown, ����Ѻ�����
        }

        lastDamageTime = Time.time;
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Game Over!");
            // ����� Game Over �����
        }
        UpdateUI();
    }

    // �ѧ��ѹ���Ѻ XP
    public void GainXp(float amount)
    {
        currentXp += amount;
        if (currentXp >= xpToNextLevel)
        {
            LevelUp();
        }
        UpdateUI();
    }

    // �ѧ��ѹ�����������Ѿ
    private void LevelUp()
    {
        level++;
        currentXp = currentXp - xpToNextLevel; // ��� XP ��ǹ�Թ�����
        xpToNextLevel *= 1.5f; // ���� XP ����ͧ�������ŶѴ�

        Debug.Log("Level Up! Current Level: " + level);
        // ���������Ѻ˹�ҵ�ҧ���͡�ѻ�ô���ظ/ʡ�ŷ����
    }

    // �ѻവ UI
    private void UpdateUI()
    {
        // if (healthSlider != null) healthSlider.value = (float)currentHealth / maxHealth;
        // if (xpSlider != null) xpSlider.value = currentXp / xpToNextLevel;
        // if (levelText != null) levelText.text = "Lv: " + level;
    }
}
