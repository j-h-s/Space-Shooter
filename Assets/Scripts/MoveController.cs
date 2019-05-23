using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity = transform.forward * (-speed * PlayerPrefs.GetInt("difficulty", 1));
    }
}
