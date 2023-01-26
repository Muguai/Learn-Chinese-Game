using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventCallbacks;

public class ResumeButton : MonoBehaviour
{
    private Button myButton;
    // Start is called before the first frame update
    void Start()
    {
        myButton = this.GetComponent<Button>();
        myButton.onClick.AddListener(delegate { resumeGame(); });
    }


    static void resumeGame()
    {
        DebugEvent udei = new DebugEvent();
        EventSystem.Current.FireEvent(EVENT_TYPE.RESUME_GAME, udei);
    }
}
