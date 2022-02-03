using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DampCamera : MonoBehaviour
{

    private Transform target;
    public float lerpPercent;

    Vector3 targetOffset;

    private void Awake()
    {
        var player = FindObjectOfType<PlayerControllerPc>();
        target = player.transform;
    }

    private void Start()
    {
        targetOffset = transform.position - target.position;
    }

    private void Update()
    {   
        if(target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, lerpPercent);
        }
    }

    private void LateUpdate()
    {
    }
}