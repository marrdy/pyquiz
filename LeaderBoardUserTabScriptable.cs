using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderBoardUserTabScriptable : MonoBehaviour
{
    string nameUser;
    float score;
    public TMP_Text NameTab;
    public TMP_Text ScoreTab;
    [SerializeField]Color highlightcolor;
    public void setTab(string NameSet, float ScoreSet)
    {
        nameUser = NameSet;
        score = ScoreSet;
        NameTab.text = NameSet;
        ScoreTab.text = ScoreSet.ToString("0.00");
    }
    public void highlighted() 
    {
        Debug.Log("User Highlighted");
        GetComponent<Image>().color = highlightcolor;
    }
}
