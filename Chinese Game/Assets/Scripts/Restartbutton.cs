using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restartbutton : MonoBehaviour
{
    private Button myButton;

    // Start is called before the first frame update
    void Start()
    {
        myButton = this.GetComponent<Button>();
        myButton.onClick.AddListener(delegate { RestartScene(); });
    }
    void RestartScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
