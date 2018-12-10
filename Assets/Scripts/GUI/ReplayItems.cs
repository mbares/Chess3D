using System.Collections.Generic;
using UnityEngine;

public class ReplayItems : MonoBehaviour
{
    [SerializeField]
    private ReplayData replayData;
    [SerializeField]
    private GameObject replayPrefab;
    [SerializeField]
    private ReplaysButton replaysButton;
    [SerializeField]
    private GameEvent playReplayEvent;

    private Dictionary<string, PlayerMoves> allReplays = new Dictionary<string, PlayerMoves>();

    private void OnEnable()
    {
        foreach (KeyValuePair<string, PlayerMoves> item in PlayerMovesSerializer.GetAllReplays()) {
            if (!allReplays.ContainsKey(item.Key)) {
                allReplays.Add(item.Key, item.Value);
                GameObject replayObject = Instantiate(replayPrefab, transform);
                Replay replay = replayObject.GetComponent<Replay>();
                replay.playerMoves = item.Value;
                replay.button.onClick.AddListener(() => replaysButton.HideReplaysPanel());
                replay.button.onClick.AddListener(() => PlayReplay(replay));
                replay.text.text = item.Key;
            }
        }
    }

    public void PlayReplay(Replay replay)
    {
        replayData.playerMoves = replay.playerMoves;
        playReplayEvent.Raise();
    }
}
