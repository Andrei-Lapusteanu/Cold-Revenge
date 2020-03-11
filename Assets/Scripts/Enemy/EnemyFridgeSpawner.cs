using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFridgeSpawner : MonoBehaviour
{
    private const string ENEMY_PREFAB_PATH = "_Mine/Enemy/EnemyFridgeSoftEdges_V3";
    GameObject EnemyFridge;

    // Start is called before the first frame update
    void Start()
    {
        EnemyFridge = Resources.Load(ENEMY_PREFAB_PATH) as GameObject;

        StartCoroutine(SpawnEnemiesAsync());
    }

    IEnumerator SpawnEnemiesAsync()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(Random.Range(35f, 60f));
            Instantiate(EnemyFridge, transform.position, Quaternion.identity);
        }
    }
}
