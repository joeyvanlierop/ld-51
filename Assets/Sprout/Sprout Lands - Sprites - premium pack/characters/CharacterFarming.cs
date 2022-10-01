using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterFarming : MonoBehaviour
{
  public Tile tilledTile;
  public Tile tilledPreviewTile;
  public Tilemap groundTilemap;
  public Tilemap tilledTilemap;
  public Tilemap tilledPreviewTilemap;
  private CharacterMovement characterMovement;

  public InputAction action;

  private Vector3Int previousPos;
  private const float REACH = 0.65f;
  private bool holdingItem = false;

  void Awake()
  {
    action.performed += _ => PerformAction();
    characterMovement = gameObject.GetComponent<CharacterMovement>();
  }

  void Update()
  {
    var target = GetTarget();
    if (!target.Equals(previousPos))
    {
      tilledPreviewTilemap.SetTile(previousPos, null);
      tilledPreviewTilemap.SetTile(target, tilledPreviewTile);
      previousPos = target;
    }
  }

  void PerformAction()
  {
    if (!holdingItem)
      Till();
    else
      Plant();
  }

  void Till()
  {
    var target = GetTarget();
    var groundTile = groundTilemap.GetTile(target);
    if (!groundTile)
      return;
    tilledTilemap.SetTile(target, tilledTile);
  }

  void Plant()
  {
    var target = GetTarget();
    var tile = tilledTilemap.GetTile(target);
    if (!tile)
      return;
    // tilledTilemap.SetTile(target, tilledTile);
  }

  Vector3Int GetTarget()
  {
    var adjustedPos = transform.position;
    switch (characterMovement.direction)
    {
      case Direction.UP:
        adjustedPos.y += REACH;
        break;
      case Direction.DOWN:
        adjustedPos.y -= REACH;
        break;
      case Direction.LEFT:
        adjustedPos.x -= REACH;
        break;
      case Direction.RIGHT:
        adjustedPos.x += REACH;
        break;
    }
    var pos = tilledTilemap.WorldToCell(adjustedPos);
    return pos;
  }

  private void OnEnable()
  {
    action.Enable();
  }

  private void OnDisable()
  {
    action.Disable();
  }
}
