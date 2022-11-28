using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    private const float YBorder = -0.4f;
    private const float borderXZ = -1;

    [SerializeField] private GameObject groundObj;
    [SerializeField] private GameObject borderObj;

    [SerializeField] private int width;
    [SerializeField] private int height;

    private static int size;
    public static int SizeGround { get { return size; } private set { } }

    public int Width => width;
    public int Height => height;

    private static GroundGenerator instance;
    public static GroundGenerator Instance => instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);

        GenerateGround();
        GenerateBorder();
        size = width * height;
    }

    private void GenerateGround()
    {
        for(int i = 0; i< width; i++)
        {
            for(int j = 0; j< height; j++)
            {
                GameObject ground = Instantiate(groundObj, new Vector3(i, 0, j), Quaternion.identity, transform);
            }
        }
    }

    private void GenerateBorder()
    {
        for (int i = -1; i < width + 1; i++)
        {
            GameObject borderDown = Instantiate(borderObj, new Vector3(i, YBorder, borderXZ), Quaternion.identity, transform);
            GameObject borderUp = Instantiate(borderObj, new Vector3(i, YBorder, height), Quaternion.identity, transform);
        }

        for (int j = -1; j < height + 1; j++)
        {
            GameObject borderLeft = Instantiate(borderObj, new Vector3(borderXZ, YBorder, j), Quaternion.identity, transform);
            GameObject borderRight = Instantiate(borderObj, new Vector3(width, YBorder, j), Quaternion.identity, transform);
        }
    }
}
