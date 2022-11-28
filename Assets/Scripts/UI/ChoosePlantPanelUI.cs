using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePlantPanelUI : MonoBehaviour
{
    [SerializeField] private ContentSizeFitter content;
    // Start is called before the first frame update
    void Start()
    {
        AddButtonToContent();
    }

    private void AddButtonToContent()
    {
        foreach (GameObject buttonGO in SOUnpacker.Instance.PlantsUIObj)
            Instantiate(buttonGO, content.transform);
    }
}
