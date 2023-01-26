using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class EnemyCharacter : MonoBehaviour
{
    private Transform target;
    public float speed = 0.3f;
    private Health hp;
    // Start is called before the first frame update
    void Awake()
    {
        if(target == null && SceneManager.GetActiveScene().name == "StartGame")
        {
            target = GameObject.FindWithTag("Health").transform;
            hp = target.gameObject.GetComponent<Health>();
        }
        else
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(this.transform.position, target.position, step);
    }

    public void setTarget(GameObject T)
    {
        target = T.transform;
    }
    public void IncreaseSpeed(float increase)
    {
        speed += increase;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Health" )
        {
            Debug.Log("HIT");
            hp.DecreaseHealth(1);
            Destroy(this.gameObject, 0.1f);
        }
    }
}
