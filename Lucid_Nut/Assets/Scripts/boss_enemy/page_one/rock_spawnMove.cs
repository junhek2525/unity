using UnityEngine;

public class rock_spawnMove : MonoBehaviour
{
    // ����ٴ� Ÿ�� ������Ʈ�� �����մϴ�.
    public Transform target;

    void Update()
    {
        if (target != null)
        {
            // ���� ������Ʈ�� y, z ��ǥ�� �����ϰ� x ��ǥ�� Ÿ���� x ��ǥ�� �����մϴ�.
            transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
        }
    }
}

