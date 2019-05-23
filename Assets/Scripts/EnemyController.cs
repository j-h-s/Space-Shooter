using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float    dodge, smoothing, tilt;
    public Vector2  startDelay, moveTime;
    public Boundary boundaries;

    private float      speed, targetDirection;
    private Rigidbody  enemy;
    private GameObject player;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemy  = GetComponent<Rigidbody>();
        speed  = enemy.velocity.z;
        StartCoroutine(TargetPlayer());
    }


    IEnumerator TargetPlayer()
    {
        yield return new WaitForSeconds(Random.Range(startDelay.x, startDelay.y));

        while (player) {
            targetDirection =(player.transform.position.x - enemy.position.x) * Random.Range(1, dodge);
            yield return new WaitForSeconds(Random.Range(moveTime.x, moveTime.y));
            targetDirection = 0;
        }
    }


    void Update()
    {
        float newMove            = Mathf.MoveTowards(enemy.velocity.x, targetDirection, Time.deltaTime * smoothing);
        float positionHorizontal = Mathf.Clamp(enemy.position.x, boundaries.xMin, boundaries.xMax);
        float positionVertical   = Mathf.Clamp(enemy.position.z, boundaries.zMin, boundaries.zMax);

        enemy.velocity = new Vector3(newMove, 0.0f, speed);
        enemy.position = new Vector3(positionHorizontal, 0.0f, positionVertical);
        enemy.rotation = Quaternion.Euler(0.0f, 0.0f, ((tilt * 10) + (enemy.velocity.x * -tilt)));
    }
}
