using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] Slider musicVol;
    [SerializeField] Slider SFXvol;
    SMScript sms;
    private void Start()
    {
       
        while (sms == null)
        {
            sms = FindAnyObjectByType<SMScript>();
        }
        try
        {
            musicVol.value = sms.musicVolume;
            SFXvol.value = sms.sfxVolume;
        }
        catch (Exception)
        {
            while (sms == null)
            {
                sms = FindAnyObjectByType<SMScript>();
            }
            musicVol.value = sms.musicVolume;
            SFXvol.value = sms.sfxVolume;
        }
      
    }
    public void setmusicvol() 
    {
        sms.MusicVolume(musicVol.value);
    }
    public void setsfxvol() 
    {
        sms.SFXVolume(SFXvol.value);
    }
}
