using UnityEngine;
using UnityEngine.Tilemaps;

public class Plant : MonoBehaviour
{
  public int currentStage = 0;
  public Tile[] stages;
  public Time lastGrow;
}
