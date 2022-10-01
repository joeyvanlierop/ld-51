using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantManager : MonoBehaviour
{


  private Tilemap plantTilemap;
  //   private List<(Plant, Vector3Int)> plants = new List<(Plant, Vector3Int)>();

  void Awake()
  {
    plantTilemap = gameObject.GetComponent<Tilemap>();
  }

  public void PlantPlant(Plant plant, Vector3Int pos)
  {
    // plants.Add((plant, pos));
    StartCoroutine(StartGrowing(plant, pos));
  }

  IEnumerator StartGrowing(Plant plant, Vector3Int pos)
  {
    plantTilemap.SetTile(pos, plant.stages[0]);
    while (plant.currentStage < plant.stages.Length)
    {
      var growTime = Random.Range(1, 5);
      yield return new WaitForSeconds(growTime);
      plant.currentStage += 1;
      plantTilemap.SetTile(pos, plant.stages[plant.currentStage]);
    }
  }
}
