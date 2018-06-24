using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour {

    private const int START_TIME = 99;
    private int startTime;

    public GameObject timer;

	// Use this for initialization
	void Start () {
        //this.enabled = false;
        startTime = (int)Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        int currTime = ((int)Time.time) - startTime;
        string currTimeStr = currTime.ToString();
        timer.GetComponent<Text>().text = currTimeStr;
	}

    public void StartTimer()
    {
        startTime = (int)Time.time;
        this.enabled = true;
    }

    public void EndTimer()
    {
        this.enabled = false;
    }

    public void RestartTimer()
    {

    }

    private void SetTimer(int time)
    {

    }
}
