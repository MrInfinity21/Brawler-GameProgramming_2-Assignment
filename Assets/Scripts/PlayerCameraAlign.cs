using UnityEngine;

public class PlayerCameraAlign : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float moveThreshold = 0.1f;
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(horizontal, 0f, vertical);

        if (input.magnitude > moveThreshold)
        {
            Vector3 forward = cameraTransform.forward;
            forward.y = 0f;

            if (forward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(forward);
            }
        }
    }
}
