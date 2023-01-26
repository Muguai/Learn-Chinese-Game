using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventCallbacks;

public class Health : MonoBehaviour
{
    public int maxHealth = 5;
    Color lerpedColor = Color.white;
    Color OriginalColor;
    SpriteRenderer sr;
    private bool hit = false;
    private float hitTimer = 0;

    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();

        OriginalColor = sr.color;
        lerpedColor = OriginalColor;


    }

    void Update()
    {
        if (hit == true)
        {
            hitTimer += 1 * Time.deltaTime;
            lerpedColor = Color.Lerp(OriginalColor, Color.red, Mathf.PingPong(Time.time, 0.5f));
            sr.color = lerpedColor;
            if (hitTimer > 0.5f)
            {
                if(sr.color != OriginalColor)
                {
                    sr.color = OriginalColor;
                }
                hit = false;
                hitTimer = 0;
            }
        }
    }

    public void DecreaseHealth(int decrease)
    {
        if(maxHealth != 0)
        {
            hit = true;
            maxHealth -= decrease;
            Debug.Log("Hp got decreased by " + decrease + " and is now " + maxHealth);
            HitEvent uhei = new HitEvent();
            uhei.UnitGameObject = this.gameObject;
            uhei.UnitSound = this.GetComponent<AudioSource>().clip;
            uhei.CurrentHealth = maxHealth;
            EventSystem.Current.FireEvent(EVENT_TYPE.HEART_HIT, uhei);
            hitTimer = 0;

            if (maxHealth <= 0)
            {
                this.GetComponent<EnemySpawner>().DestroyAllCharacterPresent();

                EndGameEvent ueei = new EndGameEvent();
                EventSystem.Current.FireEvent(EVENT_TYPE.END_GAME, ueei);
                this.GetComponent<EnemySpawner>().enabled = false;
            }
        }
    }
}
