using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterFewSeconds : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float destroyAfterThisTIme;
    void Awake()
    {
        Destroy(this.gameObject, destroyAfterThisTIme);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
