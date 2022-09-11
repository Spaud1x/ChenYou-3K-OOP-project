using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float sensX;
    public float sensY;

    public Transform orientation;


    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  //locks cursor to center of screen
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensX * Time.deltaTime;   //gets input for mousex 
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY * Time.deltaTime;   //gets input for mousey

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  //player won't overrotate

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);   //rotate cam and orientation
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
       
    }
}
