using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AfterLaunchAbility : MonoBehaviour
{
    /*  
        Use Trigger collider(?) to check for pucks in set range
        Pull Friendly pucks and push enemy pucks respectively
        Do this through apply force and set origin to puck transform center
        Puck using ability should freeze before ability
        unfreeze after effect and turn change
    */

    LevelManager lm;

    public string ability;
    public float range, power;

    GameObject[] possibleObjects;
    public Transform[] targetObjects;


    private void Awake()
    {
        lm = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();
    }

    public void UseAbility()
    {
        Debug.Log("Running Ability Now");

        switch (ability)
        {
            // Target Friendly
            case "Pull":
                GetObjects("Player Piece", lm.prefabPlayerPuck.Length);            
                Pull();
                break;
            // Target Enemy 
            case "Push":
                GetObjects("Enemy Piece", lm.prefabEnemyPuck.Length);
                Push();
                break;
        }
    }

    public void GetObjects(string targets, int maxSize)
    {
        possibleObjects = new GameObject[maxSize];
        targetObjects = new Transform[maxSize];
        int i = 0;
        float distance = 0;

        possibleObjects = GameObject.FindGameObjectsWithTag(targets);

        foreach (GameObject puck in possibleObjects)
        {
            distance = Mathf.Abs(Vector3.Distance(transform.position, puck.transform.position));

            if (distance <= range && distance > 0)
            {
                targetObjects[i] = puck.GetComponent<Transform>();
                i++;
            }
        }
        Array.Resize(ref targetObjects, i);
        Debug.Log(i + " Objects in array.");
    }

    void Pull()
    {
        if (targetObjects != null && targetObjects.Length > 0)
        {
            foreach (Transform tf in targetObjects)
            {
                Debug.Log(tf);
                // Make them move towards target for 1 second
                tf.position = Vector3.MoveTowards(tf.position, transform.position, power * Time.deltaTime);
                StartCoroutine(StopAbility());
                tf.position = tf.position;
            }
        }
    }

    void Push()
    {

    }

    IEnumerator StopAbility()
    {
        yield return new WaitForSeconds(.5f);        
    }
}
