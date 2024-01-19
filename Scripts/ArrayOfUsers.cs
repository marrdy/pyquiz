using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SimpleFileBrowser;

public class ArrayOfUsers : MonoBehaviour
{
    public UserButtonScript UserButton;
    GameManager GM;
    public GameObject AdduserInput;
    public GameObject UserSelection;
    public GameObject YesNoDialog;
    public TMP_InputField UserNameNew;
    public RawImage UserDp;
    [SerializeField] TMP_Text ActiveUser;
    [SerializeField] TMP_Text Rank;
    [SerializeField] public BoxedMessage message;
    public RawImage rawImage;
    public byte []imagetosave;
    public GameObject Toc;
    bool hookedActive;
    GameManager gm;
    void Start()
    {
        LoadUserList();
        Toc.SetActive(!DataSaver.termsAndCondition());
    }
    private void Update()
    {
        while(gm == null) 
        {
            gm = FindAnyObjectByType<GameManager>();
        }
        
        if(gm.ActiveUser.UserName.Length != 0 && !hookedActive) 
        {
            Debug.Log("active "+gm.ActiveUser);
            SelectUser(gm.ActiveUser);
            UserSelection.SetActive(false);
            hookedActive = true;
        }
       
    }
   
    public void LoadUserList()
    {
        GM = FindAnyObjectByType<GameManager>();
        
        GM.User = DataSaver.LoadUser();
        foreach (UserInfo U in GM.User)
        {
            UserInfo user = U;
            string username = U.UserName;
            float rnk = U.Score;
            byte[] pic = U.userDP;
            GameObject addbutton = Instantiate(UserButton.gameObject);
            UserButtonScript UBS = addbutton.GetComponent<UserButtonScript>();
            addbutton.GetComponent<UserButtonScript>().Name.text = U.UserName;
            addbutton.transform.SetParent(transform);
            addbutton.transform.localScale = new Vector3(1, 1, 1);
            UBS.Name.GetComponent<Button>().onClick.AddListener(delegate { SelectUser(user); });
            UBS.DeleteButton.GetComponent<Button>().onClick.AddListener(delegate { RemoveUserInfo(username, addbutton); });
        }
    }
    public void acceptingTOR()
    {
        DataSaver.acceptTor();
    }
    public void SelectUser(UserInfo ui)
    {
        FindAnyObjectByType<SMScript>().playtrack("click");
        ActiveUser.text = ui.UserName;
        Rank.text = "Rank: " + ui.Score.ToString("0.00");
        Texture2D texture = new Texture2D(2, 2);
        if (texture.LoadImage(ui.userDP))
        {
            UserDp.texture = texture;
            UserDp.gameObject.SetActive(true);
        }
        UserSelection.SetActive(false);
        GM.ActiveUser.UserName = ui.UserName;
        GM.ActiveUser.userDP = ui.userDP;
        GM.ActiveUser.Score = ui.Score;
        GM.activeNow = true;
    }
    public void AddUser()
    {
       
        AdduserInput.SetActive(true);
        FindAnyObjectByType<SMScript>().playtrack("click");
    }
    public void CancelAddUser()
    {
        AdduserInput.SetActive(false);
        FindAnyObjectByType<SMScript>().playtrack("click");
    }
    public void UserInput()
    {
        string userName = UserNameNew.text;
        byte[] pic = imagetosave;

        if (string.IsNullOrWhiteSpace(userName))
        {

            message.Message.text = "Username cannot be empty or contain only whitespace...";
            GameObject msg = Instantiate(message.gameObject);
            return;
        }

        if (GM.User.Any(user => string.Equals(user.UserName, userName, StringComparison.OrdinalIgnoreCase)))
        {
            message.Message.text = "This user name already existed, try different Name...";
            GameObject msg = Instantiate(message.gameObject);
            return;
        }
        if (imagetosave.Length <= 0)
        {
            message.Message.text = "Please Add your Picture...";
            GameObject msg = Instantiate(message.gameObject);
            return;
        }

        UserInfo newUser = new()
        {
            UserName = userName,
            Score = 0,
            userDP = imagetosave,
            correctAns = 0,
            WrongAns = 0
              
        };

        // Add the new UserInfo to the array
        Array.Resize(ref GM.User, GM.User.Length + 1);
        GM.User[^1] = newUser;
        GameObject addbutton = Instantiate(UserButton.gameObject);
        UserButtonScript UBS = addbutton.GetComponent<UserButtonScript>();
        addbutton.GetComponent<UserButtonScript>().Name.text = UserNameNew.text;
        addbutton.transform.SetParent(this.transform);
        addbutton.transform.localScale = new Vector3(1, 1, 1);
        UBS.Name.GetComponent<Button>().onClick.AddListener(delegate { SelectUser(newUser); });
        UBS.DeleteButton.GetComponent<Button>().onClick.AddListener(delegate { RemoveUserInfo(userName, addbutton); });
        DataSaver.SaveUserInfo(GM.User);
        UserNameNew.text = "";
        CancelAddUser();
        FindAnyObjectByType<SMScript>().playtrack("click");
    }
    public void RemoveUserInfo(string userName, GameObject addbut)
    {
        // Instantiate the confirmation dialog prefab
        ConfirmationDialog dialog = Instantiate(YesNoDialog).GetComponent<ConfirmationDialog>();

        // Show a message in the dialog
        dialog.Show("Are you sure you want to remove this user?");

        // Check the result of the dialog
        StartCoroutine(WaitForConfirmationAndRemove(dialog, userName, addbut));
    }
    public void OpenFileBrowser()
    {
        FindAnyObjectByType<SMScript>().playtrack("click");
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));
        FileBrowser.SetDefaultFilter(".jpg");
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);
        StartCoroutine(ShowLoadDialogCoroutine());
    }
    private IEnumerator WaitForConfirmationAndRemove(ConfirmationDialog dialog, string userName, GameObject addbut)
    {
        while (!dialog.GetConfirmationResult())
        {
            yield return null; // Wait for user input
        }

        // Process removal based on the confirmation result
        if (dialog.GetConfirmationResult())
        {
            // Find the index of the user with the specified userName
            int userIndex = -1;
            for (int i = 0; i < GM.User.Length; i++)
            {
                if (string.Equals(GM.User[i].UserName, userName, StringComparison.OrdinalIgnoreCase))
                {
                    userIndex = i;
                    break;
                }
            }

            // If the user was found, remove them from the array
            if (userIndex >= 0)
            {
                List<UserInfo> userList = new List<UserInfo>(GM.User);
                userList.RemoveAt(userIndex);
                GM.User = userList.ToArray();
                DataSaver.SaveUserInfo(GM.User);
                Destroy(addbut);
            }
        }

        // Destroy the dialog
        Destroy(dialog.gameObject);
    }
    IEnumerator ShowLoadDialogCoroutine()
    {
        // Allow both JPEG and PNG files
        List<string> allowedFileExtensions = new List<string> { "jpg", "png" };

        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, allowedFileExtensions.ToString(), null, "Load Files and Folders", "Load");
        Debug.Log(FileBrowser.Success);
        if (FileBrowser.Success)
        {
            
            if (FileBrowser.Result.Length > 0)
            {
                string selectedFilePath = FileBrowser.Result[0];
                byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(selectedFilePath);
                imagetosave = bytes;
                Texture2D texture = new Texture2D(2, 2);
                if (texture.LoadImage(bytes))
                {
                    rawImage.texture = texture;
                    rawImage.gameObject.SetActive(true);
                }
            }
        }
    }
}


