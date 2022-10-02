using UnityEngine;

public class StarItem : Item
{
  public override void Consume(Vector3Int _)
  {
    gameObject.SetActive(false);
  }


}
