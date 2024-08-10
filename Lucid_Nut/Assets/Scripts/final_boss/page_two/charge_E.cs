using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charge_E : MonoBehaviour
{
    public GameObject sideObject;    // �翷�� ��ġ�� ������Ʈ
    public GameObject topObject;     // ���� ��ġ�� ������Ʈ

    public float sideOffset = 10.0f;  // �翷 ������Ʈ�� ��ġ ������
    public float topOffset = 5.0f;   // �� ������Ʈ�� ��ġ ������

    public bool AE_charge = true;    // AE_charge ������ �߰��Ͽ� ������Ʈ ��ġ�� �����մϴ�.
    public float deleteDelay = 8.0f; // ������Ʈ ���� ���� �ð�

    private List<GameObject> instantiatedObjects = new List<GameObject>();

    void Start()
    {
        if (AE_charge)
        {
            PlaceObjects();
            StartCoroutine(DeleteObjectsAfterDelay());
        }
    }

    void PlaceObjects()
    {
        if (sideObject == null || topObject == null)
        {
            Debug.LogError("�翷 �Ǵ� �� ������Ʈ�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        // ���� ��ũ��Ʈ�� �پ� �ִ� ���� ������Ʈ�� �⺻ ������Ʈ�� ���
        Vector3 basePosition = transform.position;

        // �翷 ������Ʈ ��ġ ����
        GameObject leftSideObject = Instantiate(sideObject, basePosition + new Vector3(-sideOffset, 0, 0), Quaternion.identity);
        GameObject rightSideObject = Instantiate(sideObject, basePosition + new Vector3(sideOffset, 0, 0), Quaternion.identity);

        // �� ������Ʈ ��ġ ����
        GameObject top = Instantiate(topObject, basePosition + new Vector3(0, topOffset, 0), Quaternion.identity);

        // ��ȯ�� ������Ʈ ����Ʈ�� �߰�
        instantiatedObjects.Add(leftSideObject);
        instantiatedObjects.Add(rightSideObject);
        instantiatedObjects.Add(top);
    }

    IEnumerator DeleteObjectsAfterDelay()
    {
        // ������ �ð� ���� ���
        yield return new WaitForSeconds(deleteDelay);

        // ���� ��ȯ�� ������Ʈ�� ������ ������ ������Ʈ ����
        foreach (GameObject obj in instantiatedObjects)
        {
            // ������ ������Ʈ�� topObject�� �������� ��
            if (obj != null && obj.name != topObject.name)
            {
                Destroy(obj);
            }
        }

        // ����Ʈ �ʱ�ȭ
        instantiatedObjects.Clear();
    }
}
