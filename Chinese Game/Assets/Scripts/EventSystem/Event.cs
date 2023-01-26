using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public abstract class Event
    {
        public string EventDescription;
    }

    public class DebugEvent : Event
    {
    }
    public class EndGameEvent : Event
    {

    }
    public class IndicatorEvent : Event
    {
        public int currentIndexIndicator;

    }


    public class CreateCharsEvent : Event
    {
        public List<string> chars;

    }
    public class HitEvent : Event
    {
        public int CurrentHealth;
        public GameObject UnitGameObject;
        public AudioClip UnitSound;

    }

    public class AddCharacterEvent : Event
    {
        public GameObject UnitGameObject;
    }

    public class DieEvent : Event
    {
        public GameObject UnitGameObject;
        public AudioClip UnitSound;
        public GameObject UnitParticle;
        public Material UnitHighlightColor;
    }

}
