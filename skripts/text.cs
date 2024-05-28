using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class text : MonoBehaviour
{
    public GameObject next;
    public GameObject stey;
    public float f = 0.0f;
    // Start is called before the first frame update
    public void dalee()
    {
        next.SetActive(true);
        stey.SetActive(false);
        
    }
    public void konec()
    {
        PlayerPrefs.SetInt("LVL",PlayerPrefs.GetInt("LVL")+1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(PlayerPrefs.GetInt("LVL"));
    }
    public void Update(){
        if ((PlayerPrefs.GetInt("LVL")>1)&&(f<=30)){
            f=f+Time.deltaTime;
        }
        if (f>=30){
            stey.SetActive(false);
        }
    }

    
    
}
