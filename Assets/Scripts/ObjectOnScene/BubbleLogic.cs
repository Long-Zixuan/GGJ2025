using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleLogic : MonoBehaviour
{
    public float destoryTime;
    
    public GameObject bubblePrefab;
    
    [SerializeField]
    private float quan_zhong;
    
    private bool isFree_ = false;
    public bool IsFree
    {
        get { return isFree_; }
    }

    public float QuanZhong
    {
        set { quan_zhong = value; }
        get { return quan_zhong; }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (destoryTime > 0)
        {
            Invoke("SelfDestroy", destoryTime);
        }
        Invoke("SetFree", 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetFree()
    {
        isFree_ = true;
    }


    void SelfDestroy()
    {
        StartCoroutine(SelfDestoryAsync());
    }
    IEnumerator SelfDestoryAsync()
    {
        //播放动画
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    
    public static Vector3 GetScaleAtSameSize(Vector3 scale1, Vector3 scale2)
    {
        return new Vector3(Mathf.Sqrt(Mathf.Pow(scale1.x, 2.0f) 
                                      + Mathf.Pow(scale2.x, 2.0f)), Mathf.Sqrt(Mathf.Pow(scale1.y, 2.0f) 
                                                                               + Mathf.Pow(scale2.y, 2.0f)), Mathf.Sqrt(Mathf.Pow(scale1.z, 2.0f) 
            + Mathf.Pow(scale2.z, 2.0f)));
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
           
           /* newBubble.transform.localScale = new Vector3(Mathf.Sqrt(Mathf.Pow(other.transform.localScale.x ,2.0f)
                                                                    + Mathf.Pow(transform.localScale.x, 2.0f)), Mathf.Sqrt(Mathf.Pow(other.transform.localScale.y,2.0f)
                + Mathf.Pow(transform.localScale.y, 2.0f)), Mathf.Sqrt(Mathf.Pow(other.transform.localScale.z,2.0f) + Mathf.Pow(transform.localScale.z,2.0f)));*/
            newBubble.transform.localScale = GetScaleAtSameSize(other.transform.localScale, transform.localScale);
            Destroy(gameObject);
        }
    }

    void BubbleColPlayerLogic()
    {
        if (isFree_)
        {
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            BubbleColBubbleLogic(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            BubbleColPlayerLogic();
        }
    }
}
