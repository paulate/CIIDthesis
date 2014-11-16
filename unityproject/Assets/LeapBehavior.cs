using UnityEngine;
using System.Collections;
using Leap;

public class LeapBehavior : MonoBehaviour {
	Controller controller;
	public float fingerHeight = 0;
	public float prevFingerHeight;

	void Start ()
	{
		controller = new Controller();
	}
	
	void Update ()
	{
		prevFingerHeight = fingerHeight;
		Frame frame = controller.Frame();

		Pointable pointable = frame.Pointables.Frontmost;
		Vector stabilizedPosition = pointable.StabilizedTipPosition;

//		Debug.Log (stabilizedPosition.y);
		fingerHeight = stabilizedPosition.y;

	}
}
