using UnityEngine;

public class StarItem : Item
{
  
  void Awake() {
    Event myTrigger = new Event();
  }
  public override void Consume(Vector3Int _)
  {
    Event.Instance.Eat();
    gameObject.SetActive(false);
  }


}
