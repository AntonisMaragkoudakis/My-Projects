using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager1 : MonoBehaviour
{

    public GameObject[] tilePrefabs;
    public GameObject EndPlane;

    private Transform SphereTransfom;
    private float spawnZ = -5.0f;
    private float tileLength = 10.0f;
    private int tilesOnScreen = 45;

    private int lastPrefabIndex = 0;
    private float safeZone = 200.0f;

    private List<GameObject> activeTiles = new List<GameObject>();


    private void Start()
    {
        activeTiles = new List<GameObject>();
        SphereTransfom = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < tilesOnScreen; i++)
        {
            if (i < 3)
                SpawnTile(0);
            else
                SpawnTile();
        }
    }


    private void Update()
    {
        if (SphereTransfom.position.z - safeZone > (spawnZ - tilesOnScreen * tileLength))
        {
            SpawnTile();
            Deletetile();
        }
    }

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        if (prefabIndex == -1)
            if (SphereTransfom.position.z < 780)
            {
                go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
            }
            else if (SphereTransfom.position.z < 800)
            {
                go = Instantiate(tilePrefabs[0]) as GameObject;
            }
            else
            {
                go = Instantiate(EndPlane) as GameObject;
            }
            
        else
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;

        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }

    private void Deletetile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int RandomIndex = lastPrefabIndex;

        while (RandomIndex == lastPrefabIndex)
        {
            RandomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastPrefabIndex = RandomIndex;
        return RandomIndex;

    }


}