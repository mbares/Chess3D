using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FlexibleUIText : FlexibleUI
{
    public enum TextType
    {
        Default,
        Score,
        Title,
        Currency        
    }

    public TextType type;
    public Text text;
    public FlexibleUITextData flexibleUITextData;

    public override void OnSkinUI()
    {
        base.OnSkinUI();

        text = GetComponent<Text>();

        switch (type) {
            case TextType.Default:
                text.font = flexibleUITextData.defaultTextFont;
                break;
            case TextType.Score:
                text.font = flexibleUITextData.scoreTextFont;
                break;
            case TextType.Title:
                text.font = flexibleUITextData.titleTextFont;
                break;
            case TextType.Currency:
                text.font = flexibleUITextData.currencyTextFont;
                break;
        }
    }
}
