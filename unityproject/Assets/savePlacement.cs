using UnityEngine;
using System.Collections;

public class savePlacement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("up")) {
			Debug.Log ("Pressed up. Saving Placement.");

			//find the frameMarkers
			GameObject[] frameMarkers = GameObject.FindGameObjectsWithTag("primitive2d");
			foreach (GameObject frameMarker in frameMarkers){
				GameObject shape = frameMarker.transform.GetChild(0).gameObject; //get the shape in each frameMarker 
				Renderer renderer = shape.transform.GetComponentInChildren<Renderer>(); //get the renderer,
				if (renderer.enabled == true) {											//check to see if the frameMarker is in the playing field
					GameObject planeFrame = new GameObject("planeFrame");				//create new plane.. oo?
					
					planeFrame.transform.position = frameMarker.transform.position;
					planeFrame.transform.rotation = frameMarker.transform.rotation;
					planeFrame.transform.localScale = frameMarker.transform.localScale;
					
					GameObject ARcam = GameObject.Find ("ARCamera");
					GameObject clone = Instantiate(shape) as GameObject;
					clone.transform.parent = planeFrame.transform;
					planeFrame.transform.parent=ARcam.transform;
				}
			}
		}


		if (Input.GetKey ("down")) {
			//		MeshFilter[] filters = GameObject.Find ("Block01")
			MeshFilter[] filters = GetComponentsInChildren<MeshFilter> ();
						Debug.Log ("Pressed down.");

			STL.ExportBinary (filters);
		}
	}
}
