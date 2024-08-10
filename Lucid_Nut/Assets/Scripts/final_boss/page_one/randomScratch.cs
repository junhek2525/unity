using UnityEngine;
using System.Collections;

public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // ��ȯ�� ������Ʈ
    public float spawnInterval = 0.05f; // ��ȯ ����
    public float spawnDuration = 14f; // ��ȯ ���� �ð�
    public Vector2 spawnAreaMin; // ��ȯ ������ �ּ� ��ǥ
    public Vector2 spawnAreaMax; // ��ȯ ������ �ִ� ��ǥ

    public bool RandScratch = false; // ��ȯ Ȱ��ȭ ���θ� �����ϴ� ����

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        float elapsedTime = 0f;

        while (elapsedTime < spawnDuration)
        {
            if (RandScratch)
            {
                SpawnRandomObject();
                yield return new WaitForSeconds(spawnInterval);
                elapsedTime += spawnInterval;
            }
            else
            {
                yield return null; // RandScratch�� false�� ��� ���� �����ӱ��� ��ٸ�
            }
        }

        RandScratch = false; // ��ȯ �Ⱓ�� ������ RandScratch�� false�� ����
    }

    void SpawnRandomObject()
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}
