using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class snowcrystal : MonoBehaviour
{
    public GameObject projectilePrefab;  // �߻��� ����ü ������
    public float projectileSpeed = 1f;  // ����ü �ӵ�
    /*public float fireRate = 1f;*/     // �߻� ����
    public float time;
    public Transform firePoint;          // ����ü �߻� ��ġ
    public float projectileRange = 15f; // ����ü ��Ÿ�
    public int snowcrystalnmber = 20; //����ü ����
    public float delay = 0.25f; //��ȯ ����

    public bool on = true;

    private float cooldownTime = 10f;     // ���� �߻� �ð�

    void Update()
    {

        if (on == true)
        {
            time += Time.deltaTime;
        }

        if (time >= cooldownTime)
        {
            on = false;
            time = 0;
            StartCoroutine(re());

        }

        IEnumerator re()
        {
            
            for (int i = 0; i <= snowcrystalnmber; i++)
            {

                FireProjectile();
                yield return new WaitForSeconds(delay);

            }
            on = true;

        }

        void FireProjectile()
        {
            // ����ü ����
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // ������ ���� ����
            Vector2 randomDirection = Random.insideUnitCircle.normalized;

            // ����ü �̵� ��ũ��Ʈ ����
            ProjectileMovement projectileMovement = projectile.AddComponent<ProjectileMovement>();
            projectileMovement.Initialize(randomDirection, projectileSpeed, projectileRange);
        }
    }

    public class ProjectileMovement : MonoBehaviour
    {
        private Vector2 direction;   // �̵� ����
        private float speed;         // �̵� �ӵ�
        private float range;         // ��Ÿ�
        private Vector2 startPoint;  // ���� ��ġ

        public void Initialize(Vector2 dir, float spd, float rng)
        {
            direction = dir;
            speed = spd;
            range = rng;
            startPoint = transform.position;
        }

        void Update()
        {
            float rangeup = 1f;
            // ����ü�� �������� �̵�
            transform.Translate(direction * speed * Time.deltaTime);

            // ��Ÿ� üũ
            if (Vector2.Distance(startPoint, transform.position) >= rangeup)
            {
                speed = speed + 0.045f;
                rangeup = rangeup + 0.5f;
            }
                if (Vector2.Distance(startPoint, transform.position) >= range)
            {
                Destroy(gameObject);
            }
        }
        
    }
}