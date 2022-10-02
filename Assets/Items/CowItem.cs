using UnityEngine;

public class CowItem : Item
{
  public GameObject cowObject;

  public override void Consume(Vector3Int target)
  {
    Instantiate(cowObject, target, Quaternion.identity);
    gameObject.SetActive(false);
  }
}
