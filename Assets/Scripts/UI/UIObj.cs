using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIObj : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI fillText;
    private float timer;
    private float timerClone;
    private bool isStartTimer;

    private void Start()
    {
        if (fillImage == null || fillText == null)
            throw new System.InvalidOperationException($"{nameof(UIObj)} script error, fillImage or fillText is null");
    }
    void Update()
    {
        if (isStartTimer && timerClone < timer)
        {
            timerClone += Time.deltaTime;
            fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, timerClone / timer, timer / timerClone);
            fillText.text = $"{Mathf.RoundToInt(timerClone)}";
        }
    }

    public void SetTimer(float value)
    {
        timer = value;
        timerClone = 0;
        isStartTimer = true;
    }
}
