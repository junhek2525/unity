/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lcehit : MonoBehaviour
{
    public GameObject hitPrefab;
    public float hit;
    public float cooldownTime = 1f;
    private float lastShootTime = 0f;

    private Transform playerTransform;


    private GameObject[] rainObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastShootTime >= cooldownTime)
        {
            // �÷��̾ �����ϴ��� Ȯ��
            if (playerTransform != null)
            {
                hiton();
                lastShootTime = Time.time; // �߻� �ð� ������Ʈ
            }
        }

        void hiton()
        {

            {
                Vector3 shooterPosition = transform.position;


                hitObjects = new GameObject[numberOfRains];
                for (int i = 0; i < numberOfRains; i++)
                {
                    // ��ġ�� �ʰ� ���η� �������� �����Ͽ� ���� ������Ʈ ��ġ ����
                    Vector3 spawnPosition = shooterPosition +  Vector3(Random.Range(-10.0f, 10.0f), 10, 0);

                    // ���� ������Ʈ ����
                    rainObjects[i] = Instantiate(rainPrefab, spawnPosition, Quaternion.identity);
                }

                // �߻� �� ��� �ð� �� �߻� ����
                StartCoroutine(FireRainsAfterDelay());
            }
        }
    }
}*/
