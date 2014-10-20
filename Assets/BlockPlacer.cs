using Assets;
using UnityEngine;
using System.Collections;

public class BlockPlacer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlaceObject(GameObject jengaBlock)
    {
        var jengaStartPosition = Vector3.zero;//magic number just like SOMEONE did in jenga tower creation
        Vector3 highestFreeAtPosition = JengaTowerUtils.FindHighestFreeAtPosition(jengaStartPosition);
        jengaBlock.transform.position = highestFreeAtPosition;
    }
}
