using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FlexibleUIProgressBar : FlexibleUI
{
    public FlexibleUIProgressBarData flexibleUIProgressBarData;

    public Image background;
    public Image fill;

    public override void OnSkinUI()
    {
        base.OnSkinUI();

        background.sprite = flexibleUIProgressBarData.backgroundSprite;
        fill.sprite = flexibleUIProgressBarData.fillSprite;
    }
}
