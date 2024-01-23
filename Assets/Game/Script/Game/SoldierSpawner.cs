using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class SoldierSpawner : MonoBehaviour
{
    [Header("--- CONFIG ---")]
    public int MoneyCost;
    public int BuildTime;

    [Space(8)]
    [Header("--- REFERENCES ---")]
    public Soldier Soldier;
    public Grid Grid;
    public Transform SpawnTransform;
    public GameObject DetectArea, Tent;
    public TMP_Text CostText;
    public Slider Progress;

    bool built;

    private void Awake()
    {
        CostText.text = $"{MoneyCost}";
        // StartCoroutine(SpawningSolider());
        CompleteBuild();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (built) return;
        if (other.CompareTag("Player"))
        {
            GameManager.Get.Pay(MoneyCost, Build);
        }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (upgraded) return;
    //     if (other.CompareTag("Player"))
    //     {
    //         Debug.Log(2);
    //     }
    // }

    void Build()
    {
        Debug.Log($"[[{name}] Building...");
        built = true;
        CostText.text = "Progess...";
        Progress.DOValue(Progress.maxValue, BuildTime).SetEase(Ease.Linear).OnComplete(CompleteBuild);
    }

    void CompleteBuild()
    {
        Debug.Log($"[[{name}] Build Complete!");
        DetectArea.SetActive(false);
        Tent.SetActive(true);
        Grid.Initialize();
        StartCoroutine(SpawningSolider());
    }

    IEnumerator SpawningSolider()
    {
        yield return new WaitForSeconds(2f);

        foreach (var position in Grid.Points)
        {
            yield return new WaitForSeconds(1f);
            var soldier = Instantiate(Soldier, SpawnTransform.position, Soldier.transform.rotation, WorldSpace.Transform);
            soldier.InitializeState(true);
            soldier.ChangeState(soldier.MoveState.SetDestination(position));
        }
    }
}
