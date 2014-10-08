using UnityEngine;
using System.Collections;

public class Jenga_SetUpBlocks : MonoBehaviour {

	public GameObject jengaBlock;

	private GameObject[] blocks;

	// Use this for initialization
	void Start () {
		int size = 54;
		blocks = new GameObject [size];
		SetUpBlocks (size);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetUpBlocks(int size)
	{
		Vector3 tempVec3;

		for (int i = 0; i < size; ++i)
		{
			blocks[i] = (GameObject) Instantiate(jengaBlock);
			blocks[i].transform.parent = transform;
			tempVec3 = blocks[i].transform.position;
			if ((i/3)%2 == 0)
			{
				tempVec3.z = (i%3 - 1) * 2.5f;
			}
			else
			{
				blocks[i].transform.Rotate(new Vector3(0,90,0));
				tempVec3.x = ((i%3 - 1) * 2.5f) + 1f;
				//if (i%3 == 0)
				//{
				//	tempVec3.x = -1.5f;
				//}
				//else if (i%3 == 1)
				//{
				//	tempVec3.x = 1f;
				//}
				//else
				//{
				//	tempVec3.x = 3.5f;
				//}
				////tempVec3.x = (i/3) * 2.5f;
			}
			tempVec3.y = (i/3) * 1.5f; 
			blocks[i].transform.position = tempVec3;
		}
	}
}
