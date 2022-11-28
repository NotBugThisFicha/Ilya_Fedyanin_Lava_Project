using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOUnpacker : MonoBehaviour
{
    [SerializeField] private PlantInfo[] plantsInfo;

    private Plant[] plants;
    private GameObject[] plantsUiObj;

    private static SOUnpacker instance;
    public static SOUnpacker Instance => instance;
    public GameObject[] PlantsUIObj => plantsUiObj;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this.gameObject);

        UnpackPlantsObj();
    }

    private void Start()
    {
        FillPool();
    }

    private void FillPool()
    {
        foreach (Plant plant in plants)
            Pooler.Instance.AddPool(plant.Name, plant.gameObject, GroundGenerator.SizeGround);
    }
    private void UnpackPlantsObj()
    {
        plants = new Plant[plantsInfo.Length];
        plantsUiObj = new GameObject[plants.Length];

        int i = 0;
        foreach(PlantInfo plantInfo in plantsInfo)
        {
            plantInfo.SetPlantPropertys();
            plants[i] = plantInfo.PlantGroupe;
            plantsUiObj[i] = plantInfo.PlantUIButton.gameObject;
            i++;
        }
    }
}
