using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    [SerializeField] private PhysicsRaycaster physicsRaycaster;
    [SerializeField] private GameObject choosePanel;

    private static GameController instance;
    public static GameController Instance => instance;

    public Action<GardenBed> MovePlayerToBedEvent;
    public Action<string> SetNamePlantEvent;

    private GardenBed gardenBed;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        GardenBed.ClickBedEvent += SetTargetPlant;
    }
    private void SetTargetPlant(GardenBed targetPlant)
    {
        gardenBed = targetPlant;

        if(!gardenBed.plant)
        {
            physicsRaycaster.enabled = false;
            choosePanel.SetActive(true);
            SetTimeScale(0);
        }

        MovePlayerToBedEvent?.Invoke(gardenBed);
    } 

    private void SetTimeScale(float value) => Time.timeScale = value;

    public void SetPlant(string tagPlant)
    {
        if (!Pooler.Instance.poolDictionary.ContainsKey(tagPlant))
            throw new InvalidOperationException($"~{tagPlant}~ name not contains in Pooler! Try name which contains");

        SetNamePlantEvent?.Invoke(tagPlant);
        physicsRaycaster.enabled = true;
        SetTimeScale(1);
    } 



    private void OnDestroy()
    {
        GardenBed.ClickBedEvent -= SetTargetPlant;
    }
}
