using UnityEngine;

namespace Assets
{
    public class CameraController : MonoBehaviour
    {

        public float upDownSpeedFactor = 10f;
        public float closerFartherFactor = 10f;
        public float rotateFactor = 10f;

        private Vector3 lookingAt = Vector3.up*5;

        // Use this for initialization
        void Start () {
	        
        }
	
        // Update is called once per frame
        void Update () {
            transform.LookAt(lookingAt);

            ControllUpDownMovement();
            ControllRotation();
        }

        private void ControllRotation()
        {

            if (Input.GetMouseButton(1))
            {
                float mouseDeltaX = Input.GetAxis("Mouse X");
                float mouseDeltaY = Input.GetAxis("Mouse Y");

                this.transform.RotateAround(lookingAt, Vector3.up, mouseDeltaX*rotateFactor);
                this.transform.RotateAround(lookingAt, camera.transform.right, - mouseDeltaY*rotateFactor);
            }
        }

        private void ControllUpDownMovement()
        {
            if (Input.GetKey(KeyCode.Q))
            {
                Vector3 move = Vector3.down*upDownSpeedFactor*Time.deltaTime;
                this.transform.position = this.transform.position + move;
                lookingAt = lookingAt + move;
            }

            if (Input.GetKey(KeyCode.E))
            {
                Vector3 move = Vector3.up*upDownSpeedFactor*Time.deltaTime;
                this.transform.position = this.transform.position + move;
                lookingAt = lookingAt + move;
            }
        }
    }
}
