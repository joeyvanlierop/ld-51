using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class IPlant : MonoBehaviour
{
  public int currentStage = 0;
  public Tile[] stages;
  public int minGrowTime = 1;
  public int maxGrowTime = 5;

  public abstract void Harvest(Vector3 pos);

  protected void Burst(GameObject obj, Vector3 pos)
  {
    var newObj = Instantiate(obj, pos, Quaternion.identity);
    var rb = newObj.GetComponent<Rigidbody2D>();
    var force = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    force *= 10;
    rb.AddForce(force, ForceMode2D.Impulse);
  }
}
