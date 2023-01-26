using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialButton : MonoBehaviour
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

    private void loadLevel()
    {
        SceneManager.LoadScene("Tutorial");

    }

}