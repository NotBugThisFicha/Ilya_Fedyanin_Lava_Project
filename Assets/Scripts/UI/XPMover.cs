using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPMover : MonoBehaviour
{
    RectTransform wallet = null;

    [SerializeField] private GameObject coinUIPrefab;
    [SerializeField] private Text textCoins;
    [SerializeField] private string namePlant;

    int colvMonetok;
    int colvoM_UI;

    public static int moneyOnRound;

    Vector2 orePos;

    private Camera _camera;

    public Camera GetCamera
    {
        get
        {
            if (_camera == null)
                _camera = Camera.main;
            return _camera;
        }
    }

    private void Awake()
    {
        wallet = gameObject.GetComponent<RectTransform>();
    }
    [SerializeField] float speed = 50;
    protected virtual void Start()
    {
        Plant.PlantFarmedEvent += Add;
    }


    protected void Add(Vector3 startPoint, int colvoMonetok)
    {
        orePos = GetCamera.WorldToScreenPoint(startPoint);
        this.colvMonetok = colvoMonetok;
    }

    float timer = 0.1f;

    void Update()
    {
        textCoins.text = $"{colvoM_UI}";
        if (colvMonetok > 0 && wallet != null)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    StartCoroutine(SpawnCoins());
                    timer = 0.1f;
                }
            }
        }
    }

    IEnumerator SpawnCoins()
    {
        GameObject newCoin = Instantiate(coinUIPrefab, transform);
        newCoin.transform.localScale = Vector3.one;
        newCoin.transform.position = orePos;
        StartCoroutine(MoveCoin(newCoin.GetComponent<RectTransform>(), new Vector3(wallet.position.x, wallet.position.y, wallet.position.z)));
        colvMonetok--;
        yield return null;
    }
    IEnumerator MoveCoin(RectTransform coin, Vector3 target)
    {
        while (true)
        {
            coin.transform.position = Vector2.MoveTowards(coin.position, target, speed * Time.deltaTime);
            yield return new Keyframe();
            if (Vector2.Distance(coin.transform.position, target) <= 0.1f)
            {
                colvoM_UI++;
                moneyOnRound++;
                
                Destroy(coin.gameObject);
                break;
            }
        }
    }

    protected virtual void OnDestroy()
    {
        moneyOnRound = 0;
        Plant.PlantFarmedEvent -= Add;
    }
}
