using UnityEngine;

public class ReplaysButton : MonoBehaviour
{
    [SerializeField]
    private GameObject replaysPanel;

    public void ShowReplaysPanel()
    {
        replaysPanel.SetActive(true);
    }

    public void HideReplaysPanel()
    {
        replaysPanel.SetActive(false);
    }
}
