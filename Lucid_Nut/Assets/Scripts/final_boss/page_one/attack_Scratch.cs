using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_Scratch : MonoBehaviour
{
    public GameObject alternateObject; // ��ü�� ������Ʈ
    public float delayBeforeSwap = 1f; // ��ü�ϱ� ���� ��ٸ� �ð�

    void Start()
    {
        // ��ü�� �����ϴ� �ڷ�ƾ ȣ��
        StartCoroutine(SwapObjectAfterDelay(delayBeforeSwap));
    }

    IEnumerator SwapObjectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // ������ �ð� ���� ���

        // ���� ������Ʈ�� ��ġ�� ȸ���� ����
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;

        // ���� ������Ʈ�� ����
        Destroy(gameObject);

        // ��ü�� ������Ʈ�� ���� ��ġ�� ȸ������ ��ȯ
        Instantiate(alternateObject, currentPosition, currentRotation);
    }
}
