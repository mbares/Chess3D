using System.Collections.Generic;
using UnityEngine;

public class ReplayItems : MonoBehaviour
{
    [SerializeField]
    private ReplayManager replayManager;
    [SerializeField]
    private GameObject replayPrefab;

    private Dictionary<string, PlayerMoves> allReplays;

    private void Start()
    {
        allReplays = PlayerMovesSerializer.GetAllReplays();
        foreach (KeyValuePair<string, PlayerMoves> item in allReplays) {
            GameObject replayObject = Instantiate(replayPrefab, transform);
            Replay replay = replayObject.GetComponent<Replay>();
            replay.playerMoves = item.Value;
            replay.button.onClick.AddListener(() => replayManager.PlayReplay(replay));
            replay.text.text = item.Key;
        }
    }
}
