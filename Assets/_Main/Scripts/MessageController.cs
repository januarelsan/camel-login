using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : Singleton<MessageController>
{
    [SerializeField]
    private GameObject messageBackground;

    [SerializeField]
    private Text messageText;
    public void ShowMessage(string message){
        messageBackground.SetActive(true);
        messageText.text = message;
    }
}
