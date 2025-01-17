using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMoveUI : MonoBehaviour
{
    public Button toggleButton; // 用于触发移动的按钮
    public RectTransform uiObject; // 要移动的UI对象
    public float movePercentage = 0.1f; // 移动距离占屏幕宽度的百分比

    private Vector2 originalPosition;
    private bool isMoved = false;

    void Start()
    {
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(OnToggleButtonClick);
        }
        else
        {
            Debug.LogError("请将按钮拖动到Toggle Button字段中！");
        }

        // 记录原始位置
        originalPosition = uiObject.anchoredPosition;
    }

    // 当按钮被点击时调用此方法
    private void OnToggleButtonClick()
    {
        StartCoroutine(MoveUI());
    }

    IEnumerator MoveUI()
    {
        Vector2 targetPosition;
        if (!isMoved)
        {
            // 计算目标位置，向左移动movePercentage * 屏幕宽度
            float moveAmount = Screen.width * movePercentage;
            targetPosition = new Vector2(originalPosition.x - moveAmount, originalPosition.y);
        }
        else
        {
            // 返回原始位置
            targetPosition = originalPosition;
        }

        // 平滑移动
        while (Vector2.Distance(uiObject.anchoredPosition, targetPosition) > 0.01f)
        {
            uiObject.anchoredPosition = Vector2.Lerp(uiObject.anchoredPosition, targetPosition, Time.deltaTime * 5f);
            yield return null;
        }

        // 确保最终位置精确
        uiObject.anchoredPosition = targetPosition;

        // 切换状态
        isMoved = !isMoved;
    }
}