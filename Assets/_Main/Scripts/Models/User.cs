using System;

[Serializable]
public class User {
	public string id;	    
	public string name;

}

[Serializable]
public class UserCollection {
	
    public User[] users;	
	
}
