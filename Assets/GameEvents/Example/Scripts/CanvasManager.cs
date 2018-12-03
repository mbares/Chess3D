using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Text text;

    public void ShowRedBallText()
    {
        StopAllCoroutines();
        text.text = "RED ball touched";
        StartCoroutine(ClearText());
    }

    public void ShowBlueBallText()
    {
        StopAllCoroutines();
        text.text = "BLUE ball touched";
        StartCoroutine(ClearText());
    }

    public IEnumerator ClearText()
    {
        yield return new WaitForSeconds(1f);
        text.text = "";
    }
}
