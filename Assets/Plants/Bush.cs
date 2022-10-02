using UnityEngine;

public class Bush : IPlant
{
  public GameObject harvestItem;
  public float maxHarvestDropped = 3;
  public float minFruitTime = 4;
  public float maxFruitTime = 8;

  public override void Harvest(Vector3 pos)
  {
    var adjustedPos = new Vector3(pos.x + 0.5f, pos.y + 0.5f, pos.z);
    for (int i = 0; i < Random.Range(0, maxHarvestDropped); i++)
    {
      Burst(harvestItem, adjustedPos);
    }
  }
}
