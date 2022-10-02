using UnityEngine;
using UnityEngine.Tilemaps;

public class Plant : MonoBehaviour
{
  public int currentStage = 0;
  public Tile[] stages;
  public GameObject harvestItem;
  public string harvestSeedName;
  private GameObject harvestSeed;
  public int minGrowTime = 1;
  public int maxGrowTime = 5;
  public float maxHervestDropped = 1;
  public float maxSeedsDropped = 3;

  void Awake()
  {
    harvestSeed = (GameObject)Resources.Load(harvestSeedName);
  }

  public void Harvest(Vector3 pos)
  {
    var adjustedPos = new Vector3(pos.x + 0.5f, pos.y + 0.5f, pos.z);
    for (int i = 0; i < Random.Range(1, maxSeedsDropped); i++)
    {
      Burst(harvestItem, adjustedPos);
    }
    for (int i = 0; i < Random.Range(1, maxSeedsDropped); i++)
    {
      Burst(harvestSeed, adjustedPos);
    }
  }

  void Burst(GameObject obj, Vector3 pos)
  {
    var newObj = Instantiate(obj, pos, Quaternion.identity);
    var rb = newObj.GetComponent<Rigidbody2D>();
    var force = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    force *= 10;
    rb.AddForce(force, ForceMode2D.Impulse);
  }
}
