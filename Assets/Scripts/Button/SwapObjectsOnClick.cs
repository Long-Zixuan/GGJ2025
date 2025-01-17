using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwapObjectsOnClick : MonoBehaviour
{
    //按下按钮后交换两个任意对象的位置，并让它们平滑地移动到新的位置

    public GameObject object1; // 第一个对象
    public GameObject object2; // 第二个对象
    public Button swapButton; // 用于触发交换的按钮
    public float moveSpeed = 5f; // 平移速度

    private bool isSwapping = false;

    void Start()
    {
        if (swapButton != null)
        {
            swapButton.onClick.AddListener(OnSwapButtonClick);
        }
        else
        {
            Debug.LogError("请将按钮拖动到Swap Button字段中！");
        }
    }

    // 当按钮被点击时调用此方法
    private void OnSwapButtonClick()
    {
        if (!isSwapping)
        {
            StartCoroutine(SwapPositions());
        }
    }

    IEnumerator SwapPositions()
    {
        isSwapping = true;

        Vector3 pos1 = object1.transform.position;
        Vector3 pos2 = object2.transform.position;

        float elapsedTime = 0f;

        while (elapsedTime < (Vector3.Distance(pos1, pos2) / moveSpeed))
        {
            object1.transform.position = Vector3.Lerp(pos1, pos2, (elapsedTime * moveSpeed) / Vector3.Distance(pos1, pos2));
            object2.transform.position = Vector3.Lerp(pos2, pos1, (elapsedTime * moveSpeed) / Vector3.Distance(pos1, pos2));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保最终位置精确
        object1.transform.position = pos2;
        object2.transform.position = pos1;

        isSwapping = false;
    }
}