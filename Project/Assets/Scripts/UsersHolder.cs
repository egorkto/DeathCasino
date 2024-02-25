using System.Collections.Generic;

public class UsersHolder
{
    private static List<UserConnectionData> _users = new List<UserConnectionData>();

    public static List<UserConnectionData> GetUsers()
    {
        var list = new List<UserConnectionData>();
        foreach (var user in _users)
            list.Add(user);
        return list;
    }

    public static void SetUsers(List<UserConnectionData> users)
    {
        _users = users;
    }
} 
