using UnityEngine;
using System.Collections;

public class RandomRockSpawners : MonoBehaviour
{
    public GameObject rockPrefab; // �� �������� �Ҵ��մϴ�.
    public int numberOfRocksPerInterval = 10; // 10�ʸ��� ������ ���� ����
    public float spawnInterval = 10f; // ���� ����(��)
    public GameObject suckRockObject; // suck_rock2 ��ũ��Ʈ�� �پ��ִ� ������Ʈ�� �Ҵ��մϴ�.
    public GameObject[] colliderObjects; // ���� ���� �ݶ��̴��� ����� ������Ʈ�� �Ҵ��մϴ�.
    public bool SRR;

    private suck_rock2 suckRockScript;

    void Start()
    {
        SRR = false;

        if (colliderObjects.Length > 0)
        {
            // �⺻������ ù ��° �ݶ��̴��� ����
            // Collider �ʱ�ȭ�� Start()���� �ϸ�, ������ �ݶ��̴� �ε����� ������� �ʽ��ϴ�.
        }
        if (suckRockObject != null)
        {
            suckRockScript = suckRockObject.GetComponent<suck_rock2>();
        }
    }

    void Update()
    {
        if (SRR)
        {
            StartCoroutine(SpawnRocksRoutine()); // �ڷ�ƾ ���
            SRR = false;
        }
    }

    IEnumerator SpawnRocksRoutine()
    {
        while (SRR)
        {
            // �� �ݶ��̴����� ���� �����ϵ��� ����
            int rocksPerCollider = Mathf.CeilToInt((float)numberOfRocksPerInterval / colliderObjects.Length);

            for (int i = 0; i < colliderObjects.Length; i++)
            {
                GameObject currentColliderObject = colliderObjects[i];
                BoxCollider currentBoxCollider = currentColliderObject.GetComponent<BoxCollider>();

                for (int j = 0; j < rocksPerCollider; j++)
                {
                    Vector3 randomPosition = GetRandomPositionInBox(currentBoxCollider);
                    Instantiate(rockPrefab, randomPosition, Quaternion.identity);
                }
            }

            // suck_rock ��ũ��Ʈ�� SR ������ true�� �����մϴ�.
            if (suckRockScript != null)
            {
                suckRockScript.SR = true;
            }

            yield return new WaitForSeconds(spawnInterval); // ���� ���ݸ�ŭ ���
        }
    }

    Vector3 GetRandomPositionInBox(BoxCollider boxCollider)
    {
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider is not assigned.");
            return Vector3.zero;
        }

        Vector3 center = boxCollider.transform.position + boxCollider.center;
        Vector3 size = boxCollider.size;

        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
        float randomZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

        return new Vector3(randomX, randomY, randomZ);
    }
}
