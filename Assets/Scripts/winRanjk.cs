using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class winRanjk : MonoBehaviour
{
    public TextMeshProUGUI rankText;


    // Start is called before the first frame update
    void Start()
    {
        if (timerScript.levelRank == 1)
        {
            if (Salud.Srank)
            {
                rankText.color = Color.magenta;
                rankText.text = "RANK S";
            }
            else
            {
                rankText.color = Color.yellow;
                rankText.text = "RANK A";
            }
        }
        else if (timerScript.levelRank == 2)
        {
            rankText.color = Color.green;
            rankText.text = "RANK B";
        }
        else if (timerScript.levelRank == 3)
        {
            rankText.color = Color.blue;
            rankText.text = "RANK C";
        }
        else if (timerScript.levelRank == 4)
        {
            rankText.color = Color.red;
            rankText.text = "RANK D";
        }
        else if (timerScript.levelRank == 5)
        {
            rankText.color = Color.gray;
            rankText.text = "RANK F";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
