using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehaviour : MonoBehaviour
{
    public PowerupController controller;

    [SerializeField]
    private Powerup powerup;

    private Transform transform_;


    private void Awake()
    {
        transform_ = transform;
    }


    private void ActivatePowerup()
    {
        controller.ActivatePowerup(powerup);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            ActivatePowerup();
            gameObject.SetActive(false);
        }
    }


    public void SetPowerup(Powerup powerup)
    {
        this.powerup = powerup;
        gameObject.name = powerup.name;
    }

}
