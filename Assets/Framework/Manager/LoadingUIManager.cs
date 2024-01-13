using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;

public class LoadingUIManager : MonoBehaviour
{
    [Range(0f, 6f)] public float loadTime;

    [Header("---UI REFERENCES---")]
    public Slider loadingVisual;

    bool isAnimated;
    void Awake()
    {
        Global.SetScreen(SCREEN.LOADING);
    }

    private void Start()
    {
        // StartCoroutine(LoadingHomeScene("Home"));
        InitializeScenes();
        AnimateLoadingBar();
    }

    void AnimateLoadingBar()
    {
        loadingVisual.value = 0f;
        loadingVisual.maxValue = loadTime;

        float delay = 0f;
        float delayTime = loadTime * 0.2f;
        delay = UnityEngine.Random.Range(0.2f, 0.6f);

        Sequence sq = DOTween.Sequence();
        sq.Append(loadingVisual.DOValue(delay * loadingVisual.maxValue, loadTime * 0.6f).SetEase(Ease.OutSine));
        sq.AppendInterval(delay);
        sq.Append(loadingVisual.DOValue(loadingVisual.maxValue, loadTime * 0.4f).SetEase(Ease.OutSine));
        // sq.AppendInterval(1f);
        sq.AppendCallback(OnDone);

    }

    void OnDone() => isAnimated = true;

    async void InitializeScenes()
    {
        await InitializingScenes();
    }

    async Task InitializingScenes()
    {
        var initTask = ScreenManager.Get.Initializing();

        while (!isAnimated || !initTask.IsCompleted)
        {
            await Task.Yield();
        }

        var unloadOperation = SceneManager.UnloadSceneAsync("Loading");
        while (!unloadOperation.isDone)
        {
            await Task.Yield();
        }

        ScreenManager.LoadScene("Home");
    }
}
