using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Quest_Studio;

namespace Quest_Studio
{
    public class LoadingProgressIndicator : MonoBehaviour
    {
        // Instance
        #region
        private static LoadingProgressIndicator instance;
        public static LoadingProgressIndicator GetInstance() { return instance; }
        #endregion

        // Loading Text
        #region
        [Header("Loading Text Reference")]
        [SerializeField]
        private Text loadingText;
        private Text GetLoadingText()
        {
            if (loadingText == null)
            {
                Debug.Log("Missing loading text reference!");
                return null;
            }
            return loadingText;
        }
        public static void SetLoadingText(string text)
        {
            instance.GetLoadingText().text = text;
        }
        #endregion

        // Loading Bar
        #region
        [Header("Loading Bar Reference")]
        [SerializeField]
        private Slider loadingBarSlider;
        private Slider GetLoadingBarSlider()
        {
            if (loadingBarSlider == null)
            {
                Debug.Log("Missing loading bar slider reference!");
                return null;
            }
            return loadingBarSlider;
        }
        public static void SetLoadingBarSliderValue(float value) { instance.GetLoadingBarSlider().value = value; }
        public static void UpdateLoadingBarSliderValue(IProgress<float> progress) { progress.Report(instance.GetCompletionRate()); }
        public static void SetLoadingBarSliderActive(bool isActive) { instance.GetLoadingBarSlider().gameObject.SetActive(isActive); }
        #endregion

        // Variables
        #region
        [Header("Variables")]
        // - Task Number -
        #region
        private int tasksNumber = 0;
        public static int GetTasksNumber() { return instance.tasksNumber; }
        public static void SetTasksNumber(int tasksNumber) { instance.tasksNumber = tasksNumber; }
        #endregion

        // - Completed Count -
        #region
        private int completedCount = 0;
        public static int GetCompletedCount() { return instance.completedCount; }
        public static void SetCompletedCount(int completedCount) { instance.completedCount = completedCount; }
        public static void AddCompletedCount() { instance.completedCount++; }
        #endregion

        #endregion

        // Method
        #region
        // - Get Completion Rate -
        #region
        private float GetCompletionRate() { return (float)GetCompletedCount() / (float)GetTasksNumber() * 100f; }
        #endregion

        // - Reset To Default -
        #region
        public static void ResetToDefault()
        {
            SetTasksNumber(0);
            SetCompletedCount(0);
            SetLoadingText(string.Empty);
            SetLoadingBarSliderValue(0);
        }
        #endregion

        #endregion

        // Debug
        #region
        private async void DebugMethod()
        {
            Debug.Log("Start Debug Method!");
            var progress = new Progress<float>(value =>
            {
                SetLoadingBarSliderValue(value);
            });
            foreach (string url in imageUrls)
            {
                Task task = LoadMediaImageFromWeb(url, progress);
                ArrayExtension.AddElement(ref tasks, ref task);
            }
            await Task.WhenAll(tasks);
            await Task.Delay(1000);
            SetLoadingText("Completed!");
        }

        [SerializeField]
        private string[] imageUrls = new string[0];
        private Task[] tasks = new Task[0];
        [SerializeField]
        private Sprite[] sprites = new Sprite[0];

        // - Load Media Image From Web -
        #region
        private async Task LoadMediaImageFromWeb(string mediaUrl, IProgress<float> progress)
        {
            WWW www = new WWW(mediaUrl);
            string fileName = mediaUrl.Substring(mediaUrl.LastIndexOf("/") + 1, mediaUrl.Length - mediaUrl.LastIndexOf("/") - 1);
            while (!www.isDone)
            {
                await Task.Yield();
            }
            Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
            ArrayExtension.AddElement(ref sprites, ref sprite);
            www.Dispose();
            completedCount++;
            progress.Report(GetCompletionRate());
            SetLoadingText("Tasks: (" + completedCount + "/" + tasks.Length + ")");
            Debug.Log(fileName + " | (" + completedCount + "/" + tasks.Length + ")");
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
        }

        private void Start()
        {

        }

        private void Update()
        {
            // Debug
            #region
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DebugMethod();
            }
            #endregion
        }
    }
}