using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feather_Attack : MonoBehaviour
{
    public GameObject objectToSpawn; // ��ȯ�� ������Ʈ
    public float spawnInterval = 1f; // ������Ʈ�� ��ȯ�� ���� (�� ��ȯ ����)
    public int numberOfSpawns = 5; // ��ȯ�� ������Ʈ ��
    public float delayBetweenSpawns = 5f; // �� ��° ��ȯ������ ��� �ð�
    public Collider2D spawnAreaCollider; // ��ȯ ������ �����ϴ� �ݶ��̴�
    public bool feather_attack = false; // ��ȯ�� �����ϴ� ����
    public float minDistanceBetweenObjects = 3f; // ������Ʈ �� �ּ� ����

    private List<Vector2> occupiedPositions = new List<Vector2>(); // ��ȯ�� ������Ʈ�� ��ġ ���

    private void Update()
    {
        // feather_attack�� true�� ���� ��ȯ�� ����
        if (feather_attack)
        {
            StartCoroutine(SpawnObjects());
            feather_attack = false; // ��ȯ �Ŀ��� feather_attack�� false�� ����
        }
    }

    private IEnumerator SpawnObjects()
    {
        // ù ��° ��ȯ
        for (int i = 0; i < numberOfSpawns; i++)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }

        // ù ��° ��ȯ �� ���
        yield return new WaitForSeconds(delayBetweenSpawns);

        occupiedPositions = new List<Vector2>();

        // �� ��° ��ȯ
        for (int i = 0; i < numberOfSpawns; i++)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnObject()
    {
        if (objectToSpawn == null || spawnAreaCollider == null)
        {
            Debug.LogWarning("Object to spawn or spawn area collider not set.");
            return;
        }

        Vector2 spawnPosition = Vector2.zero; // ���� �ʱ�ȭ
        bool validPosition = false;

        // �ִ� 100���� �õ��� ���� ��ȿ�� ��ġ�� ã���ϴ�
        for (int attempt = 0; attempt < 100; attempt++)
        {
            // �ݶ��̴� ���� ������ ���� ��ġ ���
            spawnPosition = GetRandomPositionInCollider(spawnAreaCollider);

            // ��ġ�� ��ȿ���� Ȯ��
            if (IsValidPosition(spawnPosition))
            {
                validPosition = true;
                break;
            }
        }

        if (validPosition)
        {
            // ������Ʈ ����
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            occupiedPositions.Add(spawnPosition); // ������ ��ġ�� ����մϴ�
        }
        else
        {
            Debug.LogWarning("Failed to find a valid spawn position.");
        }
    }

    private Vector2 GetRandomPositionInCollider(Collider2D collider)
    {
        // �ݶ��̴��� bounds�� ����Ͽ� ���� �� ���� ��ġ ���
        Bounds bounds = collider.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }

    private bool IsValidPosition(Vector2 position)
    {
        foreach (Vector2 occupiedPosition in occupiedPositions)
        {
            if (Vector2.Distance(position, occupiedPosition) < minDistanceBetweenObjects)
            {
                return false; // �ּ� ������ �������� ������ ��ġ�� ��ȿ���� ����
            }
        }
        return true; // ��� ��ġ�� ��ȿ�ϸ� true ��ȯ
    }
}
