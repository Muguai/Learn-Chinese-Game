using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class ParticleListener : MonoBehaviour
    {
        private Vector3 diePlace;
        // Start is called before the first frame update
        void Start()
        {
            EventSystem.Current.RegisterListener(EVENT_TYPE.UNIT_DIED, OnDiePlayParticleEffect);

        }
        void OnDiePlayParticleEffect(Event eventInfo)
        {
            DieEvent unitDieEvent = (DieEvent)eventInfo;
            diePlace = unitDieEvent.UnitGameObject.transform.position;
            GameObject myParticle = unitDieEvent.UnitParticle;
            myParticle.GetComponent<ParticleSystem>().playOnAwake = true;
            myParticle.GetComponent<CFX_AutoDestructShuriken>().enabled = true;
            if(myParticle != null)
            {
                Instantiate(myParticle, diePlace, Quaternion.identity);
            }

        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
