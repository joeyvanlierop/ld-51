using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class CharacterFarming : MonoBehaviour
{
  public Tile tilledTile;
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
    var groundTile = groundTilemap.GetTile(pos);
    if (!groundTile)
      return;
    tilledTilemap.SetTile(pos, tilledTile);
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
