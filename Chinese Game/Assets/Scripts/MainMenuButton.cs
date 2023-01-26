using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    private Button myButton;

    // Start is called before the first frame update
    void Start()
    {
        myButton = this.GetComponent<Button>();
        myButton.onClick.AddListener(delegate { MainMenuScene(); });
    }
    void MainMenuScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");

    }
}
