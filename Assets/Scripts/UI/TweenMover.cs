using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TweenMover : MonoBehaviour
{
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    [SerializeField] private float timeAnim = 1;

    private void OnEnable()
    {
        MoveTo(1);
    }
    private void MoveTo(int module)
    {
        iTween.MoveTo(gameObject, iTween.Hash(
            "x", transform.position.x + (offsetX * module),
            "y", transform.position.y + (offsetY * module), 
            "z", transform.position.z, 
            "time", timeAnim, "ignoretimescale", true,
            "easyType", iTween.EaseType.easeInOutBounce));
    }

    public async void MoveBack()
    {
        MoveTo(-1);
        await Task.Delay((int)timeAnim * 1000);
        gameObject.SetActive(false);
    }
}
