using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleHTTP;

public class LobbyPrivateController : MonoBehaviour
{
    [SerializeField] private InputField codeInputField;
    [SerializeField] private Text responseText;

    [SerializeField]
    private GameObject lobbyHomePanel;

    public void CallLobbyJoinAPI()
    {
        List<string> parameters = new List<string>() { codeInputField.text, PlayerPrefController.Instance.GetIdentityNumber() };
        APIController.Instance.Get("lobby/join", CallLobbyJoinAPIResponse, parameters);
    }

    void CallLobbyJoinAPIResponse(Client http)
    {

        if (http.IsSuccessful())
        {
            Response resp = http.Response();

            if (resp.IsOK())
            {

                Debug.Log(resp.ToString());
                TryJoin(resp);

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

    public void TryJoin(Response resp)
    {


        GeneralResponse generalResponse = JsonUtility.FromJson<GeneralResponse>(resp.ToString());


        if (generalResponse.status == "success")
        {
            Lobby lobby = generalResponse.lobby;
            lobbyHomePanel.SetActive(true);
            lobbyHomePanel.GetComponent<LobbyHomeController>().Setup(lobby);
            gameObject.SetActive(false);

        } else {            
            MessageController.Instance.ShowMessage(generalResponse.message);
        }

    }
}
