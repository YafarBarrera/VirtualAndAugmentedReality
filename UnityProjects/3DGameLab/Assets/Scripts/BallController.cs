using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float bounceForce = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        // Encuentra la pared más cercana
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        GameObject closestWall = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject wall in walls)
        {
            float distance = Vector3.Distance(transform.position, wall.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestWall = wall;
            }
        }

        if (closestWall != null)
        {
            // Calcula la dirección hacia la pared más cercana y aplica una fuerza
            Vector3 direction = (closestWall.transform.position - transform.position).normalized;
            rb.AddForce(direction * bounceForce, ForceMode.Impulse);
        }
    }
}

