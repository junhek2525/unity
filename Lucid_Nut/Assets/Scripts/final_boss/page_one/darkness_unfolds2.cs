using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class darkness_unfolds2 : MonoBehaviour
{
    public GameObject lightObject; // Light 2D ������Ʈ�� �����ϴ� ������Ʈ
    public darkness_unfolds mainLightController; // ���� ����Ʈ ��Ʈ�ѷ� ����
    public float fadeDuration = 2.0f; // ������ ���ϴ� �ð� (��)
    public float darkDuration = 10.0f; // ��ο� ���� ���� �ð� (��)

    private Light2D light2D;
    private float originalIntensity;

    private void Start()
    {
        // lightObject���� Light 2D ������Ʈ ��������
        if (lightObject != null)
        {
            light2D = lightObject.GetComponent<Light2D>();
            if (light2D != null)
            {
                originalIntensity = light2D.intensity;
            }
            else
            {
                Debug.LogError("Light 2D component not found on the assigned object.");
            }
        }
        else
        {
            Debug.LogError("Light object is not assigned.");
        }
    }

    private void Update()
    {
        // ���� ����Ʈ ��Ʈ�ѷ��� Darkness_Unfolds ������ true�� ����Ǹ� �ڷ�ƾ ����
        if (mainLightController.Darkness_Unfolds && light2D != null)
        {
            StartCoroutine(BrightenLight());
        }
    }

    private IEnumerator BrightenLight()
    {
        // ���� ��⿡�� ���� ������ ������ ����
        float startIntensity = light2D.intensity;
        for (float t = 0.0f; t < fadeDuration; t += Time.deltaTime)
        {
            light2D.intensity = Mathf.Lerp(startIntensity, originalIntensity, t / fadeDuration);
            yield return null;
        }
        light2D.intensity = originalIntensity; // Ȯ���ϰ� ���� ���� ����

        // ���� ���� ���� �ð� ���
        yield return new WaitForSeconds(darkDuration);

        // ���� ��⿡�� 0���� ������ ���̱�
        for (float t = 0.0f; t < fadeDuration; t += Time.deltaTime)
        {
            light2D.intensity = Mathf.Lerp(originalIntensity, 0, t / fadeDuration);
            yield return null;
        }
        light2D.intensity = 0; // Ȯ���ϰ� 0���� ����
    }
}
