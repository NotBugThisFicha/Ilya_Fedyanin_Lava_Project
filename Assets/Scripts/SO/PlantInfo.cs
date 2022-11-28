using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Plant", menuName = "PlantInfo/new PlantInfo")]
public class PlantInfo : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int coolDown;
    [SerializeField] private int colvoXP;

    [SerializeField] private Plant plantGroupe;
    [SerializeField] private Button plantUIButton;

    public Plant PlantGroupe => this.plantGroupe;
    public Button PlantUIButton => this.plantUIButton;

    public void SetPlantPropertys()
    {
        PlantGroupe.name = _name;
        PlantGroupe.Name = _name;
        plantGroupe.timerIncrease = coolDown;
        PlantGroupe.colvoXP = colvoXP;
    }
}
