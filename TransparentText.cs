using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransparentText : MonoBehaviour
{
    private Text textLabel;
    void Start()
    {   
        textLabel = GetComponent<Text>();
        Color clr = textLabel.color;
        clr.a = 0.55f;
        textLabel.color = clr;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
