using UnityEngine;

public class Cube : MonoBehaviour
{
    public GameEvent gameEvent;

    private void OnTriggerEnter(Collider other)
    {
        gameEvent.Raise();
    }
}
