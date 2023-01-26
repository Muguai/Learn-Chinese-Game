using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventCallbacks;
using TMPro;

public class DragDrop : MonoBehaviour
{
    public float speed = 1f;
    bool canMove;
    bool dragging;
    BoxCollider2D collider;
    GameObject text;
    bool scaled = false;
    private Vector3 scaleChange;
    private Vector3 originalScale;
    bool m_Started;
    public LayerMask m_LayerMask;
    public LayerMask Text_LayerMask;

    private bool moveBack = false;
    public float speedIncrease = 1.5f;
    private float originalSpeed;
    private ContactFilter2D cf;

    void Start()
    {
        m_Started = true;
        originalSpeed = speed;
        text = this.transform.GetChild(0).gameObject;
        collider = text.GetComponent<BoxCollider2D>();
        canMove = false;
        dragging = false;
        scaleChange = new Vector3(1f, 1f, 0f);
        originalScale = text.transform.localScale;
        cf.SetLayerMask(m_LayerMask);

    }

      // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (collider == Physics2D.OverlapPoint(mousePos, Text_LayerMask) && scaled == false)
        {
            Vector2 sizeC = collider.size;
            text.transform.localScale += scaleChange;
            scaled = true;
        }
        else if(collider != Physics2D.OverlapPoint(mousePos, Text_LayerMask) && scaled == true)
        {
                Vector2 sizeC = collider.size;

                text.transform.localScale = originalScale;
                collider.size = sizeC;

                scaled = false;
            
        }
        

        if (Input.GetMouseButtonDown(0) && moveBack == false)
        {
            if (collider == Physics2D.OverlapPoint(mousePos, Text_LayerMask))
            {
                canMove = true;
            }
            else
            {
                Debug.Log("¨COLLIDER NOT OVERLAPING");

                canMove = false;
            }
            if (canMove)
            {
                dragging = true;
            }


        }
        else if(Input.GetMouseButtonDown(0) && moveBack == true)
        {
            Debug.Log("MOVEBACK WAS true");
        }


        if (dragging)
        {
            this.transform.position = mousePos;
        }
        else
        {
            float step = speed * Time.deltaTime;
            this.transform.position = Vector2.MoveTowards(transform.position, this.transform.parent.position, step);
            float distance = Vector2.Distance(this.transform.position, this.transform.parent.position);

            if (distance < 0.1f)
            {
                moveBack = false;
                speed = originalSpeed;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Collider2D[] results = new Collider2D[4];
            results = Physics2D.OverlapBoxAll(collider.gameObject.transform.position, transform.localScale / 2, 0f, m_LayerMask);
            float highestDistance = -1;
            Collider2D hitColliders = null;
            foreach (Collider2D c in results)
            {
                float distance = Vector2.Distance(collider.gameObject.transform.position, c.gameObject.transform.position);
                if(highestDistance == -1)
                {
                    highestDistance = distance;

                }
                if(distance < highestDistance)
                {
                    highestDistance = distance;
                    hitColliders = c;
                }
            }
            if(hitColliders == null)
            {
                hitColliders = Physics2D.OverlapBox(this.transform.position, this.transform.localScale / 2, 0f, m_LayerMask);
               // Debug.Log("IT WAS NULL");
            }
            else
            {
                // Debug.Log("IT WAS NOT NULL");

            }
            if (hitColliders != null){
                string[] goName = hitColliders.gameObject.name.Split('-');
                if (hitColliders != null && text.GetComponent<TextMeshPro>().text == goName[0])
                {
                    
                    speed = speed + speedIncrease;

                    DieEvent udei = new DieEvent();
                    udei.UnitGameObject = hitColliders.gameObject;
                    udei.EventDescription = "This Object Died: " + hitColliders.gameObject.name;
                    Debug.Log(hitColliders.gameObject.transform.GetChild(0).gameObject.name);

                    udei.UnitParticle = hitColliders.gameObject.transform.GetChild(0).gameObject;
                    udei.UnitSound = hitColliders.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().clip;

                    EventSystem.Current.FireEvent(EVENT_TYPE.UNIT_DIED, udei);
                }

            }
            moveBack = true;
            canMove = false;
            dragging = false;
        }
    }

   


    public bool getDragging()
    {
        return this.dragging;
    }
}

