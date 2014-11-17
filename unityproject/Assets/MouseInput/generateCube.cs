using UnityEngine;
using System.Collections;

public class generateCube : MonoBehaviour {
	public GameObject spawncube;
	public GameObject[] spawns;
	public Ray ray;
	// Use this for initialization
	void Start () {
//		spawncube = GameObject.Find ("Spawncube");
	}
	
	// Update is called once per frame
	void Update () {
		RightClickToSaveSTL ();
		DrawRayHit ();

	}

	void RightClickToSaveSTL(){
		if (Input.GetMouseButtonDown(1)){
			Debug.Log("Pressed right click.");
			spawns = GameObject.FindGameObjectsWithTag("Spawncube");
			MeshFilter[] filters = new MeshFilter[spawns.Length];
			for (int i = 0; i < spawns.Length; i++){
				filters[i] = spawns[i].GetComponent<MeshFilter>();
			}
			
			//		MeshFilter[] filters = gameObject.GetComponentsInChildren<MeshFilter>();
			STL.ExportBinary( filters );
		}
		}
	void DrawRayHit() {
		if (ray.direction != Vector3.zero)
			Debug.DrawRay(ray.origin, ray.direction * 20, Color.yellow);

	}
	//OnMouseDown is called when the user has pressed the mouse button while over the GUIElement or Collider.
	public void OnMouseDown() {
		Debug.Log ("onmousedown");
		Vector3 mouseScreenPosition = Input.mousePosition;
		//create a ray that goes into the scene from the camera, through the mouse position
		ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
		float depth;
		RaycastHit hitInfo; //create a variable to store information about the object hit (if any)
		
		//Check to see if the ray hits any objects in the scene
		//Also pass in hitInfo, so that Raycast can store the information about the hit there
		//The out keyword is a parameter modifier used to tell C# that this object should be passed by reference, instead of by value
		//basically it makes it so we can properly access hitInfo.
		//It's important to note that the objects we're hoping to hit with our ray must have a collider component attached to them
		if (Physics.Raycast (ray, out hitInfo)) 
		{
			Vector3 hitPosition = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z-.5f);
			//Move this object to the postion we hit.
			Instantiate(spawncube,hitPosition, Quaternion.identity);
//			spawncube.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z-1f);
			
		} else {

		}
	}

}
