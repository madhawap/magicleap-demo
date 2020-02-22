using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float damageDealt = 5;
    public float moveSpeed = 0.5f;
    float health = 10;

    AudioSource hitSound;
    public GameObject DeathEffect;
    public GameObject DeadEnemy;

    GameObject targetObject;

    private Rigidbody _rigidbody;
    //Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        hitSound = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
        targetObject = GameObject.FindGameObjectsWithTag("Base")[0];
    }

    void FixedUpdate(){
        Vector3 direction = (transform.position - targetObject.transform.position).normalized;
        _rigidbody.MovePosition(Vector3.MoveTowards(transform.position, targetObject.transform.position, (moveSpeed * Time.deltaTime)));
        //transform.rotation = Quaternion.LookRotation(direction, new Vector3(0, 0, -1f));
        // Vector3 direction = (closestEnemy.transform.position - transform.position).normalized;
        // transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        _rigidbody.MoveRotation(Quaternion.LookRotation(direction, Vector3.up));
    }


    void OnCollisionEnter(Collision other) {
        if  (other.gameObject.CompareTag("Base")){
            other.gameObject.SendMessage("takeDamage", damageDealt);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Projectile"))
        {
            takeDamage(2);
            Destroy(other.gameObject);
        }
    }

    void  takeDamage(float damage){
        hitSound.Play();
        health -= damage;
        if (health < 0){
            die();
        }
    }

    void die()
    {
        GameObject baseObject = GameObject.FindWithTag("Base");
        if (baseObject != null) baseObject.GetComponent<BaseBehaviour>().gainPoints(5);
        GameObject newEn = Instantiate(DeadEnemy, transform.position, transform.rotation);
        newEn.transform.localScale = transform.localScale;
        GameObject newEf = Instantiate(DeathEffect, transform.position, Quaternion.identity);
        newEf.transform.localScale = transform.localScale;
        Destroy(gameObject);
    }
  
  
    
}
