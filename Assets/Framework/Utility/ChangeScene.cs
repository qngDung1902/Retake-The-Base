#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

public class ChangeScene : Editor
{
    [MenuItem("Open Scene/Loading #1")]
    public static void OpenLoading()
    {
        OpenScene("Loading");
    }

    [MenuItem("Open Scene/Home #2")]
    public static void OpenHome()
    {
        OpenScene("Home");
    }

    [MenuItem("Open Scene/Gameplay #3")]
    public static void OpenGameplay()
    {
        OpenScene("Gameplay");
    }
    private static void OpenScene(string sceneName)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Game/Scene/" + sceneName + ".unity");
        }
    }
}
#endif