using UnityEngine.SceneManagement;
using System.Collections.ObjectModel;

public static class SceneChanger
{
    private const string STAGE_NAME = "stage";
    private const string MENU_NAME = "menu";

    public static readonly int StageCount = 0;
    private static readonly ReadOnlyCollection<string> _sceneNames;
    
    static SceneChanger()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        string[] sceneNames = new string[sceneCount];
        for (int i = 0; i < sceneCount; i++)
        {
            sceneNames[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            if (sceneNames[i].Contains(STAGE_NAME))
            {
                StageCount++;
            }
        }
        _sceneNames = new ReadOnlyCollection<string>(sceneNames);
    }

    public static int SceneCount
    {
        get
        {
            return SceneManager.sceneCountInBuildSettings;
        }
    }

    public static void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void SwitchToStage()
    {
        SceneManager.LoadScene(STAGE_NAME);
    }

    public static void SwitchToStage(int stageNumber)
    {
        SceneManager.LoadScene(STAGE_NAME + stageNumber);
    }

    public static void SwitchToMenu()
    {
        SceneManager.LoadScene(MENU_NAME);
    }



}
