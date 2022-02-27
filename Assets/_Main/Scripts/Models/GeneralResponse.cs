using System;

[Serializable]
public class GeneralResponse {
	public string status = "";    
    public string message = "";

    public Lobby lobby;

    public LinkItem link_item;

    
	
}

[Serializable]
public class GeneralResponseCollection {
	
    public GeneralResponse[] generalResponses;	
	
}
