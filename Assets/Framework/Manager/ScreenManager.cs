using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Threading.Tasks;

public class ScreenManager : MonoSingleton<ScreenManager>
{
    public string[] Scenes;

    static readonly Dictionary<string, GameObject> RootDictionary = new();
    // public List<GameObject> Roots;

    static GameObject lastRootActived = null;
    public async Task Initializing()
    {
        // Task[] tasks = new Task[Scenes.Length];
        for (int i = 0; i < Scenes.Length; i++)
        {
            await LoadSceneAsync(Scenes[i]);
        }

        // await Task.WhenAll(tasks);
    }

    async Task LoadSceneAsync(string sceneName)
    {
        var ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!ao.isDone) await Task.Yield();

        var sceneRoot = SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[0];

        RootDictionary.Add(sceneName, sceneRoot);
        // #if UNITY_EDITOR
        Debug.Log($"Scene <size=12><color=#00FF20>[{sceneName.ToUpper()}]</color></size> has been loaded successfully!");
        // #endif
        // Roots.Add(sceneRoot);
    }

    public static void LoadScene(string sceneName)
    {
        if (lastRootActived != null)
        {
            lastRootActived.SetActive(false);
        }

        lastRootActived = RootDictionary[sceneName];
        lastRootActived.SetActive(true);
    }

    public static void ReloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
        var ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        ao.completed += (ao) =>
        {
            var sceneRoot = SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[0];
            RootDictionary[sceneName] = sceneRoot;
            LoadScene(sceneName);
        };
    }
}
