using System.Collections;
using UnityEngine;
using TMPro;
public class FlipCard : MonoBehaviour
{
    [SerializeField] TMP_Text TimerText;
    [SerializeField] TMP_Text TimerTextOnPause;
    [SerializeField] GameObject Gamesection;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] ScriptableCards FeatureCard;
    public BoxedMessage completeMessage;
    float CurrentTime = 0f;
    bool TimeRuns;
    bool cooldown;
    bool ready;
    public int cardSelected;
    ScriptableCards card1;
    ScriptableCards card2;
    public void GameStart()
    {
        Gamesection.SetActive(true);
        RandomizeChildOrder(this.gameObject);
        StartCoroutine(CloseAllCards());
    }
    private void Update()
    {
        if (TimeRuns)
        {
            CurrentTime += Time.deltaTime;
            System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(CurrentTime);
            TimerText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        }
    }

    public void RandomizeChildOrder(GameObject parent)
    {

        int childCount = parent.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            int randomIndex = Random.Range(i, childCount);
            parent.transform.GetChild(i).SetSiblingIndex(randomIndex);
            parent.transform.GetChild(i).GetComponent<ScriptableCards>().animator.SetTrigger("show");
        }
    }

    public void SelectedCard(ScriptableCards Scritpcard)
    {
       
        if (Scritpcard == card1)
        {
            FeatureCardShow(card1);
            return;
        }
        if (Scritpcard.Matched || cooldown || Scritpcard.selected) return;
        cardSelected++;
        if (cardSelected==1) 
        {
           
            card1 = Scritpcard;
            Scritpcard.selected = true;
            Scritpcard.animator.SetTrigger("show");
        }
        else if(cardSelected== 2)
        {
            card2 = Scritpcard;
            Scritpcard.animator.SetTrigger("show");
            if (card1.IdMatcher == card2.IdMatcher) 
            {
                FindAnyObjectByType<SMScript>().playtrack("correct");
                card1.Matched = true;
                card2.Matched = true;
                card1.matchFound();
                card2.matchFound();
                card1 = null;
                card2 = null;
                cardSelected = 0;
                checkingIfComplete();
            }
            else 
            {
                
                FindAnyObjectByType<SMScript>().playtrack("wrong");
                StartCoroutine(CloseContentDelay());
            }
        }
    }
    IEnumerator CloseContentDelay()
    {
        
        cooldown = true;
        card1.NotMatched();
        card2.NotMatched();
        yield return new WaitForSeconds(1f);
        card1.animator.SetTrigger("hide");
        card2.animator.SetTrigger("hide");
        card1.selected = false;
        card1 = null;
        card2 = null;
        cardSelected = 0;
        cooldown = false;
    }
    IEnumerator CloseAllCards()
    {
        ready = false;
        cooldown = true;
        yield return new WaitForSeconds(2f);
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            _ = Random.Range(i, childCount);
            transform.GetChild(i).GetComponent<ScriptableCards>().animator.SetTrigger("hide");
            yield return new WaitForSeconds(0.3f);
        }
        cooldown = false;
        TimeRuns = true;
        ready = true;
    }

    public void pauseGame()
    {
        TimeRuns=false;
        PauseMenu.SetActive(true);
    }
    public void UnpauseGame()
    {
        if (gameObject.activeSelf ) 
        {
            PauseMenu.SetActive(false);
            TimerTextOnPause.text = TimerText.text;
            if (ready)TimeRuns = true;
        }  
    }
    public void FeatureCardShow(ScriptableCards contentcard) 
    {
        FeatureCard.gameObject.SetActive(true);
        FeatureCard.Content = contentcard.Content;
        FeatureCard.contentcolor = contentcard.contentcolor;
        FeatureCard.showContent();
    }
    public void FeatureCardHide()
    {    
        FeatureCard.gameObject.SetActive(false);
    }
    public void checkingIfComplete()
    {
        Debug.Log("checking");
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if (!transform.GetChild(i).GetComponent<ScriptableCards>().Matched) 
            {
                Debug.Log("not yet finished");
                return;
            }  
        }
        completeMessage.Message.text = "Well Done! You completed the flip card in " + CurrentTime.ToString("0") + "seconds!";
        completeMessage.gameObject.SetActive(true);
    }
}

