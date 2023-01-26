using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleScreenButton : MonoBehaviour
{
    private Button myButton;

    // Start is called before the first frame update
    void Start()
    {
        myButton = this.GetComponent<Button>();
        myButton.onClick.AddListener(delegate { loadLevel(); });

    }

    private void loadLevel()
    {
        SceneManager.LoadScene("TitleScreen");

    }
}
