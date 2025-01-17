using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoadScene : MonoBehaviour
{
    //按钮控制跳转场景

    public string targetSceneName; // 目标场景的名称

    public void LoadTargetScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}