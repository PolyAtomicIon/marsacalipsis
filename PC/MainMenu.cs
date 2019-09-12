using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour {
    const int size = 15;
    string[] phrases = new string[size] {
        "Storm Area 51, They Can't Stop All of Us",
        "Team Naruto Runners!",
        "Team Kyles!",
        "Aliens are absent, check Area 51",
        "Free the Aliens!",
        "Hello, Elon!",
        "Welcome to Mars!",
        "You're breathtaking!",
        "Ah, here we go again...",
        "Advancement made! Stone age",
        "Am I a joke to you?",
        "You are playing MARSACALIPSiS",
        "I AM INEVITABLE!",
        "excuse me, what the duck?",
        "subscribe @2powerofn"
    };
    public TextMeshProUGUI textLabel;
    public TimerScript timer;
    public Text highScore;
    private float ScreenWidth;
    System.Random rand = new System.Random();
    void Start() {

        ScreenWidth = Screen.width;

        textLabel = GetComponent<TextMeshProUGUI>();
        textLabel.text = phrases[ rand.Next(0,16) ];
        
        highScore.text = timer.get_high_score();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown (KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}