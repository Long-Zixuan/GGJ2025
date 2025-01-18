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
    public GameObject cameraMain;


    private CanvasGroup T1CanvasGroup;
    private CanvasGroup T2CanvasGroup;
    private CanvasGroup T3CanvasGroup;
    private CanvasGroup T4CanvasGroup;

    private float fadeInDuration = 1.5f; // 淡入持续时间  
    private float fadeOutDuration = 1.5f; // 淡出持续时间  

    private void Start()
    {
        T1CanvasGroup = T1.GetComponent<CanvasGroup>();
        T2CanvasGroup = T2.GetComponent<CanvasGroup>();
        T3CanvasGroup = T3.GetComponent<CanvasGroup>();
        T4CanvasGroup = T4.GetComponent<CanvasGroup>();

        StartCoroutine(FadeIn());
        T2.transform.DOMoveX(0, 5f, false);
        T3.transform.DOMoveX(0, 8f, false);
        T4.transform.DOMoveX(0, 11f, false);
    }

    private IEnumerator FadeIn()
    {
        T1CanvasGroup.alpha = 0f; // 初始化为完全透明  
        T2CanvasGroup.alpha = 0f;
        T3CanvasGroup.alpha = 0f;
        T4CanvasGroup.alpha = 0f;

        // 淡入  
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            T1CanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        T1CanvasGroup.alpha = 1f; // 确保完全不透明  

        // 等待 1 秒  
        yield return new WaitForSeconds(1.5f);

        // 淡出  
        elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            T1CanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);

            // 在 T1 淡出的一半时间点开始淡入 T2  
            if (elapsedTime >= fadeOutDuration / 2)
            {
                T2CanvasGroup.alpha = Mathf.Lerp(0f, 1f, (elapsedTime - fadeOutDuration / 2) / (fadeOutDuration / 2));
            }

            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        T1CanvasGroup.alpha = 0f; // 确保完全透明  
        T2CanvasGroup.alpha = 1f;

        //等待1s
        yield return new WaitForSeconds(1.5f);


        elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            T2CanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);

            // 在 T2 淡出的一半时间点开始淡入 T3 
            if (elapsedTime >= fadeOutDuration / 2)
            {
                T3CanvasGroup.alpha = Mathf.Lerp(0f, 1f, (elapsedTime - fadeOutDuration / 2) / (fadeOutDuration / 2));
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        T2CanvasGroup.alpha = 0f;
        T3CanvasGroup.alpha = 1f;

        //持续时间,其余默认
        cameraMain.transform.DOShakePosition(1);
        //持续时间、强度（下为只在X、Y方向上震动）
        cameraMain.transform.DOShakePosition(1, new Vector3(1, 1, 0));

        //等待1s
        yield return new WaitForSeconds(2f);



        elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            T3CanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);

            // 在 T2 淡出的一半时间点开始淡入 T3 
            if (elapsedTime >= fadeOutDuration / 2)
            {
                T4CanvasGroup.alpha = Mathf.Lerp(0f, 1f, (elapsedTime - fadeOutDuration / 2) / (fadeOutDuration / 2));
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        T3CanvasGroup.alpha = 0f;
        T4CanvasGroup.alpha = 1f;


        //等待1s
        yield return new WaitForSeconds(1f);

       
            SceneManager.LoadScene("S_Level_1");
        
        

    }


}
