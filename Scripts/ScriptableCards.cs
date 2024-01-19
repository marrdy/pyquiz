using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScriptableCards : MonoBehaviour
{
    [SerializeField] TMP_Text ContentDisplay;
    public Color contentcolor=Color.black;
    public Animator animator;
    FlipCard FC;
    public bool Matched;
    [TextArea(20,20)]public string Content = "Not Set";
    public int IdMatcher;
    public bool selected;
    public cardsfx s;
    public Image StatusImage;
    public Sprite cross;
    public Sprite check;
    public Color wrongColor;
    public Color rightcolor;
    public Image border;
    private void Start()
    {
        FC = FindAnyObjectByType<FlipCard>();
    }
    private void Awake()
    {
        s.source = this.gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.clip = s.clip;
        s.source.loop = s.loop;
    }
    public void hideContent()
    {

        s.source.Play();
        ContentDisplay.text = "?";
        ContentDisplay.color = Color.black;
        StatusImage.gameObject.SetActive(false);
        border.gameObject.SetActive(false);
    }
    public void showContent()
    {
        s.source.Play();
        ContentDisplay.text = Content;
        ContentDisplay.color = contentcolor;
        
    }
   public void matchFound() 
    {
       StatusImage.color = rightcolor;
       StatusImage.sprite = check;
       StatusImage.color =rightcolor;
       border.color =rightcolor;
       StatusImage.sprite = check;
        StatusImage.gameObject.SetActive(true);
        border.gameObject.SetActive(true);
    }
    public void NotMatched() 
    {
        StatusImage.color = wrongColor;
        StatusImage.sprite = cross;
        StatusImage.color = wrongColor;
        border.color = wrongColor;
        StatusImage.sprite = cross;
        StatusImage.gameObject.SetActive(true);
        border.gameObject.SetActive(true);
    }
}
[System.Serializable]
public class cardsfx 
{
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    [HideInInspector]
    public AudioSource source;
    public bool loop;
}