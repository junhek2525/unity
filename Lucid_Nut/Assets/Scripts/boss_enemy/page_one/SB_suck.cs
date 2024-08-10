using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_suck : MonoBehaviour
{
    public float attractionSpeed = 5f; // ���Ƶ��̴� �ӵ�

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player �±׸� ���� ������Ʈ�� �ݶ��̴��� ������ ��
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the collider!");
        }
        // Wall �±׸� ���� ������Ʈ�� �ݶ��̴��� ������ ��
        else if (other.CompareTag("wall"))
        {
            Debug.Log("Wall entered the collider, destroying this object!");
            Destroy(gameObject); // ���� ������Ʈ �ı�
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Player �±׸� ���� ������Ʈ�� �ݶ��̴� �ȿ� ���� ��
        if (other.CompareTag("Player"))
        {
            // Player ������Ʈ�� ���� ������Ʈ�� ���Ƶ���
            Vector3 direction = transform.position - other.transform.position;
            other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, attractionSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Player �±׸� ���� ������Ʈ�� �ݶ��̴����� ������ ��
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the collider!");
        }
    }
}
