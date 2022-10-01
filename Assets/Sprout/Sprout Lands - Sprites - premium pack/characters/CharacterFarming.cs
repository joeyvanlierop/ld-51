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
    var heldPlant = characterInventory.heldItem?.GetComponent<Plant>();
    if (!heldPlant)
      Till();
    else
      Plant(heldPlant);
  }

  void Till()
  {
    var target = GetTarget();
    var groundTile = groundTilemap.GetTile(target);
    if (!groundTile)
      return;
    tilledTilemap.SetTile(target, tilledTile);
  }

  void Plant(Plant plant)
  {
    var target = GetTarget();
    var tile = tilledTilemap.GetTile(target);
    if (!tile)
      return;
    plantManager.PlantPlant(plant, target);
    characterInventory.heldItem = null;
    plant.gameObject.SetActive(false);
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
