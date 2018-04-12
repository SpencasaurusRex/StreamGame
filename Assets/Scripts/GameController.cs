using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab; 
    public Transform startingTransform;

    public GameObject enemyPrefab;
    public List<Transform> enemySpawnTransforms;

    public float walkoffDistance;

    PlayerController player;

    void Start()
    {
        player = Instantiate(playerPrefab).GetComponent<PlayerController>();
        StartRun();
    }

    private void Update()
    {
        if (player.transform.position.x > walkoffDistance)
        {
            StartRun();
        }
    }

    public void StartRun()
    {
        // Spawn the player
        var player = GameObject.FindObjectOfType<PlayerController>();
        player.transform.position = startingTransform.position;

        // Spawn the enemies
        foreach (Transform spawnTransform in enemySpawnTransforms)
        {
            var enemy = Instantiate(enemyPrefab, spawnTransform);
            var enemyController = enemy.GetComponent<EnemyController>();
            enemyController.playerTransform = player.transform;
        }

        player.StartRun();
        player.TargetClosestEnemy();
    }
}
