using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class DeathListener : MonoBehaviour
    {
       
        void Start()
        {
            EventSystem.Current.RegisterListener(EVENT_TYPE.UNIT_DIED, DestroyUnit);
        }
        void DestroyUnit (Event eventInfo)
        {
            DieEvent unitDieEvent = (DieEvent)eventInfo;            
            Destroy(unitDieEvent.UnitGameObject);
        }
      

    }
}
