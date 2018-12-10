using UnityEngine;

public class ReplaysButton : MonoBehaviour
{
    [SerializeField]
    private GameObject replaysPanel;
    [SerializeField]
    private GameEvent guiShown;
    [SerializeField]
    private GameEvent guiHidden;

    public void ShowReplaysPanel()
    {
        replaysPanel.SetActive(true);
        guiShown.Raise();
    }

    public void HideReplaysPanel()
    {
        replaysPanel.SetActive(false);
        guiHidden.Raise();
    }
}
