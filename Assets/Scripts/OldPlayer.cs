// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [RequireComponent(typeof(PlayercontrollerPc))]
// public class Player : MonoBehaviour
// {
//     //public static event Action moved;
//     public delegate void Moving(Vector3 pos);
//     public static Moving movingDelegateInstance;

//     public float moveSpeed = 5;

//     private PlayercontrollerPc controllerPc;
//     private Camera viewCamera;

//     void Awake()
//     {
//         controllerPc = GetComponent<PlayercontrollerPc>();
//         viewCamera = Camera.main;
        

//     }
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         controllerPc.Movement(moveSpeed);

//         transform.position = controllerPc.MoveWithMouse(transform.position, moveSpeed);

//         //moved?.Invoke();
//         movingDelegateInstance?.Invoke(transform.position);

//     }
// }
