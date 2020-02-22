using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject spawnGameObject;
    public GameObject baseObject;
    public List<GameObject> spawnPositions;
    public float spawnSeconds;
    private float lastSpawnTime = 0f;
    



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > (lastSpawnTime + spawnSeconds)){
            int newRandomNumber = Random.Range(0, 3);
            Vector3 positionOfRandomlyChosenSpwan = spawnPositions[newRandomNumber].transform.position;
            Instantiate(spawnGameObject, positionOfRandomlyChosenSpwan, Quaternion.identity);
            lastSpawnTime = Time.time;
            //print(Time.time);
        }
    }
}
