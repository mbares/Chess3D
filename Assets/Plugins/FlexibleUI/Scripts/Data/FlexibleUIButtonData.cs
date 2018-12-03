using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ButtonData", menuName = "Flexible UI Data/Flexible UI Button Data", order = 0)]
public class FlexibleUIButtonData : FlexibleUIData
{
    [Header("Play Button")]
    public Sprite playButtonSprite;
    public SpriteState playButtonSpriteState;

    [Header("Retry Button")]
    public Sprite retryButtonSprite;
    public SpriteState retryButtonSpriteState;

    [Header("Options Button")]
    public Sprite optionsButtonSprite;
    public SpriteState optionsButtonSpriteState;

    [Header("Back Button")]
    public Sprite backButtonSprite;
    public SpriteState backButtonSpriteState;
}
