using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargerManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("KnifePrefab"))
        {
            score++;
            ScoreUpdate();
        }
    }

    void ScoreUpdate()
    {
        scoreText.text = "Score" + score;
    }
}
