using UnityEngine;

public class CompassDirection : MonoBehaviour
{
	public static CompassDirection main;

	void Awake()
	{
		main = this;
	}

}