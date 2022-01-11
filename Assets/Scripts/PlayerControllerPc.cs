using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerPc : MonoBehaviour
{
    private Player player;
    private Vector3 targetPos;
    private Vector3 lookDirection;
    private Quaternion rotation;
    private float moveSpeed;
    private float rotSpeed;
    private bool moving;
    private Vector3 posPlusRange;
    private Vector3 posMinusRange;

    //testing rigidbody
    new private Rigidbody rigidbody;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        rigidbody = GetComponent<Rigidbody>();
        moveSpeed = player.moveSpeed;
        rotSpeed = player.rotationSpeed;
        moving = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            SetTargetPositon();
        }
    }

    void FixedUpdate()
    {
        if(moving)
        {
            Move();
            //MoveRigidbody();
            //MovePlayerObject();
        }
    }

    private void MovePlayerObject()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.fixedDeltaTime);
        
        moving = false;
    }

    private void MoveRigidbody()
    {
        rigidbody.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.fixedDeltaTime);

        // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.fixedDeltaTime);


        rigidbody.velocity = (targetPos - transform.position).normalized * moveSpeed; 

        if(transform.position.x < posPlusRange.x && transform.position.z < posPlusRange.z && transform.position.x > posMinusRange.x && transform.position.z > posMinusRange.z)
        {
            moving = false;
            print("Not moving");
        }
    }

    private void SetTargetPositon()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hitInfo, 1000))
        {
            targetPos = hitInfo.point;
            
            lookDirection = new Vector3(targetPos.x - transform.position.x, transform.position.y, targetPos.z - transform.position.z);
            rotation = Quaternion.LookRotation(lookDirection);

            posPlusRange = targetPos + new Vector3(0.5f,0.5f,0.5f);
            posMinusRange = targetPos - new Vector3(0.5f,0.5f,0.5f);
            
            print(posPlusRange.ToString() + " plus range");
            print(posMinusRange.ToString() + " minus range");
            
            moving = true;
            print("moving");
        }
    }

    private void Move()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.fixedDeltaTime);

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);

        if(transform.position.x < posPlusRange.x && 
            transform.position.z < posPlusRange.z && 
            transform.position.x > posMinusRange.x && transform.position.z > posMinusRange.z)
        {
            moving = false;
            print("Not moving");            
        }
    }
}
