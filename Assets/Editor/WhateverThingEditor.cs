using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WhateverThing))]
public class WhateverThingEditor : Editor
{
	WhateverThing we;
	int idx;

	public void OnEnable()
	{
		we = (WhateverThing)target;
		idx = Undo.GetCurrentGroup();
	}

	public void OnDisable()
	{
		Undo.CollapseUndoOperations(idx);
	}

	public override void OnInspectorGUI()
	{
		we.intValue = EditorGUILayoutCustomUndoField("Int Value", we.intValue);
		we.floatValue = EditorGUILayoutCustomUndoField("Float Value", we.floatValue);
		we.doubleValue = EditorGUILayoutCustomUndoField("Double Value", we.doubleValue);
		we.stringValue = EditorGUILayoutCustomUndoField("String Value", we.stringValue);
		we.boolValue = EditorGUILayoutCustomUndoField("Bool Value", we.boolValue);
		we.vector2Value = EditorGUILayoutCustomUndoField("Vector2 Value", we.vector2Value);
		we.vector3Value = EditorGUILayoutCustomUndoField("Vector3 Value", we.vector3Value);
		we.vector4Value = EditorGUILayoutCustomUndoField("Vector4 Value", we.vector4Value);
		we.rectValue = EditorGUILayoutCustomUndoField("Rect Value", we.rectValue);
		we.colorValue = EditorGUILayoutCustomUndoField("Color Value", we.colorValue);
		we.curveValue = EditorGUILayoutCustomUndoField("Curve Value", we.curveValue);

		we.whateverThingValue = EditorGUILayoutCustomUndoField("WhateverThing Value", we.whateverThingValue);
	}

	T EditorGUILayoutCustomUndoField<T>(string label, T fieldValue)
	{
		object x = System.Convert.ChangeType(fieldValue, typeof(T));

		if (typeof(T) == typeof(int))
		{
			x = EditorGUILayout.IntField(label, (int)x);
		}
		else if (typeof(T) == typeof(float))
		{
			x = EditorGUILayout.FloatField(label, (float)x);
		}
		else if (typeof(T) == typeof(double))
		{
			x = EditorGUILayout.DoubleField(label, (double)x);
		}
		else if (typeof(T) == typeof(string))
		{
			x = EditorGUILayout.TextField(label, (string)x);
		}
		else if (typeof(T) == typeof(bool))
		{
			x = EditorGUILayout.ToggleLeft(label, (bool)x);
		}
		else if (typeof(T) == typeof(Vector2))
		{
			x = EditorGUILayout.Vector2Field(label, (Vector2)x);
		}
		else if (typeof(T) == typeof(Vector3))
		{
			x = EditorGUILayout.Vector3Field(label, (Vector3)x);
		}
		else if (typeof(T) == typeof(Vector4))
		{
			x = EditorGUILayout.Vector4Field(label, (Vector4)x);
		}
		else if (typeof(T) == typeof(Rect))
		{
			x = EditorGUILayout.RectField(label, (Rect)x);
		}
		else if (typeof(T) == typeof(Color))
		{
			x = EditorGUILayout.ColorField(label, (Color)x);
		}
		else if (typeof(T) == typeof(AnimationCurve))
		{
			//this does not undo just setting from the curve palette, have to adjust keys
			//to trigger change tracking
			x = EditorGUILayout.CurveField(label, (AnimationCurve)x);
		}
		else
		{
			x = EditorGUILayout.ObjectField(label, (Object)x, typeof(T), true);
		}

		if (GUI.changed)
		{
			Undo.RecordObject(target, "Changed " + label);
			GUI.changed = false;
		}

		return (T)System.Convert.ChangeType(x, typeof(T));
	}
}
