using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}


[Serializable]
public class Shot
{
    public GameObject  bolt;
    public int         strength = 1;
    public float       delay;
    public Transform[] spawns;
}


public class PlayerController : MonoBehaviour
{
    public float    speed, tilt;
    public Boundary boundaries;
    public Shot     shot;

    private float   nextShot = 0;

    private Rigidbody   player;
    private AudioSource audioSource;


    void Start()
    {
        player      = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextShot) {
            FireShot();
        }

        float moveHorizontal     = Input.GetAxis("Horizontal");
        float moveVertical       = Input.GetAxis("Vertical");
        float positionHorizontal = Mathf.Clamp(player.position.x, boundaries.xMin, boundaries.xMax);
        float positionVertical   = Mathf.Clamp(player.position.z, boundaries.zMin, boundaries.zMax);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        player.position  = new Vector3(positionHorizontal, 0.0f, positionVertical);
        player.velocity  = movement * speed;
        player.rotation  = Quaternion.Euler(0.0f, 0.0f, ((tilt * 10) + (player.velocity.x * -tilt)));
    }


    private void FireShot()
    {
        nextShot = Time.time + shot.delay;
        foreach (var shotSpawn in shot.spawns) {
            Instantiate(shot.bolt, shotSpawn.position, shotSpawn.rotation);
        }
        audioSource.Play();
    }
}
