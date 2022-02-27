using System;

[Serializable]
public class AccessToken {
	public string status = "";
    public string access_token = "";
    public string expired_in = "";

    public string message = "";

    
	
}

[Serializable]
public class AccessTokenCollection {
	
    public AccessToken[] accessTokens;	
	
}
