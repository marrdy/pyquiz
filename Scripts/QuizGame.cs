using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuizGame : MonoBehaviour
{
    QuizQuestions chosenquestion;
   
    float right;
    float wrong;
    float correctrate;
    bool TimeRuns;
    int counter;
    float CurrentTime =120;
    [SerializeField] GameObject startpromt;
    [SerializeField] GameObject pause;
    [SerializeField]QuizQuestions[] quiz;
    [SerializeField] TMP_Text Score;
    [SerializeField] TMP_Text highestScore;
    [SerializeField] TMP_Text TimerText;
    [SerializeField] TMP_Text timeOnPause;
    [SerializeField]TMP_Text Questiontext;
    [SerializeField] TMP_Text choicetext1;
    [SerializeField] TMP_Text choicetext2;
    [SerializeField] TMP_Text choicetext3;
    [SerializeField] TMP_Text choicetext4;
    UserInfo currentuser;
    void Start()
    {
        getUSerInfo();
        
    }
    private void Update()
    {
        if (TimeRuns)
        {
            CurrentTime -= Time.deltaTime;
            System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(CurrentTime);
            TimerText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        }
        if (CurrentTime <= 0) 
        {
            endgame();
        }


    }
    public void getUSerInfo() 
    {
        GameManager gm = new();
        while (gm == null) 
        {
            gm = FindAnyObjectByType<GameManager>();
        }
         
       UserInfo []users = gm.User;
        foreach(UserInfo ui in users) 
        {
            if(ui.UserName == gm.ActiveUser.UserName) 
            {
                Debug.Log("user found");
                currentuser = ui;
                highestScore.text = currentuser.Score.ToString("0.00");
                Score.text = "Score : " + currentuser.Score.ToString("0.00");
                right = currentuser.correctAns;
                wrong = currentuser.WrongAns;
                return;
            }
        }

        
    }
    public void overRideScore() 
    {
        GameManager gm = new();
        while (gm == null)
        {
            gm = FindAnyObjectByType<GameManager>();
        }

        UserInfo[] users = gm.User;
        foreach (UserInfo ui in users)
        {
            if (ui.UserName == gm.ActiveUser.UserName)
            {
                Debug.Log("score over ridden");
                ui.Score = correctrate;
                ui.correctAns = right;
                ui.WrongAns = wrong;
                DataSaver.SaveUserInfo(users);
                return;
            }
        }
    }
    public void endgame() 
    {
        foreach(QuizQuestions q in quiz) 
        {
            q.done = false;
        }
        startpromt.SetActive(true);
        TimeRuns = false;
        highestScore.text = correctrate.ToString("0.00");
        CurrentTime = 120;
        overRideScore();
    }
    public void startgame() 
    {
        AskQuestion();
        startpromt.SetActive(false);
        TimeRuns = true;
    }
    public float calculateRate() 
    {
        float cr;
        if (right + wrong > 0)
        {
            cr = (float)right / (right + wrong);
        }
        else
        {
            // Handle the case where there are no answers (to avoid division by zero).
            cr = 0.0f;
        }
        return cr;
    }
    public void togglepause() 
    {
        TimeRuns = false;
        pause.SetActive(true);
        timeOnPause.text = TimerText.text;
    }
    public void unpause()
    {
        TimeRuns = true;
        pause.SetActive(false);
    }
    public void AskQuestion()
    {
        correctrate = calculateRate();
        Score.text = "Score : "+correctrate.ToString("0.00");
        chosenquestion = quiz[Random.Range(0, quiz.Length)];
       
        while (chosenquestion.done) 
        {
            chosenquestion = quiz[Random.Range(0, quiz.Length)];
            counter++;
            if (counter >= quiz.Length) 
            {
                endgame();
                return;
            }
        }
        counter = 0;    
        Questiontext.text = chosenquestion.QuestionTextl;
        choicetext1.text = chosenquestion.choice1;
        choicetext2.text = chosenquestion.choice2;
        choicetext3.text = chosenquestion.choice3;
        choicetext4.text = chosenquestion.choice4;
    }
    public void answer(int ans) 
    {
        if (chosenquestion.correctAns==ans)
        {
            Debug.Log("nice");
            right++;
            FindAnyObjectByType<SMScript>().playtrack("correct");
        }
        else 
        {
            FindAnyObjectByType<SMScript>().playtrack("wrong");
            Debug.Log("Nope");
            wrong++;
        }
        chosenquestion.done = true;
        AskQuestion();
    }
}
[System.Serializable]
public class QuizQuestions 
{
    public int correctAns;
    public bool done;
    public string QuestionTextl;
    public string choice1;
    public string choice2;
    public string choice3;
    public string choice4;
}