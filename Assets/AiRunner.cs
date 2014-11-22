using UnityEngine;

namespace Assets
{
    public class AiRunner : MonoBehaviour {

        private BlockPlacer blockPlacer;
        private BlockSelector blockSelector;

        // Use this for initialization
        void Start()
        {
            blockPlacer = GetComponent<BlockPlacer>();
            blockSelector = GetComponent<BlockSelector>();
        }

        public void Turn()
        {
            this.DeferAction(0.3f, () =>
            {
                GameObject aiJengaObject = blockSelector.GetAiJengaObject();
                blockPlacer.PlaceObject(aiJengaObject);
            });

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
