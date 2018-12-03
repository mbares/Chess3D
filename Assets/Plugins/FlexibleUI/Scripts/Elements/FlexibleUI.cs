using UnityEngine;

[ExecuteInEditMode]
public class FlexibleUI : MonoBehaviour
{
    public virtual void OnSkinUI() { }

    public virtual void Awake()
    {
        if (Application.isPlaying) {
            OnSkinUI();
        }
    }
}
