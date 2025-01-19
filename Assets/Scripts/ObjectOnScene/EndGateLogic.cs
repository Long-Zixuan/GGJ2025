using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGateLogic : MonoBehaviour
{
    public float daoJvCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManage.Instance.Player.GetComponent<PlayerMoveLogic>().GetDaoJvCount == daoJvCount)
        {
            Destroy(gameObject);
        }
    }
}
