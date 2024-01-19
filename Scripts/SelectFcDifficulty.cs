using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFcDifficulty : MonoBehaviour
{
    public enum Gamemode { basic, advance };
    [SerializeField] FlipCard Basic;
    [SerializeField] FlipCard Advance;
    public Gamemode gameMode;
    public void StartBasic() 
    {
        FindAnyObjectByType<SMScript>().playtrack("click");
        Basic.GameStart();
        gameMode = Gamemode.basic;
        gameObject.SetActive(false);
    }
    public void StartAdvance()
    {
        FindAnyObjectByType<SMScript>().playtrack("click");
        Advance.GameStart();
        gameMode = Gamemode.advance;
        gameObject.SetActive(false);
    }
}
