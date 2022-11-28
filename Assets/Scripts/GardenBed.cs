using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class GardenBed : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Outline outline;

    [SerializeField] private float timeDelayCamera;
    private CinemachineVirtualCamera virtualCamera;


    public static Action<GardenBed> ClickBedEvent;
    public static Action ClickBedForNavmeshEvent;
    public static Action PlantIsIncreasedBedEvent;

    private Plant _plant;

    public Plant plant => _plant;
    public bool PlantIsIncreased { get {
            if (_plant && _plant.gameObject.activeInHierarchy)
                return _plant.IsIncreased;
            else { _plant = null; return false; } } private set { } }

    public void OnPointerClick(PointerEventData eventData)
    {
        outline.enabled = true;
        if(_plant == null || _plant.IsIncreased)
        {
            ClickBedEvent?.Invoke(this);
            ClickBedForNavmeshEvent?.Invoke();
        }
    }

    void Start()
    {
        outline.enabled = false;
        ClickBedEvent += DisableOutlineBeside;
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    private void FixedUpdate()
    {
        if (PlantIsIncreased)
            outline.OutlineColor = Color.green;
        else outline.OutlineColor = Color.yellow;
    }
    private void OnDestroy()
    {
        ClickBedEvent -= DisableOutlineBeside;
    }

    private void DisableOutlineBeside(GardenBed gardenBed)
    {
        if (this.transform != gardenBed.transform && !plant)
            outline.enabled = false;
    }

    public void PlantOnBed(string plantName)
    {
        _plant = Pooler.Instance.SpawnFromPool(plantName, transform.position, Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0, 180), 0))).GetComponent<Plant>();
        _plant.gameObject.transform.SetParent(transform);
        virtualCamera.Priority = 7;
        Invoke(nameof(InvokeDelayVirtCamera), timeDelayCamera);
    }

    private void InvokeDelayVirtCamera()
    {
        virtualCamera.Priority = 0;
    }
}
