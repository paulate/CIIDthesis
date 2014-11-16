using UnityEngine;
using System.Collections;

public class extrusionCubeLeap : MonoBehaviour {
	
	public float newHeight; 
	public float initHeight;
	public bool isScaling = false;
	public float scaleFactor = 0.01f;
	public float oldHeight = 0.1f;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject go = GameObject.Find("LeapManager");
		LeapBehavior lb = go.GetComponent<LeapBehavior>();
		float fingerHeight = lb.fingerHeight;
		float prevFingerHeight = lb.prevFingerHeight;

//		Debug.Log(fingerHeight);

		if ((fingerHeight == 0)&&(prevFingerHeight != 0)) { 
			oldHeight = newHeight;
			isScaling = false;
		}
		else if ((fingerHeight != 0)&&(prevFingerHeight == 0)) {
			isScaling = true;
		}
	
		//Debug.Log (mouseScreenPosition.y);
		if (isScaling) {

			newHeight = fingerHeight*scaleFactor;//(mouseScreenPosition.y - initMouseScreenPos.y)*scaleFactor;
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




