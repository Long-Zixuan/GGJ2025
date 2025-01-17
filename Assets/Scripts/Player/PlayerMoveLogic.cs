using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMoveLogic : MonoBehaviour
{
    public GameObject bubblePrefab;
    
    public float vSpeed = 5f;

    public float shrinkRate = 0.9f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        BlowBubble();
    }

    void Move()
    {
        transform.position += new Vector3(vSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), 0 ,0);
    }

    void BlowBubble()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.localScale *= shrinkRate;
            GameObject newBubble = Instantiate(bubblePrefab);
            newBubble.transform.position = this.transform.position;
            newBubble.GetComponent<BubbleLogic>().QuanZhong = Random.Range(0, 100);

        }
    }
}
