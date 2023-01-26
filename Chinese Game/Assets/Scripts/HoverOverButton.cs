using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using TMPro;

public class HoverOverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    GameObject examples;
    private TextMeshProUGUI translation;
    private TextMeshProUGUI romanji;

    private TextMeshProUGUI exampleText;

    private GameObject prefabEnemy;


    private string English;
    private string Mandarin;
    private string examplePhrase;
    private string examplePhrase2;

    private string[] words = new string[4];
    private bool currentText = false;


    void Start()
    {
        examples = GameObject.FindWithTag("ExampleText");
        translation = examples.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        romanji = examples.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        exampleText = examples.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        
        string phrase = this.gameObject.name;
        string[] words = phrase.Split('-');

        English = words[0];
        Mandarin = words[1];
        examplePhrase = words[3];
        examplePhrase2 = words[4];


        // Debug.Log("make prefabenemy name " + prefabEnemy.name + " " + words[0] + words[1]);
    }
    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        currentText = true;
        translation.text = "Used As: " + English;
        romanji.text = "Pinyin: " + Mandarin;
        romanji.fontSize = translation.fontSize;
        exampleText.text = "Phrase: " + "\n" + examplePhrase + "\n" + examplePhrase2;
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + this.gameObject.name + " GameObject" + words[0] + words[1]);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        currentText = false;
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");
    }
    void LateUpdate()
    {
        if(romanji.fontSize != translation.fontSize && currentText == true)
        {
            romanji.fontSize = translation.fontSize;
        }
    }

}
