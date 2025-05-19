using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Quest_Studio
{
    public class CountingTimer
    {
        private static List<CountingTimer> activeTimerList;
        private static GameObject initGameObject;

        private static void InitIfNeeded()
        {
            if (initGameObject == null)
            {
                initGameObject = new GameObject("CountingTimer_InitGameObject");
                activeTimerList = new List<CountingTimer>();
            }
        }

        public static CountingTimer CreateCountDown(float timeInSecond, bool enableOverTime, string timerName = null, bool debugLog = true, Text timerText = null)
        {
            InitIfNeeded();
            GameObject gameObject = new GameObject("CountingTimer", typeof(MonoBehaviourHook));

            CountingTimer countingTimer = new CountingTimer(Counting.Down, gameObject, timeInSecond, enableOverTime, timerName, debugLog, timerText);

            gameObject.GetComponent<MonoBehaviourHook>().onUpdate = countingTimer.Update;

            activeTimerList.Add(countingTimer);

            return countingTimer;
        }

        public static CountingTimer CreateCountUp(string timerName = null, bool debugLog = false, Text timerText = null)
        {
            InitIfNeeded();
            GameObject gameObject = new GameObject("CountingTimer", typeof(MonoBehaviourHook));

            CountingTimer countingTimer = new CountingTimer(Counting.Up, gameObject, 0, false, timerName, debugLog, timerText);

            gameObject.GetComponent<MonoBehaviourHook>().onUpdate = countingTimer.Update;

            activeTimerList.Add(countingTimer);

            return countingTimer;
        }

        private static void RemoveTimer(CountingTimer countingTimer)
        {
            InitIfNeeded();
            activeTimerList.Remove(countingTimer);
        }

        public static void PauseTimer(string timerName)
        {
            for (int i = 0; i < activeTimerList.Count; i++)
            {
                if (activeTimerList[i].timerName == timerName)
                {
                    // Pause this timer
                    activeTimerList[i].isPause = true;
                }
            }
        }

        public static void ResumeTimer(string timerName)
        {
            for (int i = 0; i < activeTimerList.Count; i++)
            {
                if (activeTimerList[i].timerName == timerName)
                {
                    // Resume this timer
                    activeTimerList[i].isPause = false;
                }
            }
        }

        public static void StopTimer(string timerName)
        {
            for (int i = 0; i < activeTimerList.Count; i++)
            {
                if (activeTimerList[i].timerName == timerName)
                {
                    // Stop this timer
                    activeTimerList[i].DestroySelf();
                    i--;
                }
            }
        }

        public static float RecordTime(string timerName)
        {
            float timeRecord = 0;
            for (int i = 0; i < activeTimerList.Count; i++)
            {
                if (activeTimerList[i].timerName == timerName)
                {
                    if (activeTimerList[i].counting == Counting.Down)
                    {
                        if (activeTimerList[i].overTime == 0f)
                        {
                            // In Time
                            timeRecord = activeTimerList[i].timeInSecond - activeTimerList[i].elastTime;
                        }
                        else
                        {
                            // Over Time
                            timeRecord = activeTimerList[i].timeInSecond + activeTimerList[i].overTime;
                        }
                    }
                    else
                    {
                        timeRecord = activeTimerList[i].elastTime;
                    }
                }
            }
            return timeRecord;
        }

        // Dummy class to have access to MonoBehaviour functions
        private class MonoBehaviourHook : MonoBehaviour
        {
            public Action onUpdate;
            private void Update()
            {
                if (onUpdate != null)
                {
                    onUpdate();
                }
            }
        }

        //UI Elements
        private Text timerText;

        //Valuables
        private float timeInSecond;
        private float elastTime;
        private bool enableOverTime;
        private float overTime;
        public enum Counting { Down, Up }
        private Counting counting;
        private string timerName;
        private GameObject gameObject;
        private bool isPause;
        private bool isDestroyed;
        private bool triggerOnce;

        private bool debugLog;

        public CountingTimer(Counting counting, GameObject gameObject, float timeInSecond = 0, bool enableOverTime = false, string timerName = null, bool debugLog = true, Text timerText = null)
        {
            this.timerText = timerText;
            this.timeInSecond = timeInSecond;
            switch (counting)
            {
                case Counting.Down:
                    elastTime = timeInSecond;
                    break;

                case Counting.Up:
                    elastTime = 0;
                    break;
            }
            this.enableOverTime = enableOverTime;
            this.overTime = 0;
            this.counting = counting;
            this.timerName = timerName;
            this.gameObject = gameObject;
            isPause = false;
            isDestroyed = false;
            triggerOnce = false;
            this.debugLog = debugLog;
        }

        private string FormatTime(float time)
        {
            int minutes = (int)time / 60;
            int seconds = (int)time - 60 * minutes;
            int milliseconds = (int)(100 * (time - minutes * 60 - seconds));
            return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }

        private void Update()
        {
            if (!isDestroyed)
            {
                if (isPause)
                {
                    return;
                }

                switch (counting)
                {
                    case Counting.Down:
                        if (elastTime > 0)
                        {
                            elastTime -= Time.deltaTime;
                            if (timerText != null)
                            {
                                timerText.text = FormatTime(elastTime);
                            }
                            else
                            {
                                if (debugLog)
                                {
                                    Debug.Log(FormatTime(elastTime));
                                }
                            }
                        }
                        else if (elastTime < 0 && enableOverTime == false)
                        {
                            elastTime = 0;
                            if (timerText != null)
                            {
                                timerText.text = FormatTime(elastTime);
                            }
                            else
                            {
                                if (debugLog)
                                {
                                    Debug.Log(FormatTime(elastTime));
                                }
                            }

                            if (!triggerOnce)
                            {
                                triggerOnce = true;
                                Debug.Log("Times up!");
                            }

                            DestroySelf();
                        }
                        else if (elastTime <= 0 && enableOverTime == true)
                        {
                            elastTime = 0;
                            overTime += Time.deltaTime;
                            if (timerText != null)
                            {
                                timerText.text = FormatTime(overTime);
                            }
                            else
                            {
                                if (debugLog)
                                {
                                    Debug.Log(FormatTime(overTime));
                                }
                            }

                            if (!triggerOnce)
                            {
                                triggerOnce = true;
                                Debug.Log("Over time!");
                            }
                        }
                        break;

                    case Counting.Up:
                        if (elastTime >= 0)
                        {
                            elastTime += Time.deltaTime;
                            if (timerText != null)
                            {
                                timerText.text = FormatTime(elastTime);
                            }
                            else
                            {
                                if (debugLog)
                                {
                                    Debug.Log(FormatTime(elastTime));
                                }
                            }
                        }
                        break;
                }
            }
        }

        private void DestroySelf()
        {
            isDestroyed = true;
            UnityEngine.Object.Destroy(gameObject);
            RemoveTimer(this);
        }
    }
}