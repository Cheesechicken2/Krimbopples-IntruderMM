#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Assets.IntruderMM.Editor
{
	[CustomEditor(typeof(GoalPointProxy)), CanEditMultipleObjects]
	public class GoalPointProxyEditor : UnityEditor.Editor
	{

		GoalPointProxy goalPointTarget;
		List<BriefcaseProxy> briefcases = new List<BriefcaseProxy>();

		private void OnEnable()
		{
			goalPointTarget = (GoalPointProxy)target;
			foreach (BriefcaseProxy item in GameObject.FindObjectsOfType<BriefcaseProxy>())
			{
				briefcases.Add(item);
			}
		}

		private void OnDisable()
		{
			briefcases.Clear();
		}

		private void OnSceneGUI()
		{
			Color color = Color.magenta;

			if (Preferences.renderLines)
			{
				for (int i = 0; i < 20; i++)
				{
					color.a = Preferences.alphaLines;
					Handles.color = color;
					Handles.DrawWireDisc(goalPointTarget.transform.position - new Vector3(0, 2.33252f, 0) + (new Vector3(0, 0.25f * 1, 0) * i), goalPointTarget.transform.up, 1);
				}
			}

			foreach (BriefcaseProxy item in briefcases)
			{
				EditorTools.RenderPoints(goalPointTarget.gameObject, item.gameObject, Color.cyan, "", 1);
			}
		}
	}

}

#endif