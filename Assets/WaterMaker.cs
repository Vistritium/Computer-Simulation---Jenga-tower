using UnityEngine;
using System.Collections;

public class WaterMaker : MonoBehaviour
{


    private Material material;
    private Vector2 currentOffset = new Vector2(1,1);
    private float additionFactor = 0.05f;

	// Use this for initialization
	void Start ()
	{
	    this.material = this.renderer.material;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Vector2 newOffset = currentOffset + new Vector2(additionFactor, 0) * Time.deltaTime;
	    currentOffset = newOffset;
	    material.SetTextureOffset("_MainTex", newOffset);
	}
}
