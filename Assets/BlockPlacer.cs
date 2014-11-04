using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    public class BlockPlacer : MonoBehaviour {
        private List<GameObject> jengaBlocks;
        private JengaTower jengaTower;

        public GameObject debugObj;

        // Use this for initialization
        void Start () {
	    
        }
	
        // Update is called once per frame
        void Update () {
	
        }


        public void DrawPlacesToConsider()
        {
            foreach (var vec in GetPlacesToConsider())
            {
                var instantiate = (GameObject)Instantiate(debugObj);
                instantiate.transform.position = vec;

            }
        }


        private List<Vector3> GetPlacesToConsider()
        {
            jengaBlocks = GameObject.Find("Jenga_Blocks_Controller").GetComponent<Jenga_SetUpBlocks>().blockList;
            jengaTower = new JengaTower(jengaBlocks);
            List<Vector2> pointsAvailibleForPlacing = GameObject.Find("Jenga_Blocks_Controller").GetComponent<Jenga_Controller>().pointsAvailibleForPlacing;
            var result =
                pointsAvailibleForPlacing.Select(
                    x => JengaTowerUtils.FindHighestFreeAtPosition(new Vector3(x.x, 0, x.y))).ToList();
            return result;
        }

        public void PlaceObject(GameObject jengaBlock)
        {
            List<Vector3> placesToConsider = GetPlacesToConsider();
            var lowest = placesToConsider.Aggregate((agg, next) =>
            {
                if (agg.y < next.y)
                    return agg;
                return next;
            });

            jengaBlock.transform.position = lowest;
            JengaTowerUtils.SetProperRotation(jengaBlock);
            JengaTowerUtils.SetProperPosition(jengaBlock);
        }

        private static void PlacePrimitiveTop(GameObject jengaBlock)
        {
            var jengaStartPosition = Vector3.zero; //magic number just like SOMEONE did in jenga tower creation
            Vector3 highestFreeAtPosition = JengaTowerUtils.FindHighestFreeAtPosition(jengaStartPosition);


            highestFreeAtPosition.x += 1;

            jengaBlock.transform.rotation = new Quaternion();
            if (((int) (highestFreeAtPosition.y/1.5f))%2 == 0)
            {
                jengaBlock.transform.Rotate(new Vector3(0, 90, 0));
            }

            jengaBlock.transform.position = highestFreeAtPosition;
        }
    }
}
