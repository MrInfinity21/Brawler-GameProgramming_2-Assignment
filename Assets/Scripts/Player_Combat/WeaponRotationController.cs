using UnityEngine;

public class WeaponRotationController : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main == null) return;

        float cameraYaw = Camera.main.transform.eulerAngles.y;

        transform.rotation = Quaternion.Euler(0f, cameraYaw, 0f);
    }
}
