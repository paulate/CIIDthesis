using UnityEngine;
using System.Collections;

public class MouseInput : MonoBehaviour {

	public float depthIntoScene = 10;

	float defaultDepthIntoScene = 5;
	float selectScale = .3f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		MoveToMouseAtSpecifiedDepth(depthIntoScene);
		//MoveToMouseAtObjectDepth();
	}

	void MoveToMouseAtObjectDepth() {
		Vector3 mouseScreenPosition = Input.mousePosition;

		//create a ray that goes into the scene from the camera, through the mouse position
		Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);

		float depth;
		RaycastHit hitInfo; //create a variable to store information about the object hit (if any)
		
		//Check to see if the ray hits any objects in the scene
		//Also pass in hitInfo, so that Raycast can store the information about the hit there
		//The out keyword is a parameter modifier used to tell C# that this object should be passed by reference, instead of by value
		//basically it makes it so we can properly access hitInfo.
		//It's important to note that the objects we're hoping to hit with our ray must have a collider component attached to them
		if (Physics.Raycast (ray, out hitInfo)) 
		{
			//Move this object to the postion we hit.
			this.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
			
		} else {
			//if we didn't hit anything, set the depth to the arbitrary depth
			depth = depthIntoScene;
			//now we can reuse our previous code to position the object using the depth we defined here
			MoveToMouseAtSpecifiedDepth(depth);
		}
	}

	void MoveToMouseAtSpecifiedDepth(float depth) {
		Vector3 mouseScreenPosition = Input.mousePosition;
		mouseScreenPosition.z = depth;
		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
		this.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, mouseWorldPosition.z);
	}
	
	//OnMouseDown is called when the user has pressed the mouse button while over the GUIElement or Collider.
	public void OnMouseDown() {
		Debug.Log ("onmousedown");
		//Get the vector from the camera to the object
		Vector3 headingToObject = this.transform.position - Camera.main.transform.position;
		//find the projection on the forward vector of the camera
		depthIntoScene = Vector3.Dot(headingToObject, Camera.main.transform.forward);
	}
	
	//OnMouseDrag is called when the user has clicked on a GUIElement or Collider and is still holding down the mouse.
	public void OnMouseDrag() {
		//when the mouse button is held and we move the mouse, move the object along with it
		//this provides simple click and drag functionality
		MoveToMouseAtSpecifiedDepth(depthIntoScene);
	}
	
	//OnMouseEnter is called when the mouse entered the GUIElement or Collider.
	public void OnMouseEnter() {
		//change the scale of the object to make it clear it's been selected
		this.transform.localScale += new Vector3(selectScale,selectScale,selectScale);
	}
	
	//OnMouseExit is called when the mouse is not any longer over the GUIElement or Collider.
	public void OnMouseExit() {
		//reset the scale to default when the object is no longer selected
		this.transform.localScale -= new Vector3(selectScale,selectScale,selectScale);
	}
	
	//OnMouseOver is called every frame while the mouse is over the GUIElement or Collider.
	public void OnMouseOver() {
		//while the mouse is over the object, rotate the object to show it's selected and give us a chance
		// to inspect all sides of the object.
		this.transform.Rotate(Vector3.up, 45 * Time.deltaTime, Space.Self);
	}
	
	//OnMouseUp is called when the user has released the mouse button.
	public void OnMouseUp() {
		//Always clean up when we're done. Reset the depth into scene back to the default when we're no
		//longer selecting an object.
		depthIntoScene = defaultDepthIntoScene;
	}
}
