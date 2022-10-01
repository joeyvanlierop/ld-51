using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class CharacterFarming : MonoBehaviour
{
  public RuleTile tilledTile;
  public Tilemap groundTilemap;
  public Tilemap tilledTilemap;

  public InputAction tillAction;

  void Awake()
  {
    tillAction.performed += _ => Till();
  }

  void OnUpdate()
  {
    var pos = groundTilemap.WorldToCell(transform.position);
    var groundTile = groundTilemap.GetTile(pos);
  }

  void Till()
  {
    var pos = tilledTilemap.WorldToCell(transform.position);
    for (int yOffset = -1; yOffset <= 1; yOffset++)
    {
      for (int xOffset = -1; xOffset <= 1; xOffset++)
      {
        Debug.Log(xOffset + " " + yOffset);
        var newPos = new Vector3Int(pos.x + xOffset, pos.y + yOffset);
        tilledTilemap.SetTile(newPos, tilledTile);
      }
    }
  }

  private void OnEnable()
  {
    tillAction.Enable();
  }

  private void OnDisable()
  {
    tillAction.Disable();
  }
}
