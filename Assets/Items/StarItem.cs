using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarItem : Item
{
  public override void Consume()
  {
    gameObject.SetActive(false);
  }
}
