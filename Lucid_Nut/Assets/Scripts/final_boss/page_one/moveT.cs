using UnityEngine;

public class moveT : MonoBehaviour
{
    public float speed = 8f; // �̵� �ӵ�
    private bool movingLeft = true; // �ʱ� ������ �������� ����
    public float lifetime = 14f; // ������Ʈ�� ���� �ð�

    void Start()
    {
        // ������Ʈ�� 12�� �Ŀ� �ı�
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // ���� ���⿡ ���� �̵�
        if (movingLeft)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("wall"))
        {
            Debug.Log("������ȯ"); // �浹 ���� Ȯ�ο� �α�
            movingLeft = !movingLeft;
        }
    }

    // ����׿�: �ݶ��̴� ������ �ð������� ǥ��
    void OnDrawGizmos()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, collider.bounds.size);
        }
    }
}
