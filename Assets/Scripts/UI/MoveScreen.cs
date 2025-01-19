using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class MoveScreen : MonoBehaviour
{
    public GameObject T1;
    public GameObject T2;
    public GameObject T3;
    public GameObject T4;
    public GameObject T5;
    public GameObject cameraMain;


    private CanvasGroup T1CanvasGroup;
    private CanvasGroup T2CanvasGroup;
    private CanvasGroup T3CanvasGroup;
    private CanvasGroup T4CanvasGroup;
    private CanvasGroup T5CanvasGroup;

    private bool canLoad = false;

    private float fadeInDuration = 1.5f; // �������ʱ��  
    private float fadeOutDuration = 1.5f; // ��������ʱ��  

    private void Start()
    {
        T1CanvasGroup = T1.GetComponent<CanvasGroup>();
        T2CanvasGroup = T2.GetComponent<CanvasGroup>();
        T3CanvasGroup = T3.GetComponent<CanvasGroup>();
        T4CanvasGroup = T4.GetComponent<CanvasGroup>();
        T5CanvasGroup = T5.GetComponent<CanvasGroup>();

        StartCoroutine(FadeIn());
        T2.transform.DOMoveX(0, 5f, false);
        T3.transform.DOMoveX(0, 8f, false);
        T4.transform.DOMoveX(0, 11f, false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canLoad)
        {
            T5.SetActive(false);
            SceneManager.LoadScene("S_Level_1");
        }
    }

    private IEnumerator FadeIn()
    {
        T1CanvasGroup.alpha = 0f; // ��ʼ��Ϊ��ȫ͸��  
        T2CanvasGroup.alpha = 0f;
        T3CanvasGroup.alpha = 0f;
        T4CanvasGroup.alpha = 0f;
        T5CanvasGroup.alpha = 0f;

        // ����  
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            T1CanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        T1CanvasGroup.alpha = 1f; // ȷ����ȫ��͸��  

        // �ȴ� 1 ��  
        yield return new WaitForSeconds(1.5f);

        // ����  
        elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            T1CanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);

            // �� T1 ������һ��ʱ��㿪ʼ���� T2  
            if (elapsedTime >= fadeOutDuration / 2)
            {
                T2CanvasGroup.alpha = Mathf.Lerp(0f, 1f, (elapsedTime - fadeOutDuration / 2) / (fadeOutDuration / 2));
            }

            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        T1CanvasGroup.alpha = 0f; // ȷ����ȫ͸��  
        T2CanvasGroup.alpha = 1f;

        //�ȴ�1s
        yield return new WaitForSeconds(1.5f);


        elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            T2CanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);

            // �� T2 ������һ��ʱ��㿪ʼ���� T3 
            if (elapsedTime >= fadeOutDuration / 2)
            {
                T3CanvasGroup.alpha = Mathf.Lerp(0f, 1f, (elapsedTime - fadeOutDuration / 2) / (fadeOutDuration / 2));
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        T2CanvasGroup.alpha = 0f;
        T3CanvasGroup.alpha = 1f;

        //����ʱ��,����Ĭ��
        cameraMain.transform.DOShakePosition(1);
        //����ʱ�䡢ǿ�ȣ���Ϊֻ��X��Y�������𶯣�
        cameraMain.transform.DOShakePosition(1, new Vector3(1, 1, 0));

        //�ȴ�1s
        yield return new WaitForSeconds(2f);



        elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            T3CanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);

            // �� T2 ������һ��ʱ��㿪ʼ���� T3 
            if (elapsedTime >= fadeOutDuration / 2)
            {
                T4CanvasGroup.alpha = Mathf.Lerp(0f, 1f, (elapsedTime - fadeOutDuration / 2) / (fadeOutDuration / 2));
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        T3CanvasGroup.alpha = 0f;
        T4CanvasGroup.alpha = 1f;


        elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            T4CanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);

            // �� T2 ������һ��ʱ��㿪ʼ���� T3 
            if (elapsedTime >= fadeOutDuration / 2)
            {
                T5CanvasGroup.alpha = Mathf.Lerp(0f, 1f, (elapsedTime - fadeOutDuration / 2) / (fadeOutDuration / 2));
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        T4CanvasGroup.alpha = 0f;
        T5CanvasGroup.alpha = 1f;

        canLoad = true;
        //�ȴ�1s
        yield return new WaitForSeconds(1f);

       
        
           // SceneManager.LoadScene("S_Level_1");
        
        

    }


}
