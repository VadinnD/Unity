using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_contr : MonoBehaviour
{
    public float speed = 5.0f;
    public float stamin1 = 100.0f;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInp();
    }
    private void GetInp()
    {
        if ((Input.GetKeyDown(KeyCode.X)) && (stamin1 != 0))  ///המכזום בûעü רטפע
        {
            speed = 7.0f;
        }
        if ((Input.GetKey(KeyCode.X)) && !(stamin1 <= 0))  ///המכזום בûעü רטפע
        {
            stamin1 = stamin1 - (Time.deltaTime *5) ;
            
        }
        if ((Input.GetKeyUp(KeyCode.X)) || (stamin1 <= 0)) ///המכזום בûעü רטפע
        {
            speed = 5.0f;
        }
        if ((stamin1 < 100) )
        {
            stamin1 = stamin1 + (Time.deltaTime*5)  ;
        }


        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition += -transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition += -transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += transform.right * speed * Time.deltaTime;
        }

    }
}
