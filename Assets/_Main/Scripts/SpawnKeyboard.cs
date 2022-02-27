using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnKeyboard : MonoBehaviour
{
    [SerializeField] private GameObject keyboard;
    
    void Update()
    {        
        if(GetComponent<InputField>().isFocused){
            keyboard.SetActive(true);
            keyboard.GetComponent<KeyboardScript>().setInputField(GetComponent<InputField>());
        }
    }
}
