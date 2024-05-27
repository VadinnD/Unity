using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class camera_control : MonoBehaviour
{
    //public float Speed = 0.1f;
    //public float SpeedRotate = 0.1f;
    // Start is called before the first frame update
    private float mouseX;
    private float mouseY;
    public Transform Player;
    [Header("Чувствительность мыши")]
    public float sM = 200f;
    void Start()
    {
        //transform.position = new Vector3(10, 4, 10);
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
//(Input.GetKey(KeyCode.W))
        //Input.GetKeyDown(KeyCode.W);
        //Input.GetKeyDown(KeyCode.W);
        // Input.GetKeyDown(KeyCode.W);
        //Input.GetAxis("Horizontal");//A=-1;D=1
        //float inputMouseY = inputMouse.GetAxis("Mouse Y")
        //transform.pasitiop += nev Vector3();

        mouseX = Input.GetAxis("Mouse X") * sM * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sM * Time.deltaTime;
        Player.Rotate(mouseX * new Vector3(0, 1, 0));
        if ((mouseY >= -90.0) && (mouseY <= 90.0) && !(Input.GetKey(KeyCode.R)))
        {
            transform.Rotate(-mouseY * new Vector3(1, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            transform.Rotate(180 * new Vector3(0, 1, 0));
        }
            if (Input.GetKeyUp(KeyCode.R))
        {
            transform.Rotate(180 * new Vector3(0, 1, 0));
        }
        


    }
}
