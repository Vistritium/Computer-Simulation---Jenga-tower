using UnityEngine;
using System.Collections;

public class Jenga_Controller : MonoBehaviour {

	GameObject selected;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetMouseButtonDown(0))
		{
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast (ray, out hit, 100.0f))
			{  
				SetSelection(hit.collider.gameObject);
			}
		}
	}

	public void SetSelection (GameObject block)
	{
		Debug.Log ("" + block.transform.position.ToString ());
	}
}
