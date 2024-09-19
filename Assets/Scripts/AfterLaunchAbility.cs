using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AfterLaunchAbility : MonoBehaviour
{
    LevelManager lm;

    public List<GameObject> targetObjects;

    public bool hasTriggered;

    public string ability;
    public float range, power;



    private void Awake()
    {
        lm = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();
    }

    public void UseAbility()
    {
        Debug.Log("Running Ability Now");

        if (!hasTriggered)
        {
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
        
    }

    public void GetObjects(string targets, int maxSize)
    {
        float distance = 0;

        // Fill a list of GameObjects with pieces of the targeted tag
        targetObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag(targets));

        // Remoe all pieces outside the range of the ability piece
        for (int i = 0; i < targetObjects.Count; i++)
        {
            distance = Mathf.Abs(Vector3.Distance(transform.position, targetObjects[i].transform.position));

            if (distance > range || distance == 0)
            {
                targetObjects.Remove(targetObjects[i]);
            }
        }
    }

    void Pull()
    {
        // Visual, Make Cat Spin
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 4, 0);

        // Ensure List is not empty
        if (targetObjects != null && targetObjects.Count > 0)
        {
            // For each piece in list, pull and spin those pieces
            for (int i = 0; i < targetObjects.Count; i++)
            {
                Transform tf  = targetObjects[i].transform;
                Rigidbody rb = targetObjects[i].GetComponent<Rigidbody>();

                // Make them move towards target
                rb.angularVelocity = new Vector3(0, 1.5f, 0);
                tf.position = Vector3.MoveTowards(tf.position, transform.position, power * Time.deltaTime);
            }
        }

        StartCoroutine(RunAbility());
    }

    void Push()
    {
        // Visual, Make Cat Spin
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, -4, 0);

        // Ensure List is not empty
        if (targetObjects != null && targetObjects.Count > 0)
        {
            // For each piece in list, push and spin those pieces
            for (int i = 0; i < targetObjects.Count; i++)
            {
                Transform tf = targetObjects[i].transform;
                Rigidbody rb = targetObjects[i].GetComponent<Rigidbody>();

                // Make them move away from target
                rb.angularVelocity = new Vector3(0, 2f, 0);
                // Height has to be raised to match dog height for smooth movement
                tf.position = Vector3.MoveTowards(tf.position, new Vector3(transform.position.x, transform.position.y + 0.12258f, transform.position.z), -power * Time.deltaTime);
            }
        }

        StartCoroutine(RunAbility());
    }

    IEnumerator RunAbility()
    {
        yield return new WaitForSeconds(1f);

        hasTriggered = true;
    }
}
