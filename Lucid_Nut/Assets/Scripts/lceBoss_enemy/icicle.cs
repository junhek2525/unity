
using UnityEngine;
using System.Collections;

public class icicle : MonoBehaviour
{
    public GameObject iciclePrefab; 
    bool on = true; //�ð� ���� ����
    float cooldownTime = 5; //���� ��ȯ �ð�
    float delay = 0.38f; //��帧 ��ȯ ����
    public float time; //�ð�
    float cicileunmber = 15; //��帧 ��
    /*public Transform player;*/
    private GameObject icicleObjects;

    // Start is called before the first frame update
    void Start()
    {
         on = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (on == true) //�ð�
        {
            time += Time.deltaTime;
        }

        if (time >= cooldownTime) //��Ÿ���� ����(�ð��� ��Ÿ�Ӻ��� ������)
        {
            on = false; //�ð� ����
            time = 0;  //�ð� �ʱ�ȭ


            StartCoroutine(Objecton());  //���� �庮 ����



        }
        
        IEnumerator Objecton()
        {
            for (int i = 0; i <= cicileunmber; i++)
            {
                
                Vector3 shooterPosition = transform.position; 
                Vector3 spawnPosition = shooterPosition + new Vector3(Random.Range(-10.0f, 10.0f), 10, 0); //��ġ ��������
                icicleObjects = Instantiate(iciclePrefab, spawnPosition, Quaternion.identity);
                GameObject icicle = Instantiate(icicleObjects); //������Ʈ ��ȯ
                yield return new WaitForSeconds(delay);

            }

            on = true; //�ð� �簡��


            /*transform.Translate(speed * Time.deltaTime);*/





        }
    }
        
}
