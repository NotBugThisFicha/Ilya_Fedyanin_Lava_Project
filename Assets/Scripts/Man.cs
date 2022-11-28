using System;
using System.Threading.Tasks;
using UnityEngine;

public class Man : Player
{
    [SerializeField] private int timeSeed;
    [SerializeField] private float speedRot = 10f;
    [SerializeField] private float distanceCloseRot = 0.8f;

    private bool isMoving = true;
    private GardenBed gardenBed;
    private string plantName = "";
    private bool isFarmed;

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.MovePlayerToBedEvent += MoveToGardenBed;
        GameController.Instance.SetNamePlantEvent += SetNamePlant;
    }
    private void OnDestroy()
    {
        GameController.Instance.MovePlayerToBedEvent -= MoveToGardenBed;
        GameController.Instance.SetNamePlantEvent -= SetNamePlant;
    }

    private void Update()
    {
        if (target != null && isMoving)
        {
            if(CloseRotateLogik())
                ChooseWhatDoing();
        }
    }

    private void ChooseWhatDoing()
    {
        if (!gardenBed.PlantIsIncreased && plantName != "")
        {
            SetBehaviorAnim("Seed");
            gardenBed.PlantOnBed(plantName);
            Wait(timeSeed);
        }
        else if (gardenBed.PlantIsIncreased)
        {
            SetBehaviorAnim(gardenBed.plant.Name + "Farm");
            gardenBed.plant.Farming(timeSeed, 3);
            Wait(timeSeed);
        }
    }

    private void Wait(float time)
    {
        isMoving = false;
        target = null;
        Invoke(nameof(WaitSeedOrFarm), time);
        plantName = "";
    }
    private bool CloseRotateLogik()
    {
        Vector3 targetNormal = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 dir = targetNormal - transform.position;
        if (Vector3.Distance(targetNormal, transform.position) < distanceCloseRot)
        {
            Quaternion rot = Quaternion.LookRotation(dir);

            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * speedRot);
            float angle = Vector3.Angle(dir, transform.forward);

            if (angle < 1f)
                return true;
            else return false;
        }
        else return false;
    }
    private void MoveToGardenBed(GardenBed gardenBed)
    {
        if(isMoving)
        {
            target = gardenBed.transform;
            this.gardenBed = gardenBed;
            if(Vector3.Distance(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), transform.position) > distanceCloseRot)
                SetBehaviorWalk();
        }
    }

    private void SetNamePlant(string tagPlant)
    {
        this.plantName = tagPlant;
    }

    private void WaitSeedOrFarm()
    {
        Debug.Log("WaitSeed");
        StopBehavior();
        isMoving = true;
    } 
}
