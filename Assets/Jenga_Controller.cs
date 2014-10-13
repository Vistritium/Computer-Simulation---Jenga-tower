using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Assets;
using UnityEngine;
using System.Collections;

public class Jenga_Controller : MonoBehaviour {

    enum State
    {
        ToSelect,
        Selected
    }

    private State state;

    private Action<GameObject> onObjectSelected;

    

	private GameObject selected, previousSelected;
    private Color oldColor;

    private Jenga_SetUpBlocks jengaSetUpBlocks;
    List<GameObject> blocks;
    List<Vector2> pointsAvailibleForPlacing = new List<Vector2>(); 

    // Use this for initialization
	void Start () {
        state = State.ToSelect;
	    jengaSetUpBlocks = GetComponent<Jenga_SetUpBlocks>();
        blocks = jengaSetUpBlocks.blockList;
	    SetUpPointsAvailibleForPlacing();
	}

    void SetUpPointsAvailibleForPlacing()
    {
        for (int i = 0; i < 6; i++)
        {
            if(i == 4) continue;
            var jengaBlockPos = blocks[i].transform.position;
            var newAvailiblePos = new Vector2(jengaBlockPos.x, jengaBlockPos.z);
            pointsAvailibleForPlacing.Add(newAvailiblePos);
        }
        
    }

    Vector2 FindCLosestAvailiblePos(Vector2 pos)
    {
        return pointsAvailibleForPlacing.Aggregate(pointsAvailibleForPlacing[0], (aggregate, current) =>
        {
            var aggDistance = Vector2.Distance(aggregate, pos);
            var currentDistance = Vector2.Distance(current, pos);
            if (currentDistance < aggDistance)
            {
                return current;
            }
            else
            {
                return aggregate;
            }
        });
    }

    
	
	// Update is called once per frame
	void Update () {
	    switch (state)
	    {
	        case State.ToSelect:
                UpdateStateToSelect();
	            break;
	        case State.Selected:
                UpdateStateSelected();
	            break;
	        default:
	            throw new ArgumentOutOfRangeException();
	    }
	}

    Vector3 FindHighestFreeAtPosition(Vector3 position)
    {
        float distance = 120.0f;
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(position + Vector3.up * distance, Vector3.down);

        if (Physics.Raycast(ray, out hit, distance))
        {
            var hitObj = hit.collider.gameObject;
            return hit.point + Vector3.up*hitObj.transform.localScale.y*0.5f;// + Vector3.up * 0.1f;
        }
        else
        {
            return Vector3.zero;
        }
        

    }

    void UpdateStateSelected()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
           
            selected.collider.gameObject.renderer.materials[0].color = oldColor;
            SwitchState(State.ToSelect);
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            selected.transform.Rotate(Vector3.up, 90.0f);
        }

		SetProperRotation();
        if (Input.GetMouseButtonDown(0))
        {
            selected.collider.enabled = true;
            selected.rigidbody.detectCollisions = true;
            selected.rigidbody.useGravity = true;
			selected.rigidbody.isKinematic = false;
			//SetProperRotation();
            SwitchState(State.ToSelect);

            return;
            //copySelected.SetActive(false);
            // copySelected.rigidbody.detectCollisions = false;
        }
		selected.rigidbody.isKinematic = true;
		//SetProperRotation();

        RayCast(hit =>
        {
            var newPosition = hit.point;
            var d2NewAvailiblePos = FindCLosestAvailiblePos(new Vector2(newPosition.x, newPosition.z));
            selected.transform.position = FindHighestFreeAtPosition(new Vector3(d2NewAvailiblePos.x, 0, d2NewAvailiblePos.y));

            selected.collider.gameObject.renderer.materials[0].color = oldColor;
            
            //Destroy(copySelected);
            //SwitchState(State.ToSelect);

        });
    }

    void UpdateStateToSelect()
    {
        
		MouseRaycastCheck(hit =>
        {
            
            selected = hit.collider.gameObject;

            if (selected.tag == "JengaBlock")
            {
                oldColor = selected.collider.gameObject.renderer.materials[0].color;
                selected.collider.gameObject.renderer.materials[0].color = Color.black;
                SwitchState(State.Selected);
                selected.collider.enabled = false;
                selected.rigidbody.detectCollisions = false;
                selected.rigidbody.useGravity = false;
				selected.rigidbody.isKinematic = true;
				selected.transform.rotation = new Quaternion ();
                //copySelected.SetActive(false);
                //copySelected.rigidbody.detectCollisions = false;
            }
        });
    }


    void SwitchState(State state)
    {
        this.state = state;
    }

    private static void RayCast(Action<RaycastHit> onRaycastHit)
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            onRaycastHit.Invoke(hit);
        }
    }

    private static void MouseRaycastCheck(Action<RaycastHit> onRaycastHit)
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                onRaycastHit.Invoke(hit);
            }
        }
    }

	void SetProperRotation() //to Selected
	{
		if ( ((int)(selected.transform.position.y / 1.5f))%2 == 1 )
		{
			selected.transform.rotation = new Quaternion();
		}
		else
		{
			selected.transform.rotation = new Quaternion();
			selected.transform.Rotate( new Vector3(0, 90, 0));
		}
	}
}
