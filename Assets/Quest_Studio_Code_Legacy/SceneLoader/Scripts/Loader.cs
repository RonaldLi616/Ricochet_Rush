using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        Loading,
        Menu,
        InGame
    }

    private static Scene targetScene;
    public static Scene GetTargetScene() { return targetScene; }
    public static int GetTargetSceneInBuildIndex()
    {
        int buildIndex = -1;
        switch (targetScene)
        {
            case Scene.Menu:
                buildIndex = 1;
                break;

            case Scene.InGame:
                buildIndex = 2;
                break;
        }
        return buildIndex;
    }

    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(Scene.Loading.ToString());

        targetScene = scene;
    }

    public static void LoadTargetScene()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
