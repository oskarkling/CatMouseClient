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

    // pathfinding
    private int currentPathIndex;
    private List<Vector3> pathVector3List;
    private PathFinding pathFinder;

    public GameObject wallPrefab;

    //testing rigidbody
    new private Rigidbody rigidbody;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        rigidbody = GetComponent<Rigidbody>();
        moveSpeed = player.moveSpeed;
        rotSpeed = player.rotationSpeed;
        moving = false;

        pathFinder = new PathFinding(10, 10);

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
            MovePathFinding();
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            if(Utility.MouseUtility.GetMousePositonOn3DSpace(out Vector3 mousePos))
            {
                pathFinder.GetGrid().GetXZ(mousePos, out int x, out int z);

                // PathNode node = pathFinding.GetNode(x, z);
                // node.isWalkable = false;
                // node.SetIsWalkable(false);
                //pathFinding.GetNode(x,z).SetIsWalkable(false);
                

                int cellsize = (int)pathFinder.GetGrid().GetCellsize();

                Vector3 middle = pathFinder.GetGrid().GetWorldPosition(x,z) + new Vector3(cellsize / 2, 0, cellsize / 2);

                pathFinder.GetNode(x, z).SetIsWalkable(!pathFinder.GetNode(x,z).isWalkable);

                Instantiate(wallPrefab, middle, Quaternion.identity);
            }
        }
    }

    void FixedUpdate()
    {
        if(moving)
        {
            //Move();
            //MoveRigidbody();
            //MovePlayerObject();
            
            HandlePathFindingMovement();
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetNotWalkable(int x, int z)
    {
        pathFinder.GetNode(x, z).SetIsWalkable(!pathFinder.GetNode(x,z).isWalkable);
    }

    public void SetWalkable(int x, int z)
    {
        pathFinder.GetNode(x, z).SetIsWalkable(pathFinder.GetNode(x,z).isWalkable);
    }

    private void StopMoving()
    {
        pathVector3List = null;
    }
    private void MovePathFinding()
    {
        currentPathIndex = 0;
        pathVector3List = pathFinder.FindPath(GetPosition(), targetPos);

        if(pathVector3List != null && pathVector3List.Count > 1)
        {
            pathVector3List.RemoveAt(0);
        }
        
    }

    private void HandlePathFindingMovement()
    {
        if(pathVector3List != null)
        {
            Vector3 pathTargetPosition = pathVector3List[currentPathIndex];
            
            if(Vector3.Distance(transform.position, pathTargetPosition) > 1f)
            {
                Vector3 moveDir = (pathTargetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, pathTargetPosition);

                // animate moving character here

                transform.position = transform.position + moveDir * moveSpeed * Time.deltaTime;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), rotSpeed * Time.fixedDeltaTime);                
                // rotate character here
            }
            else
            {
                currentPathIndex++;
                if(currentPathIndex >= pathVector3List.Count)
                {
                    StopMoving();
                    moving = false;
                    // animate stop moving character here
                }
            }
        }
        else
        {
            // animate stop moving character here
            moving = false;
        }
    }

    // private void MovePlayerObject()
    // {
    //     transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.fixedDeltaTime);
        
    //     moving = false;
    // }

    // private void MoveRigidbody()
    // {
    //     rigidbody.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.fixedDeltaTime);

    //     // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.fixedDeltaTime);


    //     rigidbody.velocity = (targetPos - transform.position).normalized * moveSpeed; 

    //     if(transform.position.x < posPlusRange.x && transform.position.z < posPlusRange.z && transform.position.x > posMinusRange.x && transform.position.z > posMinusRange.z)
    //     {
    //         moving = false;
    //         // print("Not moving");
    //     }
    // }

    private void SetTargetPositon()
    {
        if(Utility.MouseUtility.GetMousePositonOn3DSpace(out Vector3 mousePos))
        {
            targetPos = mousePos;
            
            lookDirection = new Vector3(targetPos.x - transform.position.x, transform.position.y, targetPos.z - transform.position.z);
            rotation = Quaternion.LookRotation(lookDirection);

            posPlusRange = targetPos + new Vector3(0.5f,0.5f,0.5f);
            posMinusRange = targetPos - new Vector3(0.5f,0.5f,0.5f);
            
            // print(posPlusRange.ToString() + " plus range");
            // print(posMinusRange.ToString() + " minus range");
            
            moving = true;
            // print("moving");
        }
    }

    // private void Move()
    // {
    //     transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.fixedDeltaTime);

    //     transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);

    //     if(transform.position.x < posPlusRange.x && 
    //         transform.position.z < posPlusRange.z && 
    //         transform.position.x > posMinusRange.x && transform.position.z > posMinusRange.z)
    //     {
    //         moving = false;
    //         // print("Not moving");            
    //     }
    // }
}
