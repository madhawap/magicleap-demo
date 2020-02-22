using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehaviour : MonoBehaviour
{

    public Color activeColor;
    public Color deactivatedColor;
    
    private LineRenderer line;
    public bool active;

    public GameObject enemy;

    private Material spawnerMat;
    
    private float spawnRate = 1f;
    private float secondsBetweenEnemies = 4f;
    public float timeToWait = 0f;
    
    AudioSource audioSource;
    private float timer;
    private float waitingTimer = 0;
    private float spawnIncreaseTimer = 0f;

    private bool playedSound = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spawnerMat = GetComponent<Renderer>().material;


        //timer = timeToWait;
        line = this.gameObject.GetComponent<LineRenderer>();
        line.SetWidth(0.05F, 0.05F);
        // line.SetColors(new Color(0, 0, 0), new Color(25, 25, 25));
        line.SetPosition(0, transform.position);
        line.SetPosition(1, GameObject.FindWithTag("Base").transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            spawnerMat.SetColor("_Color", activeColor);
            line.material.SetColor("_TintColor", activeColor);

            
            waitingTimer += Time.deltaTime;
            if (waitingTimer > timeToWait)
            {
                if (!playedSound){
                    audioSource.Play();
                    playedSound = true;
                }
                timer += Time.deltaTime;
                spawnIncreaseTimer += Time.deltaTime;

                if (timer > secondsBetweenEnemies)
                {
                    timer -= secondsBetweenEnemies;
                    Instantiate(enemy, transform.position, transform.rotation);
                    if (spawnIncreaseTimer > 5f)
                    {
                        spawnIncreaseTimer -= 5f;
                        secondsBetweenEnemies -= 0.5f;
                        secondsBetweenEnemies = Math.Max(secondsBetweenEnemies, 1.5f);
                    }
                }
            } else
            {
                line.material.SetColor("_TintColor", deactivatedColor);
                spawnerMat.SetColor("_Color", deactivatedColor);
            }
        }
       
    }

    void Decactivate(){
        active  = false;
    }
    void Activate(){
        active  = true;
    }

    void SetWaitTime(float time){
        timeToWait = time;
    }

}
