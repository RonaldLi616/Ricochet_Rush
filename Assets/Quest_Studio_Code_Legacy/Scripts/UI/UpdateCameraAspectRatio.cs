using System.Linq.Expressions;
using UnityEngine;

namespace Quest_Studio
{
    [RequireComponent(typeof(Camera))]
    [ExecuteAlways]
    public class UpdateCameraAspectRatio : MonoBehaviour
    {
        // Component
        #region
        private void SetComponent()
        {
            SetCamera();
        }

        // Camera
        #region 
        private Camera cam;
        private void SetCamera() { cam = this.GetComponent<Camera>(); }

        #endregion

        #endregion

        // Valuable
        #region
        // Aspect Ratio
        #region 
        private enum AspectRatio
        {
            Common, // 16:9
            Older, // 16:10
            Traditional, // 4:3
            UltraWide // 21:9
        }
        [Tooltip("Common 16:9 , Older 16:10 , Traditional 4:3 , UltraWide 21:9")]
        [SerializeField] private AspectRatio aspectRatio = AspectRatio.Common;
        private Vector2 GetTargetAspectRatio()
        {
            switch (aspectRatio)
            {
                case AspectRatio.Common:
                    return new(16, 9);

                case AspectRatio.Older:
                    return new(16, 10);

                case AspectRatio.Traditional:
                    return new(4, 3);

                case AspectRatio.UltraWide:
                    return new(21, 9);

                default:
                    return new(16, 9);
            }
        }
        #endregion

        //private readonly Vector2 targetAspectRatio = new(16, 9);
        private readonly Vector2 rectCenter = new(0.5f, 0.5f);

        private Vector2 lastResolution;
        #endregion

        private void CalculateCameraRect(Vector2 currentScreenResolution)
        {
            var normalizedAspectRatio = GetTargetAspectRatio() / currentScreenResolution;
            var size = normalizedAspectRatio / Mathf.Max(normalizedAspectRatio.x, normalizedAspectRatio.y);
            cam.rect = new Rect(default, size) { center = rectCenter };
        }

        private void OnValidate()
        {
            // Set Component
            #region 
            SetComponent();
            #endregion

        }

        private void LateUpdate()
        {
            var currentScreenResolution = new Vector2(Screen.width, Screen.height);

            // Don't run all the calculations if the screen resolution has not changed
            if (lastResolution != currentScreenResolution)
            {
                CalculateCameraRect(currentScreenResolution);
            }

            lastResolution = currentScreenResolution;
        }
    }
}
