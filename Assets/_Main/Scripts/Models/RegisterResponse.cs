using System;

[Serializable]
public class RegisterResponse {

    public string status = "";
	public string user_token = "";
    public string expired_in = "";    
    public string message = "";    
	
    public ErrorDetail error_details;
}

[Serializable]
public class ErrorDetail {

    public string[] fullname;
	public string[] identity_no;
    public string[] birthday;  
    public string[] email;   
    public string[] password;
	
}

[Serializable]
public class RegisterResponseCollection {
	
    public RegisterResponse[] registerResponses;	
	
}
