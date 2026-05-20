using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OPieceController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine(Fall());
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            transform.position += Vector3.left;

        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            transform.position += Vector3.right;

        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            transform.position += Vector3.down;
    }

}
