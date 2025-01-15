using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HybridationSystem : MonoBehaviour
{
    public class Plant
    {
        public string PlantType;
        public bool IsHybrid;
    }

    public class Crop
    {
        public Plant CurrentPlant;
        public Vector2Int Position;
    }

    public int Width;
    public int Height;
    public Crop[,] Crops;

    void Start()
    {
        Crops = new Crop[Width, Height];
        InitializeCrops();
    }

    void InitializeCrops()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Crops[x, y] = new Crop
                {
                    Position = new Vector2Int(x, y),
                    CurrentPlant = null
                };
            }
        }
    }

    public void CheckForHybridization(Vector2Int position)
    {
        Crop crop = Crops[position.x, position.y];
        if (crop.CurrentPlant == null) return;

        List<Crop> neighbors = GetNeighbors(position);

        foreach (Crop neighbor in neighbors)
        {
            if (neighbor.CurrentPlant != null &&
                neighbor.CurrentPlant.PlantType == crop.CurrentPlant.PlantType &&
                !neighbor.CurrentPlant.IsHybrid)
            {
                TryHybridize(neighbors);
            }
        }
    }

    void TryHybridize(List<Crop> neighbors)
    {
        foreach (Crop emptyCrop in neighbors)
        {
            if (emptyCrop.CurrentPlant == null)
            {
                float chance = 1f;
                if (Random.value < chance)
                {
                    emptyCrop.CurrentPlant = new Plant
                    {
                        PlantType = "Hybrid " + neighbors[0].CurrentPlant.PlantType,
                        IsHybrid = true
                    };
                    Debug.Log($"Une plante hybride a poussé à la position {emptyCrop.Position}");
                    return;
                }
            }
        }
    }

    public List<Crop> GetNeighbors(Vector2Int position)
    {
        List<Crop> neighbors = new List<Crop>();

        Vector2Int[] directions = {
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0)
    };

        foreach (Vector2Int dir in directions)
        {
            Vector2Int neighborPos = position + dir;
            if (neighborPos.x >= 0 && neighborPos.x < Width &&
                neighborPos.y >= 0 && neighborPos.y < Height)
            {
                neighbors.Add(Crops[neighborPos.x, neighborPos.y]);
            }
        }

        return neighbors;
    }
}