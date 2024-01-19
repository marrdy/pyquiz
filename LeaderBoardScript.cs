using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardScript : MonoBehaviour
{
    [SerializeField] LeaderBoardUserTabScriptable LBUTS;
    GameManager gm;
    private void Start()
    {
        while(gm == null) 
        {
            gm = FindAnyObjectByType<GameManager>();
        }

        foreach (UserInfo ui in gm.User) 
        {
            GameObject Lblist = Instantiate(LBUTS.gameObject);
            Lblist.GetComponent<LeaderBoardUserTabScriptable>().setTab(ui.UserName, ui.Score);
            Lblist.transform.SetParent(transform);
            Lblist.transform.transform.localScale = new Vector3(1, 1, 1);
            if(gm.ActiveUser.UserName == ui.UserName) 
            {
                Lblist.GetComponent<LeaderBoardUserTabScriptable>().highlighted();
            }
        }
    }
}
