using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class DebugListener : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            EventSystem.Current.RegisterListener(EVENT_TYPE.UNIT_DIED, OnDieDebugLog);
        }

        // Update is called once per frame
        void OnDieDebugLog(Event eventInfo)
        {
            DieEvent dieEventInfo = (DieEvent)eventInfo;
            Debug.Log(dieEventInfo.EventDescription);
        }
    }
}
