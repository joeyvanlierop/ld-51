using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantManager : MonoBehaviour
{


  private Tilemap plantTilemap;
  private Dictionary<Vector3Int, Plant> plants = new Dictionary<Vector3Int, Plant>();

  void Awake()
  {
    plantTilemap = gameObject.GetComponent<Tilemap>();
  }

  public bool Plant(Plant plant, Vector3Int pos)
  {
    if (HasPlant(pos))
      return false;
    plants.Add(pos, plant);
    plant.transform.SetParent(transform);
    StartCoroutine(StartGrowing(plant, pos));
    return true;
  }

  IEnumerator StartGrowing(Plant plant, Vector3Int pos)
  {
    plantTilemap.SetTile(pos, plant.stages[0]);
    while (plant.currentStage < plant.stages.Length - 1)
    {
      var growTime = Random.Range(plant.minGrowTime, plant.maxGrowTime);
      Debug.Log(plant.name + ": " + growTime);
      yield return new WaitForSeconds(growTime);
      plant.currentStage += 1;
      plantTilemap.SetTile(pos, plant.stages[plant.currentStage]);
    }
  }

  public void Harvest(Vector3Int pos)
  {
    var plant = plants[pos];
    if (!plant)
      return;
    if (plant.currentStage != plant.stages.Length - 1)
      return;
    plantTilemap.SetTile(pos, null);
    plant.Harvest(pos);
    plants.Remove(pos);
  }

  internal bool HasPlant(Vector3Int pos)
  {
    return plants.ContainsKey(pos);
  }
}
