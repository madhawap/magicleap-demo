using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemyBehaviour : MonoBehaviour
{

    float timer = 0;

    public float duration = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 randomDirection = Random.onUnitSphere; 
        GetComponent<Rigidbody>().velocity += randomDirection;

        int x_switcher = Random.value > 0.5f ? -1 : 1;
        int y_switcher = Random.value > 0.5f ? -1 : 1;
        int z_switcher = Random.value > 0.5f ? -1 : 1;
        GetComponent<Rigidbody>().angularVelocity = new Vector3(3f * x_switcher, 2f * y_switcher, 1f * z_switcher);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer > duration)
        {
            Destroy(gameObject);
        }
    }
}
