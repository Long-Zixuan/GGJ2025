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

    private Vector3 targetScale;
    // Start is called before the first frame update
    void Start()
    {
        targetScale = transform.localScale;
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
        transform.localScale = Vector3.Lerp(this.transform.localScale, targetScale, 0.1f);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetScale = this.transform.localScale * shrinkRate;
            GameObject newBubble = Instantiate(bubblePrefab);
            newBubble.transform.position = this.transform.position;
            newBubble.GetComponent<BubbleLogic>().QuanZhong = Random.Range(0, 100);

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble") && collision.gameObject.GetComponent<BubbleLogic>().IsFree)
        {
            targetScale = BubbleLogic.GetScaleAtSameSize(this.transform.localScale, collision.gameObject.transform.localScale);
        }
    }
}
