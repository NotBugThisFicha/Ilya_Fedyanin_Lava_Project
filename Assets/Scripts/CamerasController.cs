using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamerasController : MonoBehaviour
{
    private const int priorityActivSideCamera = 6;

    [SerializeField] private CinemachineVirtualCamera[] sidesCameras;
    [SerializeField] private float heightCameras;
    private GroundGenerator generator;
    private int[] priorityCameras = new int[4];
    private int currentCameraID;

    // Start is called before the first frame update
    void Start()
    {
        generator = GroundGenerator.Instance;
        sidesCameras[0].transform.position = new Vector3(-1, heightCameras, -1);
        sidesCameras[1].transform.position = new Vector3(generator.Width, heightCameras, -1);
        sidesCameras[2].transform.position = new Vector3(generator.Width, heightCameras, generator.Height);
        sidesCameras[3].transform.position = new Vector3(-1, heightCameras, generator.Height);

        int i = 0;
        foreach(CinemachineVirtualCamera camera in sidesCameras)
        {
            priorityCameras[i] = camera.Priority;
            i++;
        }
            
    }

    public void SwithSideCamera(string side)
    {
        sidesCameras[currentCameraID].Priority = priorityCameras[currentCameraID];

        if (side == "Right")
            currentCameraID++;
        else currentCameraID--;

        if (currentCameraID < 0)
            currentCameraID = 3;

        if (currentCameraID > 3)
            currentCameraID = 0;

        sidesCameras[currentCameraID].Priority = priorityActivSideCamera;
    }
}
