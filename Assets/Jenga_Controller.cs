using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    public class Jenga_Controller : MonoBehaviour
    {
        private int MoveIterator;
        public bool blockPicked = false;
        public List<GameObject> blocks;

        public bool canMove = true;
        private Jenga_SetUpBlocks jengaSetUpBlocks;
        private Color oldColor;
        private Action<GameObject> onObjectSelected;
        public List<Vector2> pointsAvailibleForPlacing = new List<Vector2>();
        private GameObject previousSelected;
        private GameObject selected;
        private State state;

        public List<Action> toInvokeOnNewTurn = new List<Action>();
        public static bool GameFinished = false;

        public int moveIterator
        {
            get { return MoveIterator; }
            set
            {
                if (MoveIterator != value)
                {
                    MoveIterator = value;
                    toInvokeOnNewTurn.ForEach(x => x.Invoke());
                }
            }
        }

        // Use this for initialization
        private void Start()
        {
            state = State.ToSelect;
            jengaSetUpBlocks = GetComponent<Jenga_SetUpBlocks>();
            blocks = jengaSetUpBlocks.blockList;
            SetUpPointsAvailibleForPlacing();
        }

        private void SetUpPointsAvailibleForPlacing()
        {
            for (int i = 0; i < 6; i++)
            {
                if (i == 4) continue;
                Vector3 jengaBlockPos = blocks[i].transform.position;
                var newAvailiblePos = new Vector2(jengaBlockPos.x, jengaBlockPos.z);
                pointsAvailibleForPlacing.Add(newAvailiblePos);
            }
        }

        private Vector2 FindCLosestAvailiblePos(Vector2 pos)
        {
            return pointsAvailibleForPlacing.Aggregate(pointsAvailibleForPlacing[0], (aggregate, current) =>
            {
                float aggDistance = Vector2.Distance(aggregate, pos);
                float currentDistance = Vector2.Distance(current, pos);
                if (currentDistance < aggDistance)
                {
                    return current;
                }
                return aggregate;
            });
        }


        // Update is called once per frame
        private void Update()
        {
            if (canMove == false)
            {
                return;
            }

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


        private void UpdateStateSelected()
        {
            blockPicked = true;
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


            JengaTowerUtils.SetProperRotation(selected);
           
            if (Input.GetMouseButtonDown(0) || GameFinished)
            {
                selected.collider.enabled = true;
                selected.rigidbody.detectCollisions = true;
                selected.rigidbody.useGravity = true;
                selected.rigidbody.isKinematic = false;
                //SetProperRotation();
                SwitchState(State.ToSelect);
                ++moveIterator;
                blockPicked = false;

                return;
                //copySelected.SetActive(false);
                // copySelected.rigidbody.detectCollisions = false;
            }
            selected.rigidbody.isKinematic = true;
            //SetProperRotation();

            RayCast(hit =>
            {
                Vector3 newPosition = hit.point;
                Vector2 d2NewAvailiblePos = FindCLosestAvailiblePos(new Vector2(newPosition.x, newPosition.z));
                selected.transform.position =
                    JengaTowerUtils.FindHighestFreeAtPosition(new Vector3(d2NewAvailiblePos.x, 0, d2NewAvailiblePos.y));

                selected.collider.gameObject.renderer.materials[0].color = oldColor;
                JengaTowerUtils.SetProperPosition(selected);
                //Destroy(copySelected);
                //SwitchState(State.ToSelect);
            });
            
        }

        private void UpdateStateToSelect()
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
                    selected.transform.rotation = new Quaternion();
                    //copySelected.SetActive(false);
                    //copySelected.rigidbody.detectCollisions = false;
                }
            });
        }


        private void SwitchState(State state)
        {
            this.state = state;
        }

        private static void RayCast(Action<RaycastHit> onRaycastHit)
        {
            var hit = new RaycastHit();
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
                var hit = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    onRaycastHit.Invoke(hit);
                }
            }
        }




        private enum State
        {
            ToSelect,
            Selected
        }
    }
}