using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class TimerScript : MonoBehaviour {
    public Text timerLabel;
    private float time;
    private float Seconds = 0f, period_ = 0.1f, overall = 0f;
    private string high_score_value = "00 : 00 : 00";

    private int game = 0;

    public string get_high_score(){
        return PlayerPrefs.GetString("high_score_value", high_score_value);
    }

    public bool is_game(){
        return "GameAndroid" == SceneManager.GetActiveScene().name || SceneManager.GetActiveScene().name == "Game";
    } 

    void Start() {

        Debug.Log( PlayerPrefs.GetString("high_score_value", "00 : 00 : 00") );
        Debug.Log( PlayerPrefs.GetFloat("Seconds", 0f) );
  /*
        PlayerPrefs.SetString("high_score_value", "00 : 00 : 00");
        PlayerPrefs.SetFloat("Seconds", 0f);
*/
        time = 0f;
        overall = 0f;
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
        if( time > PlayerPrefs.GetFloat("Seconds") && is_game() ){
            Debug.Log("HighScore");
            PlayerPrefs.SetString("high_score_value", score);
            PlayerPrefs.SetFloat("Seconds", time);
        }
    }
 }