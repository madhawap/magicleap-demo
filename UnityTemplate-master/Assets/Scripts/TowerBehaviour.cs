using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{

    public AudioSource fireSound;
    public float range = 3;
    public float projectileDamage = 2;

    public float  fireRate = 0.5f;
    public GameObject projectile;
    float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        currentTime += Time.deltaTime;

        if (currentTime > fireRate){
            
            currentTime = 0;
            float closestDist = float.MaxValue;
            GameObject closestEnemy = null;

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0 ; i < enemies.Length; i ++){
                float distance = (enemies[i].transform.position - transform.position).magnitude;
                if  (distance < closestDist){
                    closestDist = distance;
                    closestEnemy = enemies[i];
                }
            }

            if (closestEnemy != null){
                fireSound.Play();
                //closestEnemy.SendMessage("takeDamage", projectileDamage);
                GameObject instance = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
                instance.GetComponent<ProjectileScript>().targetObject = closestEnemy;
                Vector3 direction = (closestEnemy.transform.position - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            }
        }
        

    }

}
