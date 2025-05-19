using UnityEngine;
using UnityEngine.UI;

namespace Quest_Studio
{
    public class ScreenCaptureHandler : MonoBehaviour
    {
        // Instance
        #region
        private static ScreenCaptureHandler instance;
        public static ScreenCaptureHandler GetInstance()
        {
            return instance;
        }
        #endregion

        // Assets
        #region
        [SerializeField]
        private Sprite sprite;
        public static Sprite GetSprite()
        {
            return instance.sprite;
        }

        [SerializeField]
        private Sprite[] sprites = new Sprite[0];
        public static Sprite[] GetSprites()
        {
            return instance.sprites;
        }
        #endregion

        // Components
        #region
        private Camera mainCamera;
        #endregion

        // Variables
        #region
        // - Capture Once by Next Frame -
        #region
        private bool takeScreenshotOnNextFrame;
        #endregion

        // - Capture As Sprite Saved as PNG -
        #region
        [Header("Capture As Sprite Saved as PNG")]
        [SerializeField]
        private bool enableSaveAsPNG = false;
        #endregion

        // - Save Detail -
        #region
        [Header("Save Detail")]
        [Tooltip("Directory Example: /MyFolder/MyInnerFolder")]
        [SerializeField]
        private string savePath = "";
        private string saveURL;

        [SerializeField]
        private string fileName = "";

        private void CheckSaveDetail()
        {
            if (savePath == "")
            {
                Debug.Log("Missing save path!");
                return;
            }

            if (fileName == "")
            {
                Debug.Log("Missing save file name!");
                return;
            }

            saveURL = Application.dataPath + savePath;
            if (!System.IO.Directory.Exists(saveURL))
            {
                Debug.Log("Not Exists!");
                System.IO.Directory.CreateDirectory(saveURL);
            }
        }
        #endregion

        #endregion

        // Methods
        #region
        // - Capture Once by Next Frame -
        #region
        private void OnPostRender()
        {
            if (takeScreenshotOnNextFrame)
            {
                takeScreenshotOnNextFrame = false;
                RenderTexture renderTexture = mainCamera.targetTexture;

                //Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
                Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
                Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
                renderResult.ReadPixels(rect, 0, 0);

                byte[] byteArray = renderResult.EncodeToPNG();
                string timeInString = System.DateTime.Now.ToString("_yyyyMMdd_HHmm");
                string filePath = saveURL + "/" + fileName + timeInString + ".png";
                System.IO.File.WriteAllBytes(filePath, byteArray);

                renderResult.Apply();
                Vector2 vector = new Vector2(renderTexture.width, renderTexture.height);
                Sprite sprite = Sprite.Create(renderResult, rect, vector);
                this.sprite = sprite;

                Debug.Log("Saved " + fileName + timeInString + ".png");

                RenderTexture.ReleaseTemporary(renderTexture);
                mainCamera.targetTexture = null;
            }
        }

        private void TakeScreenshot(int width, int height)
        {
            mainCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
            takeScreenshotOnNextFrame = true;
        }

        public static void TakeScreenshot_Static(/*int width, int height*/)
        {
            instance.TakeScreenshot(instance.mainCamera.scaledPixelWidth, instance.mainCamera.scaledPixelHeight);
        }
        #endregion

        // - Capture As Sprite Saved as PNG -
        #region
        public static void CaptureScreen()
        {
            //RenderTexture renderTexture = new RenderTexture(width, height, depth);
            RenderTexture renderTexture = RenderTexture.GetTemporary(instance.mainCamera.scaledPixelWidth, instance.mainCamera.scaledPixelHeight);
            //Rect rect = new Rect(0, 0, width, height);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);

            instance.mainCamera.targetTexture = renderTexture;
            instance.mainCamera.Render();

            RenderTexture currentRenderTexture = RenderTexture.active;
            RenderTexture.active = renderTexture;
            texture.ReadPixels(rect, 0, 0);
            texture.Apply();

            instance.mainCamera.targetTexture = null;
            RenderTexture.active = currentRenderTexture;
            Destroy(renderTexture);

            Sprite sprite = Sprite.Create(texture, rect, Vector2.zero);

            ArrayExtension.AddElement(ref instance.sprites, ref sprite);

            if (instance.enableSaveAsPNG)
            {
                instance.SavedAsPNG();
            }

            //return (capturedSprites.Length - 1);
        }

        private void SavedAsPNG()
        {
            Sprite sprite = sprites[0];
            Texture2D texture2D = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Rect r = sprite.textureRect;
            Color[] pixels = sprite.texture.GetPixels((int)r.x, (int)r.y, (int)r.width, (int)r.height);
            texture2D.SetPixels(pixels);
            texture2D.Apply();

            saveURL = Application.dataPath + savePath;
            if (!System.IO.Directory.Exists(saveURL))
            {
                Debug.Log("Not Exists!");
                System.IO.Directory.CreateDirectory(saveURL);
            }

            string timeInString = System.DateTime.Now.ToString("_yyyyMMdd_HHmm");
            string filePath = saveURL + "/" + fileName + timeInString + ".png";
            System.IO.File.WriteAllBytes(filePath, texture2D.EncodeToPNG());

            Debug.Log("Saved " + fileName + timeInString + ".png");
        }
        #endregion

        // - Capture As PNG -
        #region
        public static void CaptureAsPNG()
        {
            instance.saveURL = Application.dataPath + instance.savePath;
            if (!System.IO.Directory.Exists(instance.saveURL))
            {
                Debug.Log("Not Exists!");
                System.IO.Directory.CreateDirectory(instance.saveURL);
            }

            string timeInString = System.DateTime.Now.ToString("_yyyyMMdd_HHmmss");
            string filePath = instance.saveURL + "/" + instance.fileName + timeInString + ".png";

            ScreenCapture.CaptureScreenshot(filePath);

            FunctionTimer.Create(() =>
            {
                if (!System.IO.File.Exists(filePath))
                {
                    Debug.Log("File path not exists!");
                    return;
                }
                Texture2D texture2D = new Texture2D(2, 2);
                byte[] fileData = System.IO.File.ReadAllBytes(filePath);
                texture2D.LoadImage(fileData);
                Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0), 100f);
                ArrayExtension.AddElement(ref instance.sprites, ref sprite);
            }, 1f);
        }
        #endregion

        #endregion

        private void Awake()
        {
            // Instance
            #region
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
            #endregion

            mainCamera = this.GetComponent<Camera>();
            CheckSaveDetail();
        }
        private void Start()
        {

        }

        private void Update()
        {

        }
    }
}
