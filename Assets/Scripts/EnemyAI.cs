using UnityEngine;

/// <summary>
/// ʤ�Ի������Ѻ�Ǻ����ĵԡ����ͧ�ѵ��
/// </summary>
public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;    // �������Ǣͧ�ѵ��
    public int damage = 10;         // ��ѧ���բͧ�ѵ��
    public int health = 100;        // ��ѧ���Ե�ͧ�ѵ��
    public GameObject xpGemPrefab;  // Prefab �ͧ��� XP ���д�ͻ

    private Transform player;       // ���˹觢ͧ������

    void Start()
    {
        // ���� GameObject ����� Tag "Player" ������㹡�õԴ���
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        // ����Ҽ������� �������͹��������
        if (player != null)
        {
            // �ӹǳ��ȷҧ��ѧ������
            Vector2 direction = (player.position - transform.position).normalized;
            // ����͹���仵����ȷҧ���
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    // �ѧ��ѹ����Ѻ�Ѻ�����������
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    // �ѧ��ѹ������ѵ�ٵ��
    private void Die()
    {
        // ���ҧ��� XP ����� � ���˹觷����
        if (xpGemPrefab != null)
        {
            Instantiate(xpGemPrefab, transform.position, Quaternion.identity);
        }
        // ����� GameObject �ͧ�ѵ�ٷ��
        Destroy(gameObject);
    }

    // ��Ǩ�ͺ��ê��Ѻ������
    private void OnCollisionStay2D(Collision2D collision)
    {
        // ��Ҫ��Ѻ GameObject ����� Tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // �ͧ�֧ʤ�Ի�� PlayerStats ���������ҧ�����������
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
            }
        }
    }
}
