using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class SoundListener : MonoBehaviour
    {
        private int NoOfcurrentlyPlayingSounds = 0;
        [SerializeField] private int maxNoOfSimultaneousSounds;
    
        // Start is called before the first frame update
        void Start()
        {
         //   audioSource = this.GetComponent<AudioSource>();
            EventSystem.Current.RegisterListener(EVENT_TYPE.UNIT_DIED, OnDiePlaySound);
            EventSystem.Current.RegisterListener(EVENT_TYPE.HEART_HIT, OnHitPlaySound);

        }

        void OnDiePlaySound(Event eventInfo)
        {
            DieEvent dieEventInfo = (DieEvent)eventInfo;
            AudioClip dieSound = dieEventInfo.UnitSound;

            if (NoOfcurrentlyPlayingSounds < maxNoOfSimultaneousSounds && dieSound != null)
            {
                NoOfcurrentlyPlayingSounds++;
                GameObject unit = dieEventInfo.UnitGameObject;

                AudioSource.PlayClipAtPoint(dieSound, unit.transform.position);
                Invoke("SubtractCurrentlyPlayingSounds", dieSound.length);
                
                
            }
        }

        void OnHitPlaySound(Event eventInfo)
        {
            HitEvent hitEventInfo = (HitEvent)eventInfo;
            AudioClip dieSound = hitEventInfo.UnitSound;

            if (NoOfcurrentlyPlayingSounds < maxNoOfSimultaneousSounds && dieSound != null)
            {
                NoOfcurrentlyPlayingSounds++;
                GameObject unit = hitEventInfo.UnitGameObject;

                AudioSource.PlayClipAtPoint(dieSound, unit.transform.position);
                Invoke("SubtractCurrentlyPlayingSounds", dieSound.length);


            }
        }

        public void SubtractCurrentlyPlayingSounds()
        {
            NoOfcurrentlyPlayingSounds--;
        }
    }
}
