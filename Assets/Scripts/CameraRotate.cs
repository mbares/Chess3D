using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    private Vector3 originalPosition;
    [SerializeField]
    private Vector3 originalRotation;
    [SerializeField]
    private Vector3 rotatedPosition;
    [SerializeField]
    private Vector3 rotatedRotation;

    private bool rotated = false;

    public void Reset()
    {
        transform.position = originalPosition;
        transform.eulerAngles = originalRotation;
        rotated = false;
    }

    public void Rotate()
    {
        if (gameManager.IsPlayerInteractionAllowed()) {
            if (rotated) {
                transform.position = originalPosition;
                transform.eulerAngles = originalRotation;
                rotated = false;
            } else {
                transform.position = rotatedPosition;
                transform.eulerAngles = rotatedRotation;
                rotated = true;
            }
        }
    }
}
