using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour {
	
	float distance = 25.0f;

	float xSpeed = .4f;
	float ySpeed = .04f;
	float wSpeed = .4f;

	private float x = 0.0f, prevX = 0f;

	bool rightclicked = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// Mouse manipulation

		if (Input.GetMouseButton(1)){
			rightclicked = true;
		}
		else{
			rightclicked = false;
		}

		if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
		{
			distance -= wSpeed;
			
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
		{
			distance += wSpeed;
		}

		// Keyboard manipulation

		if (Input.GetKey(KeyCode.Q))
		{
			Vector3 tempV = transform.position;
			tempV.y -= ySpeed;
			transform.position = tempV;
		}

		if (Input.GetKey(KeyCode.E))
		{
			Vector3 tempV = transform.position;
			tempV.y += ySpeed;
			transform.position = tempV;
		}
	}

	void LateUpdate()
	{
		Vector3 tPos = new Vector3(0, transform.position.y, 0);

		prevX = x;
		x = Input.mousePosition.x;

		if (rightclicked == true) {

			//x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
			var rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + (x - prevX)*xSpeed, 0);
			var position = rotation * new Vector3(0.0f, 0.0f, -distance) + tPos;
			transform.rotation = rotation;
			transform.position = position;
		}
		else
		{
			transform.position = transform.rotation * new Vector3(0.0f, 0.0f, -distance) + tPos;
		}
	}
}
