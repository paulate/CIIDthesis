using UnityEngine;
using System.Collections;

public class extrusionCube : MonoBehaviour {

	public Vector3 initMouseScreenPos;
	public float newHeight; 
	public Vector3 mouseScreenPosition;
	public float initHeight;
	public bool isScaling = false;
	public float scaleFactor = 0.1f;
	public float oldHeight = 1;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		mouseScreenPosition = Input.mousePosition;
		if (Input.GetMouseButtonDown (0)) {
			initMouseScreenPos = Input.mousePosition;

			toggleScaling ();
				}
		Debug.Log (mouseScreenPosition.y);
		if (isScaling) {

			newHeight = (mouseScreenPosition.y - initMouseScreenPos.y)*scaleFactor;
			this.transform.localScale = new Vector3(1,newHeight+oldHeight,1);
				}

	}
	
	void toggleScaling() {
		if (isScaling)
			oldHeight += newHeight;
		isScaling = !isScaling;
		Debug.Log(isScaling+" isScaling");
	}
}


