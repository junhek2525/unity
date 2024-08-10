using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class darkness_unfolds : MonoBehaviour
{
    public GameObject mainLightObject; // ���� ����Ʈ ������Ʈ
    public GameObject playerLightObject; // �÷��̾� ����Ʈ ������Ʈ
    public bool Darkness_Unfolds = false; // �ܺο��� ������ ����
    public float fadeDuration = 2.0f; // ������ ���ϴ� �ð� (��)
    public float darkDuration = 10.0f; // ��ο� ���� ���� �ð� (��)
    public float playerTargetIntensity = 1.0f; // �÷��̾� ����Ʈ�� ���� ��� ��

    private Light2D mainLight;
    private Light2D playerLight;
    private float mainOriginalIntensity;
    private float initialPlayerIntensity = 0.0f; // �÷��̾� ����Ʈ�� �ʱ� ��� �� (0���� ����)

    private void Start()
    {
        // ���� ����Ʈ ������Ʈ���� Light 2D ������Ʈ ��������
        if (mainLightObject != null)
        {
            mainLight = mainLightObject.GetComponent<Light2D>();
            if (mainLight != null)
            {
                mainOriginalIntensity = mainLight.intensity;
            }
            else
            {
                Debug.LogError("Light 2D component not found on the main light object.");
            }
        }
        else
        {
            Debug.LogError("Main light object is not assigned.");
        }

        // �÷��̾� ����Ʈ ������Ʈ���� Light 2D ������Ʈ ��������
        if (playerLightObject != null)
        {
            playerLight = playerLightObject.GetComponent<Light2D>();
            if (playerLight != null)
            {
                // �÷��̾� ����Ʈ�� �ʱ� ��� ����
                playerLight.intensity = initialPlayerIntensity;
            }
            else
            {
                Debug.LogError("Light 2D component not found on the player light object.");
            }
        }
        else
        {
            Debug.LogError("Player light object is not assigned.");
        }
    }

    private void Update()
    {
        // Darkness_Unfolds ������ true�� ����Ǹ� �ڷ�ƾ ����
        if (Darkness_Unfolds && mainLight != null && playerLight != null)
        {
            Darkness_Unfolds = false; // �ѹ��� ����ǵ��� �ٽ� false�� ����
            StartCoroutine(ControlLights());
        }
    }

    private IEnumerator ControlLights()
    {

        // ���� ����Ʈ�� ���� ��⿡�� 0���� ������ ���̱�
        // �÷��̾� ����Ʈ�� �ʱ� ��⿡�� ��ǥ ������ ������ ����
        float mainStartIntensity = mainLight.intensity;
        float playerStartIntensity = initialPlayerIntensity; // �÷��̾� ����Ʈ�� ���� ���
        for (float t = 0.0f; t < fadeDuration; t += Time.deltaTime)
        {
            mainLight.intensity = Mathf.Lerp(mainStartIntensity, 0, t / fadeDuration);
            playerLight.intensity = Mathf.Lerp(playerStartIntensity, playerTargetIntensity, t / fadeDuration);

            yield return null;
        }
        mainLight.intensity = 0; // Ȯ���ϰ� 0���� ����
        playerLight.intensity = playerTargetIntensity; // Ȯ���ϰ� ��ǥ ���� ����

        // ��ο� ���� ���� �ð� ���
        yield return new WaitForSeconds(darkDuration);

        // ���� ����Ʈ�� 0���� ���� ������ ������ ����
        // �÷��̾� ����Ʈ�� ��ǥ ��⿡�� 0���� ������ ���̱�
        for (float t = 0.0f; t < fadeDuration; t += Time.deltaTime)
        {
            mainLight.intensity = Mathf.Lerp(0, mainOriginalIntensity, t / fadeDuration);
            playerLight.intensity = Mathf.Lerp(playerTargetIntensity, 0, t / fadeDuration);

            yield return null;
        }
        mainLight.intensity = mainOriginalIntensity; // Ȯ���ϰ� ���� ���� ����
        playerLight.intensity = 0; // Ȯ���ϰ� 0���� ����
    }
}
