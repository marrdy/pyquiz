using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfirmationDialog : MonoBehaviour
{
    private bool confirmed = false;

    public TMP_Text messageText;
   
    public void Show(string message)
    {
        FindAnyObjectByType<SMScript>().playtrack("click");
        messageText.text = message;
        gameObject.SetActive(true);
    }

    public void OnConfirm()
    {
        FindAnyObjectByType<SMScript>().playtrack("click");
        confirmed = true;
        gameObject.SetActive(false);
        
    }

    public void OnCancel()
    {
        FindAnyObjectByType<SMScript>().playtrack("click");
        confirmed = false;
        gameObject.SetActive(false);
    }

    public bool GetConfirmationResult()
    {
        return confirmed;
    }
}
