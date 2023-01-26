using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using UnityEngine.SceneManagement;

namespace EventCallbacks
{
    public class UiListener : MonoBehaviour
    {
        public float timeRemaining = 0;


        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI timerText;
        public TextMeshProUGUI waveText;
        public TextMeshProUGUI healthText;
        public GameObject pauseMenu;
        public GameObject exitMenu;
        public GameObject indicator;
        public GameObject knobPrefab;
        private bool paused = false;

        private int score = 0;
        private int wave = 1;
        private int knobIndex = 0;
        private bool timerDone = false;
        private bool timeActive = true;
        private bool unPause = false;
        private List<GameObject> knobs = new List<GameObject>();
        // Start is called before the first frame update
        void Start()
        {

            if (scoreText == null && SceneManager.GetActiveScene().name == "StartGame")
            {
                scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            }
            if (timerText == null && SceneManager.GetActiveScene().name == "StartGame")
            {
                timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
            }
            else if(SceneManager.GetActiveScene().name == "Menu")
            {

                timeActive = false;
                timerDone = true;
            }
            EventSystem.Current.RegisterListener(EVENT_TYPE.UNIT_DIED, OnDieUpdateGui);
            EventSystem.Current.RegisterListener(EVENT_TYPE.RESUME_GAME, OnClickResume);
            EventSystem.Current.RegisterListener(EVENT_TYPE.NEW_WAVE, NewWaveIncrease);
            EventSystem.Current.RegisterListener(EVENT_TYPE.END_GAME, EndGame);
            EventSystem.Current.RegisterListener(EVENT_TYPE.HEART_HIT, OnHitUpdateGui);
            EventSystem.Current.RegisterListener(EVENT_TYPE.UI_INDICATOR, populateIndicator);
            EventSystem.Current.RegisterListener(EVENT_TYPE.CHANGE_UI_INDICATOR, changeUIInicator);







        }

        void Update()
        {
            if (timeActive == true)
            {
                timeRemaining += Time.deltaTime;
                timerText.text = timeRemaining.ToString("n2");    
            }
            else if(timerDone == false)
            {
                Debug.Log("Set Timer To zero");
                timeRemaining = 0f;
                timerText.text = timeRemaining.ToString("n2");
                timerDone = true;
            }
            if (Input.GetKeyDown(KeyCode.P) == true || unPause == true)
            {
                Debug.Log("Pause");
                if(paused == false)
                {
                    Time.timeScale = 0.0f;
                    paused = true;
                    pauseMenu.SetActive(true);
                }
                else
                {
                    Time.timeScale = 1.0f;
                    paused = false;
                    pauseMenu.SetActive(false);
                }
                unPause = false;



            }


        }
        void OnClickResume(Event eventInfo)
        {
            unPause = true;
        }
        void EndGame(Event eventInfo)
        {
            exitMenu.SetActive(true);
            exitMenu.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Score: " + score + "\n" +"Time: " + timeRemaining.ToString("n2") + "\n"+"Wave: " + wave;
            timerDone = true;
            timeActive = false;
        }
        void NewWaveIncrease(Event eventInfo)
        {
            wave++;
            waveText.text = "Wave: " + wave.ToString();
        }

        void populateIndicator(Event eventInfo)
        {
            CreateCharsEvent unitCharEvent = (CreateCharsEvent)eventInfo;
            for (int a = 0; a < unitCharEvent.chars.Count; a+=6)
            {
                indicator.GetComponent<GridLayoutGroup>().constraintCount += 1;

            }
            if(indicator.GetComponent<GridLayoutGroup>().constraintCount != 1)
            {
                indicator.GetComponent<GridLayoutGroup>().constraintCount -= 1;
            }
                    
            GameObject newObj;
            int i = 0;
            Debug.Log("yehaw");
            foreach(string s in unitCharEvent.chars)
            {
                Debug.Log("yehaw" + i);

                newObj = (GameObject)Instantiate(knobPrefab, indicator.transform);
                if(i == 0)
                {
                    newObj.GetComponent<Image>().color = Color.green;
                }
                string[] split = s.Split('-');
                newObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = split[0];
                newObj.transform.SetParent(indicator.transform);
                knobs.Add(newObj);
                i++;
            }

            
        }
        void changeUIInicator(Event eventInfo)
        {
            IndicatorEvent uiei = (IndicatorEvent)eventInfo;
            knobs[knobIndex].GetComponent<Image>().color = Color.white;
            knobIndex = uiei.currentIndexIndicator;
            knobs[knobIndex].GetComponent<Image>().color = Color.green;

        }
        void OnDieUpdateGui(Event eventInfo)
        {
            score++;
            scoreText.text = "Score: " + score.ToString();
        }
        void OnHitUpdateGui(Event eventInfo)
        {
            HitEvent unitHitEvent = (HitEvent)eventInfo;
            healthText.text = "Health: " + unitHitEvent.CurrentHealth.ToString();
        }
    }
}
