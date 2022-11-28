using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TweenMover))]
public class ButtonCameraSwitcher : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TweenMover[] buttonsSwitcherSide;
    private TweenMover tweenMoverThis;
    private bool once;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!once)
        {
            foreach (TweenMover button in buttonsSwitcherSide)
                button.gameObject.SetActive(true);

            once = true;
        }
        else
        {
            foreach (TweenMover tweenMover in buttonsSwitcherSide)
                tweenMover.MoveBack();

            tweenMoverThis.MoveBack();
            once = false;
        }
    }

    private void Start()
    {
        tweenMoverThis = GetComponent<TweenMover>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
