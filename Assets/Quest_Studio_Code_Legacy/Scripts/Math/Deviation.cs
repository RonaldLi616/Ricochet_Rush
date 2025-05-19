using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Quest_Studio
{
    public class Deviation
    {
        // Deviate Number
        #region
        // Deviate number by specific range
        #region
        /// <summary>
        /// Deviate number by specific range
        /// </summary>
        /// <param name="number">Number to be deviated.</param>
        /// <param name="range">Specific range for deviation.</param>
        /// <returns>number * (1 + range)</returns>
        public static float DeviateNumber(float number, float range)
        {
            float deviateNumber = number * (1 + range);
            return deviateNumber;
        }
        #endregion

        // Deviate number by min & max
        #region
        /// <summary>
        /// Deviate number by min & max
        /// </summary>
        /// <param name="number">Number to be deviated.</param>
        /// <param name="min">Minimum number for deviation.</param>
        /// <param name="max">Maximum number for deviation.</param>
        /// <returns>number * (1 + range)</returns>
        public static float DeviateNumber(float number, float min, float max)
        {
            float deviateNumber = number * (1 + Random.Range(min, max));
            return deviateNumber;
        }
        #endregion

        #endregion

        // Deviate Vector
        #region 
        // Deviate Vector3 by min & max
        /// <summary>
        /// Deviate Vector3 by min & max
        /// </summary>
        /// <param name="minVector3"></param>
        /// <param name="maxVector3"></param>
        /// <returns>new Vector3(deviationX, deviationY, deviationZ)</returns>
        #region
        public static Vector3 DeviateVector3(Vector3 minVector3, Vector3 maxVector3)
        {
            float deviationX = Random.Range(minVector3.x, maxVector3.x);
            float deviationY = Random.Range(minVector3.y, maxVector3.y);
            float deviationZ = Random.Range(minVector3.z, maxVector3.z);
            Vector3 deviateVector3 = new Vector3(deviationX, deviationY, deviationZ);
            return deviateVector3;
        }
        #endregion

        // Deviate Vector3 by min & max, ignore 1 vector
        /// <summary>
        /// Deviate Vector3 by min & max
        /// </summary>
        /// <param name="minVector3"></param>
        /// <param name="maxVector3"></param>
        /// <param name="ignore">0 = x, 1 = y, 2 = z</param>
        /// <returns>new Vector3(deviationX, deviationY, deviationZ)</returns>
        #region
        public static Vector3 DeviateVector3(Vector3 minVector3, Vector3 maxVector3, int ignore)
        {
            float deviationX = Random.Range(minVector3.x, maxVector3.x);
            float deviationY = Random.Range(minVector3.y, maxVector3.y);
            float deviationZ = Random.Range(minVector3.z, maxVector3.z);
            switch (ignore)
            {
                case 0:
                    // Ignore X
                    deviationX = 0f;
                    break;

                case 1:
                    // Ignore Y
                    deviationY = 0f;
                    break;

                case 2:
                    // Ignore Z
                    deviationZ = 0f;
                    break;

                default:
                    // Ignore Z
                    deviationZ = 0f;
                    break;
            }
            Vector3 deviateVector3 = new Vector3(deviationX, deviationY, deviationZ);
            return deviateVector3;
        }
        #endregion

        #endregion
    }
}