using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour {

    private const int START_TIME = 99;
    private const int COUNTDOWN_START = 3;
    private int startTime;
    private int countdownStartTime;

    private Text timer;
    private Text countDownTimer;

    private bool countingDown = false;

	// Use this for initialization
	void Start () {
        this.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (countingDown)
        {
            int currTime = COUNTDOWN_START - ((int)Time.time - countdownStartTime);
            string currTimeStr = currTime.ToString();
            if(currTimeStr == "0")
            {
                countDownTimer.text = "GO";
            }
            else if (currTime < 0)
            {
                countDownTimer.text = "";
                StartTimer();
                countingDown = false;
                gameObject.GetComponent<GameManager>().EnableControls();
            }
            else
            {
                countDownTimer.text = currTimeStr;
            }
        }
        else
        {
            int currTime = START_TIME - ((int)Time.time - startTime);
            string currTimeStr = currTime.ToString();
            timer.text = currTimeStr;
            if (currTimeStr.Equals("0"))
            {
                this.EndTimer();
                gameObject.GetComponent<GameManager>().EndGame();
            }
        }
	}

    public void StartCountDown()
    {
        countDownTimer = GameObject.Find("CountDownText").GetComponent<Text>();
        countingDown = true;
        countdownStartTime = (int)Time.time;
        this.enabled = true;
    }

    public void StartTimer()
    {
        timer = GameObject.Find("TimerText").GetComponent<Text>();
        
        startTime = (int)Time.time;
        
    }

    public void EndTimer()
    {
        this.enabled = false;
    }

    public void RestartTimer()
    {
        SetTimer(0);
    }

    private void SetTimer(int time)
    {
        startTime = (int)Time.time - time;
    }

    public void StopTimer()
    {
        this.enabled = false;
    }
}
