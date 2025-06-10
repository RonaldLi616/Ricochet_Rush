using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Quest_Studio;

public class RicochetRush_LoaderUpdate : LoaderUpdate
{
    public override void Start()
    {
        LoadTargetScene();
    }

    public override void LoadTargetScene()
    {
        StartCoroutine(LoadAsynchronously());
    }

    public override IEnumerator LoadAsynchronously()
    {
        int sceneBuildIndex = RicochetRush_SceneLoader.GetTargetSceneInBuildIndex();
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneBuildIndex);
        while (!operation.isDone)
        {
            float progess = Mathf.Clamp01(operation.progress / .9f);

            loadingSlider.value = progess;

            yield return null;
        }
    }
}
