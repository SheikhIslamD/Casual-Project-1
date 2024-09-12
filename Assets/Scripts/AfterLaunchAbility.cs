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

    Collider[] targetObjects;


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
        targetObjects = new Collider[targets.Length];
        int i = 0;
        float distance = 0;

        foreach (GameObject puck in targets)
        {
            distance = Mathf.Abs(Vector3.Distance(transform.position, puck.transform.position));

            if (distance <= range && distance >= 0.1)
            {
                targetObjects[i] = puck.GetComponent<Collider>();
                i++;
            }
        }
        Debug.Log(targetObjects.Length + " Objects in array.");
    }

    void Pull()
    {
        if (targetObjects != null && targetObjects.Length > 0)
        {
            foreach (Collider col in targetObjects)
            {
                if (col.GetComponent<Rigidbody>())
                {
                    Debug.Log("Got their RB");

                    col.GetComponent<Rigidbody>().linearVelocity = (transform.position - col.transform.position) * power * Time.deltaTime;
                }
            }
        }
    }

    void Push()
    {

    }
}
