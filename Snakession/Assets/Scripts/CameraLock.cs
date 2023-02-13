using UnityEngine;

public class CameraLock : MonoBehaviour
{
    [SerializeField] Camera cam;

    void LateUpdate()
    {
		//Make camera follow this object
        cam.transform.position = new Vector3(transform.position.x, transform.position.y,-10);
    }
}