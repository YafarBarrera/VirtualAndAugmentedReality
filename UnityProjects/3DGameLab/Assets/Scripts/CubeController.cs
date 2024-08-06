using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeController : MonoBehaviour
{
    public float speed = 9.0f;
    bool typeClick = false;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        typeClick = Input.GetMouseButtonDown(0);
        float horizontalMov = Input.GetAxis("Horizontal");
        float verticalMov = Input.GetAxis("Vertical");
        Vector3 vector3 = new Vector3(horizontalMov,verticalMov, 0.0f);
        transform.Translate(vector3*speed*Time.deltaTime);

        if (typeClick)
        {
            Debug.Log("Right Mouse Click Active!");
        }
    }
}
