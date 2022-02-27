using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingController : Singleton<LoadingController>
{
    [SerializeField]
    private GameObject background;

    public void SetActive(bool value){
        background.SetActive(value);        
    }
}
