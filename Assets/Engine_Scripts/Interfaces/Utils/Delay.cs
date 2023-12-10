using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interfaces
{
    public static class Delay
    {
        public static void Do(Action action, float delayTimeSec, MonoBehaviour coroutineMgr)
        {
            if (delayTimeSec == 0)
                action();
            else
                coroutineMgr.StartCoroutine(DelayExecute());

            IEnumerator DelayExecute()
            {
                yield return new WaitForSeconds(delayTimeSec);
                action();
            }
        }
    }
}
