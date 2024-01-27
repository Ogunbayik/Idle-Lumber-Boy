using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Flatbed flatbed;

    [SerializeField] private GameObject[] flatbedPrefabs;
    [SerializeField] private GameObject treePrefab;

    [SerializeField] private Transform flatbedSpawnPoint;
    [SerializeField] private int maximumTreeCount;
    [SerializeField] private float startSpawnTimer;

    private int treeCount;
    private int randomIndex;
    private Vector3 randomPosition;

    private float spawnTimer;
    void Start()
    {
        treeCount = 0;
        spawnTimer = startSpawnTimer;
        SpawnFlatbed();
    }

    private void Update()
    {
        if (flatbed == null)
            SpawnFlatbed();

        if (treeCount <= maximumTreeCount)
        {
            spawnTimer -= Time.deltaTime;

            if(spawnTimer <= 0)
            {
                treeCount++;
                spawnTimer = startSpawnTimer;
                SpawnTree();
            }
        }
    }

    private void SpawnFlatbed()
    {
        randomIndex = Random.Range(0, flatbedPrefabs.Length);

        var flatbedObj = Instantiate(flatbedPrefabs[randomIndex]);
        flatbed = flatbedObj.GetComponent<Flatbed>();
        flatbedObj.transform.position = flatbedSpawnPoint.position;
    }

    private void SpawnTree()
    {
        var tree = Instantiate(treePrefab);
        tree.transform.position = GetRandomPosition();
    }

    private Vector3 GetRandomPosition()
    {
        var borderX = 20f;
        var minimumZ = -12f;
        var maximumZ = -7f;
        var posY = 0f;
        var randomX = Random.Range(-borderX, borderX);
        var randomZ = Random.Range(minimumZ, maximumZ);
        randomPosition = new Vector3(randomX, posY, randomZ);

        return randomPosition;
    }
}
