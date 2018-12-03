using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class FlexibleUIButton : FlexibleUI
{
    public enum ButtonType
    {
        Play,
        Retry, 
        Options,
        Back
    }

    public Image image;
    public Button button;
    public ButtonType type;
    public FlexibleUIButtonData flexibleUIButtonData;

    public override void OnSkinUI()
    {
        base.OnSkinUI();

        button.transition = Selectable.Transition.SpriteSwap;
        button.targetGraphic = image;

        image.type = Image.Type.Sliced;

        switch (type) {
            case ButtonType.Play:
                image.sprite = flexibleUIButtonData.playButtonSprite;
                button.spriteState = flexibleUIButtonData.playButtonSpriteState;
                break;
            case ButtonType.Retry:
                image.sprite = flexibleUIButtonData.retryButtonSprite;
                button.spriteState = flexibleUIButtonData.retryButtonSpriteState;
                break;
            case ButtonType.Options:
                image.sprite = flexibleUIButtonData.optionsButtonSprite;
                button.spriteState = flexibleUIButtonData.optionsButtonSpriteState;
                break;
            case ButtonType.Back:
                image.sprite = flexibleUIButtonData.backButtonSprite;
                button.spriteState = flexibleUIButtonData.backButtonSpriteState;
                break;
        }
    }
}
