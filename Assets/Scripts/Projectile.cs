using UnityEngine;

/// <summary>
/// ʤ�Ի������Ѻ��ਡ��� (����ع)
/// </summary>
public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 20;
    public float lifetime = 5f; // ���ҡ�͹����ع�������ͧ

    private Vector2 moveDirection;

    void Start()
    {
        // ����µ���ͧ������������
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // ����͹���仵����ȷҧ����˹�
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    // ��˹���ȷҧ�ҡ WeaponController
    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
        // ��ع����ع����ѹ�㹷�ȷҧ���١��ͧ
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward); // -90 ��� sprite �ͧ�س�ѹ���
    }

    // ��Ǩ�ͺ��ê�Ẻ Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��Ҫ��Ѻ�ѵ��
        if (collision.CompareTag("Enemy"))
        {
            // ���ҧ�����������
            EnemyAI enemy = collision.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            // ����¡���ع���
            Destroy(gameObject);
        }
    }
}
