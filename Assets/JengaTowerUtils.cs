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
    }
}
