using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonChoosePlant : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string plantName;
    public void OnPointerClick(PointerEventData eventData)
    {
        GameController.Instance.SetPlant(plantName);
        TweenMover choosePanel = transform.GetComponentInParent<TweenMover>();
        choosePanel.MoveBack();
    }
}
