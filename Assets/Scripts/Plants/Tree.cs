using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Plant
{
    [SerializeField] private ParticleSystem vfx;

    public override void Farming(float timer, int divider)
    {
        
    }

    protected override void Increase()
    {
        base.Increase();
        StartCoroutine(ChangeTrees());
    }


    private IEnumerator ChangeTrees()
    {
        GameObject[] trees = new GameObject[plants.gameObject.transform.childCount];

        for (int i = 0; i < trees.Length; i++)
        {
            trees[i] = plants.transform.Find($"Tree{i + 1}").gameObject;
            trees[i].SetActive(false);
        }

        for (int i = 0; i< trees.Length; i++)
        {
            if (i > 0)
                trees[i - 1].SetActive(false);

            vfx.Play();
            trees[i].SetActive(true);
            yield return new WaitForSeconds(timerIncrease / trees.Length);
        }
    }
}
