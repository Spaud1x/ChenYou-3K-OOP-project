using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class MainTarget : MonoBehaviour
{


    private int score;
    [Header("Object assignation")]
    public Text scoreDisplay;
    public Text highScoreDisplay;
    public Text timerDisplay;
    public GameObject target;

    [Header("Target properties")]
    public float timeLeft;

    public float timerStartCounter = 0f;
    public bool TimerOn = false;
    public float health = 10f;
    public float healthReset = 10f;  //ensures all targets have same health after it destroys and respawns

    void Awake()
    {
        timeLeft = FindObjectOfType<IncreaseTimerButton>().UpdateTimer(); //postion sub to change
    }

    void Start()
    {
        TimerOn = false;
        highScoreDisplay.text = $"High Score:{PlayerPrefs.GetInt("HighScore", 0)}";
    }

    void Update()
    {
        
        //setting up the timer
        if (TimerOn == true)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);

            }

           
            else
            {
                timeLeft = 0;
                TimerOn = false;
                timerDisplay.enabled = false;
                scoreDisplay.enabled = false;
                target.SetActive(false);
                score = 0;
            }

        }


        UpdateScoreText();
        UpdateHighScoreText();
    }



    public void Hit(float amount)
    {
        //getting timeGiven variable from timer button script
        TimerOn = false;

        

        health -= amount;  //taking damage

        if (health <= 0f)
        {
            timerStartCounter++;
            if(timerStartCounter >= 0)  //only starts timer when first target is hit
            {

                TimerOn = true;

            }
            score++; //increase score
            transform.position = TargetBounds.Instance.GetRandomPosition(); //target changes position
            health = healthReset; //target gets back full health
            UpdateScoreText();
            UpdateHighScoreText();

        }



    }

    


    public void Reset()
    {
        PlayerPrefs.DeleteKey("HighScore");
        score = 0;
        highScoreDisplay.text = $"High Score:{score}";
    }



    private void UpdateHighScoreText()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreDisplay.text = $"High Score:{PlayerPrefs.GetInt("HighScore", 0)}";
        }


    }

    private void UpdateScoreText()
    {
        scoreDisplay.text = $"Score:{score}";
    }

    //private void UpdateTimerText()
    //{
        //timerDisplay.text = "Time left:" + timeLeft.ToString("F1");
    //}

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerDisplay.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }


}

