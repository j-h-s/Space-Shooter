using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float      fireRate, delay;
    public GameObject shot;
    public Transform  shotSpawn;

    private GameObject  player;
    private AudioSource audioSource;


    void Start()
    {
        player      = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate);
    }


    void Fire()
    {
        if (player) {
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
        }
    }

}
