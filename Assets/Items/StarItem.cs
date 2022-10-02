public class StarItem : Item
{
  public override void Consume()
  {
    gameObject.SetActive(false);
  }
}
