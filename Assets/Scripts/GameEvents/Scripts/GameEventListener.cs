using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public EventResponsePair[] eventResponsePairs;

    private void OnEnable()
    {
        for (int i = 0; i < eventResponsePairs.Length; ++i) {
            eventResponsePairs[i].gameEvent.RegisterListener(this);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < eventResponsePairs.Length; ++i) {
            eventResponsePairs[i].gameEvent.UnregisterListener(this);
        }
    }

    public void OnEventRaised(GameEvent gameEvent)
    {
        for (int i = 0; i < eventResponsePairs.Length; ++i) {
            if (gameEvent == eventResponsePairs[i].gameEvent) {
                eventResponsePairs[i].response.Invoke();
            }
        }
    }
}

[System.Serializable]
public class EventResponsePair
{
    public GameEvent gameEvent;
    public UnityEvent response;
}