using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FlexibleUIPanel : FlexibleUI
{
    public enum PanelType
    {
        Default,
        Button      
    }

    public PanelType type;
    public FlexibleUIPanelData flexibleUIPanelData;

    public Image image;

    public override void OnSkinUI()
    {
        base.OnSkinUI();

        switch (type) { 
            case PanelType.Default:
                image.sprite = flexibleUIPanelData.defaultPanelSprite;
                break;
            case PanelType.Button:
                image.sprite = flexibleUIPanelData.buttonPanelSprite;
                break;
        }
    }
}
