using System.Collections;
using UnityEngine;

namespace Quest_Studio
{
    public class CameraShake : MonoBehaviour
    {
        private static CameraShake instance;

        public static IEnumerator Shake(float duration, float magnitude)
        {
            Vector3 originalPos = instance.transform.localPosition;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                instance.transform.localPosition = new Vector3(x, y, originalPos.z);
                elapsed += Time.deltaTime;

                yield return null;
            }

            instance.transform.localPosition = originalPos;
        }

        private void Awake()
        {
            // Instance
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {

        }

        private void Update()
        {

        }
    }
}