using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleHTTP;
using System.Runtime.InteropServices;

public class LinkItemController : Singleton<LinkItemController>
{
    [DllImport("__Internal")]
    private static extern void OpenURL(string url);
    public void CallGetLinkAPI(string id)
    {
        List<string> parameters = new List<string>() { id };
        APIController.Instance.Get("link/get", CallGetLinkAPIResponse, parameters);
        
    }

    void CallGetLinkAPIResponse(Client http)
    {

        if (http.IsSuccessful())
        {
            Response resp = http.Response();

            if (resp.IsOK())
            {

                Debug.Log(resp.ToString());

                GeneralResponse generalResponse = JsonUtility.FromJson<GeneralResponse>(resp.ToString());


                if (generalResponse.status == "success")
                {
                    LinkItem link = generalResponse.link_item;                    
                    Debug.Log(link.value);
                    OpenURL(link.value);

                }
                else
                {
                    MessageController.Instance.ShowMessage(generalResponse.message);
                }

            }
            else
            {
                Debug.Log(resp.ToString());
            }

        }
        else
        {
            Debug.Log("error: " + http.Error());
            MessageController.Instance.ShowMessage("Something Error, Please Try Again!");
        }
    }
}
