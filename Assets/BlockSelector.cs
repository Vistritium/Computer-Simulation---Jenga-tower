using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    public class BlockSelector : MonoBehaviour {
        private List<GameObject> jengaBlocks;

        // Use this for initialization
        void Start ()
        {
            jengaBlocks = GameObject.Find("Jenga_Blocks_Controller").GetComponent<Jenga_SetUpBlocks>().blockList;
        }

        List<List<GameObject>> getLevelGroupedBlocks()
        {
            List<List<GameObject>> groupedByLevel = new List<List<GameObject>>();
            var jengaTower = new JengaTower(jengaBlocks);
            bool stop = false;
            for (int i = 0; !stop; i++)
            {
                var elementsOnCurrentLevel = jengaTower.GetBlocksOnLevel(i);
                if (elementsOnCurrentLevel.Count > 0)
                {
                    groupedByLevel.Add(elementsOnCurrentLevel);
                }
                else
                {
                    stop = true;
                }
            }
            return groupedByLevel;

        }


        public List<GameObject> GetAllValidBlocks()
        {
            List<List<GameObject>> levelGroupedBlocks = getLevelGroupedBlocks();

            //remove top blocks
            levelGroupedBlocks.RemoveAt(levelGroupedBlocks.Count - 1);

            List<List<GameObject>> only3Blocks = levelGroupedBlocks.Where(x => x.Count == 3).ToList();

            //select only middle ones
            var result = only3Blocks.Select(level =>
            {
                var first = level[0];
                var second = level[1];
                var thirt = level[2];

                var firstLength = Vector3.Distance(second.transform.position, thirt.transform.position);
                var secondLength = Vector3.Distance(first.transform.position, thirt.transform.position);
                var thirtLength = Vector3.Distance(first.transform.position, second.transform.position);

                if (firstLength > secondLength && firstLength > thirtLength)
                {
                    return first;
                }
                else if (secondLength > firstLength && secondLength > thirtLength)
                {
                    return second;
                }
                else
                {
                    return thirt;
                }
            }).ToList();

            return result;
        }


        public GameObject GetAiJengaObject()
        {
            return GetAllValidBlocks().Random();
        }

        // Update is called once per frame
        void Update () {
	
        }
    }
}
