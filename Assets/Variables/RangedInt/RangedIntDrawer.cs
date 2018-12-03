using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RangedIntData), true)]
public class RangedIntDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);

        float leftPadding = position.xMin;

        position = EditorGUI.PrefixLabel(position, label);

        SerializedProperty minProp = property.FindPropertyRelative("minValue");
        SerializedProperty maxProp = property.FindPropertyRelative("maxValue");

        float minValue = minProp.intValue;
        float maxValue = maxProp.intValue;

        SerializedProperty rangeMinProp = property.FindPropertyRelative("rangeMin");
        SerializedProperty rangeMaxProp = property.FindPropertyRelative("rangeMax");

        int rangeMin = rangeMinProp.intValue;
        int rangeMax = rangeMaxProp.intValue;

        const float rangeBoundsLabelWidth = 40f;
        const float rangeBoundLabelHeight = 20f;
        const float padding = 10f;

        var sliderPosition = position;
        sliderPosition.yMin += rangeBoundLabelHeight * 3 + padding;

        var rangeMinPosition = position;
        rangeMinPosition.yMin += rangeBoundLabelHeight * 2;
        rangeMinPosition.width = rangeBoundsLabelWidth;
        rangeMinPosition.height = rangeBoundLabelHeight;

        var rangeMaxPosition = position;
        rangeMaxPosition.yMin += rangeBoundLabelHeight * 2;
        rangeMaxPosition.height = rangeBoundLabelHeight;
        rangeMaxPosition.xMin += rangeBoundsLabelWidth;
        rangeMaxPosition.xMax = rangeMaxPosition.xMin + rangeBoundsLabelWidth;

        var rangeBoundsField1Rect = new Rect(sliderPosition);
        rangeBoundsField1Rect.width = rangeBoundsLabelWidth;
        rangeBoundsField1Rect.height = rangeBoundLabelHeight;

        sliderPosition.xMin += rangeBoundsLabelWidth;
        var rangeBoundsField2Rect = new Rect(sliderPosition);
        rangeBoundsField2Rect.xMin = rangeBoundsField2Rect.xMax - rangeBoundsLabelWidth;
        rangeBoundsField2Rect.height = rangeBoundLabelHeight;
        sliderPosition.xMax -= rangeBoundsLabelWidth;
        sliderPosition.height = rangeBoundLabelHeight;

        int lastMinValue = (int)minValue;
        int lastMaxValue = (int)maxValue;

        int lastRangeMin = rangeMin;
        int lastRangeMax = rangeMax;

        EditorGUI.BeginChangeCheck();
        EditorGUI.MinMaxSlider(sliderPosition, ref minValue, ref maxValue, rangeMin, rangeMax);
        minValue = EditorGUI.IntField(rangeBoundsField1Rect, (int)minValue);
        maxValue = EditorGUI.IntField(rangeBoundsField2Rect, (int)maxValue);
        rangeMin = EditorGUI.IntField(rangeMinPosition, rangeMin);
        rangeMax = EditorGUI.IntField(rangeMaxPosition, rangeMax);
        if (EditorGUI.EndChangeCheck()) {
            if (minValue > maxValue) {
                minValue = lastMinValue;
            }
            if (maxValue < minValue) {
                maxValue = lastMaxValue;
            }
            minProp.intValue = (int)minValue;
            maxProp.intValue = (int)maxValue;

            if (rangeMin > rangeMax) {
                rangeMin = lastRangeMin;
            }
            if (rangeMax < rangeMin) {
                rangeMax = lastRangeMax;
            }
            rangeMinProp.intValue = rangeMin;
            rangeMaxProp.intValue = rangeMax;
        }

        rangeMinPosition.xMin = leftPadding;
        EditorGUI.LabelField(rangeMinPosition, new GUIContent("Range limits"));

        rangeBoundsField1Rect.xMin = leftPadding;
        EditorGUI.LabelField(rangeBoundsField1Rect, new GUIContent("Range"));

        EditorGUI.EndProperty();
    }
}
