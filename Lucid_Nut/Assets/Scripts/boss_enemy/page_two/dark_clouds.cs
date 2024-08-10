using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dark_clouds : MonoBehaviour
{
    public GameObject objectToSpawn; // ��ȯ�� ������Ʈ�� �巡�� �� ������� ����
    public Transform spawnLocation; // ������Ʈ�� ��ȯ�� ��ġ�� ����
    public bool DCS = false; // ��ȯ ���θ� �����ϴ� boolean ����

    void Update()
    {
        // DCS�� true�� �� ��ȯ�� �õ�
        if (DCS)
        {
            SpawnObject();
            DCS = false; // ��ȯ �� DCS�� false�� ����
        }
    }

    void SpawnObject()
    {
        if (objectToSpawn != null && spawnLocation != null)
        {
            Instantiate(objectToSpawn, spawnLocation.position, spawnLocation.rotation);
        }
        else
        {
            Debug.LogWarning("Object to spawn or spawn location not assigned.");
        }
    }
}
