using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float minX, maxX, minY, maxY;
    private int enemyCounter = 0;
    [SerializeField] private int maxEnemies;
    [SerializeField] private Transform[] points;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float enemyTime;
    [SerializeField] private float minimumDistance = 2.0f;  // Minimum distance between enemies

    private List<Vector2> recentSpawns = new List<Vector2>();
    private float timeNextEnemy;

    void Start()
    {
        maxX = points.Max(point => point.position.x);
        minX = points.Min(point => point.position.x);
        maxY = points.Max(point => point.position.y);
        minY = points.Min(point => point.position.y);
    }

    void Update()
    {
        timeNextEnemy += Time.deltaTime;

        if (timeNextEnemy >= enemyTime && enemyCounter < maxEnemies)
        {
            timeNextEnemy = 0;
            CreateEnemy();
        }
    }

    private void CreateEnemy()
    {
        Vector2 randomPosition;
        bool positionValid;
        int attempts = 0;

        do
        {
            randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            positionValid = true;

            // Check if the new position is too close to any recent spawn
            foreach (var pos in recentSpawns)
            {
                if (Vector2.Distance(pos, randomPosition) < minimumDistance)
                {
                    positionValid = false;
                    break;
                }
            }

            attempts++;
            if (attempts > 10) // Prevent an infinite loop
            {
                break;
            }
        }
        while (!positionValid);

        if (positionValid)
        {
            GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], randomPosition, Quaternion.identity);
            enemyCounter++;
            recentSpawns.Add(randomPosition);
            
            // Optionally clear the list periodically to avoid unchecked growth
            if (recentSpawns.Count > 10)
            {
                recentSpawns.RemoveAt(0);
            }
        }
    }
}