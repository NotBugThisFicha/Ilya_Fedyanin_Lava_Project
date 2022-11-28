using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Plant
{
    [SerializeField] private ParticleSystem vfx;
    public override void Farming(float timer, int divider)
    {
        StartCoroutine(FarmingGrassCor(timer, divider));
    }
    private IEnumerator FarmingGrassCor(float timer, int divider)
    {

        var time = timer / divider;
        var posDivideY = (transform.position.y - heightPlant) / divider;

        while (divider > 0)
        {
            divider -= 1;
            yield return new WaitForSeconds(time);
            vfx.gameObject.SetActive(true);
            vfx.Play();
            transform.position = new Vector3(transform.position.x, transform.position.y + posDivideY, transform.position.z);
        }
        PlantFarmedEvent?.Invoke(transform.position, colvoXP);
        transform.SetParent(Pooler.Instance.transform);
        isFarmed = true;
        gameObject.SetActive(false);
    }
}
