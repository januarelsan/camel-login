using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleHTTP;

public class LobbyPublicController : MonoBehaviour
{

    [SerializeField]
    private GameObject lobbyItemPrefab;
    
    [SerializeField]
    private Transform lobbyItemParent;

    // Start is called before the first frame update
    void Start()
    {
        CallLobbyPublicListAPI();
    }

    public void CallLobbyPublicListAPI(){                
        APIController.Instance.Get("lobby/public", CallLobbyPublicAPIResponse);
    }

    void CallLobbyPublicAPIResponse(Client http)
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

    private void InstantiateLobbyItemGO(Lobby lobby){
        GameObject lobbyItemGO = Instantiate(lobbyItemPrefab,lobbyItemParent);
        LobbyItemGO lobbyItemGOComponent = lobbyItemGO.GetComponent<LobbyItemGO>();
        lobbyItemGOComponent.Setup(lobby);
    }
}
