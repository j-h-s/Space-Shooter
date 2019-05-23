using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    public float scrollSpeed, tileSize;

    private float   speed;
    private Vector3 start;


    void Start()
    {
        start = transform.position;
        speed = -scrollSpeed * PlayerPrefs.GetInt("difficulty", 1);
    }


    void Update()
    {
        float newPosition  = Mathf.Repeat(Time.time * speed, tileSize);
        transform.position = start + Vector3.forward * newPosition;
    }
}
