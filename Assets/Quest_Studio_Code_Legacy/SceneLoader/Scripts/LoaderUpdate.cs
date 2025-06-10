using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Quest_Studio
{
    public class LoaderUpdate : MonoBehaviour
    {
        // Components
        #region
        [Header("Components")]
        public Slider loadingSlider;
        #endregion

        public virtual void Start()
        {
            LoadTargetScene();
        }

        public virtual void LoadTargetScene()
        {
            StartCoroutine(LoadAsynchronously());
        }

        public virtual IEnumerator LoadAsynchronously()
        {
            int sceneBuildIndex = Loader.GetTargetSceneInBuildIndex();
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneBuildIndex);
            while (!operation.isDone)
            {
                float progess = Mathf.Clamp01(operation.progress / .9f);

                loadingSlider.value = progess;

                yield return null;
            }
        }
    }
}