using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour {

    private const int START_TIME = 99;
    private int startTime;

    private Text timer;

	// Use this for initialization
	void Start () {
        this.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        int currTime = START_TIME - ((int)Time.time - startTime);
        string currTimeStr = currTime.ToString();
        timer.text = currTimeStr;
        if (currTimeStr.Equals("0"))
        {
            this.EndTimer();
            gameObject.GetComponent<GameManager>().EndGame();
        }
	}

    public void StartTimer()
    {
        timer = GameObject.Find("TimerText").GetComponent<Text>();
        startTime = (int)Time.time;
        this.enabled = true;
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
}
