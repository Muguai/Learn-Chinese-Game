using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EventCallbacks;

public class SpawnLines : MonoBehaviour
{
    [Header("Line Prefab")]
    public GameObject prefabLine;
    [Header("Distance between line and heart")]
    public float distance = 2f;
    [Header("Number Of lines Created)")]
    [Range(1, 8)]
    public int numberOfLines = 3;

    void Awake()
    {
        EventSystem.Current.RegisterListener(EVENT_TYPE.CREATE_CHARS, getCharsFromMenu);
        GameObject go = GameObject.Find("ScrollbarListener");
        this.GetComponent<EnemySpawner>().charPrefab = go.GetComponent<ScrollbarListener>().GetPrefabList();
        numberOfLines = go.GetComponent<ScrollbarListener>().linesSpawned;

    }

    void getCharsFromMenu(EventCallbacks.Event eventInfo)
    {
        CreateCharsEvent ucei = (CreateCharsEvent)eventInfo;
        this.GetComponent<EnemySpawner>().charPrefab = ucei.chars;

    }
    // Start is called before the first frame update
    void Start()
    {
        //numberOfLines = SceneProperty.SceneProperties.GetNumberOfLines();
        float Y = 0.0f;
        float X = 0.0f;
        Vector2 spawnPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        //Even
        for (int i = 0; i < numberOfLines; i++)
        {
            if (i >= 2 )
            {
                X = distance;
                Y = 0.0f;
                if (i == 3 && numberOfLines == 5 || i == 4 && numberOfLines == 5)
                {
                    Y = distance;
                    X -= distance / 3;
                    if (numberOfLines == 5 && i == 3)
                    {

                        X = 0;
                    }

                        //   Y -= distance / 3;

                 }
                else if ( i == 2 && numberOfLines == 5 || numberOfLines >= 6 && i != 6 && i != 7)
                {
                    Y = distance;
                    if(numberOfLines == 5)
                    {
                        X -= distance / 3;
                    }
                }
                else if(numberOfLines == 5)
                {

                    X = 0;
                }else if(i == 6 || i == 7)
                {
                    X += distance / 4;
                }
                if (numberOfLines <= 3)
                {
                    X = 0.0f;
                    Y = distance;
                }
            }
            else if(i == 1 || i == 0)
            {

                X = 0.0f;
                Y = distance;
                if (i == 0 && numberOfLines == 5 || i == 1 && numberOfLines == 5)
                {
                    X = distance;
                    X -= distance / 3;

                }else if(i == 0 && numberOfLines == 7)
                {
                    X = distance + (distance / 4);
                    Y = 0f;
                }else if(i == 1 && numberOfLines == 5)
                {
                    X = distance;
                    X += distance / 3;
                }


                if(numberOfLines <= 3)
                {
                    X = distance;
                    Y = 0.0f;
                }
            }

          //  GameObject tmpObj = new GameObject("line " + (i + 1));
          //  tmpObj.transform.SetParent(this.transform);
            GameObject tmpObj = Instantiate(prefabLine);
            tmpObj.transform.SetParent(this.transform);
            tmpObj.name = "Line" + (i + 1);

            if(i == 4 || numberOfLines == 3 && i == 2 || numberOfLines == 5 && i == 0)
            {
                tmpObj.transform.position = spawnPosition + new Vector2(-X, Y);
                if(i==4 && numberOfLines == 5)
                {
                    tmpObj.transform.position = spawnPosition + new Vector2(-X, -Y);

                }
            }
            else if (i == 5 || numberOfLines == 5 && i == 1 || numberOfLines == 7 && i == 0 )
            {
                tmpObj.transform.position = spawnPosition + new Vector2(X, -Y);

            }
            else if ((i + 1) % 2 == 0 || i == 2 && numberOfLines == 5)
            {
                tmpObj.transform.position = spawnPosition + new Vector2(X, Y);

            }
            else
            {
                tmpObj.transform.position = spawnPosition + new Vector2(-X, -Y);
            }
            

        }
        this.GetComponent<EnemySpawner>().enabled = true;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
