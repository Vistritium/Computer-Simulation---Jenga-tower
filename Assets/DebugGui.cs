using UnityEngine;

namespace Assets
{
    public class DebugGui : MonoBehaviour
    {
        private JengaTower jengaTower;
        private string textAreaString = "0";


        // Use this for initialization
        private void Start()
        {
            var jengaSetUpBlocks = GameObject.Find("Jenga_Blocks_Controller").GetComponent<Jenga_SetUpBlocks>();
            jengaTower = new JengaTower(jengaSetUpBlocks.blockList);
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void OnGUI()
        {
/*        string tempAreString =
            GUI.TextArea(new Rect(Screen.width*0.9f, Screen.height*0.05f, Screen.width*0.1f, Screen.height*0.05f),
                textAreaString);
        int parsed;
        if (int.TryParse(tempAreString, out parsed))
        {
            textAreaString = parsed.ToString();
        }*/


            if (GUI.Button(new Rect(Screen.width*0.9f, 0, Screen.width*0.1f, Screen.height*0.05f), "DebugButton"))
            {
                GameObject.Find("AI").GetComponent<BlockPlacer>().DrawPlacesToConsider();
                
                //GameObject.Find("AI").GetComponent<AiRunner>().Turn();
                //List<GameObject> blockToDebug = GameObject.Find("AI").GetComponent<BlockSelector>().GetAllValidBlocks();
                //blockToDebug.ForEach(x => x.renderer.material.color = Color.blue);
            }
        }
    }
}