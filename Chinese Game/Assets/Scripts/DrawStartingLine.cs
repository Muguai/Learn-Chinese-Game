using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DrawStartingLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        Transform daddyTransform = this.transform.parent;
        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;

        //For drawing line in the world space, provide the x,y,z values
        lineRenderer.SetPosition(0, new Vector2(this.transform.position.x, this.transform.position.y)); //x,y and z position of the starting point of the line
        lineRenderer.SetPosition(1, new Vector2(daddyTransform.position.x, daddyTransform.position.y)); //x,y and z position of the starting point of the line
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
