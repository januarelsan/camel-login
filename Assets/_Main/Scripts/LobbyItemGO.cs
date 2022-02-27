using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyItemGO : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text joinedUserCountText;
    private Lobby lobby;
    public void Setup(Lobby lobby){
        this.lobby = lobby;
        nameText.text = lobby.name + " - " + lobby.code;

        if(lobby.joined_identities != null)
            joinedUserCountText.text = "Joined " + lobby.joined_identities.Length + "/4";
    }

    public void OpenLobby(){
        LobbyController.Instance.OpenLobbyHome(lobby);
    }
}
