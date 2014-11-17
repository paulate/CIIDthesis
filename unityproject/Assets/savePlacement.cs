using UnityEngine;
using System.Collections;

public class savePlacement : MonoBehaviour {
	public Ray ray;
//	public GameObject globalFrame = GameObject.Find ("globalFrameTest");
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
					GameObject planeFrame = new GameObject("planeFrame");				//create new plane which isn't being tracked


					GameObject globalFrame = GameObject.Find ("globalFrameTest");
					GameObject clone = Instantiate(shape) as GameObject;
					clone.transform.parent = planeFrame.transform;

					RaycastHit hitInfo;
					LayerMask layerMask = 1 << 8 ; // only want the plane
					if (Physics.Raycast (Camera.main.transform.position,(shape.transform.position-Camera.main.transform.position),out hitInfo, 5000,layerMask)){
						planeFrame.transform.position = hitInfo.point;
					}
					else {
						planeFrame.transform.localPosition = frameMarker.transform.position;	
					}	//copy position, rotation, and scale
					planeFrame.transform.rotation = frameMarker.transform.rotation;
					planeFrame.transform.localScale = frameMarker.transform.localScale;

					planeFrame.transform.parent=globalFrame.transform;

					//
//					ray = Camera.main.ScreenPointToRay(frameMarker.transform.position);

				}
			}
		}


		if (Input.GetKey ("down")) {
			//		MeshFilter[] filters = GameObject.Find ("Block01")
			MeshFilter[] filters = GetComponentsInChildren<MeshFilter> ();
						Debug.Log ("Pressed down.");

			STL.ExportBinary (filters);
		}
		DrawRayHit ();
	}

	void DrawRayHit() {
		GameObject shape = GameObject.Find("FrameMarker0").transform.GetChild (0).transform.GetChild (0).gameObject;
		RaycastHit hitInfo;
		LayerMask layermask = 1 << 8;
		if(Physics.Raycast (Camera.main.transform.position,-( Camera.main.transform.position-shape.transform.position), out hitInfo, 20, layermask)){
			Vector3 hitPosition = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
			Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.red, .01f, true);
		}
//		Vector3 screenPos = Camera.main.WorldToScreenPoint (frameMarker.transform.localPosition);
//		ray = Camera.main.ScreenPointToRay(frameMarker.transform.localPosition);
		//Debug.DrawRay (Camera.main.transform.position,-( Camera.main.transform.position-shape.transform.position)*20, Color.red);
	
	}

}



