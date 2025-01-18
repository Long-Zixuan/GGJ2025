using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDoorLogic : MonoBehaviour
{

    public float maxScale = 2f;
    public float minScale = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenTheDoor()
    {
        if (SceneManage.Instance.GetPlayerScale() >= maxScale || SceneManage.Instance.GetPlayerScale() <= minScale)
        {
            List<Transform> allChildren = GetAllChildObjects(transform);
            foreach (Transform child in allChildren)
            {
                child.gameObject.SetActive(false);
            }
            
        }
    }
    
    List<Transform> GetAllChildObjects(Transform parent)
    {
        List<Transform> childObjects = new List<Transform>();
        foreach (Transform child in parent)
        {
            childObjects.Add(child);
            childObjects.AddRange(GetAllChildObjects(child));
        }
        return childObjects;
    }
}
