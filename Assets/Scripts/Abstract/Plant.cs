using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIObj))]
public abstract class Plant : MonoBehaviour
{
    private const float timeInactivSeeds = 1f;

    [SerializeField] protected float heightPlant;

    [HideInInspector] public float timerIncrease;
    [HideInInspector] public int colvoXP;
    [HideInInspector] public bool isOnInstantiate;
    [HideInInspector] public string Name = "";

    [SerializeField] protected GameObject plants;
    [SerializeField] private GameObject seeds;
    [SerializeField] private GameObject staticObj;

    private Vector3 staticPos;

    private UIObj uIObj;

    protected bool isIncreased;
    protected bool isFarmed;

    public bool IsFarmed => isFarmed;
    public bool IsIncreased => isIncreased;

    public static Action<Vector3, int> PlantFarmedEvent;

    void Awake()
    {
        uIObj = GetComponent<UIObj>();
        gameObject.name = Name;
    }

    private void OnEnable()
    {
        isFarmed = false;
        isIncreased = false;
        staticPos = staticObj.transform.position;
        StartCoroutine(IncreaseAnim());
    }

    private void OnDisable()
    {
        plants.SetActive(false);
        if(seeds && staticObj)
        {
            seeds.SetActive(true);
            staticObj.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        if(staticPos != Vector3.zero)
            staticObj.transform.position = staticPos;
    }
    private IEnumerator IncreaseAnim()
    {
        plants.SetActive(false);
        staticObj.SetActive(false);

        if (seeds != null)
        {
            seeds.SetActive(true);
        }

        yield return StartCoroutine(Mover(timeInactivSeeds, -heightPlant, false));
        staticObj.SetActive(true);
        Increase();
        yield break;
    }

    protected virtual void Increase()
    {
        uIObj.SetTimer(timerIncrease);
        StartCoroutine(Mover(timerIncrease, heightPlant, true));
    }

    public abstract void Farming(float timer, int divider);

    private IEnumerator Mover(float timer, float height, bool enabledObj)
    {
        float time = timer;
        Vector3 pose = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        Vector3 originPos = transform.position;

        if (!enabledObj)
            yield return new WaitForSeconds(.5f);

        while (time > 0)
        {
            time -= Time.deltaTime;
            transform.position = Vector3.Lerp(originPos, pose, (timer - time) / timer);
            yield return null;
        }
        plants.SetActive(true);

        if (seeds)
            seeds.SetActive(false);

        if(staticObj != null && staticObj.activeInHierarchy)
            staticObj.SetActive(true);

        isIncreased = enabledObj;

        yield break;
    }
}
