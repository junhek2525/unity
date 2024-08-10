using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;  // �߻��� ������Ʈ�� ������
    public float launchSpeed = 10f;  // �߻� �ӵ�
    public bool ST = false;  // ��ų Ȱ��ȭ ���¸� �����ϴ� ����

    private Vector3 playerPosition;  // �÷��̾��� ��ġ�� ������ ����
    private bool hasStoredPosition = false;  // �÷��̾� ��ġ�� �����ߴ��� ����
    private GameObject player;  // "Player" �±׸� ���� ������Ʈ

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");  // "Player" �±׸� ���� ������Ʈ�� ã���ϴ�.
    }

    void Update()
    {
        if (ST && !hasStoredPosition)
        {
            // �÷��̾��� ��ġ�� �����մϴ�.
            playerPosition = player.transform.position;
            hasStoredPosition = true;
            Debug.Log("�÷��̾� ��ġ ����: " + playerPosition);

            // �߻�ü�� �����ϰ� �߻��մϴ�.
            LaunchProjectile();
        }
        else if (!ST)
        {
            // ��ų�� ��Ȱ��ȭ�Ǹ� ���¸� �ʱ�ȭ�մϴ�.
            hasStoredPosition = false;
        }
    }

    void LaunchProjectile()
    {
        if (projectilePrefab != null)
        {
            // �߻�ü�� �����մϴ�.
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            // �÷��̾� ������ ����մϴ�.
            Vector3 direction = (playerPosition - transform.position).normalized;
            // �߻�ü�� �÷��̾� �������� �߻��մϴ�.
            projectile.GetComponent<Rigidbody2D>().velocity = direction * launchSpeed;
        }
    }
}
