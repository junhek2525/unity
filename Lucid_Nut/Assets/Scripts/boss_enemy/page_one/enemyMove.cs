using UnityEngine;

public class enemyMove : MonoBehaviour
{
    public float speed = 2f; // ���� �̵� �ӵ�
    public dash dashScript; // �뽬 ��ũ��Ʈ

    private Transform player; // �÷��̾��� Ʈ������

    void Start()
    {
        // �±װ� "Player"�� ��ü�� ã���ϴ�.
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        // �÷��̾� ��ü�� �����ϴ��� Ȯ���ϰ� Ʈ�������� �����մϴ�.
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        // �÷��̾ �����ϰ� �뽬 ���� �ƴϸ� �뽬 �غ� ���� �ƴ� ���� ���� �����̵��� �մϴ�.
        if (player != null && !dashScript.isDashing && !dashScript.isPreparingToDash)
        {
            // �÷��̾�� �� ������ ������ ����մϴ�.
            Vector2 direction = player.position - transform.position;
            direction.Normalize();

            // ���� �÷��̾� �������� �̵���ŵ�ϴ�.
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �ݶ��̴� �ȿ� �±װ� "Player"�� ��ü�� ���Դ��� Ȯ���մϴ�.
        if (other.CompareTag("Player"))
        {
            // Dash ��ũ��Ʈ�� ���� �뽬�� �����մϴ�.
            dashScript.TryStartDash(other.transform.position);
        }
    }
}
