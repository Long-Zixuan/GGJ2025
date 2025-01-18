using UnityEngine;
using UnityEngine.UI;

public class QuitGameButton : MonoBehaviour
{
    //按钮关闭游戏

    private void Start()
    {
        // 在 Start 方法中添加按钮点击事件的监听器
        GetComponent<Button>().onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        Debug.Log("你关闭了游戏");
        // 退出游戏
        Application.Quit();
    }
}