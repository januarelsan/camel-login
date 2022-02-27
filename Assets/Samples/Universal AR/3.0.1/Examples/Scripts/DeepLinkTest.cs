using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Web;
using System.Runtime.InteropServices;

public class DeepLinkTest : MonoBehaviour
{
    public Text urlText;
    public Text usernameText;
    public Text numberText;
    public Text deviceText;

    private string url;
    // Start is called before the first frame update

    [DllImport("__Internal")]
    private static extern void Hello();

    [DllImport("__Internal")]
    private static extern void MaskingURL();

    void Start()
    {
        // Hello();
        MaskingURL();

        Debug.Log(Application.absoluteURL);

        if(!PlayerPrefs.HasKey("token")){
            int x = UnityEngine.Random.Range(0,100);
            PlayerPrefs.SetInt("token", x);
            numberText.text = PlayerPrefs.GetInt("token").ToString();
        } else {
            numberText.text = PlayerPrefs.GetInt("token").ToString();
        }

        deviceText.text = SystemInfo.deviceName + " && " + SystemInfo.deviceUniqueIdentifier + " && " + SystemInfo.deviceModel + " && " + SystemInfo.deviceType ;

        
        
    }

    // Update is called once per frame
    void Update()
    {
        url =  Application.absoluteURL;
        // url =  "https://hbgww.zappar.io/3989882179920520397/1.0.3/?username=januarelsan";
        

        Uri myUri = new Uri(url);
        string param1 = HttpUtility.ParseQueryString(myUri.Query).Get("username");

        usernameText.text = param1;
    }

    public void logout(){
        PlayerPrefs.DeleteKey("token");
    }
}
