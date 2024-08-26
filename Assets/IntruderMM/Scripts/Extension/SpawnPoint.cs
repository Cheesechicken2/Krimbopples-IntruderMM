#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Assets.IntruderMM.Editor
{
	public class SpawnPoint : MonoBehaviour
	{
		private bool isDirty;

		private Mesh gizmoMesh;
		private Quaternion gizmoMeshRotation;

		private Color meshColor;
		private Color wireColor;

		private Transform cachedParent;

		private void OnDrawGizmos()
		{
			if (cachedParent != transform.parent)
			{
				isDirty = true;
				if (transform.parent != null) cachedParent = transform.parent;
				else cachedParent = null;
			}

			// Set up spawn model
			if (gizmoMesh == null || isDirty)
			{
				if (transform.parent != null)
				{
					if (transform.parent.name == "SpawnA") // If spawnpoint is for guards
					{
						gizmoMesh = (Mesh)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Scripts/Extension/Models/mdl_Guard.fbx", typeof(Mesh));
						meshColor = new Color(0.525f, 0.768f, 0.894f, 0.25f);
						wireColor = new Color(0.525f, 0.768f, 0.894f, 0.025f);
					}
					else if (transform.parent.name == "SpawnB") // if spawnpoint is for intruder
					{
						gizmoMesh = (Mesh)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Scripts/Extension/Models/mdl_Intruder.fbx", typeof(Mesh));
						meshColor = new Color(0.784f, 0.560f, 0.839f, 0.25f);
						wireColor = new Color(0.784f, 0.560f, 0.839f, 0.025f);
					}
				}
			}

			// Sets up mesh rotations
			if (gizmoMeshRotation != transform.rotation * Quaternion.Euler(90, 0, 0)) gizmoMeshRotation = transform.rotation * Quaternion.Euler(90, 0, 0);

			// Wireframe
			Gizmos.color = wireColor;
			Gizmos.DrawWireMesh(gizmoMesh, 0, transform.position, gizmoMeshRotation, transform.localScale);

			// Mesh
			Gizmos.color = meshColor;
			Gizmos.DrawMesh(gizmoMesh, 0, transform.position, gizmoMeshRotation, transform.localScale);
		}
	}
}
#endif