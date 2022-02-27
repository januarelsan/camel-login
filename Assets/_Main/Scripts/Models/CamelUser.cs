using System;

[Serializable]
public class CamelUser {

    public string status = "";
	public string user_token = "";
    public string expired_in = "";    
    public string message = "";    
	
}

[Serializable]
public class CamelUserCollection {
	
    public CamelUser[] camelUsers;	
	
}
