using UnityEngine;

public class Plant : IPlant
{
  public GameObject harvestItem;
  public string harvestSeedName;
  private GameObject harvestSeed;
  public float maxHarvestDropped = 1;
  public float maxSeedsDropped = 3;

  void Awake()
  {
    harvestSeed = (GameObject)Resources.Load(harvestSeedName);
  }

  public override void Harvest(Vector3 pos)
  {
    var adjustedPos = new Vector3(pos.x + 0.5f, pos.y + 0.5f, pos.z);
    for (int i = 0; i < Random.Range(0, maxHarvestDropped); i++)
    {
      Burst(harvestItem, adjustedPos);
    }
    for (int i = 0; i < Random.Range(0, maxSeedsDropped); i++)
    {
      Burst(harvestSeed, adjustedPos);
    }
  }
}
