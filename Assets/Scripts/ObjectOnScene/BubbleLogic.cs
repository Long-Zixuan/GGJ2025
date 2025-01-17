using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleLogic : MonoBehaviour
{
    
    public GameObject bubblePrefab;
    
    [SerializeField]
    private float quan_zhong;

    public float QuanZhong
    {
        set { quan_zhong = value; }
        get { return quan_zhong; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BubbleColBubbleLogic(GameObject other)
    {
        if (other.GetComponent<BubbleLogic>().QuanZhong > quan_zhong)
        {
            Destroy(gameObject);
        }
        else if (other.GetComponent<BubbleLogic>().QuanZhong < quan_zhong)
        {
            Vector3 midpoint = (transform.position + other.transform.position) / 2;
            GameObject newBubble = Instantiate(bubblePrefab, midpoint, Quaternion.identity);
            newBubble.gameObject.GetComponent<BubbleLogic>().QuanZhong = Random.Range(0, 100);
            //临时逻辑，以后要改
            newBubble.transform.localScale = new Vector3(other.transform.localScale.x + transform.localScale.x, other.transform.localScale.y + transform.localScale.y, other.transform.localScale.z + transform.localScale.z);
            Destroy(gameObject);
        }
    }

    void BubbleColPlayerLogic()
    {
        gameObject.GetComponent<PooledObject>().Release();
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            BubbleColBubbleLogic(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
           // BubbleColPlayerLogic();
        }
    }
}
