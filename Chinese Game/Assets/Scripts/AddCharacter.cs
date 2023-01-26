using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventCallbacks;

public class AddCharacter : MonoBehaviour
{

    private Button myButton;
    public bool Destroy = false;
    // Start is called before the first frame update
    void Start()
    {
        myButton = this.GetComponent<Button>();
        myButton.onClick.AddListener(delegate { onClickAddCharacter(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyWhenClick()
    {
        Destroy = true;
    }
    public bool isDestroyed()
    {
        return this.Destroy; 
    }

    void onClickAddCharacter()
    {

        Debug.Log(this.gameObject.name);
        if (Destroy == true)
        {
            AddCharacterEvent udei = new AddCharacterEvent();
            udei.UnitGameObject = this.gameObject;
            EventSystem.Current.FireEvent(EVENT_TYPE.ADD_CHARACTER, udei);
            Debug.Log("DESTROY");
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("CLICKADD");
            AddCharacterEvent udei = new AddCharacterEvent();
            GameObject copy;
            copy = this.gameObject;
            udei.UnitGameObject = copy;
            copy.transform.SetParent(this.transform.parent);
            EventSystem.Current.FireEvent(EVENT_TYPE.ADD_CHARACTER, udei);


        }

    }
}
