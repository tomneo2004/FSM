using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NP.FiniteStateMachine; 
using NP.Util;

namespace NP.NPEditor.FSMEditor{

	[CustomEditor(typeof(NP.FiniteStateMachine.FSM), isFallback = true)]
	[CanEditMultipleObjects]
	public class FSMEditor : Editor {

		GUIStyle stateOnStyle = new GUIStyle ();
		GUIStyle stateOffStyle = new GUIStyle ();

		public override void OnInspectorGUI(){

			FSM machine = target as FSM;
			SerializedProperty machineName = serializedObject.FindProperty("machineName");
			SerializedProperty stateStash = serializedObject.FindProperty ("states");

			serializedObject.Update ();

			//name
			machineName.stringValue = EditorGUILayout.TextField ("Name", machineName.stringValue);

			//state stash
			EditorGUILayout.LabelField ("State stash");
			EditorGUI.indentLevel++;
			EditorGUILayout.TextField ("Stash size", stateStash.arraySize.ToString());
			for (int i = 0; i < stateStash.arraySize; i++) {

				//EditorGUILayout.ObjectField (stateStash.GetArrayElementAtIndex (i));
				EditorGUILayout.ObjectField(stateStash.GetArrayElementAtIndex(i).objectReferenceValue, typeof(FSMState), false);
			}
			EditorGUI.indentLevel--;

			//state queue
			FSMState[] stateQTA = new FSMState[machine.StateQueue.Count];
			machine.StateQueue.CopyTo(stateQTA, 0);

			stateOnStyle.normal.background = TextureUtil.Texture2DSolidColor(new Vector2Int(2,2), Color.green);
			stateOnStyle.alignment = TextAnchor.MiddleCenter;
			stateOnStyle.fontStyle = FontStyle.Bold;

			stateOffStyle.normal.background = TextureUtil.Texture2DSolidColor(new Vector2Int(2,2), Color.red);
			stateOffStyle.alignment = TextAnchor.MiddleCenter;
			stateOffStyle.fontStyle = FontStyle.Bold;

			EditorGUILayout.LabelField ("State queue");
			EditorGUI.indentLevel++;
			EditorGUILayout.TextField ("Size", stateQTA.Length.ToString());

			for (int i = 0; i < stateQTA.Length; i++) {

				EditorGUILayout.ObjectField (stateQTA [i] as Object, typeof(FSMState), false);

				if (machine.CurrentState == stateQTA [i]) {

					EditorGUILayout.LabelField ("Running", stateOnStyle);

				} else {

					EditorGUILayout.LabelField ("Off",stateOffStyle);
				}
					
			}
			EditorGUI.indentLevel--;

			serializedObject.ApplyModifiedProperties ();
		}

	}
}
