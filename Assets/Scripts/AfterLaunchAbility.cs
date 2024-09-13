using System.Collections;
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

    GameObject[] targetObjects;


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
                GetObjects(lm.prefabPlayerPuck);            
                Pull();
                break;
            // Target Enemy 
            case "Push":
                GetObjects(lm.prefabEnemyPuck);
                Push();
                break;
        }
    }

    public void GetObjects(GameObject[] targets)
    {
        targetObjects = new GameObject[targets.Length];
        int i = 0;
        float distance = 0;

        foreach (GameObject puck in targets)
        {
            distance = Mathf.Abs(Vector3.Distance(transform.position, puck.transform.position));

            if (distance <= range && distance > 0)
            {
                targetObjects[i] = puck;
                i++;
            }
        }
        Debug.Log(i + " Objects in array.");
    }

    void Pull()
    {
        if (targetObjects != null && targetObjects.Length > 0)
        {
            foreach (GameObject gm in targetObjects)
            {
                if (gm.GetComponent<Rigidbody>())
                {
                    Rigidbody rb = gm.GetComponent<Rigidbody>();
                    Debug.Log("Got their Transform");

                    // Make them move towards target for 2 second
                    rb.AddForce(transform.forward * 10, ForceMode.Force);
                    Debug.Log("Pulling");
                }
                else
                {
                    return;
                }
            }
        }
    }

    void Push()
    {

    }

    IEnumerator StopAbility()
    {
        Debug.Log("Waiting for Ability");
        yield return new WaitForSeconds(2f);        
    }
}
