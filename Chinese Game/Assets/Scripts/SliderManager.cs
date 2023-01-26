using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{

    private Slider mySlider;

    private static TMP_Text myText;
    // Start is called before the first frame update
    void Start()
    {
        myText = this.gameObject.transform.GetChild(3).GetComponent<TMP_Text>();

        mySlider = this.GetComponent<Slider>();
        mySlider.onValueChanged.AddListener(delegate { valueChanged(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void valueChanged()
    {
        myText.text = "Amount of Spawner: " + mySlider.value;
    }
}
