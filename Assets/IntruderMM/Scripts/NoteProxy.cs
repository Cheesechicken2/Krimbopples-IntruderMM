using UnityEngine;
using System;

public class NoteProxy : MonoBehaviour
{
	[TextAreaAttribute(10, 10)]
	public string message;
	public Activator activatorToActivate; //Activator that activates when you read the note

}