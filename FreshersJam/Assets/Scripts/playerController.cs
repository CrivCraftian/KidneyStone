using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float movementSpeed;
    Vector2 inputValue = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        getInputValue();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3((movementSpeed * inputValue.x) * Time.deltaTime, 0, (movementSpeed * inputValue.y) * Time.deltaTime);
    }

    void getInputValue()
    {
        // get input value of x
        if (Input.GetKeyDown("w")) { inputValue.x = 1; }
        else if (Input.GetKeyDown("s")) { inputValue.x = -1; }

        //reset input value of x if there is no input
        if (Input.GetKeyUp("w") || Input.GetKeyUp("s")) { inputValue.x = 0; }

        // get input value of y
        if (Input.GetKeyDown("a")) { inputValue.y = 1; }
        else if (Input.GetKeyDown("d")) { inputValue.y = -1; }

        //reset input value of y if there is no input
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")) { inputValue.y = 0; }
    }
}
