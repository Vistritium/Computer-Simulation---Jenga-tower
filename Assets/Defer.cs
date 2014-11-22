using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public static class Defer
    {

        public static void DeferAction(this MonoBehaviour monoBehaviour, float delayInSecs,  Action action)
        {
            monoBehaviour.StartCoroutine(WaitAndInvoke(delayInSecs, action));
        }

        private static IEnumerator WaitAndInvoke(float waitTime, Action toInvoke)
        {
            yield return new WaitForSeconds(waitTime);
            toInvoke.Invoke();
        }  

    }
}
