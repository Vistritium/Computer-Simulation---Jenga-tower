using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour {
	
	float distance = 25.0f;

	float xSpeed = .4f;
	float ySpeed = .1f;
	float wSpeed = 1.5f;


	private float 	x = 0.0f, prevX = 0f,
					y = 0.0f, prevY = 0f;

	bool rightclicked = false;
	bool middleclicked = false;

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

/*		if (Input.GetMouseButton(2)){
			middleclicked = true;
		}
		else{
			middleclicked = false;
		}*/

		if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
		{
			distance += wSpeed;
			
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
		{
			distance -= wSpeed;
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
		prevY = y;
		x = Input.mousePosition.x;
		y = Input.mousePosition.y;

		if (rightclicked == true) {

			//x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
			var rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + (x - prevX)*xSpeed, 0);
			var position = rotation * new Vector3(0.0f, 0.0f, -distance) + tPos;
			transform.rotation = rotation;
			transform.position = position;
		}
		else
		{
			//transform.position = transform.rotation * new Vector3(0.0f, 0.0f, -distance) + tPos;
		}

		if (middleclicked == true)
		{
			Vector3 tempV = transform.position;
			tempV.y += (y-prevY)*ySpeed;
			transform.position = tempV;
		}

	}
}
