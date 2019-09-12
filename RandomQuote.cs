using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomQuote : MonoBehaviour {

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
    public Text textLabel;
    System.Random rand = new System.Random();
    void Start() {

        textLabel = GetComponent<Text>();
        textLabel.text = phrases[ rand.Next(0,size) ];
        
    }

    // Update is called once per frame
    void Update() {
       
    }
}