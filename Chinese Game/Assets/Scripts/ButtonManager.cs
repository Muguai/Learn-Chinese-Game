using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SceneProperty;
using EventCallbacks;

public class ButtonManager : MonoBehaviour
{

    private Button myButton;

    private static TMP_Text myText;
    // Start is called before the first frame update
    void Start()
    {
        myButton = this.GetComponent<Button>();
        myText = this.gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        myButton.onClick.AddListener(delegate { loadLevel(); });


    }

    static void loadLevel()
    {
        DieEvent udei = new DieEvent();

        EventSystem.Current.FireEvent(EVENT_TYPE.START_GAME, udei);
        Debug.Log("Its working");
    }
}
