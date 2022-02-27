using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleHTTP;

public class LoginController : Singleton<LoginController>
{
    [SerializeField]
    private InputField emailField;
    [SerializeField]
    private InputField passwordField;

    
    [SerializeField]
    private GameObject loginPage;

    [SerializeField]
    private GameObject lobbyPage;

    

    public void CallRegisterAPI(string email, string name, string password){
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("name", name);
        parameters.Add("email", email);
        parameters.Add("password", password);
        APIController.Instance.PostWithFormData("register",CallRegisterAPIResponse, parameters);
    }

    void CallRegisterAPIResponse(Client http)
    {        
        if (http.IsSuccessful ()) {
			Response resp = http.Response ();

            if(resp.IsOK()){
                Debug.Log("Register Success");
                Debug.Log(resp.ToString());
                APIController.Instance.SetToken(resp.ToString());
                ToLobbyPage();
                
            } else {
                Debug.Log(resp.ToString());                
            }

		} else {
			Debug.Log("error: " + http.Error());
            MessageController.Instance.ShowMessage("Something Error, Please Try Again!");
		}
    }
        
    public void CallLoginAPI(){
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("email", emailField.text);
        parameters.Add("password", passwordField.text);
        APIController.Instance.PostWithFormData("login",CallLoginAPIResponse, parameters);
    }

    void CallLoginAPIResponse(Client http)
    {        
        if (http.IsSuccessful ()) {
			Response resp = http.Response ();

            if(resp.IsOK()){
                Debug.Log("Login Success");
                Debug.Log(resp.ToString());
                APIController.Instance.SetToken(resp.ToString());
                ToLobbyPage();
                
            } else {
                Debug.Log(resp.ToString());
                
            }

		} else {
			Debug.Log("error: " + http.Error());
            MessageController.Instance.ShowMessage("Something Error, Please Try Again!");
		}
    }

    public void ToLobbyPage(){
        loginPage.SetActive(false);
        lobbyPage.SetActive(true);
    }


}
