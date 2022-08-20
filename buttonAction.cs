using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonAction : MonoBehaviour
{

    public Target targetScript;
    public GameObject target;
    public Text scoreDisplay;

    void Awake()
    {
        targetScript = target.GetComponent<Target>();
    }

    void Start()
    {
        scoreDisplay.enabled = false;
        targetScript.enabled = false;
        target.SetActive(false);
    }


    public void buttonMethode()
    {
        Debug.Log("Button pressed");
        scoreDisplay.enabled = true;
        targetScript.enabled = true;
        target.SetActive(true);

    }

}
