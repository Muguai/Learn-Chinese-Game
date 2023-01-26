using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using EventCallbacks;
using TMPro;

public class EnemySpawner : MonoBehaviour
{

    public float spawnRate = 2f;
    private float spawnCount = 0;
    public int totalAmountEnemiesSpawned;
    private int currentlySpawned;
    private bool stopSpawning = false;
    public float increaseSpawnRateEachWave = 0.1f;
    public float speedIncreaseEachWave = 0.1f;
    public float spawnRateIncreaseEachWave = 0.1f;

    private float speedIncrease = 0f;
    public float charSpeed = 0.1f;
    public Vector2 sizeOfCollider = new Vector2(0.27f, 0.27f);
    public GameObject genericPrefab;
    [HideInInspector] public List<string> charPrefab = new List<string>();



    private Transform[] allChildren;
    private Transform[] allChildren2;
    private List<Spawner> childSpawners;
    private int spawnAmount = 0;
    List<Spawner> readySpawners;
    private static System.Random rnd = new System.Random();
    private Health hp;
    private SpawnLines sl;
    private int numberOfLinesSpawned;

    void Awake()
    {
        hp = this.GetComponent<Health>();
        sl = this.GetComponent<SpawnLines>();

    }



    void Start()
    {

        numberOfLinesSpawned = sl.numberOfLines;
        Debug.Log("Enemy Spawner Enabled");
        allChildren = GetComponentsInChildren<Transform>();
        childSpawners = new List<Spawner>();
        readySpawners = new List<Spawner>();
        foreach (Transform child in allChildren)
        {
            if(child.gameObject != null && child.gameObject != this.gameObject && child.gameObject.name != "StartPositions" && child.parent == this.transform)
            {
                childSpawners.Add(new Spawner(child.gameObject, true));
            }
        }
        genericPrefab.GetComponent<EnemyCharacter>().speed = charSpeed;

        
        Debug.Log("Amount of spawners" + childSpawners.Count);
        this.transform.GetChild(0).GetComponent<spawnWeapons>().enabled = true;
        CreateCharsEvent ucei = new CreateCharsEvent();
        ucei.chars = charPrefab;
        EventSystem.Current.FireEvent(EVENT_TYPE.UI_INDICATOR, ucei);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnAmount > 0 && !stopSpawning)
        {
            readySpawners.Clear();

            foreach (Spawner s in childSpawners)
            {
                if (s.IsReady() == true)
                {
                    readySpawners.Add(s);
                }
                else{
                    s.SetReadyAgain();
                    if(s.GetReadyAgain() > ((numberOfLinesSpawned / 2) - 1))
                    {
                        s.ResetReadyAgain();
                        s.SetReady(true);
                    }
                }
            }

            if (readySpawners.Count != 0)
            {
                currentlySpawned++;
                spawnAmount--;
                int r = rnd.Next(readySpawners.Count);
                Spawner s = readySpawners[r];
                s.SetReady(false);


                r = rnd.Next(charPrefab.Count);
                GameObject prefabCh = Instantiate(genericPrefab);
                string[] splitArray = charPrefab[r].Split('-');
                string chineseChar = splitArray[2];
                prefabCh.transform.GetChild(2).GetComponent<TextMeshPro>().text = chineseChar;
                prefabCh.name = splitArray[0] + "-" + splitArray[1];
              
                prefabCh.transform.position = s.getGameObject().transform.position;
                prefabCh.transform.SetParent(s.getGameObject().transform);

                if (currentlySpawned >= totalAmountEnemiesSpawned)
                {
                    stopSpawning = true;
                }

            }
            

        }
        if (spawnCount >= spawnRate && !stopSpawning)
        {
            spawnCount = 0;
            spawnAmount++;

        }
        else if(!stopSpawning)
        {
            spawnCount += 1 * Time.deltaTime;
        }
        if (stopSpawning)
        {
            int i = 0;
            foreach (Spawner s in childSpawners)
            {
                allChildren2 = s.getGameObject().GetComponentsInChildren<Transform>();
                foreach (Transform child in allChildren2)
                {
                    if (child.gameObject != null && child.gameObject != s.getGameObject())
                    {
                        i++;
                    }
                }
            }
         //   Debug.Log("this is the allchildren length " + i);

            if (i == 0)
            {

                DebugEvent udei = new DebugEvent();
                EventSystem.Current.FireEvent(EVENT_TYPE.NEW_WAVE, udei);
                stopSpawning = false;
                currentlySpawned = 0;
                if(spawnRate > 0.5f)
                {
                    spawnRate -= increaseSpawnRateEachWave;
                }
                speedIncrease += speedIncreaseEachWave;
                genericPrefab.GetComponent<EnemyCharacter>().IncreaseSpeed(speedIncrease);
            }
        }
    }

   

    public void DestroyAllCharacterPresent()
    {
        foreach (Spawner s in childSpawners)
        {
            allChildren2 = s.getGameObject().GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                if (child.gameObject != null && child.gameObject != s.getGameObject())
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    public List<string> getPrefabList()
    {
        return charPrefab;
    }

    private class Spawner
    {

        bool IsSpawnerReady;
        GameObject spawnObject;
        private int readyAgain = 0;

        public Spawner(GameObject go, bool ready)
        {
            this.IsSpawnerReady = ready;
            this.spawnObject = go;
        }
        public int GetReadyAgain()
        {
            return readyAgain;
        }
        public void SetReadyAgain()
        {
            readyAgain++;
        }
        public void ResetReadyAgain()
        {
            readyAgain = 0;
        }
        public bool IsReady()
        {
            return IsSpawnerReady;
        }

        public void SetReady(bool set)
        {
            IsSpawnerReady = set;
        }

        public GameObject getGameObject()
        {
            return spawnObject;
        }

    }

}
