using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(CharacterInput))]
public class CharacterFarming : MonoBehaviour
{
  public Tile tilledTile;
  public Tile tilledPreviewTile;
  public Tilemap groundTilemap;
  public Tilemap tilledTilemap;
  public Tilemap tilledPreviewTilemap;
  private CharacterInput characterInput;

  public InputAction tillAction;

  private Vector3Int previousPos;
  private const float REACH = 0.65f;

  void Awake()
  {
    tillAction.performed += _ => Till();
    characterInput = gameObject.GetComponent<CharacterInput>();
  }

  void Update()
  {
    var pos = GetTarget();
    if (!pos.Equals(previousPos))
    {
      tilledPreviewTilemap.SetTile(previousPos, null);
      tilledPreviewTilemap.SetTile(pos, tilledPreviewTile);
      previousPos = pos;
    }
  }

  void Till()
  {
    var pos = GetTarget();
    var groundTile = groundTilemap.GetTile(pos);
    if (!groundTile)
      return;
    tilledTilemap.SetTile(pos, tilledTile);
  }

  Vector3Int GetTarget()
  {
    var adjustedPos = transform.position;
    switch (characterInput.direction)
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
    tillAction.Enable();
  }

  private void OnDisable()
  {
    tillAction.Disable();
  }
}
