﻿using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;


public class Area : MonoBehaviour
{
    [SerializeField]
    [Required]
    AreaData data = null;
    [SerializeField]
    [Required]
    GameObject host = null;
    [SerializeField]
    [ReadOnly]
    List<NPCController> nPCs = new List<NPCController>();

    void Start()
    {
        PopulateArea();
    }
    void OnDrawGizmos()
    {
        if (data)
        {
            var color = Color.green;
            color.a = 0.3f;
            Gizmos.color = color;
            Gizmos.DrawSphere(host.transform.position, data.size);
        }
    }

    private void PopulateArea()
    {
        foreach (var inhabitantRatio in data.typeOfInhabitants)
        {
            int amountToSpawn = Mathf.FloorToInt(inhabitantRatio.ratio * data.maxPopulation);
            for (int i = 0; i < amountToSpawn; i++)
            {
                var randomPos = RandomPointInArea();
                var newInhabitant = GameObject.Instantiate(inhabitantRatio.inhabitant, randomPos, Quaternion.identity, this.transform);
                var npcControl = newInhabitant.GetComponentInChildren<NPCController>();
                var collider = newInhabitant.GetComponentInChildren<Collider>(true);
                if (collider != null)
                {
                    collider.enabled = true;
                }
                if (npcControl)
                {
                    nPCs.Add(npcControl);
                    npcControl.SetHome(this.transform);
                    npcControl.SetPatrolRange(data.size);
                    npcControl.SetAiActive(true);
                }

            }

        }
    }

    private Vector3 RandomPointInArea()
    {
        Vector3 randomPos = (UnityEngine.Random.insideUnitSphere * data.size) + host.transform.position;
        float baseRange = 5f;
        NavMeshHit hit;
        while ((NavMesh.SamplePosition(randomPos, out hit, baseRange, NavMesh.AllAreas) == false))
        {
            baseRange += 5f;
        }
        return hit.position;
    }
}
