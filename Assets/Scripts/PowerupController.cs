using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public GameController gameController;
    public GameObject     powerupPrefab;
    public int            spawnDelay;
    public List<Powerup>  powerups;

    public Dictionary<Powerup, float> activePowerups = new Dictionary<Powerup, float>();

    private List<Powerup> keys = new List<Powerup>();
    private Vector3       spawnValues;


    void Start()
    {
        spawnValues = gameController.spawnValues;
        StartCoroutine(PowerupWaves());
    }


    void Update()
    {
        HandleActivePowerups();

        // if (Input.GetKeyDown(KeyCode.T)) {
        //     SpawnPowerup(powerups[Random.Range(0, powerups.Count)]);
        // }
    }


    IEnumerator PowerupWaves()
    {
        while (true) {

            if (gameController.gameOver) {
                break;
            }

            yield return new WaitForSeconds(spawnDelay * gameController.difficulty);
            SpawnPowerup(powerups[Random.Range(0, powerups.Count)]);
        }
    }


    public GameObject SpawnPowerup(Powerup powerup)
    {
        GameObject powerupGameObject = Instantiate(powerupPrefab);
        powerupGameObject.GetComponent<Renderer>().material.color = powerup.color;
        gameController.powerupText.color = powerup.color;

        var powerupBehaviour = powerupGameObject.GetComponent<PowerupBehaviour>();
        powerupBehaviour.controller = this;
        powerupBehaviour.SetPowerup(powerup);

        float   spawnRandomX  = Random.Range(-spawnValues.x, spawnValues.x);
        Vector3 spawnPosition = new Vector3(spawnRandomX, spawnValues.y, spawnValues.z);
        powerupGameObject.transform.position = spawnPosition;

        return powerupGameObject;
    }


    public void HandleActivePowerups()
    {
        bool statusChanged = false;

        foreach(Powerup powerup in keys) {
            if (activePowerups[powerup] > 0 && gameController.gameOver == false) {
                int displayTime = (int)activePowerups[powerup] + 1;
                gameController.powerupTime.text = displayTime + "s remaining";
                activePowerups[powerup] -= Time.deltaTime;

            } else {
                statusChanged = true;
                activePowerups.Remove(powerup);
                powerup.End();

                gameController.powerupText.text =
                gameController.powerupTime.text = "";
            }
        }

        if (statusChanged) {
            keys = new List<Powerup>(activePowerups.Keys);
        }
    }


    public void ActivatePowerup(Powerup powerup)
    {
        float duration = (powerup.duration / gameController.difficulty);
        powerup.Start();
        activePowerups.Add(powerup, duration);

        gameController.powerupText.text = powerup.name + "!";

        keys = new List<Powerup>(activePowerups.Keys);
    }

}
