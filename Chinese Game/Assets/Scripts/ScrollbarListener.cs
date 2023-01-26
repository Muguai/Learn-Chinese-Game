using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventCallbacks;

using UnityEngine.SceneManagement;
public class ScrollbarListener : MonoBehaviour
{
    public GameObject content;
    public Slider slider;
    private PopulateGrid pg;
    public List<string> prefabList = new List<string>();
    public static ScrollbarListener Instance;
    public int linesSpawned = 1;

    void Awake()
    {

        if (Instance == null)
        {

            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        if (SceneManager.GetActiveScene().name == "StartGame")
        {
            Debug.Log("do the thing");
            CreateCharsEvent ucei = new CreateCharsEvent();
            ucei.chars = prefabList;
            EventSystem.Current.FireEvent(EVENT_TYPE.CREATE_CHARS, ucei);

        }
        else
        {
            pg = content.GetComponent<PopulateGrid>();
            EventSystem.Current.RegisterListener(EVENT_TYPE.ADD_CHARACTER, OnClickCharacterAdd);
            EventSystem.Current.RegisterListener(EVENT_TYPE.START_GAME, OnClickStartGame);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
       

        pg = content.GetComponent<PopulateGrid>(); 
        EventSystem.Current.RegisterListener(EVENT_TYPE.ADD_CHARACTER, OnClickCharacterAdd);
        EventSystem.Current.RegisterListener(EVENT_TYPE.START_GAME, OnClickStartGame);




    }
    void Update()
    {

       
    }

    public List<string> GetPrefabList()
    {
        return prefabList;
    }
    void OnClickStartGame(EventCallbacks.Event eventInfo)
    {
        if (pg.GetPrefabList().Count > 1)
        {

            Instance.prefabList = pg.GetPrefabList();
            Instance.linesSpawned = (int)slider.value;
            SceneManager.LoadScene("StartGame");

        }

    }

    void OnClickCharacterAdd(EventCallbacks.Event eventInfo)
    {
        AddCharacterEvent CharacterEvent = (AddCharacterEvent)eventInfo;
        if (CharacterEvent.UnitGameObject.GetComponent<AddCharacter>().isDestroyed())
        {
            Debug.Log("DEPOPULATE");
            pg.DePopulate(CharacterEvent.UnitGameObject);

        }
        else
        {
            Debug.Log("POPULATE");

            pg.ExternalPopulate(CharacterEvent.UnitGameObject);

        }
    }
}
