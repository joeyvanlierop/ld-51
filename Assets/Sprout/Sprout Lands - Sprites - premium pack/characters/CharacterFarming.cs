using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class CharacterFarming : MonoBehaviour
{
  public Tilemap groundTilemap;
  public Tilemap tilledTilemap;

  public InputAction tillAction;

  void Awake()
  {
    tillAction.performed += _ => Till();
  }

  void Till()
  {
    var pos = groundTilemap.WorldToCell(transform.position);
    var tile1 = groundTilemap.GetTile(pos);
    var tile2 = tilledTilemap.GetTile(pos);
    Debug.Log("Till: " + tile1.name);
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
