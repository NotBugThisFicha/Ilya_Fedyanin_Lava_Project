using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class NavMeshRebaker : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;
    // Start is called before the first frame update
    void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        NavMeshBake();
        GardenBed.ClickBedForNavmeshEvent += NavMeshBake;
    }

    private void NavMeshBake()
    {
        navMeshSurface.BuildNavMesh();
    }

    private void OnDestroy()
    {
        GardenBed.ClickBedForNavmeshEvent -= NavMeshBake;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
