using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;
using Cinemachine;

public class CameraFollowPlayer : MonoBehaviour
{

    private GameObject player;
    private CinemachineVirtualCamera vcam;
 
    // Use this for initialization
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("LocalPlayer");

        if (player != null)
        {
            vcam.LookAt = player.transform;
            vcam.Follow = player.transform;
        }
    }
}
