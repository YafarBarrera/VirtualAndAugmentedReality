using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatUnitController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 15f;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
            moveX = -1f;
        if (Input.GetKey(KeyCode.RightArrow))
            moveX = 1f;
        if (Input.GetKey(KeyCode.UpArrow))
            moveZ = 1f;
        if (Input.GetKey(KeyCode.DownArrow))
            moveZ = -1f;

        Vector3 movement = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        if (Input.GetKeyDown(KeyCode.Backspace) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jumping Mode!");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
