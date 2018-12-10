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

    private Dictionary<string, PlayerMoves> allReplays;

    private void Start()
    {
        allReplays = PlayerMovesSerializer.GetAllReplays();
        foreach (KeyValuePair<string, PlayerMoves> item in allReplays) {
            GameObject replayObject = Instantiate(replayPrefab, transform);
            Replay replay = replayObject.GetComponent<Replay>();
            replay.playerMoves = item.Value;
            replay.button.onClick.AddListener(() => replaysButton.HideReplaysPanel());
            replay.button.onClick.AddListener(() => PlayReplay(replay));
            replay.text.text = item.Key;
        }
    }

    public void PlayReplay(Replay replay)
    {
        replayData.playerMoves = replay.playerMoves;
        playReplayEvent.Raise();

    }
}
