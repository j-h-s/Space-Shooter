using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupAction : MonoBehaviour
{
    [SerializeField]
    public PlayerController playerController;

    private float defaultSpeed, defaultDelay;


    public void SpeedupStartAction()
    {
        defaultSpeed = playerController.speed;
        playerController.speed *= 2;
    }

    public void SpeedupEndAction()
    {
        playerController.speed = defaultSpeed;
    }


    public void QuickShotStartAction()
    {
        defaultDelay = playerController.shot.delay;
        playerController.shot.delay /= 2;
    }

    public void QuickShotEndAction()
    {
        playerController.shot.delay = defaultDelay;
    }

}
