using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float moveSpeed = 3f;
    public GameObject targetObject = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(targetObject != null)
        {
            Vector3 direction = (transform.position - targetObject.transform.position).normalized;
            //GetComponent<Rigidbody>().transform.Translate(direction * -moveSpeed * Time.deltaTime);
            transform.Translate(direction * -moveSpeed * Time.deltaTime, Space.World);
            transform.localRotation = Quaternion.LookRotation(direction, Vector3.up);

        }
        else
        {
            Destroy(gameObject);
        }
    }

}
