using UnityEngine;

public class ObjCanvasLookAt : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z));
        transform.Rotate(0, 180, 0);
    }
}
