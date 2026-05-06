using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject spawnPrefab;
    public int spawnCount = 3;
    public float spawnTime = 3f;
    
    PlayerHealth player;

    List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        player = FindAnyObjectByType<PlayerHealth>();
        StartCoroutine(SpawnPrefabRoutine());
    }

    IEnumerator SpawnPrefabRoutine()
    {
        while (player)
        {
            yield return new WaitForSeconds(spawnTime);

            //obj => obj == null -->take obj and if it's null then return true
            spawnedObjects.RemoveAll(obj => obj == null); 
            
            if (spawnedObjects.Count < spawnCount)
            {
                GameObject obj = Instantiate(spawnPrefab, transform.position, Quaternion.identity);
                spawnedObjects.Add(obj);
            }
        }
    }
}
