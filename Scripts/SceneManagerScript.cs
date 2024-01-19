using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] GameObject ExitDialog;
    [SerializeField] GameObject UserSelection;


    public void Exit() 
    {
        FindAnyObjectByType<SMScript>().playtrack("click");
        ExitDialog.SetActive(true);
    }
    public void ExitCancel()
    {
        FindAnyObjectByType<SMScript>().playtrack("click");
        ExitDialog.SetActive(false);
    }
    public void QuitApp() 
    {
        Application.Quit();
    }
    public void ChangeScene(int sceneNumber)
    {
        FindAnyObjectByType<SMScript>().playtrack("click");
        SceneManager.LoadScene(sceneNumber);
    }
    public void Back()
    {
        FindAnyObjectByType<SMScript>().playtrack("click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
    public void changeuser()
    {
        FindAnyObjectByType<SMScript>().playtrack("click");
        UserSelection.SetActive(true);
    }
    public void reset()
    {
        FindAnyObjectByType<SMScript>().playtrack("click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
