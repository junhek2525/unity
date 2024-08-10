using UnityEngine;

public class BlankBandAttack : MonoBehaviour
{
    public float chaseDuration = 6f;
    public float ascendDuration = 3f;
    public float returnSpeed = 4f;
    public float moveSpeed = 10f; // �̵� �ӵ��� �����ϴ� ����
    public bool isActive = false; // ���� ���θ� �����ϴ� ����

    public GameObject additionalObject; // �߰��� ������Ʈ
    public float additionalObjectActivationDelay = 0.5f; // �߰� ������Ʈ Ȱ��ȭ ���� �ð�

    private Vector3 startPosition;
    private GameObject player;
    private float chaseStartTime;
    private float ascendStartTime;
    private bool hasMovedToInitial = false; // �ʱ� ��ġ�� �̵��ߴ��� ���θ� ����

    void Start()
    {
        startPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");

        // �ʱ� ��ġ ���� (Ȱ��ȭ���� ���� ��� �̵����� ����)
        transform.position = startPosition;

        if (additionalObject != null)
        {
            additionalObject.SetActive(false); // ��ũ��Ʈ ���� �� �߰� ������Ʈ ��Ȱ��ȭ
        }
    }

    void Update()
    {
        if (isActive)
        {
            if (!hasMovedToInitial)
            {
                // ���� ���� �� Ư�� ��ġ�� �̵� (�ӵ� ����)
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, -15, 10), (moveSpeed * 2) * Time.deltaTime);

                if (Vector3.Distance(transform.position, new Vector3(0, -15, 10)) < 0.1f)
                {
                    transform.position = new Vector3(0, -15, 10); // ��Ȯ�� ��ġ�� �̵�
                    hasMovedToInitial = true; // �̵� �Ϸ�� ǥ��
                    chaseStartTime = Time.time; // ���� ���� �ð� ���

                    // �߰� ������Ʈ�� ���� �ð� �� Ȱ��ȭ
                    if (additionalObject != null)
                    {
                        Invoke(nameof(ActivateAdditionalObject), additionalObjectActivationDelay);
                    }
                }
            }
            else if (Time.time - chaseStartTime < chaseDuration)
            {
                // �÷��̾� ���� (X�ุ)
                Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);
            }
            else if (Time.time - chaseStartTime < chaseDuration + ascendDuration)
            {
                // ���� �̵� (X�� �ӵ� 0���� ����)
                if (ascendStartTime == 0)
                {
                    ascendStartTime = Time.time;
                }
                float t = (Time.time - ascendStartTime) / ascendDuration;
                // ������ t�� �״�� ����Ͽ� ���
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, startPosition.y, transform.position.z), t);
                // X�� �ӵ� 0���� ����
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                // �ʱ� ��ġ�� ���� (�ӵ� ����)
                transform.position = Vector3.MoveTowards(transform.position, startPosition, returnSpeed * Time.deltaTime);

                // ���Ͱ� �Ϸ�Ǹ� ���¸� �ʱ�ȭ
                if (Vector3.Distance(transform.position, startPosition) < 0.1f)
                {
                    ResetAttack();
                }
            }
        }
    }

    void ActivateAdditionalObject()
    {
        if (additionalObject != null)
        {
            additionalObject.SetActive(true);
        }
    }

    // Optional: Reset method to set `isActive` to false
    public void ResetAttack()
    {
        isActive = false;
        hasMovedToInitial = false;
        chaseStartTime = 0f; // �ʱ�ȭ
        ascendStartTime = 0f; // �ʱ�ȭ
        transform.position = startPosition;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // �߰� ������Ʈ ��Ȱ��ȭ
        if (additionalObject != null)
        {
            additionalObject.SetActive(false);
        }
    }

    public void StartAttack()
    {
        isActive = true;
    }
}
