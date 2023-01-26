using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EventCallbacks;

public class spawnWeapons : MonoBehaviour
{

    private EnemySpawner enemySpawner;
    private List<string> English = new List<string>();
    private List<string> Mandarin = new List<string>();
    private GameObject startingPos;
    private TextMeshPro[] allText;
    private int currentIndex;
    private TextMeshPro mText;
    private DragDrop dragDropInstance;
    // Start is called before the first frame update
    void Start()
    {
        dragDropInstance = this.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<DragDrop>();
        enemySpawner = this.transform.parent.gameObject.GetComponent<EnemySpawner>();
        List<string> stringList = enemySpawner.getPrefabList();
        foreach (string s in stringList)
        {
            string[] words = s.Split('-');
            English.Add(words[0]);
            Mandarin.Add(words[1]);
        }
        Transform[] allChildren;
        allChildren = GetComponentsInChildren<Transform>();
        int index = 0;
        if(English.Count >= 1)
        {
            Debug.Log("This is the amount of English Words " + (English.Count));
            index = (English.Count - 1);
            int i = 0;
            foreach (Transform child in allChildren)
            {
                if(child.gameObject.name == "CharPos")
                {
                    startingPos = child.gameObject;
                }else if (index != i && child.gameObject != this.gameObject && child.parent == this.transform)
                {
                    child.gameObject.SetActive(false);

                }
                else if (child.parent == this.transform)
                {

                    startingPos = child.gameObject;
                    Debug.Log("This is the Starting positions " + startingPos.name);
                }
                i++;

            }
            allText = startingPos.GetComponentsInChildren<TextMeshPro>();

        }
        index = 0;
        if(allText.Length == 0)
        {
            Debug.Log("Wrong");
        }
        foreach (TextMeshPro child in allText)
        {
                Debug.Log(English[index]);
                mText = child.gameObject.GetComponent<TextMeshPro>();
                mText.text = English[index];
                index++;
            
        }
        currentIndex = index;
        //Text (TMP)


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Horizontal"))
        {
            if (dragDropInstance.getDragging() == false)
            {
                currentIndex -= (int)Input.GetAxisRaw("Horizontal");
                if ((currentIndex + 1) > English.Count)
                {
                    currentIndex = 0;
                }
                else if ((currentIndex) < 0)
                {
                    currentIndex = (English.Count - 1);
                }
                mText.text = English[currentIndex];
                IndicatorEvent uiei = new IndicatorEvent();
                uiei.currentIndexIndicator = currentIndex;
                EventSystem.Current.FireEvent(EVENT_TYPE.CHANGE_UI_INDICATOR, uiei);
            }
        }
    }
}
