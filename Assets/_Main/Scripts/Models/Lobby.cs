using System;

[Serializable]
public class Lobby {
    
	public string status;
    public string name;
	public string code;	    

    public User[] joined_users;

}

[Serializable]
public class LobbyCollection {
	
    public Lobby[] lobbies;	
	
}
