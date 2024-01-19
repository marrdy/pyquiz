using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LessonContent : MonoBehaviour
{
    [SerializeField]GameObject contentChooser;
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text content;
    [SerializeField] ContentToShow[] Cts;
    public void showTopic(int i)
    {
        FindAnyObjectByType<SMScript>().playtrack("pageturn");
        contentChooser.SetActive(false);
        title.text = Cts[i].TopicTitle;
        content.text = Cts[i].TopicContnent;
    }
    public void back()
    {
        FindAnyObjectByType<SMScript>().playtrack("click");
        contentChooser.SetActive(true);
    }
}
[System.Serializable]
public class ContentToShow 
{
    
    public string TopicTitle;
    [TextArea(20,20)]
    public string TopicContnent;
}