using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Carrot : Plant
{
    public static Action<Vector3, int> CarrotFarmedEvent;
    public override void Farming(float timer, int divider)
    {
        StartCoroutine(FindCarrotAndDeactiv(timer));
    }

    private IEnumerator FindCarrotAndDeactiv(float timer)
    {
        int carrotMassLength = plants.transform.childCount;
        GameObject[] carrots = new GameObject[carrotMassLength];
        for(int i = 0; i< carrotMassLength; i++)
        {
            carrots[i] = plants.transform.Find($"Carrot{i+1}").gameObject;
            carrots[i].SetActive(false);
            yield return new WaitForSeconds(timer/carrotMassLength);
        }
        for (int i = 0; i < carrotMassLength; i++)
        {
            carrots[i].SetActive(true);
        }
        transform.SetParent(Pooler.Instance.transform);
        PlantFarmedEvent?.Invoke(transform.position, colvoXP);
        CarrotFarmedEvent?.Invoke(transform.position, carrotMassLength);
        isFarmed = true;
        gameObject.SetActive(false);
    }
}
