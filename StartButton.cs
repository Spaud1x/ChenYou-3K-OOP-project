using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{

    public MainTarget targetScript;
    public GameObject target;
    public Text scoreDisplay;
    public Text timeDisplay;

    void Awake()
    {
        targetScript = target.GetComponent<MainTarget>();
    }

    void Start()
    {
        scoreDisplay.enabled = false;
        timeDisplay.enabled = false;
        targetScript.enabled = false;
        target.SetActive(false);
    }


    public void buttonMethode()
    {
        Debug.Log("Starting...");
        scoreDisplay.enabled = true;
        timeDisplay.enabled = true;
        targetScript.enabled = true;
        target.SetActive(true);
    }

}
