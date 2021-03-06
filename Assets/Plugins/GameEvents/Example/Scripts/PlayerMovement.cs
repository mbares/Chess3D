﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float speed = 10f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A)) {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.W)) {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S)) {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
    }
}
