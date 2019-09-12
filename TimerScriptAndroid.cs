using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TimerScriptAndroid : MonoBehaviour {
    public Text timerLabel;
    private float time;
    public string time_value = "00 : 00 : 00";
    public float Seconds = 0f, period_ = 0.1f, overall = 0f;

    void Start() {
        time = 0f;
        overall = 0f;
        time_value = PlayerPrefs.GetString("time_value", time_value);
        Seconds = PlayerPrefs.GetFloat("Seconds", Seconds);
    }

    void Update() {
        /* 
        theTime += Time.deltaTime * speed;
        string hours = Mathf.Floor((theTime % 216000) / 3600).ToString("00");
        string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
        string seconds = (theTime % 60).ToString("00");
        text.text = hours + ":" + minutes + ":" + seconds;
        */
        time += Time.deltaTime;

        float minutes = Mathf.Floor((time % 3600) / 60); //Divide the guiTime by sixty to get the minutes.
        float seconds = time % 60; //Use the euclidean division for the seconds.
        float fraction = (time * 100) % 100;

        overall += period_;

        //update the label value
        string score = string.Format ("{0:00} : {1:00} : {2:00}", minutes, seconds, fraction);
        timerLabel.text = score;
        PlayerPrefs.SetString("time_value", score);
        PlayerPrefs.SetFloat("Seconds", overall);
    }
 }