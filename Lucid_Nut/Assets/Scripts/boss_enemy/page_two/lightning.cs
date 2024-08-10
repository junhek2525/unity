using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightning : MonoBehaviour
{
    private Transform player; // �÷��̾��� Transform
    private Vector3 lastDirection;
    private bool isFollowing = true;
    private float followDuration = 3f; // �÷��̾ ���󰡴� �ð�
    private float destroyTime = 7f; // ������Ʈ�� �ı��Ǳ������ �ð�
    public float speed = 15f; // ������Ʈ�� �̵� �ӵ�

    private void Start()
    {
        // Player �±׸� ���� ������Ʈ�� ã�Ƽ� �Ҵ��մϴ�.
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        // 3�� �Ŀ� �÷��̾� ������ ���߰� ���������� �ٶ� �������� �̵��ϵ��� �մϴ�.
        Invoke("StopFollowing", followDuration);
        // 7�� �Ŀ� ������Ʈ�� �ı��մϴ�.
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        if (isFollowing && player != null)
        {
            // �÷��̾��� ���� ��ġ�� ��� �޾ƿɴϴ�.
            Vector3 direction = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // ���������� �ٶ� ������ ������Ʈ�մϴ�.
            lastDirection = direction;
        }
        else
        {
            // ���������� �ٶ� �������� ��� �̵��մϴ�.
            transform.position += lastDirection * speed * Time.deltaTime;
        }
    }

    private void StopFollowing()
    {
        isFollowing = false;
    }
}