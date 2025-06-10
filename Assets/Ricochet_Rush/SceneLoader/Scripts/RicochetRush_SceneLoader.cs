using UnityEngine.SceneManagement;

public static class RicochetRush_SceneLoader
{
    public enum Scene
    {
        RicochetRush_Loading,
        Menu,
        TestingGround
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

            case Scene.TestingGround:
                buildIndex = 2;
                break;
        }
        return buildIndex;
    }

    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(Scene.RicochetRush_Loading.ToString());

        targetScene = scene;
    }

    public static void LoadTargetScene()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
