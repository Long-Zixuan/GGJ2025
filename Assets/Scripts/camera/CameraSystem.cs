using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public Transform playerTarget;
    public float cameraMoveSpeed;
    private void LateUpdate()
    {
        if(playerTarget!=null)
        {
            if(playerTarget.position!=transform.position)
            {
                transform.position = Vector3.Lerp(transform.position, playerTarget.position, cameraMoveSpeed * Time.deltaTime);

            }
        }
    }
}
