using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    //public GameObject tri;
    public Renderer rend;
    public float i=0;
    public float t=50.0f;
    public float y=0;
    //public GameObject sphere;
    void Start()
    {
        //rend = GetComponent<Renderer>();
        rend.enabled = false;
    }
    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.name == "Player") 
        {   
            i=i+1;
        }
    }
    
     void OnTriggerStay (Collider other)
    {
        if  ((other.gameObject.name == "Player") && (Input.GetKey(KeyCode.E)) )
        { 
            
            rend.enabled = true;
            i=1;
            
        }
        if  ((other.gameObject.name == "S") && ((y<=50)&&(y>=1))){
            rend.enabled = true;
            i=2;
        }

    }
    
    void Update(){
        if (i>=2){
            i=i+Time.deltaTime;
        }
        if (i>=20){
            rend.enabled = false;
            i=0;
        }
        /////////////////////////
        {
        if (Input.GetKeyDown(KeyCode.F))
       {
            y=1; 
       }
       if ((y>=1)&&(y<=50)){
            y=y+Time.deltaTime*10;
       }
       if ((t>=1)&&(t<=50)&&(y>=50)){
            t=t-Time.deltaTime*10;
            
       }
       if ((t<=1)&&(y>=50)){
        y=0;
        t=50.0f;
       }
    }
    }
}
