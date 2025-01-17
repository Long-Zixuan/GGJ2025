using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GeneralFadeScript : MonoBehaviour
{
    //控制任意对象淡入淡出，延时，以及淡出后是否跳转场景

    [Header("淡入，延时，淡出")]
    public float fadeInDuration = 1f; // 淡入时长
    public float delayBeforeFadeOut = 2f; // 延迟淡出时长
    public float fadeOutDuration = 1f; // 淡出时长
    [Header("跳转场景")]
    public bool shouldLoadNextScene = true; // 控制是否在淡出后跳转场景的开关
    public string nextSceneName; // 下一个场景的名称

    private bool fadeInFinished = false; // 是否完成淡入
    private float timer; // 计时器
    private float delayTimer = 0f; // 延迟计时器
    private CanvasRenderer[] renderers; // 存储所有相关的渲染器

    void Start()
    {
        // 获取所有CanvasRenderer组件
        renderers = GetComponentsInChildren<CanvasRenderer>(true);

        // 初始化透明度为0
        foreach (var renderer in renderers)
        {
            ModifyAlpha(renderer, 0f);
        }

        timer = 0f; // 计时器初始化
    }

    void Update()
    {
        if (!fadeInFinished)
        {
            timer += Time.deltaTime; //  计时器累加

            if (timer <= fadeInDuration) // 计时器小于等于淡入时长
            {
                float alpha = Mathf.Lerp(0f, 1f, timer / fadeInDuration); // 计算淡入透明度
                ApplyAlphaToAll(alpha); // 应用淡入透明度
            }
            else
            {
                // 淡入完成
                fadeInFinished = true;
            }
        }
        else
        {
            // 延迟计时器
            delayTimer += Time.deltaTime;

            if (delayTimer >= delayBeforeFadeOut) // 达到延迟时间
            {
                float remainingTime = delayTimer - delayBeforeFadeOut;
                if (remainingTime <= fadeOutDuration)
                {
                    float alpha = Mathf.Lerp(1f, 0f, remainingTime / fadeOutDuration); // 计算淡出透明度
                    ApplyAlphaToAll(alpha); // 应用淡出透明度
                }
                else if (shouldLoadNextScene)
                {
                    // 跳转到下一个场景（仅当开关开启时）
                    SceneManager.LoadScene(nextSceneName);
                }
            }
        }
    }

    private void ApplyAlphaToAll(float alpha)
    {
        foreach (var renderer in renderers)
        {
            ModifyAlpha(renderer, alpha);
        }
    }

    private void ModifyAlpha(CanvasRenderer renderer, float alpha)
    {
        var color = renderer.GetColor();
        color.a = alpha;
        renderer.SetColor(color);
    }
}