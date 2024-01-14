using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;

public class Grid : MonoBehaviour
{
    public Transform dot;
    public GridConfig DefaultConfig;
    public List<Vector3> Points = new();
    public Dictionary<int, int> indexVolume = new();
    readonly Dictionary<string, int> indexDictionary = new();

    // private void Awake()
    // {
    //     InitGrid(DefaultConfig);
    // }

    public void Initialize(GridConfig config = null)
    {
        CreateNewGrid(config ?? DefaultConfig);
    }

    void CreateNewGrid(GridConfig config)
    {
        float x, z;
        Points = new();

        for (int j = config.Height - 1; j >= 0; j--)
        {
            for (int i = 0; i < config.Width; i++)
            {
                x = i * config.Spacing.x - ((config.Width * config.Spacing.x - config.Spacing.x) / 2);
                z = j * config.Spacing.y - (config.Height * config.Spacing.y - config.Spacing.y) / 2;

                Points.Add(new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z));
                Instantiate(dot, new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z), Quaternion.Euler(new Vector2(90, 0)), transform);
            }
        }
    }

    int currentIndex = 0;
    public Vector3 GetNextGridPosition(string key)
    {
        Vector3 result;

        result = Points[currentIndex];
        if (currentIndex + 1 < Points.Count)
        {
            if (indexDictionary.ContainsKey(key))
            {
                result = Points[indexDictionary[key]];
                indexVolume[indexDictionary[key]]++;
            }
            else
            {
                result = Points[currentIndex];
                indexVolume.Add(currentIndex, 1);
                indexDictionary.Add(key, currentIndex);
                currentIndex++;
            }
        }

        return result;
    }
}

[System.Serializable]
public class GridConfig
{
    public int Width;
    public int Height;
    public Vector2 Spacing;

    public GridConfig(int width, int height, Vector2 spacing)
    {
        Width = width;
        Height = height;
        Spacing = spacing;
    }
}

