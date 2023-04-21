using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] int quantityEnemy = 2;
    [SerializeField] int spawnRate = 30;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        for (int i = 0; i < quantityEnemy; i++)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(Spawner());
    }
}
