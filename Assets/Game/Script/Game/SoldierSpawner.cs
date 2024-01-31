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

    Dictionary<int, Soldier> Soldiers = new();
    bool built;

    private void Awake()
    {
        CostText.text = $"{MoneyCost}";
        // StartCoroutine(SpawningSolider());
        // CompleteBuild();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (built) return;
        if (other.CompareTag("Player"))
        {
            GameManager.Get.Pay(MoneyCost, Build);
        }
    }

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
        for (int i = 0; i < Grid.Points.Count; i++)
        {
            Soldiers.Add(i, null);
        }
        Debug.Log(Soldiers.Count);
        StartCoroutine(SpawningSolider());
    }

    int? GetSpawnPositionIndex()
    {
        for (int i = 0; i < Soldiers.Count; i++)
        {
            if (Soldiers[i] == null || Soldiers[i].IsDead)
            {
                return i;
            }
        }

        return null;
    }

    int? index;
    IEnumerator SpawningSolider()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            index = GetSpawnPositionIndex();
            if (index != null)
            {
                Soldiers[(int)index] = SpawnSoldier(Grid.Points[(int)index]);
            }
        }
    }

    Soldier SpawnSoldier(Vector3 position)
    {
        var soldier = Instantiate(Soldier, SpawnTransform.position, Soldier.transform.rotation, WorldSpace.Transform);
        soldier.InitializeState(true);
        soldier.ChangeState(soldier.MoveState.SetDestination(position));
        return soldier;
    }
}
