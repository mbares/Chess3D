using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChessboardPiecesLayout))]
public class ChessboardLayoutCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ChessboardPiecesLayout chessboardPiecesLayout = (ChessboardPiecesLayout)target;
        EditorGUILayout.Space();

        EditorGUI.indentLevel = 0;

        GUIStyle tableStyle = new GUIStyle("box");
        tableStyle.padding = new RectOffset(10, 10, 10, 10);
        tableStyle.margin.left = 32;

        GUIStyle headerColumnStyle = new GUIStyle();
        headerColumnStyle.fixedWidth = 35;

        GUIStyle columnStyle = new GUIStyle();
        columnStyle.fixedWidth = 65;

        GUIStyle rowStyle = new GUIStyle();
        rowStyle.fixedHeight = 25;

        GUIStyle rowHeaderStyle = new GUIStyle();
        rowHeaderStyle.fixedWidth = columnStyle.fixedWidth - 1;

        GUIStyle columnHeaderStyle = new GUIStyle();
        columnHeaderStyle.fixedWidth = 30;
        columnHeaderStyle.fixedHeight = 25.5f;

        GUIStyle columnLabelStyle = new GUIStyle();
        columnLabelStyle.fixedWidth = rowHeaderStyle.fixedWidth - 6;
        columnLabelStyle.alignment = TextAnchor.MiddleCenter;
        columnLabelStyle.fontStyle = FontStyle.Bold;

        GUIStyle cornerLabelStyle = new GUIStyle();
        cornerLabelStyle.fixedWidth = 42;
        cornerLabelStyle.alignment = TextAnchor.MiddleRight;
        cornerLabelStyle.fontStyle = FontStyle.BoldAndItalic;
        cornerLabelStyle.fontSize = 14;
        cornerLabelStyle.padding.top = -5;

        GUIStyle rowLabelStyle = new GUIStyle();
        rowLabelStyle.fixedWidth = 25;
        rowLabelStyle.alignment = TextAnchor.MiddleRight;
        rowLabelStyle.fontStyle = FontStyle.Bold;

        GUIStyle enumStyle = new GUIStyle("popup");
        rowStyle.fixedWidth = 65;

        EditorGUILayout.BeginHorizontal(tableStyle);
        for (int i = -1; i < 8; i++) {
            EditorGUILayout.BeginVertical((i == 8) ? headerColumnStyle : columnStyle);
            for (int j = 8; j > -2; j--) {
                if (i == -1 && j > 0) {
                    EditorGUILayout.BeginVertical(columnHeaderStyle);
                    EditorGUILayout.LabelField(j.ToString(), rowLabelStyle);
                    EditorGUILayout.EndHorizontal();
                } else if (j == -1 && i >= 0) {
                    EditorGUILayout.BeginVertical(rowHeaderStyle);
                    EditorGUILayout.LabelField(ChessboardPosition.columnLabels[i], columnLabelStyle);
                    EditorGUILayout.EndHorizontal();
                }

                if (i >= 0 && j >= 0 && j < 8) {
                    EditorGUILayout.BeginHorizontal(rowStyle);
                    chessboardPiecesLayout.chessboardSquaresInfo[i].row[j] = (PieceLabel)EditorGUILayout.EnumPopup(chessboardPiecesLayout.chessboardSquaresInfo[i].row[j], enumStyle);
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();

        if (GUI.changed) {
            Undo.RecordObject(chessboardPiecesLayout, "Edit layout");
            EditorUtility.SetDirty(chessboardPiecesLayout);
        }
    }
}
