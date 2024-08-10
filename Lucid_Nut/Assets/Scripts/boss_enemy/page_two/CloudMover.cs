using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudMover : MonoBehaviour
{
    public float speed = 5.0f; // �̵� �ӵ�
    public GameObject attackPointPrefab; // ���� ����Ʈ�� �߰��� ������Ʈ ������
    public GameObject temporaryObjectPrefab; // 1�� ���� �δ� �ӽ� ������Ʈ ������
    public float initialDelay = 3.0f; // �ʱ� ���� ����Ʈ ���� �� ��� �ð�
    public float waitTimeAtAttackPoint = 6.0f; // ���� ����Ʈ ���� �� ��� �ð�
    public float temporaryObjectDuration = 1.0f; // �ӽ� ������Ʈ�� ������ �ð�
    public float objectLifetime = 20.0f; // ������Ʈ�� ���� �ð�

    private Transform pointA;
    private Transform pointB;
    private Transform targetPoint;
    private Vector3 attackPoint;
    private GameObject currentAttackPointObject; // ���� ���� ����Ʈ ������Ʈ

    private List<GameObject> instantiatedPrefabs = new List<GameObject>(); // �ν��Ͻ�ȭ�� �������� �����ϴ� ����Ʈ

    void Start()
    {
        // �±װ� 'cloud_move_point'�� ������Ʈ ã��
        GameObject[] cloudPoints = GameObject.FindGameObjectsWithTag("cloud_move_point");

        // �� ���� ������Ʈ�� ã�ƾ� �ϹǷ� Ȯ��
        if (cloudPoints.Length != 2)
        {
            Debug.LogError("There should be exactly two objects with the tag 'cloud_move_point'");
            return;
        }

        // ������ ������Ʈ�� pointA�� pointB�� �Ҵ�
        pointA = cloudPoints[0].transform;
        pointB = cloudPoints[1].transform;

        // �ʱ� ��ǥ ������ pointA�� ����
        targetPoint = pointA;

        // 3�� �Ŀ� �ʱ� ���� ����Ʈ ����
        Invoke("InitializeAttackPoint", initialDelay);

        // 20�� �Ŀ� �� ������Ʈ�� �ı�
        Invoke("DestroyAll", objectLifetime);
    }

    void Update()
    {
        if (pointA == null || pointB == null)
            return;

        // ���� ��ġ���� ��ǥ �������� �̵�
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, step);

        // ��ǥ ������ �������� ���� ó��
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // ��ǥ ������ ����
            targetPoint = (targetPoint == pointA) ? pointB : pointA;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �浹�� ������Ʈ�� �±װ� 'abc'���� Ȯ��
        if (other.CompareTag("abc"))
        {
            if (currentAttackPointObject != null)
            {
                // ���� ���� ����Ʈ ������Ʈ ���� ��, �ӽ� ������Ʈ ����
                Vector3 position = currentAttackPointObject.transform.position;
                Destroy(currentAttackPointObject);

                // �ӽ� ������Ʈ�� �����Ͽ� 1�� ���� ����
                StartCoroutine(CreateAndDestroyTemporaryObject(position));
            }

            Debug.Log("������������");
            // 6�� ��� �� ���� ����Ʈ�� �����ϵ��� �ڷ�ƾ ����
            StartCoroutine(WaitAndSetRandomAttackPoint());
        }
    }

    IEnumerator WaitAndSetRandomAttackPoint()
    {
        // 6�� ���
        yield return new WaitForSeconds(waitTimeAtAttackPoint);

        // ���� ���� ����Ʈ ����
        SetRandomAttackPoint();
    }

    IEnumerator CreateAndDestroyTemporaryObject(Vector3 position)
    {
        // �ӽ� ������Ʈ�� ����
        if (temporaryObjectPrefab != null)
        {
            GameObject temporaryObject = Instantiate(temporaryObjectPrefab, position, Quaternion.identity);
            instantiatedPrefabs.Add(temporaryObject); // ����Ʈ�� �߰�
            // ������ �ð� ���� ����
            yield return new WaitForSeconds(temporaryObjectDuration);
            Destroy(temporaryObject);
            instantiatedPrefabs.Remove(temporaryObject); // ����Ʈ���� ����
        }
    }

    void InitializeAttackPoint()
    {
        // �ʱ� ���� ����Ʈ ����
        SetRandomAttackPoint();
    }

    void SetRandomAttackPoint()
    {
        // pointA�� pointB ������ ���� ��ġ�� ����
        float t = Random.value;
        attackPoint = Vector3.Lerp(pointA.position, pointB.position, t);

        // ���ο� ���� ����Ʈ ������Ʈ�� ����
        if (attackPointPrefab != null)
        {
            currentAttackPointObject = Instantiate(attackPointPrefab, attackPoint, Quaternion.identity);
            instantiatedPrefabs.Add(currentAttackPointObject); // ����Ʈ�� �߰�
        }
    }

    void DestroyAll()
    {
        // �ν��Ͻ�ȭ�� ��� �������� �ı�
        foreach (GameObject prefab in instantiatedPrefabs)
        {
            if (prefab != null)
            {
                Destroy(prefab);
            }
        }

        // �� ������Ʈ�� �ı�
        Destroy(gameObject);
    }
}
