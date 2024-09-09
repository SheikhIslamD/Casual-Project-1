using UnityEngine;

public class PuckAbility : MonoBehaviour
{
    /*  
        Use Trigger collider(?) to check for pucks in set range
        Pull Friendly pucks and push enemy pucks respectively
        Do this through apply force and set origin to puck transform center
        Puck using ability should freeze before ability
        unfreeze after effect and turn change
    */

    public int ability;
    public float range;

    Collider[] targetObjects;
    public GameObject[] checkArray;

    private void Awake()
    {
        switch (ability)
        {
            // Target Friendly
            case 1:
                checkArray = GameObject.FindGameObjectsWithTag("Player Piece");
                break;
            // Target Enemy 
            case 2:
                checkArray = GameObject.FindGameObjectsWithTag("Enemy Piece");
                break;
        }
    }

    public void GetObjects()
    {
        int i = 0;
        float distance = 0;

        foreach (GameObject puck in checkArray)
        {
            distance = Mathf.Abs(Vector3.Distance(transform.position, puck.transform.position));

            if (distance <= range)
            {
                targetObjects[i] = puck.GetComponent<Collider>();
                i++;
            }
        }
        Debug.Log(targetObjects.Length + " Objects in array.");
    }
}
