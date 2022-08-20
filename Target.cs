using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    private int score;
    public Text scoreDisplay;
    public GameObject target;

    public float targetNumber = 30f;
    public float health = 50f;  
    public float healthReset = 50f;  //ensures all targets have same health after it destroys and respawns


    void Update()
    {
        UpdateScoreText();
    }

    public void Hit(float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            score++;
            transform.position = TargetBounds.Instance.GetRandomPosition();
            health = healthReset;
            UpdateScoreText();
        }

        if(score == targetNumber)
        {
            scoreDisplay.enabled = false;
            target.SetActive(false);
            score = 0;
        }
        
        
    }

    private void UpdateScoreText()
    {
        scoreDisplay.text = $"Score:{score}";
    }




}
