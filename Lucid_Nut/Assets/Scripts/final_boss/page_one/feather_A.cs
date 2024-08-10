using UnityEngine;

public class Chaser : MonoBehaviour
{
    public float moveSpeed = 13f; // �̵� �ӵ� ���� ����
    private Transform playerTransform;
    private Vector3 targetPosition;
    private bool hasTarget = false;

    void Start()
    {
        // "Player" �±׸� ���� ������Ʈ�� ã���ϴ�.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            hasTarget = true;
        }
        else
        {
            Debug.LogWarning("No object with tag 'Player' found.");
        }

        // 1�ʸ��� UpdateTargetPosition �޼��带 ȣ���մϴ�.
        InvokeRepeating("UpdateTargetPosition", 0f, 2f);
    }

    void Update()
    {
        if (hasTarget)
        {
            // ���� ��ġ�� ��ǥ ��ġ ������ ������ ����մϴ�.
            Vector3 direction = (targetPosition - transform.position).normalized;

            // ���� ���͸� ������� ������Ʈ�� ȸ����ŵ�ϴ�.
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // �̵��� �������� �̵��մϴ�.
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void UpdateTargetPosition()
    {
        if (playerTransform != null)
        {
            // ��ǥ ��ġ�� ���� �÷��̾��� ��ġ�� ������Ʈ�մϴ�.
            targetPosition = playerTransform.position;
        }
    }
}
