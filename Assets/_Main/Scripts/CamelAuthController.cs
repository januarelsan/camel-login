using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleHTTP;
using UnityEngine.SceneManagement;

public class CamelAuthController : MonoBehaviour
{

    private const string baseURL = "https://letscml.id/apiv2/";

    private AccessToken access_token = new AccessToken();

    [SerializeField]
    private InputField emailField;
    [SerializeField]
    private InputField passwordField;

    [SerializeField]
    private InputField fullnameField;

    [SerializeField]
    private InputField identityNoField;

    [SerializeField]
    private InputField dayField, monthField, yearField;

    [SerializeField]
    private InputField emailRegisterField;
    [SerializeField]
    private InputField passwordRegisterField;


    [SerializeField]
    private InputField fullnameFieldVerify;

    [SerializeField]
    private InputField identityNoFieldVerify;

    [SerializeField]
    private InputField dayFieldVerify, monthFieldVerify, yearFieldVerify;



    void Start()
    {
        CallGetTokenAPI();
    }


    public void CallRegisterAPI()
    {

        if (emailRegisterField.text == "" || passwordRegisterField.text == "" || fullnameField.text == "" || identityNoField.text == "" || dayField.text == "" || monthField.text == "" || yearField.text == "")
        {
            Debug.Log("All Fields Cannot be Empty!");
            return;
        }

        Dictionary<string, string> parameters = new Dictionary<string, string>();

        string birtdayString = yearField.text + "-" + monthField.text + "-" + dayField.text;

        parameters.Add("fullname", fullnameField.text);
        parameters.Add("identity_no", identityNoField.text);
        parameters.Add("birthday", birtdayString);
        parameters.Add("email", emailRegisterField.text);
        parameters.Add("password", passwordRegisterField.text);

        PostWithFormData("register", CallRegisterAPIResponse, parameters);
    }

    void CallRegisterAPIResponse(Client http)
    {
        if (http.IsSuccessful())
        {
            Response resp = http.Response();

            if (resp.IsOK())
            {
                Debug.Log(resp.ToString());


                string wrappedJSON = resp.WrapJSONArray("registerResponses");

                var collection = JsonUtility.FromJson<RegisterResponseCollection>(wrappedJSON);

                foreach (var item in collection.registerResponses)
                {
                    if (item.status == "success")
                    {
                        Debug.Log("Camel Register Success");
                        LoginController.Instance.CallRegisterAPI(emailRegisterField.text, fullnameField.text, passwordRegisterField.text);
                    }
                    else
                    {

                        string message = " - " + item.message + "\r\n\r\n";

                        if (item.error_details.fullname != null)
                            message += " - " + item.error_details.fullname[0] + "\r\n\r\n";

                        if (item.error_details.identity_no != null)
                            message += " - " + item.error_details.identity_no[0] + "\r\n\r\n";

                        if (item.error_details.birthday != null)
                            message += " - " + item.error_details.birthday[0] + "\r\n\r\n";

                        if (item.error_details.email != null)
                            message += " - " + item.error_details.email[0] + "\r\n\r\n";

                        if (item.error_details.password != null)
                            message += " - " + item.error_details.password[0] + "\r\n\r\n";

                        MessageController.Instance.ShowMessage(message);
                    }

                }

            }
            else
            {
                Debug.Log(resp.ToString());
                MessageController.Instance.ShowMessage("Something Error, Please Try Again!");
            }

        }
        else
        {
            Debug.Log("error: " + http.Error());
            MessageController.Instance.ShowMessage("Something Error, Please Try Again!");
        }
    }













    public void CallLoginAPI()
    {

        if (emailField.text == "" || passwordField.text == "")
        {
            Debug.Log("Email & Password Field Cannot be Empty!");
            return;
        }

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("email", emailField.text);
        parameters.Add("password", passwordField.text);
        PostWithFormData("login", CallLoginAPIResponse, parameters);
    }

    void CallLoginAPIResponse(Client http)
    {
        if (http.IsSuccessful())
        {
            Response resp = http.Response();

            if (resp.IsOK())
            {

                string wrappedJSON = resp.WrapJSONArray("camelUsers");

                var collection = JsonUtility.FromJson<CamelUserCollection>(wrappedJSON);

                foreach (var item in collection.camelUsers)
                {
                    if (item.status == "success")
                    {
                        Debug.Log("Camel Login Success");
                        Debug.Log(item.user_token);
                        LoginController.Instance.CallLoginAPI();
                    }
                    else
                    {
                        MessageController.Instance.ShowMessage(item.message);

                    }

                }

            }
            else
            {
                Debug.Log(resp.ToString());
                MessageController.Instance.ShowMessage("Something Error, Please Try Again!");
            }

        }
        else
        {
            Debug.Log("error: " + http.Error());
            MessageController.Instance.ShowMessage("Something Error, Please Try Again!");
        }
    }





    public void CallVerifyAPI()
    {

        if (fullnameFieldVerify.text == "" || identityNoFieldVerify.text == "" || dayFieldVerify.text == "" || monthFieldVerify.text == "" || yearFieldVerify.text == "")
        {
            Debug.Log("All Fields Cannot be Empty!");
            return;
        }

        Dictionary<string, string> parameters = new Dictionary<string, string>();

        string birtdayString = yearFieldVerify.text + "-" + monthFieldVerify.text + "-" + dayFieldVerify.text;

        parameters.Add("fullname", fullnameFieldVerify.text);
        parameters.Add("identity_no", identityNoFieldVerify.text);
        parameters.Add("birthday", birtdayString);

        PostWithFormData("verify-identity", CallVerifyAPIResponse, parameters);
    }

    void CallVerifyAPIResponse(Client http)
    {
        if (http.IsSuccessful())
        {
            Response resp = http.Response();

            if (resp.IsOK())
            {
                Debug.Log(resp.ToString());


                string wrappedJSON = resp.WrapJSONArray("registerResponses");

                var collection = JsonUtility.FromJson<RegisterResponseCollection>(wrappedJSON);

                foreach (var item in collection.registerResponses)
                {
                    if (item.status == "success")
                    {
                        Debug.Log("Camel Verify Success");
                        CallIdentityCreateAPI(identityNoFieldVerify.text);
                        
                    }
                    else
                    {


                        string message = " - " + item.message + "\r\n\r\n";

                        if (item.error_details.fullname != null)
                            message += " - " + item.error_details.fullname[0] + "\r\n\r\n";

                        if (item.error_details.identity_no != null)
                            message += " - " + item.error_details.identity_no[0] + "\r\n\r\n";

                        if (item.error_details.birthday != null)
                            message += " - " + item.error_details.birthday[0] + "\r\n\r\n";

                        if (item.error_details.email != null)
                            message += " - " + item.error_details.email[0] + "\r\n\r\n";

                        if (item.error_details.password != null)
                            message += " - " + item.error_details.password[0] + "\r\n\r\n";

                        MessageController.Instance.ShowMessage(message);
                    }

                }

            }
            else
            {
                Debug.Log(resp.ToString());
                MessageController.Instance.ShowMessage("Something Error, Please Try Again!");
            }

        }
        else
        {
            Debug.Log("error: " + http.Error());
            MessageController.Instance.ShowMessage("Something Error, Please Try Again!");

        }
    }




    public void CallIdentityCreateAPI(string identityNumber)
    {

        Dictionary<string, string> parameters = new Dictionary<string, string>();

        parameters.Add("identity_number", identityNumber);        
        APIController.Instance.PostWithFormData("identity/create", CallIndentityCreateAPIResponse, parameters);
    }

    void CallIndentityCreateAPIResponse(Client http)
    {
        if (http.IsSuccessful())
        {
            Response resp = http.Response();

            if (resp.IsOK())
            {
                Debug.Log(resp.ToString());


                string wrappedJSON = resp.WrapJSONArray("identities");

                var collection = JsonUtility.FromJson<IdentityCollection>(wrappedJSON);

                foreach (var item in collection.identities)
                {
                    Debug.Log(item.number);
                    PlayerPrefController.Instance.SetIdentityNumber(item.number);
                    LoginController.Instance.ToLobbyPage();
                }

            }
            else
            {
                Debug.Log(resp.ToString());
                MessageController.Instance.ShowMessage("Something Error, Please Try Again!");
            }

        }
        else
        {
            Debug.Log("error: " + http.Error());
            MessageController.Instance.ShowMessage("Something Error, Please Try Again!");

        }
    }















    public void CallGetTokenAPI()
    {
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("appId", "101");
        parameters.Add("appSecret", "sXuZw4z6B8EbGdKgNjQnTqVsYv2x5A7C9FcHeKhPkRnUrWtZw3y5B8DaFdJfMhQmSpVsXuZx4z6B9EbGdKgNjRnTqVtYv2x5A7C9");
        PostWithFormData("auth", CallGetTokenAPIResponse, parameters);
    }

    void CallGetTokenAPIResponse(Client http)
    {
        if (http.IsSuccessful())
        {
            Response resp = http.Response();


            if (resp.IsOK())
            {

                string wrappedJSON = resp.WrapJSONArray("accessTokens");

                var collection = JsonUtility.FromJson<AccessTokenCollection>(wrappedJSON);

                foreach (var item in collection.accessTokens)
                {
                    if (item.status == "success")
                    {
                        access_token = item;
                    }
                    else
                    {
                        Debug.Log(item.message);
                    }
                }


            }
            else
            {
                Debug.Log(resp.ToString());
                MessageController.Instance.ShowMessage("Something Error, Please Try Again!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

        }
        else
        {
            Debug.Log("error: " + http.Error());
            MessageController.Instance.ShowMessage("Something Error, Please Try Again!");
        }
    }


    public void PostWithFormData(string url, System.Action<Client> callback, Dictionary<string, string> parameters = null)
    {
        StartCoroutine(PostWithFormDataIE(url, callback, parameters));
    }
    IEnumerator PostWithFormDataIE(string url, System.Action<Client> callback, Dictionary<string, string> parameters = null)
    {
        FormData formData = new FormData();

        if (parameters != null)
        {
            foreach (KeyValuePair<string, string> kvp in parameters)
            {
                formData.AddField(kvp.Key, kvp.Value);
            }
        }

        string fullURL = baseURL + url;
        Debug.Log(fullURL);

        Request request = new Request(fullURL)
            .AddHeader("Accept", "application/json")
            .AddHeader("access_token", access_token.access_token)
            .AddHeader("Access-Control-Allow-Credentials", "true")
            .AddHeader("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time")
            .AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS")
            .AddHeader("Access-Control-Allow-Origin", "https://hbgww.zappar.io")


            .Post(RequestBody.From(formData));

        Client http = new Client();
        LoadingController.Instance.SetActive(true);
        yield return http.Send(request);
        LoadingController.Instance.SetActive(false);
        callback(http);
    }
}
