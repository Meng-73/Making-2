using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;
    public float zSpawn = 0;


    public float tileLength = 30;
    
    public int numberOfTiles = 3;

    public Transform playerTransform;

    private List<GameObject> activateTiles = new List<GameObject>();




    // Start is called before the first frame update
    void Start()
    {
         for (int i = 0; i < numberOfTiles; i++)
        {
            if(i==0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, tilePrefabs.Length));
                //SpawnTile(Random.Range(0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
         if(playerTransform.position.z -35 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();

        }
    }

     public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex],transform.forward *zSpawn , transform .rotation);
        activateTiles.Add(go);

        zSpawn +=tileLength;

    }

    private void DeleteTile()
    {
        Destroy(activateTiles[0]);

        //activeTiles[0].SetActive(false);
        activateTiles.RemoveAt(0);
        //PlayerManager.score += 3;
    }

        

}
