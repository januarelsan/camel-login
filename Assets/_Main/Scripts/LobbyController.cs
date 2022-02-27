using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleHTTP;

public class LobbyController : Singleton<LobbyController>
{
    [SerializeField]
    private InputField lobbyNameField;

    [SerializeField]
    private GameObject lobbyItemPrefab;
    
    [SerializeField]
    private Transform lobbyItemParent;
    
    [SerializeField]
    private GameObject lobbyHomePanel;

    [SerializeField]
    private GameObject createLobbyPanel;    
    [SerializeField]
    private GameObject publicLobbyPanel;  

    [SerializeField] private Toggle publicToggle;    

    void Start() {
        
    }
    
    public void CallLobbyCreateAPI(){
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("name", lobbyNameField.text);        

        string visibility = "0";
        if(publicToggle.isOn)
            visibility = "1";

        parameters.Add("visibility", visibility);
        
        APIController.Instance.PostWithFormData("lobby/create",CallLobbyCreateAPIResponse, parameters);
    }

    void CallLobbyCreateAPIResponse(Client http)
    {        
        if (http.IsSuccessful ()) {
			Response resp = http.Response ();

            if(resp.IsOK()){

                Debug.Log(resp.ToString());
                // UpdateMyRoomList();

                string wrappedJSON = resp.WrapJSONArray("lobbies");
                            

                LobbyCollection lobbyCollection = JsonUtility.FromJson<LobbyCollection>(wrappedJSON);
                            
                foreach (Lobby lobby in lobbyCollection.lobbies)
                {         
                    Debug.Log(lobby.name);
                    OpenLobbyHome(lobby);
                }
                
                
            } else {
                Debug.Log(resp.ToString());
            }

		} else {
			Debug.Log("error: " + http.Error());
            MessageController.Instance.ShowMessage("Something Error, Please Try Again!");
		}
    }

    public void CallLobbyMyRoomListAPI(){                
        APIController.Instance.Get("lobby/myRoomList",CallLobbyMyRoomListAPIResponse);
    }

    void CallLobbyMyRoomListAPIResponse(Client http)
    {        
        if (http.IsSuccessful ()) {
			Response resp = http.Response ();

            if(resp.IsOK()){                

                string wrappedJSON = resp.WrapJSONArray("lobbies");
                            

                LobbyCollection lobbyCollection = JsonUtility.FromJson<LobbyCollection>(wrappedJSON);
                            
                foreach (Lobby lobby in lobbyCollection.lobbies)
                {
                    InstantiateLobbyItemGO(lobby);
                }
                
            } else {
                Debug.Log(resp.ToString());
            }

		} else {
			Debug.Log("error: " + http.Error());
            MessageController.Instance.ShowMessage("Something Error, Please Try Again!");
		}
    }

    public void UpdateMyRoomList(){
        
        for (int i = 0; i < lobbyItemParent.childCount; i++)
        {
            Destroy(lobbyItemParent.GetChild(i).gameObject);
        }

        CallLobbyMyRoomListAPI();
    }

    private void InstantiateLobbyItemGO(Lobby lobby){
        GameObject lobbyItemGO = Instantiate(lobbyItemPrefab,lobbyItemParent);
        LobbyItemGO lobbyItemGOComponent = lobbyItemGO.GetComponent<LobbyItemGO>();
        lobbyItemGOComponent.Setup(lobby);
    }

    public void OpenLobbyHome(Lobby lobby){                        
        CallLobbyJoinAPI(lobby.code);        
    }

    public void CallLobbyJoinAPI(string code){
        List<string> parameters = new List<string>(){code,PlayerPrefController.Instance.GetIdentityNumber()};
        APIController.Instance.Get("lobby/join", CallLobbyJoinAPIResponse, parameters);        
    }

    void CallLobbyJoinAPIResponse(Client http)
    {
        
        if (http.IsSuccessful ()) {
			Response resp = http.Response ();

            if(resp.IsOK()){

                Debug.Log(resp.ToString());
                TryJoin(resp);     
                
            } else {
                Debug.Log(resp.ToString());
            }

		} else {
			Debug.Log("error: " + http.Error());
            MessageController.Instance.ShowMessage("Something Error, Please Try Again!");
		}
    }

    public void CallLobbyJoinedUserListAPI(string code){
        List<string> parameters = new List<string>(){code};
        APIController.Instance.Get("lobby/joinedUserList", CallLobbyJoinedUserListAPIResponse, parameters);        
    }

    void CallLobbyJoinedUserListAPIResponse(Client http)
    {
        
        if (http.IsSuccessful ()) {
			Response resp = http.Response ();

            if(resp.IsOK()){

                TryJoin(resp);
                
            } else {
                Debug.Log(resp.ToString());
            }

		} else {
			Debug.Log("error: " + http.Error());
            MessageController.Instance.ShowMessage("Something Error, Please Try Again!");
		}
    }

    public void TryJoin(Response resp){
        
        GeneralResponse generalResponse = JsonUtility.FromJson<GeneralResponse>(resp.ToString());


        if (generalResponse.status == "success")
        {
            Lobby lobby = generalResponse.lobby;            

            publicLobbyPanel.SetActive(false);
            createLobbyPanel.SetActive(false);
            lobbyHomePanel.SetActive(true);
            lobbyHomePanel.GetComponent<LobbyHomeController>().Setup(lobby);     

        } else {            
            MessageController.Instance.ShowMessage(generalResponse.message);
        }
        
                   
                  
    }

}
