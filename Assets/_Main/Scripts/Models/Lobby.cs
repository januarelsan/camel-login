using System;

[Serializable]
public class Lobby {
    
	public string status;
    public string name;
	public string code;	    

    public Identity[] joined_identities;

}

[Serializable]
public class LobbyCollection {
	
    public Lobby[] lobbies;	
	
}
