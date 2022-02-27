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
        joinedUserCountText.text = "Joined " + lobby.joined_users.Length + "/4";
    }

    public void OpenLobby(){
        LobbyController.Instance.OpenLobbyHome(lobby);
    }
}
