
using UnityEngine;
using System.Collections;

public class coldwave : MonoBehaviour
{
    public GameObject coldPrefab; 
    public GameObject redPrefab;
    bool on = true; //�ð� ���� ����
    float cooldownTime = 5; //���� ��ȯ �ð�
    float Duration = 7; //���� �庮 ���ӽð�
    float cooltime = 1;
    float time; //�ð�
    float time1;
    public Transform player;


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
            StartCoroutine(Objecton()); //���� �庮 ����
            
        }

        IEnumerator Objecton()
        {
            /*GameObject attention = Instantiate(redPrefab);
            attention.transform.position = player.position;
            yield return new WaitForSeconds(cooltime); // ���� ���ǥ���� �����Ǵ� �ڵ� © ����
            Destroy(attention);*/
            GameObject go = Instantiate(coldPrefab); //������Ʈ ��ȯ
            go.transform.position = player.position; //�÷��̾� ��ġ�� �̵�

            yield return new WaitForSeconds(Duration); //���ӽð��� ������
            Destroy(go); //������Ʈ ����
                time1 = 0;
                on = true; //�ð� �簡��
            
        }
    }
}
