using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneProperty
{
    public class SceneProperties : MonoBehaviour
    {
        public static int numberOfLiness = 1;

        void Awake()
        {
            DontDestroyOnLoad(this);
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public static void SetNumberOfLines(int Amount)
        {
            numberOfLiness = Amount;
        }

        public static int GetNumberOfLines()
        {
            return numberOfLiness;
        }




    }
}
