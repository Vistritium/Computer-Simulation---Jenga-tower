using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
   public class JengaTowerUtils
    {

       public static Vector3 FindHighestFreeAtPosition(Vector3 position)
       {
           float distance = 120.0f;
           RaycastHit hit = new RaycastHit();
           Ray ray = new Ray(position + Vector3.up * distance, Vector3.down);

           if (Physics.Raycast(ray, out hit, distance))
           {
               var hitObj = hit.collider.gameObject;
               return hit.point + Vector3.up * hitObj.transform.localScale.y * 0.5f;// + Vector3.up * 0.1f;
           }
           else
           {
               return Vector3.zero;
           }
       }

       public static void SetProperPosition(GameObject selected)
       {
           Vector3 position = new Vector3(selected.transform.position.x - 1, selected.transform.position.y, selected.transform.position.z);
          // Debug.Log(String.Format("Current position is {0} {1} {2}", Math.Round(position.x, 2), Math.Round(position.y), Math.Round(position.z)));
           if (((int)(position.y / 1.5f)) % 2 == 1)
           {
               if (position.x > 2f || position.x < -2f)
               {
                   selected.transform.position = new Vector3(1, position.y, position.z);
               }
           }
           else
           {
               if (position.z > 2f || position.z < -2f)
               {
                   selected.transform.position = new Vector3(position.x + 1, position.y, 0);
               }
           }
       }

       public static void SetProperRotation(GameObject selected) //to Selected
       {
           if (((int)(selected.transform.position.y / 1.5f)) % 2 == 1)
           {
               selected.transform.rotation = new Quaternion();
           }
           else
           {
               selected.transform.rotation = new Quaternion();
               selected.transform.Rotate(new Vector3(0, 90, 0));
           }
       }
    }
}
