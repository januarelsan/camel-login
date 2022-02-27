using System;

[Serializable]
public class Identity {
    
	public string id;
    public string number;
	   

}

[Serializable]
public class IdentityCollection {
	
    public Identity[] identities;	
	
}
