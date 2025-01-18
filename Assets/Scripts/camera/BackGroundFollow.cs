using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class BackGroundFollow : MonoBehaviour
{
    //需要添加的背景图,
    List<SpriteRenderer> CurrentSpriteRenderers = new List<SpriteRenderer>();

    //控制移动的对象
    List<Transform> Parents = new List<Transform>();

    //图片的宽
    public float spriteOffsetX = 50.74f;  //图片与图片之间的偏移,也就是一张背景图的宽度


    //主相机
    Camera cam;

    //屏幕宽对应2D世界中的长度
    float CameraWightTranslate2Dlength;

    //上一针相机的位置
    Vector3 cameraLastPos;

    private void Awake()
    {
        cam = Camera.main;

        //计算相机宽度
        CameraWightTranslate2Dlength = GetCameraWightTranslate2Dlength();

        CreateBackgroundAndParents();

        //获取图片的宽
        BoxCollider2D tmp = CurrentSpriteRenderers[0].gameObject.AddComponent< BoxCollider2D > ();
        spriteOffsetX = tmp.size.x * tmp.transform.localScale.x;
        Destroy (tmp);
    }

    private void Start()
    {
        cameraLastPos = cam.transform.position;
    }

    private void Update()
    {
        Follow();

        //记录相机位置
        cameraLastPos = cam.transform.position;
    }

    private void Follow()
    {

        bool isRight = false;

        //判断相机左移还是右移
        if (cameraLastPos.x < cam.transform.position.x)
        {
            isRight = true;
        }
        else if (cameraLastPos.x > cam.transform.position.x)
        {
            isRight = false;
        }
        else
        {
            //相机无移动
            return;
        }


        for (int i = 0; i < CurrentSpriteRenderers.Count; i++)
        {

            Transform trans = CurrentSpriteRenderers[i].transform;

            //当前图片的左边界
            float spriteLeft = -spriteOffsetX / 2f + trans.position.x;
            float cameraLeft = cam.transform.position.x - CameraWightTranslate2Dlength / 2f;
            //当前图片的右边界
            float spriteRight = spriteOffsetX / 2f + CurrentSpriteRenderers[i].transform.position.x;
            float cameraRight = cam.transform.position.x + CameraWightTranslate2Dlength / 2f;


            //判断新的背景图片生成的位置
            if (isRight)
            {
                if (spriteRight - cameraRight < 3f)
                {
                    //移动另一张图片
                    for (int z = 0; z < trans.parent.childCount; z++)
                    {
                        if (trans.parent.GetChild(z) != trans)
                        {
                            trans.parent.GetChild(z).position = trans.position + new Vector3(spriteOffsetX, 0, 0);
                        }
                    }
                }
                if (spriteRight - cameraRight < -3f)
                {
                    //替换列表元素
                    for (int z = 0; z < trans.parent.childCount; z++)
                    {
                        if (trans.parent.GetChild(z) != trans)
                        {
                            CurrentSpriteRenderers[i] = trans.parent.GetChild(z).GetComponent<SpriteRenderer>();
                        }
                    }
                }

            }
            else
            {

                if (Mathf.Abs(spriteLeft - cameraLeft) < 3f)
                {
                    //移动另一张图片
                    for (int z = 0; z < trans.parent.childCount; z++)
                    {
                        if (trans.parent.GetChild(z) != trans)
                        {
                            trans.parent.GetChild(z).position = trans.position - new Vector3(spriteOffsetX, 0, 0);
                        }
                    }
                }
                if (cameraLeft - spriteLeft < -3f)
                {
                    //替换列表元素
                    for (int z = 0; z < trans.parent.childCount; z++)
                    {
                        if (trans.parent.GetChild(z) != trans)
                        {
                            CurrentSpriteRenderers[i] = trans.parent.GetChild(z).GetComponent<SpriteRenderer>();
                        }
                    }
                }
            }

            //相机的移动量
            Vector3 cameraMove = cam.transform.position - cameraLastPos;

            //背景图层的移动
            //越远的图层移动越慢,反之则越快
            Parents[i].transform.position += cameraMove * 1f / (float)(Parents.Count + 1 - i);

        }


    }

    public void CreateBackgroundAndParents()
    {
        //获取所有sprite
        CurrentSpriteRenderers = transform.GetComponentsInChildren<SpriteRenderer>().ToList();
        //根据图层排序      
        //由大到小
        CurrentSpriteRenderers.Sort((x, y) => -x.sortingOrder.CompareTo(y.sortingOrder));

        //把相同层级的背景移动到对应的父物体上,方便统一移动同一层级下的背景
        for (int i = 0; i < CurrentSpriteRenderers.Count; i++)
        {
            //新的父对象
            GameObject groundParent = new GameObject("层级_" + CurrentSpriteRenderers[i].sortingOrder);
            groundParent.transform.parent = transform;

            //同层 加入一个父对象中
            CurrentSpriteRenderers[i].transform.parent = groundParent.transform;

            SpriteRenderer spr = Instantiate(CurrentSpriteRenderers[i], groundParent.transform);
            spr.transform.position = CurrentSpriteRenderers[i].transform.position;
            spr.name = CurrentSpriteRenderers[i].name;

            Parents.Add(groundParent.transform);
        }
    }

    private float GetCameraWightTranslate2Dlength()
    {
        Vector3 cornerPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));
        Vector3 cornerPos0 = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));

        //计算相机宽度
        return cornerPos.x - cornerPos0.x;
    }


}


