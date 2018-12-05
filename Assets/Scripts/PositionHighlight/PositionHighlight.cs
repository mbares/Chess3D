using UnityEngine;

public class PositionHighlight : MonoBehaviour
{
    private const float POSITION_Y = 0.01f;

    public void Activate(Vector3 position)
    {
        gameObject.SetActive(true);
        position.y = POSITION_Y;
        transform.position = position;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
