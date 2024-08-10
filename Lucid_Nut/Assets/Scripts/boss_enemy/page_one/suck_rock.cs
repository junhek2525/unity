using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suck_rock : MonoBehaviour
{
    public float moveSpeed = 20f;      // �� �̵� �ӵ�
    public float skillDuration = 5f;   // ��ų�� ���� �ð�
    public float spreadDelay = 5f;     // ���� �����ϰ� �Ѹ��� �� ��� �ð�
    public float spreadForce = 10f;    // ���� �Ѹ� �� ���� ����
    public float rbActivationDelay = 0.2f; // Rigidbody2D�� Ȱ��ȭ�� ���� �ð�
    public float extraForce = 1f;      // �߻� �� �߰������� ���� ������ ��
    public float minRotationSpeed = 50f; // ���� �ּ� ȸ�� �ӵ�
    public float maxRotationSpeed = 200f; // ���� �ִ� ȸ�� �ӵ�

    public bool SR = false;            // ��ų ������ ���� �� ��
    private bool isUsingSkill = false;  // ��ų ��� ������ ����
    private float skillEndTime = 0f;    // ��ų�� ������ �ð�
    private List<GameObject> attractedRocks = new List<GameObject>(); // ������ ������ ������ ����Ʈ

    void Start()
    {
        // �ʿ��, �� ��ũ��Ʈ�� �پ� �ִ� ������Ʈ�� Rigidbody2D�� Ȯ���ϰ� �߰��մϴ�.
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        // SR�� true�̰� ��ų�� ��� ������ ���� ��
        if (SR && !isUsingSkill)
        {
            Debug.Log("��������");
            StartSkill();
            SR = false; // ��ų�� ���۵Ǹ� SR ���� false�� ����
        }

        // ��ų�� ��� ���� ��
        if (isUsingSkill)
        {
            if (Time.time < skillEndTime)
            {
                MoveRocksTowardsObject();
            }
            else
            {
                // ��ų ���� 0.2�� ��, Rigidbody2D�� Ȱ��ȭ�մϴ�.
                StartCoroutine(ActivateRigidbodyAndSpreadRocks());
                EndSkill();
            }
        }
    }

    void StartSkill()
    {
        isUsingSkill = true;
        skillEndTime = Time.time + skillDuration;

        attractedRocks.Clear(); // ����Ʈ�� �ʱ�ȭ�մϴ�.
        // "rock" �±׸� ���� ��� ������Ʈ�� ã���ϴ�.
        GameObject[] rocks = GameObject.FindGameObjectsWithTag("rock");

        foreach (GameObject rock in rocks)
        {
            // �ߺ� �߰��� ����
            if (!attractedRocks.Contains(rock))
            {
                attractedRocks.Add(rock);
                // ���� Rigidbody2D�� ��Ȱ��ȭ�մϴ�.
                Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.isKinematic = true;  // ������ ��ȣ�ۿ��� ��Ȱ��ȭ�մϴ�.
                    rb.angularVelocity = 0f; // ȸ���� ����ϴ�.
                }

                // ������ �������� ȸ�� �ӵ��� �����մϴ�.
                rock.AddComponent<RotateRandomly>();
            }
        }
    }

    void MoveRocksTowardsObject()
    {
        foreach (GameObject rock in attractedRocks)
        {
            if (rock != null)
            {
                // ���� ������Ʈ(�� ��ũ��Ʈ�� �پ��ִ� ������Ʈ) ������ ����մϴ�.
                Vector3 direction = (transform.position - rock.transform.position).normalized;
                // ���� �� ��ũ��Ʈ�� �پ� �ִ� ������Ʈ �������� �̵���ŵ�ϴ�.
                rock.transform.position += direction * moveSpeed * Time.deltaTime;
            }
        }
    }

    IEnumerator ActivateRigidbodyAndSpreadRocks()
    {
        yield return new WaitForSeconds(rbActivationDelay);

        foreach (GameObject rock in attractedRocks)
        {
            if (rock != null)
            {
                // Rigidbody2D�� Ȱ��ȭ�մϴ�.
                Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.isKinematic = false;  // ������ ��ȣ�ۿ��� �ٽ� Ȱ��ȭ�մϴ�.
                    // ������ �������� ���� ���մϴ�.
                    Vector2 randomDirection = Random.insideUnitCircle.normalized;
                    rb.AddForce(randomDirection * spreadForce, ForceMode2D.Impulse);

                    // �߰����� �������� ���� ���մϴ�.
                    rb.velocity += randomDirection * extraForce;
                }
                else
                {
                    // Rigidbody2D�� ���� ���, ��ġ�� ������ �̵���Ű�� ���
                    Vector2 randomDirection = Random.insideUnitCircle.normalized;
                    rock.transform.position += (Vector3)randomDirection * spreadForce;
                }
            }
        }
    }

    void EndSkill()
    {
        isUsingSkill = false;

        // ��ũ��Ʈ�� �پ� �ִ� ������Ʈ�� Rigidbody2D�� �����ɴϴ�.
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Rigidbody2D�� ������ �̵��� ����ϴ�.
            rb.velocity = Vector2.zero;
        }
        else
        {
            // Rigidbody2D�� ���� ���, �ٸ� ������� ó���� �� �ֽ��ϴ�.
            Debug.LogWarning("Rigidbody2D component is missing on the object.");
        }
    }
}

public class RotateRandomly : MonoBehaviour
{
    public float minRotationSpeed = 50f; // �ּ� ȸ�� �ӵ�
    public float maxRotationSpeed = 200f; // �ִ� ȸ�� �ӵ�

    private float rotationSpeed;

    void Start()
    {
        // ������ ȸ�� �ӵ��� �����մϴ�.
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    void Update()
    {
        // �� �����Ӹ��� ȸ�� �ӵ��� ���� ���� ȸ����ŵ�ϴ�.
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
