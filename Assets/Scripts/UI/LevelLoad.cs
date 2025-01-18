using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
    public float animationTime = 1f;
    public Animator animator;
    public string levelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadSceneWithFade();
        }
    }

    public void LoadSceneWithFade()
    {
        StartCoroutine(LoadScene(levelToLoad));
    }

    IEnumerator LoadScene(string level)
    {
        animator.SetTrigger("End");
        
        yield return new WaitForSeconds(animationTime);
        
        SceneManager.LoadScene(level);
        
    }
}
