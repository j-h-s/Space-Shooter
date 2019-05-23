using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion, playerExplosion;
    public int        scoreValue, healthValue;

    private GameController gameController;


    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if (gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }


    void OnTriggerEnter(Collider other) {
        if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "Powerup") {
            return;
        }

        if (explosion != null) {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.tag == "Player") {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.UpdateLives();
 
            if (gameController.playerLives == 0) {
                Destroy(other.gameObject);
                gameController.GameOver();
            }
        }

        healthValue --;

        if (healthValue == 0) {
            Destroy(gameObject);
            gameController.AddScore(scoreValue);
        }

        if (other.tag != "Player") {
            Destroy(other.gameObject);
        }
    }
}
