using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMianRotation : MonoBehaviour
{
    public float rSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }
    
    void Rotation()
    {
        transform.Rotate(0, 0, rSpeed * Time.deltaTime);
    }
}
