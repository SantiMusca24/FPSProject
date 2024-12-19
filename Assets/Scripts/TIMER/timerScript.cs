using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class timerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText, pointsText;
    [SerializeField] float elapsedTime;
    [SerializeField] static public int levelRank = 1;
    [SerializeField] AudioSource rankBSound, rankCSound, rankDSound, rankFSound;
    [SerializeField] bool playedBSound, playedCSound, playedDSound, playedFSound;
    // Start is called before the first frame update
    void Start()
    {
        playedBSound = false;
        playedCSound = false;
        playedDSound = false;
        playedFSound = false;
        elapsedTime = 0;
        pointsText.text = "A RANK";
        pointsText.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {        
        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (elapsedTime <= 60)
        {
            levelRank = 1;
            pointsText.text = "A RANK";
            pointsText.color = Color.yellow;
        }
        else if (elapsedTime <= 120) 
        {
            if (!playedBSound)
            {
                playedBSound = true;
                rankBSound.Play();
            }
            Salud.Srank = false;
            levelRank = 2;
            pointsText.text = "B RANK";
            pointsText.color = Color.green;
        }
        else if (elapsedTime <= 180)
        {
            if (!playedCSound)
            {
                playedCSound = true;
                rankCSound.Play();
            }
            levelRank = 3;
            pointsText.text = "C RANK";
            pointsText.color = Color.blue;
        }
        else if (elapsedTime <= 240)
        {
            if (!playedDSound)
            {
                playedDSound = true;
                rankDSound.Play();
            }
            levelRank = 4;
            pointsText.text = "D RANK";
            pointsText.color = Color.red;
        }
        else
        {
            if (!playedFSound)
            {
                playedFSound = true;
                rankFSound.Play();
            }
            levelRank = 5;
            pointsText.text = "F RANK";
            pointsText.color = Color.gray;
        }
    }
}
