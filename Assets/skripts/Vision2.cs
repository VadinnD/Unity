using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision2 : MonoBehaviour
{
    //public GameObject tri;
    public GameObject sphere;
    //public Vector3 trl;
    public Renderer rend;
    public Transform y;
    public float i=0;
    public float t=50.0f; 
    void Start()
    {
        //rend = GetComponent<Renderer>();
        rend.enabled = false;
    }

    
     /*void OnTriggerStay (Collider other)
    {
        
            rend.enabled = true;
            
    }*/
    /* void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            i=i+1;
        }
    }*/
    void Update(){
        
        GetInp();
    }
    public void GetInp()
    {
        if (Input.GetKeyDown(KeyCode.F))
       {
            i=1; 
       }
       if ((i>=1)&&(i<=50)){
            i=i+Time.deltaTime*10;
            y.localScale = i*new Vector3(1,1,1);
            
       }
       if ((t>=1)&&(t<=50)&&(i>=50)){
            t=t-Time.deltaTime*10;
            y.localScale = t * new Vector3(1,1,1);
       }
       if ((t<=1)&&(i>=50)){
        i=0;
        t=50.0f;
       }
       
       
    }
}
