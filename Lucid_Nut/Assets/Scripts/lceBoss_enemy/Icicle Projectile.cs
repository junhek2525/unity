using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleProjectile : MonoBehaviour
{
    public float speed = -20f; //��帧 �ӵ�
    public float Duration = 5f; //��帧 ���ӽð�
    public float Durationtime = 0f; //��帧 ������� �ð�
    // Start is called before the first frame update
    void Start()
    {
       Durationtime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(0,speed * Time.deltaTime,0); //��帧 �̵�

        Durationtime += Time.deltaTime;
        if (Durationtime >= Duration) // ���ӽð��� ������
        {
            Destroy(gameObject); //������Ʈ ����
        }
        
    }
   /* IEnumerator start()
    {
        

    }*/
}

