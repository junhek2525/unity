using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning_fire : MonoBehaviour
{
    public GameObject objectToSpawn; // ��ȯ�� ������Ʈ ������
    public int numberOfObjects = 3; // ��ȯ�� ������Ʈ ����
    public BoxCollider2D spawnArea; // ��ȯ ���� �ݶ��̴�
    public bool LS = false; // LS ���� �߰�

    // ��ġ�� ������ ����Ʈ�� Ŭ���� ������ ����
    private List<Vector2> spawnPositions = new List<Vector2>();

    void Update()
    {
        if (LS) // LS�� true�� ���� ����
        {
            SpawnObjects();
            LS = false; // ���� �� LS�� false�� ����
        }
    }

    void SpawnObjects()
    {
        Bounds bounds = spawnArea.bounds;
        int objectsSpawned = 0;

        while (objectsSpawned < numberOfObjects)
        {
            Vector2 spawnPosition;
            int attempts = 0; // �õ� Ƚ��

            // ��ȿ�� ��ġ�� ã�� ������ �ݺ�
            while (attempts < 100)
            {
                attempts++; // �õ� Ƚ�� ����
                spawnPosition = new Vector2(
                    Random.Range(bounds.min.x, bounds.max.x),
                    Random.Range(bounds.min.y, bounds.max.y)
                );

                // ��� ��ġ�� ��ȯ �����ϵ��� ����
                spawnPositions.Add(spawnPosition); // ��ġ�� ����Ʈ�� �߰�
                Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
                objectsSpawned++;
                break; // ��ȿ�� ��ġ�� Ȯ�εǸ� ���� ����
            }
            // 100���� �õ� �Ŀ��� ��ȿ�� ��ġ�� ã�� ���� ��� ��� �޽��� ���
            if (attempts >= 100)
            {
                Debug.LogWarning("Could not find suitable spawn position after 100 attempts.");
                break;
            }
        }
    }
}