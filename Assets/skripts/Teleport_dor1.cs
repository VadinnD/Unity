using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_dor1 : MonoBehaviour
{
    public Vector3 trl;
    public GameObject gmj;
    void OnTriggerStay (Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            gmj.SetActive (true);
        }
        
        if ((other.gameObject.name == "Player") && (Input.GetKey(KeyCode.E)))
        { 
            other.transform.position = trl;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            gmj.SetActive(false);
        }
    }
}
