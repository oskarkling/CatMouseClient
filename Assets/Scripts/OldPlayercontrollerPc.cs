// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [RequireComponent(typeof (Rigidbody))]
// public class PlayercontrollerPc : MonoBehaviour
// {
//     Vector3 velocity;
//     Rigidbody myRigidBody;

//     void Start() 
//     { 
//         myRigidBody = GetComponent<Rigidbody>();
//     }

//     public void Move(Vector3 _velocity) 
//     {
//         velocity = _velocity;
//     }

//     // public void LookAt(Vector3 lookPoint) 
//     // {
//     //     Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
//     //     transform.LookAt(heightCorrectedPoint);
//     // }

//     void FixedUpdate() 
//     {
//         myRigidBody.MovePosition(myRigidBody.position + velocity * Time.fixedDeltaTime);
//     }

//     public void Movement(float moveSpeed)
//     {
//         MoveWithWASDAndArrows(moveSpeed);
//     }

//     // works with holding the mouse button key
//     public Vector3 MoveWithMouse(Vector3 pos, float movespeed)
//     {
//         Vector3 targetPos = pos;
//         if(Input.GetMouseButton(1))
//         {
//             RaycastHit hitInfo;
//             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//             if(Physics.Raycast(ray, out hitInfo))
//             {
//                 targetPos = hitInfo.point;
//             }
//         }

//         return Vector3.MoveTowards(pos, targetPos, movespeed * Time.deltaTime);
//     }

//     private void MoveWithWASDAndArrows(float moveSpeed)
//     {
//         Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
//         Vector3 moveVelocity = moveInput.normalized * moveSpeed;
//         Move(moveVelocity);
//     }
// }
