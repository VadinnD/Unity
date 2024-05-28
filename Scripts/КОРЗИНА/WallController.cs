using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    void Start()
    {
        // В начале стена невидима
        gameObject.SetActive(false);
    }

    public void MakeVisible()
    {
        gameObject.SetActive(true);
    }

    public void MakeInvisible()
    {
        gameObject.SetActive(false);
    }
}
