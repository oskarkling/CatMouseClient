using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float sensitivity = 200f;
    [SerializeField] private float clampAngle = 85f;

    private float verticalRotation;
    private float horizontalRotation;

    private void OnValidate()
    {
        if(player == null)
        {
            player = GetComponentInParent<Player>();
        }
    }

    private void Start()
    {
        // No idea how this works
        // For reference:
        // The reason we are plitting this is becouse we want to rotate the whole player object around the Y axis, but only the camera around the x axis.
        // This is to not move the player objects rotation when looking up or down.
        // But only move it when looking at the sides.
        verticalRotation = transform.localEulerAngles.x;
        horizontalRotation = player.transform.eulerAngles.y;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursorMode();
        }
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Look();
        }

        // DEBUG Purposes ___
        Debug.DrawRay(transform.position, transform.forward * 2f, Color.green);
        // END of Debug purposes
    }

    private void Look()
    {
        float mouseVertical = -Input.GetAxis("Mouse Y");
        float mouseHorizontal = Input.GetAxis("Mouse X");

        verticalRotation += mouseVertical * sensitivity * Time.deltaTime;
        horizontalRotation += mouseHorizontal * sensitivity * Time.deltaTime;

        verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);

        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        player.transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);
    }

    public void ToggleCursorMode()
    {
        Cursor.visible = !Cursor.visible;

        if(Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
