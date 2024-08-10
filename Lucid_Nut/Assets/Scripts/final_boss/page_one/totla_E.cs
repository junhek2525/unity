using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class total_E : MonoBehaviour
{
    private int energeCount = 0; // �浹�� energe �±׸� ���� ������Ʈ�� ����
    private GameObject targetObject; // ũ�⸦ ������ ��� ������Ʈ
    private GameObject playerObject; // Player ������Ʈ�� ������ ����
    private bool hasStartedMoving = false; // �̵��� �����ߴ��� ����

    private float moveSpeed = 5f; // ��ǥ ������Ʈ�� �̵���Ű�� �ӵ�
    private float speedIncreaseFactor = 2f; // �ӵ��� ������Ű�� ���

    private Vector3 moveDirection; // �̵� ����

    void Start()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }

        GameObject[] totalObjects = GameObject.FindGameObjectsWithTag("total_E");
        if (totalObjects.Length > 0)
        {
            targetObject = totalObjects[0];
            Debug.Log("Target object found: " + targetObject.name);
        }
        else
        {
            Debug.LogWarning("No object with tag 'total_E' found.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("energe"))
        {
            Debug.Log("ene");
            Destroy(other.gameObject);
            energeCount++;

            Debug.Log(energeCount);
            if (energeCount >= 100 && !hasStartedMoving)
            {
                hasStartedMoving = true;
                StartCoroutine(DetermineMoveDirection());
            }
        }
    }

    void Update()
    {
        if (!hasStartedMoving && targetObject != null)
        {
            Vector3 newScale = Vector3.one * (energeCount * 0.15f);
            targetObject.transform.localScale = newScale;
        }

        if (hasStartedMoving && targetObject != null)
        {
            // �̵� �������� ��� �̵�
            targetObject.transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }

    IEnumerator DetermineMoveDirection()
    {
        // 2�� ���
        yield return new WaitForSeconds(3f);

        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            // �÷��̾��� ��ġ�� �̵� ���� ����
            Vector3 targetPosition = playerObject.transform.position;
            moveDirection = (targetPosition - targetObject.transform.position).normalized;
            moveSpeed *= speedIncreaseFactor; // �̵� �ӵ� ����
        }
        else
        {
            Debug.LogWarning("No object with tag 'Player' found.");
        }

        // �̵��� ���۵Ǹ� ���� ������Ʈ�� �������� ����
        // Destroy(gameObject);
    }
}
