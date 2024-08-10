using UnityEngine;
using System.Collections;

public class RainEnemy : MonoBehaviour
{
    public GameObject rainPrefab; // ���� ������Ʈ�� ������
    public float rainSpeed = 20f; // ������ �ӵ�
    public int numberOfRains = 3; // �߻��� ������ ��
    public float horizontalOffset = 1f; // ���η� ��ġ�� ����
    public float delayBeforeFire = 1.5f; // �߻� �� ��� �ð� (1.5��)
    public float fireInterval = 1f; // �߻� ���� (1��)

    private Transform playerTransform; // �÷��̾��� Transform
    private GameObject[] rainObjects; // ��ȯ�� ���� ������Ʈ��
    public bool RS = false; // �߻� Ȱ��ȭ ���¸� �����ϴ� ����

    void Start()
    {
        // �±װ� "Player"�� ������Ʈ�� ã���ϴ�.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        // �÷��̾ �����ϰ� RS�� true�� �� �߻�
        if (playerTransform != null && RS)
        {
            Debug.Log("�� �߻�");
            ShootRain();
            RS = false; // �߻� �� RS�� false�� ����
        }
    }

    void ShootRain()
    {
        if (rainPrefab != null && playerTransform != null)
        {
            Vector3 shooterPosition = transform.position;

            // ���� ������Ʈ�� �� ���� ��ȯ
            rainObjects = new GameObject[numberOfRains];
            for (int i = 0; i < numberOfRains; i++)
            {
                // ��ġ�� �ʰ� ���η� �������� �����Ͽ� ���� ������Ʈ ��ġ ����
                Vector3 spawnPosition = shooterPosition + new Vector3(horizontalOffset * (i - (numberOfRains - 1) / 2f), 1 + (i * 0.5f), 0);

                // ���� ������Ʈ ����
                rainObjects[i] = Instantiate(rainPrefab, spawnPosition, Quaternion.identity);
            }

            // �߻� �� ��� �ð� �� �߻� ����
            StartCoroutine(FireRainsAfterDelay());
        }
    }

    IEnumerator FireRainsAfterDelay()
    {
        // �߻� �� ��� �ð�
        yield return new WaitForSeconds(delayBeforeFire);

        foreach (GameObject rain in rainObjects)
        {
            if (rain != null)
            {
                // ���� ������Ʈ�� ������ �÷��̾� �������� ����
                Vector3 spawnPosition = rain.transform.position;
                Vector3 direction = (playerTransform.position - spawnPosition).normalized;
                rain.GetComponent<Rigidbody2D>().velocity = direction * rainSpeed;

                // ���� ���� �߻� ������ ����
                yield return new WaitForSeconds(fireInterval);
            }
        }
    }

    // RS ���� �ܺο��� ������ �� �ֵ��� public �޼��� ����
    public void ActivateRain()
    {
        RS = true; // �ܺο��� ȣ�� �� RS�� true�� ����
    }
}
