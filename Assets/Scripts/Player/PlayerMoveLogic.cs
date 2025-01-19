using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerMoveLogic : MonoBehaviour
{
    public GameObject bubblePrefab;
    
    public float vSpeed = 5f;

    public float shrinkRate = 0.9f;

    private Vector3 targetScale;

    public float maxScale = 1.7f;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        targetScale = transform.localScale;
        //animator = GetComponent<Animator>();
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
            if(transform.localScale.x < maxScale)
            {
                targetScale = this.transform.localScale * shrinkRate;
            }
            else
            {
                targetScale = new Vector3(maxScale, maxScale, maxScale);
            }
            
            GameObject newBubble = Instantiate(bubblePrefab);
            newBubble.transform.localScale = this.transform.localScale / 4;
            newBubble.transform.position = this.transform.position;
            newBubble.GetComponent<BubbleLogic>().destoryTime = 5f;
            animator.SetTrigger("Attack");
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
