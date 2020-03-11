using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInitSpawner : MonoBehaviour
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
        yield return new WaitForSeconds(Random.Range(1f, 20f));
        Instantiate(EnemyFridge, transform.position, Quaternion.identity);
    }
}
