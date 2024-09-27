using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AfterLaunchAbility : MonoBehaviour
{
    LevelManager lm;

    List<SoundEffectTrigger> st;
    List<Transform> tf;
    List<Rigidbody> rb;

    public List<GameObject> targetObjects;

    public bool hasTriggered;
    public static bool active;
    public bool allocated;

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
        if(!allocated)
        {
            Debug.Log("Check Call Count");
            float distance = 0;

            // Fill a list of GameObjects with pieces of the targeted tag
            targetObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag(targets));
            st = new List<SoundEffectTrigger>();
            tf = new List<Transform>();
            rb = new List<Rigidbody>();

            // Remoe all pieces outside the range of the ability piece
            for (int i = 0; i < targetObjects.Count; i++)
            {
                distance = Mathf.Abs(Vector3.Distance(transform.position, targetObjects[i].transform.position));

                if (distance > range || distance == 0)
                {
                    targetObjects.Remove(targetObjects[i]);
                }
            }

            for (int i = 0; i < targetObjects.Count; i++)
            {
                st.Add(targetObjects[i].GetComponent<SoundEffectTrigger>());
                tf.Add(targetObjects[i].GetComponent<Transform>());
                rb.Add(targetObjects[i].GetComponent<Rigidbody>());
            }

            allocated = true;
        }
    }

    void Pull()
    {
        // Visual, Make Cat Spin
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 4, 0);

        // Ensure List is not empty
        if (targetObjects != null && targetObjects.Count > 0)
        {
            active = true;
            // For each piece in list, pull and spin those pieces
            for (int i = 0; i < targetObjects.Count; i++)
            {                
                rb[i].angularVelocity = new Vector3(0, 1.5f, 0);

                if (!st[i].gravityCollision)
                {
                    tf[i].position = Vector3.MoveTowards(tf[i].position, transform.position, power * Time.deltaTime);
                }
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
            active = true;
            Debug.Log("active? " + active);
            // For each piece in list, push and spin those pieces
            for (int i = 0; i < targetObjects.Count; i++)
            {
                // Make them move away from target
                rb[i].angularVelocity = new Vector3(0, 2f, 0);

                Debug.Log("Has Collided? " + st[i].gravityCollision);

                if (!st[i].gravityCollision)
                {                    
                    // Height has to be raised to match dog height for smooth movement
                    tf[i].position = Vector3.MoveTowards(tf[i].position, new Vector3(transform.position.x, tf[i].position.y, transform.position.z), -power * Time.deltaTime);
                }
                //1.196082
            }
        }

        StartCoroutine(RunAbility());
    }

    IEnumerator RunAbility()
    {
        yield return new WaitForSeconds(1f);

        hasTriggered = true;
        active = false;

        for (int i = 0; i < targetObjects.Count; i++)
        {
            st[i].gravityCollision = false;
        }
    }
}
