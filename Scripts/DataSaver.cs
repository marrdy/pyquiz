using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class DataSaver
{
    private static readonly string v = Application.persistentDataPath + "UsersInfo.Lvlpg";
    static string PathForUserInfo = v;
    static string PathForTOR = Application.persistentDataPath + "tor.Lvlpg";

   
    public static void SaveUserInfo(UserInfo[]info)
    {
        BinaryFormatter BF = new BinaryFormatter();
        FileStream FS = new FileStream(PathForUserInfo, FileMode.Create);
        BF.Serialize(FS, info);
        FS.Close();
    }
   

    public static bool termsAndCondition() 
    {
        if (File.Exists(PathForTOR))
        {

            BinaryFormatter formater = new BinaryFormatter();
            FileStream FS = new FileStream(PathForTOR, FileMode.Open);
            bool data = (bool)formater.Deserialize(FS);
            FS.Close();
            return data;
        }
        else
        {
            Debug.LogError("NO FILE LOADED");
            BinaryFormatter BF = new BinaryFormatter();
            FileStream FS = new FileStream(PathForTOR, FileMode.Create);
            BF.Serialize(FS, false);
            FS.Close();
            return true;

        }
    }
    public static void acceptTor() 
    {
        BinaryFormatter BF = new BinaryFormatter();
        FileStream FS = new FileStream(PathForTOR, FileMode.Create);
        BF.Serialize(FS, true);
        FS.Close();

    }
    public static UserInfo[] LoadUser()
    {
        if (File.Exists(PathForUserInfo))
        {

            BinaryFormatter formater = new BinaryFormatter();
            FileStream FS = new FileStream(PathForUserInfo, FileMode.Open);
            UserInfo []data = formater.Deserialize(FS) as UserInfo[];
            FS.Close();
            return data;
        }
        else
        {
            Debug.LogError("NO FILE LOADED");
            UserInfo[] emptyUserInfo = new UserInfo[0];
            SaveUserInfo(emptyUserInfo);
            return emptyUserInfo;

        }
    }
   
}
