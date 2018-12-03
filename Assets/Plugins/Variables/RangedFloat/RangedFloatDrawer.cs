using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RangedFloatData), true)]
public class RangedFloatDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);

        float leftPadding = position.xMin;

        position = EditorGUI.PrefixLabel(position, label);

        SerializedProperty minProp = property.FindPropertyRelative("minValue");
        SerializedProperty maxProp = property.FindPropertyRelative("maxValue");

        float minValue = minProp.floatValue;
        float maxValue = maxProp.floatValue;

        SerializedProperty rangeMinProp = property.FindPropertyRelative("rangeMin");
        SerializedProperty rangeMaxProp = property.FindPropertyRelative("rangeMax");

        float rangeMin = rangeMinProp.floatValue;
        float rangeMax = rangeMaxProp.floatValue;

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

        float lastMinValue = minValue;
        float lastMaxValue = maxValue;

        float lastRangeMin = rangeMin;
        float lastRangeMax = rangeMax;

        EditorGUI.BeginChangeCheck();
        EditorGUI.MinMaxSlider(sliderPosition, ref minValue, ref maxValue, rangeMin, rangeMax);
        minValue = EditorGUI.FloatField(rangeBoundsField1Rect, float.Parse(minValue.ToString("F2")));
        maxValue = EditorGUI.FloatField(rangeBoundsField2Rect, float.Parse(maxValue.ToString("F2")));
        rangeMin = EditorGUI.FloatField(rangeMinPosition, float.Parse(rangeMin.ToString("F2")));
        rangeMax = EditorGUI.FloatField(rangeMaxPosition, float.Parse(rangeMax.ToString("F2")));
        if (EditorGUI.EndChangeCheck()) {
            if (minValue > maxValue) {
                minValue = lastMinValue;
            }
            if (maxValue < minValue) {
                maxValue = lastMaxValue;
            }
            minProp.floatValue = minValue;
            maxProp.floatValue = maxValue;

            if (rangeMin > rangeMax) {
                rangeMin = lastRangeMin;
            }
            if (rangeMax < rangeMin) {
                rangeMax = lastRangeMax;
            }
            rangeMinProp.floatValue = rangeMin;
            rangeMaxProp.floatValue = rangeMax;
        }

        rangeMinPosition.xMin = leftPadding;
        EditorGUI.LabelField(rangeMinPosition, new GUIContent("Range limits"));

        rangeBoundsField1Rect.xMin = leftPadding;
        EditorGUI.LabelField(rangeBoundsField1Rect, new GUIContent("Range"));

        EditorGUI.EndProperty();
    }
}