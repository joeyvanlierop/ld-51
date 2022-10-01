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
  private CharacterInventory characterInventory;
  public PlantManager plantManager;

  public InputAction action;

  private Vector3Int previousPos;
  private const float REACH = 0.65f;

  void Awake()
  {
    action.performed += _ => PerformAction();
    characterMovement = gameObject.GetComponent<CharacterMovement>();
    characterInventory = gameObject.GetComponent<CharacterInventory>();
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
    var target = GetTarget();
    var heldPlant = characterInventory.heldItem?.GetComponent<Plant>();
    var isTilled = tilledTilemap.HasTile(target);
    var isPlanted = plantManager.HasPlant(target);
    if (heldPlant)
      Plant(heldPlant, target);
    else if (!isTilled && !isPlanted)
      Till(target);
    else if (isPlanted)
      Harvest(target);
  }

  void Till(Vector3Int target)
  {
    var groundTile = groundTilemap.GetTile(target);
    if (!groundTile)
      return;
    tilledTilemap.SetTile(target, tilledTile);
  }

  void Plant(Plant plant, Vector3Int target)
  {
    var tile = tilledTilemap.GetTile(target);
    if (!tile)
      return;
    if (plantManager.Plant(plant, target))
    {
      characterInventory.heldItem = null;
      plant.gameObject.SetActive(false);
      tilledTilemap.SetTile(target, null);
    }
  }

  void Harvest(Vector3Int target)
  {
    tilledTilemap.SetTile(target, null);
    plantManager.Harvest(target);
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
    var tile_pos = tilledTilemap.CellToWorld(pos);
    // Debug.Log(transform.position);
    // Debug.Log(pos);
    // Debug.Log(tile_pos);
    // Debug.Log(Vector2.Distance(transform.position, tile_pos));
    // RaycastHit2D hit = Physics2D.Raycast(transform.position, adjustedPos, Vector2.Distance(transform.position, adjustedPos));
    // if (hit.collider != null)
    // {
    //   Debug.Log(hit.collider.tag);
    // }
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
