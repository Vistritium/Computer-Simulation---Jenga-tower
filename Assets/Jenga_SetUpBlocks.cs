using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class Jenga_SetUpBlocks : MonoBehaviour {

        public GameObject jengaBlock;

        public GameObject[] blocks;

        public List<GameObject> blockList = new List<GameObject>();

        Color[] colorList;

        // Use this for initialization
        void Awake () {
            int size = 54;
            blocks = new GameObject [size];
            colorList = new Color[] {Color.red, Color.green, Color.blue, Color.red};//, Color.cyan, Color.magenta};
            SetUpBlocks (size);
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        void SetUpBlocks(int size)
        {
            Vector3 tempVec3;
            int CLLenght = colorList.Length - 1;

            int num = size / CLLenght;//(size / (CLLenght - 2)) - 1;

            for (int i = 0; i < size; ++i)
            {
                var newBlock = (GameObject) Instantiate(jengaBlock);
                blocks[i] = newBlock;
                blockList.Add(newBlock);
                blocks[i].renderer.materials [0].color = Color.Lerp(colorList[i/num], colorList[(i/num) +1], (((float) (i%(size / CLLenght)))/ ((float)(size / CLLenght))));//(((float) i) / (54 / colorList.Length) -  ((float) i ) % (((float) i )/ colorList.Length )) );

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
}
