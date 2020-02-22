using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    float health = 100;

    public int towerCost = 10;

    [HideInInspector]
    public int points = 25;

    private ArrayList towers = new ArrayList();

    void takeDamage(float damage){
        health -= damage;
        if (health < 0){
            // FAILURE STATE : ŸOU SUCK:
            print(points);
        }
    }

    public void gainPoints(int pointsGained)
    {
        points += pointsGained;
    }
    
    public bool canPayForTower()
    {
        return points > towerCost;
    }
    
    public void payForTower(GameObject tower)
    {
        if (points > towerCost)
        {
            points -= towerCost;
            towers.Add(tower);
        }
        else
        {
            Destroy(tower);
        }
    }

    public ArrayList getTowers()
    {
        return towers;
    } 
}