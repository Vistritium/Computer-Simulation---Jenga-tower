﻿using UnityEngine;

namespace Assets
{
    public class Jenga_Control_Block : MonoBehaviour {

        public Jenga_Controller jc;
        public float treshold = 4f;
        public float controler;

        public bool passedOnce = false;

        private float multiplier;


        // Use this for initialization
        void Start () {
            multiplier = 1f / treshold;
            passedOnce = false;
        }
	
        // Update is called once per frame
        void Update () {

            float max = 0f;
            Quaternion rot;
            Vector3 eul;

            for (int i = 0; i < jc.blocks.ToArray().Length; ++i)
            {
                rot = jc.blocks[i].transform.rotation;
                eul = rot.eulerAngles;

                if (eul.x > 180f)
                {
                    eul.x = 360f - eul.x;
                }
                if (eul.z > 180f)
                {
                    eul.z = 360f - eul.z;
                }

                if (max < eul.x)
                {
                    max = eul.x;
                }

                if (max < eul.z)
                {
                    max = eul.z;
                }
            }

            if (max > 180)
            {
                max = 360f - max;
            }
            controler = max;
            max *= multiplier;

            if ( controler >= max )
            {
                passedOnce = true;
            }

            renderer.materials [0].color = Color.Lerp(Color.green, Color.red, max);
        }
    }
}
