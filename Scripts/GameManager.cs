using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager GMInstance;
    [Range(0f,1f)]public float SFX = 1;
    [Range(0f,1f)]public float Music = 1;
    public UserInfo ActiveUser;
    public bool activeNow;
    public byte[] activedp;
    public int activeUserRank;
    public UserInfo[] User;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (GMInstance == null)
        {
            GMInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
[System.Serializable]
public class UserInfo 
{
   public string UserName;
   public float Score;
   public float correctAns;
   public float WrongAns;
   public byte[] userDP;
}
