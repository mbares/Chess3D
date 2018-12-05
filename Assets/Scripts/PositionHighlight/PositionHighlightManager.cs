using UnityEngine;

[CreateAssetMenu]
public class PositionHighlightManager : ScriptableObject
{
    [SerializeField]
    private GameObject currentPositionHighlightPrefab;
    [SerializeField]
    private GameObject validPositionHighlightPrefab;
    [SerializeField]
    private GameObject invalidPositionHighlightPrefab;

    private PositionHighlight currentPositionHighlight;
    private PositionHighlight validPositionHighlight;
    private PositionHighlight invalidPositionHighlight;

    public void ActivateCurrentPositionHighlight(Vector3 position)
    {
        if (currentPositionHighlight == null) {
            GameObject instantiatedObject = Instantiate(currentPositionHighlightPrefab);
            currentPositionHighlight = instantiatedObject.GetComponent<PositionHighlight>();
        }
        currentPositionHighlight.Activate(position);
    }

    public void DeactivateCurrentPositionHighlight()
    {
        currentPositionHighlight.Deactivate();
    }

    public void ActivateValidPositionHighlight(Vector3 position)
    {
        if (validPositionHighlight == null) {
            GameObject instantiatedObject = Instantiate(validPositionHighlightPrefab);
            validPositionHighlight = instantiatedObject.GetComponent<PositionHighlight>();
        }
        validPositionHighlight.Activate(position);
    }

    public void DeactivateValidPositionHighlight()
    {
        if (validPositionHighlight != null) {
            validPositionHighlight.Deactivate();
        }
    }

    public void ActivateInvalidPositionHighlight(Vector3 position)
    {
        if (invalidPositionHighlight == null) {
            GameObject instantiatedObject = Instantiate(invalidPositionHighlightPrefab);
            invalidPositionHighlight = instantiatedObject.GetComponent<PositionHighlight>();
        }
        invalidPositionHighlight.Activate(position);
    }

    public void DeactivateInvalidPositionHighlight()
    {
        if (invalidPositionHighlight != null) {
            invalidPositionHighlight.Deactivate();
        }
    }

    public void DeactivateAllHighlights()
    {
        if (currentPositionHighlight != null) {
            currentPositionHighlight.Deactivate();
        }
        if (validPositionHighlight != null) {
            validPositionHighlight.Deactivate();
        }
        if (invalidPositionHighlight != null) {
            invalidPositionHighlight.Deactivate();
        }
    }
}
