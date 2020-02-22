using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetupManager : MonoBehaviour
{


private const int DIRECTIONS_COUNT = 32;
 private const int MAX_DISTANCE = 10;

    public GameObject mainBasePrefab, spawnerPrefab;

    private GameObject mainBase;
    private GameObject[] spawners;
    
    private ControllerBehaviour controllerScript;

    GameObject camera;

    private bool spawnersAreCorrect = true;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        mainBase = Instantiate(mainBasePrefab, new Vector3(0, 2, 1.5f), Quaternion.identity);
        mainBase.GetComponentInChildren<BaseUIBehaviour>().enabled = false;
        mainBase.tag = "Tower";

        controllerScript = GameObject.FindWithTag("GameController").GetComponent<ControllerBehaviour>();
        
        SpawnSpawners();
    }

    // Update is called once per frame
    void Update()
    {
        // if (controllerScript.holdingTower())
        // {
        //     spawnersAreCorrect = false;
        // }
        // else
        // {
        //     if (!spawnersAreCorrect)
        //     {
        //         // SpawnSpawners();
        //         spawnersAreCorrect = true;
        //     }
        // }


        var distances = new List<float>();
 
        foreach (var direction in GetSphereDirections(DIRECTIONS_COUNT))
        {
    
            RaycastHit groundHitInfo;
            if (Physics.Raycast(transform.position, direction, out groundHitInfo, MAX_DISTANCE))
            {
                distances.Add(groundHitInfo.distance);
                Debug.DrawLine(transform.position, groundHitInfo.point, Color.blue);

            }
        }
    }

    void SpawnSpawners()
    {
        mainBase.tag = "Base";

        // DESTROY EXISTING
        for (int i = 0; i < 3; i++) {
            Destroy(spawners[i]);
            spawners[i] = null;
        }
        

        
        for (int i = 0; i < 3; i++) {
            spawners[i] = Instantiate(spawnerPrefab);
            spawners[i].SendMessage("Decactivate");

        }
        
        // DO STUFF

        mainBase.tag = "Tower";
    }
    
    void StartGame()
    {
        for (int i = 0; i < 3; i++) {
            spawners[i].SendMessage("Activate");
            spawners[i].SendMessage("SetWaitTime", i*15);
        }
        mainBase.tag = "Base";
        mainBase.GetComponentInChildren<BaseUIBehaviour>().enabled = true;
        
        Destroy(gameObject);
    }

    private Vector3[] GetSphereDirections(int numDirections)
 {
     var pts = new Vector3[numDirections];
     var inc = Mathf.PI * (3 - Mathf.Sqrt(5));
     var off = 2f / numDirections;
 
        for (int k = 0; k < numDirections; k++)
     {
         var y = k * off - 1 + (off / 2);
         var r = Mathf.Sqrt(1 - y * y);
         var phi = k * inc;
         var x = (float)(Mathf.Cos(phi) * r);
         var z = (float)(Mathf.Sin(phi) * r);
         pts[k] = new Vector3(x, y, z);
     }
 
     return pts;
 }
}
