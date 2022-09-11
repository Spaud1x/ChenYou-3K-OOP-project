using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IncreaseTimerButton : MonoBehaviour
{
    public float timeGiven = 30; //standard time set
    public float maxTime = 60;
    public float minTime = 10;
    public float increments = 5;
    public TextMeshPro timeGivenText;

    
    public float UpdateTimer()
    {
        return timeGiven;
    }


    public void buttonMethodeIN()
    {
        timeGiven += increments;

        if(timeGiven > maxTime)
        {
            timeGiven = minTime;
            timeGiven += increments;
        }



        timeGivenText.text = $"Adjust your timer:{timeGiven}s";

    }




}
