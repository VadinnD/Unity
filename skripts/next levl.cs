using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class nextlevl : MonoBehaviour
{
    
    void OnTriggerStay(Collider other){
        if (other.gameObject.name=="Player") {
            
            PlayerPrefs.SetInt("LVL",PlayerPrefs.GetInt("LVL")+1);
            PlayerPrefs.Save();
            SceneManager.LoadScene(PlayerPrefs.GetInt("LVL"));
        }
    }
}
