﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedRollerSet : MonoBehaviour
{

    public GameObject[] rollers = new GameObject[6];
    public List<Box> currentObjects;

    public float speed = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (speed == 0) {
            return;
        }

        var rollerSpeed = 1.5f * 360 * speed;
        for (int i = 0; i < rollers.Length; i++)
        {
            rollers[i].transform.localEulerAngles += new Vector3(0, -rollerSpeed * Time.fixedDeltaTime, 0);
        }

        var dir = transform.TransformVector(new Vector3(speed * Time.fixedDeltaTime, 0, 0));
        foreach (var obj in currentObjects) {
            if(!obj.onConveyor)
            {
                obj.onConveyor = true;
                obj.transform.Translate(obj.transform.InverseTransformVector(dir));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Box b = other.gameObject.GetComponent<Box>();
        if (b != null) {
            currentObjects.Add(b);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Box b = other.gameObject.GetComponent<Box>();
        currentObjects.Remove(b);
    }
}
