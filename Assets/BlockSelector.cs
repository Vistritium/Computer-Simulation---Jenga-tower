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

		System.Random random = new System.Random();
        public List<GameObject> GetAllValidBlocks()
        {
            List<List<GameObject>> levelGroupedBlocks = getLevelGroupedBlocks();

            //remove top blocks
			levelGroupedBlocks.RemoveAt(levelGroupedBlocks.Count - 1);
            levelGroupedBlocks.RemoveAt(levelGroupedBlocks.Count - 1);

//			var color = new Color ((float)random.NextDouble (), (float)random.NextDouble (), (float)random.NextDouble ());
//			levelGroupedBlocks.ForEach (x => {
//				x.ForEach( y => {
//					y.renderer.material.color = color;
//					});
//				});

            List<List<GameObject>> only3Blocks = levelGroupedBlocks.Where(x => x.Count == 3).ToList();

            List<GameObject> result = null;
            if (only3Blocks.Count >= 0)
            {
                result = SelectMiddleFrom3Blocks(only3Blocks);
            }
            else
            {
                var only2Blocks = levelGroupedBlocks.Where(x => x.Count == 2).ToList();
                Debug.Log(string.Format("found {0} block groups of number 2", only2Blocks.Count));
                var valid2Blocks = only2Blocks.Select(level =>
                {
                    var first = level[0];
                    var second = level[1];

                    Vector3 position = first.transform.position;
                    position = new Vector3(position.x - 1, position.y, position.z);
                    float f = 2f;
                    if (position.x < f && position.x > -f && position.z < f && position.z > -f)
                    {
                        var res = new List<GameObject>();
                        res.Add(second);
                        return res;
                    }

                    position = second.transform.position;
                    position = new Vector3(position.x - 1, position.y, position.z);
                    if (position.x < f && position.x > -f && position.z < f && position.z > -f)
                    {
                        var res = new List<GameObject>();
                        res.Add(first);
                        return res;
                    }

                    return new List<GameObject>();
                }).ToList();

                 result = valid2Blocks.Aggregate((one, two) => one.Concat(two).ToList());

            }
            //select only middle ones
         

            

            return result;
        }

        private static List<GameObject> SelectMiddleFrom3Blocks(List<List<GameObject>> only3Blocks)
        {
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
            List<GameObject> validBlocs = GetAllValidBlocks();
            if (validBlocs.Count > 0)
            {
                return validBlocs.Random();
            }
            else
            {
                Debug.Log("Random");
                return jengaBlocks.Random();
            }
           
        }

        // Update is called once per frame
        void Update () {
	
        }
    }
}
