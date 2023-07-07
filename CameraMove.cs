using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private float minPosY;
    [SerializeField]
    private float minPosY2;
    [SerializeField]
    private float maxPosY;
    [SerializeField]
    private float maxPosY2;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject BackGround;

    void LateUpdate()
    {
        followPlayer();
    }

    void followPlayer()
    {
        if(player.transform.position.y > minPosY && player.transform.position.y < maxPosY)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z); 
        }
        if (player.transform.position.y > minPosY2 && player.transform.position.y < maxPosY2)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        }
    }

}
