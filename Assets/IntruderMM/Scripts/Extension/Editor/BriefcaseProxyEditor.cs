#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Assets.IntruderMM.Editor
{
	[CustomEditor(typeof(BriefcaseProxy)), CanEditMultipleObjects]
	public class BriefcaseProxyEditor : UnityEditor.Editor
	{
		BriefcaseProxy briefcaseTarget;
		List<GoalPointProxy> goalPoints = new List<GoalPointProxy>();

		private void OnEnable()
		{
			briefcaseTarget = (BriefcaseProxy)target;

			foreach (GoalPointProxy item in GameObject.FindObjectsOfType<GoalPointProxy>())
			{
				goalPoints.Add(item);
			}
		}

		private void OnSceneGUI()
		{
			foreach (GoalPointProxy item in goalPoints)
			{
				EditorTools.RenderPoints(briefcaseTarget.gameObject, item.gameObject, Color.cyan, "", 1);
			}
		}
	}
}

#endif