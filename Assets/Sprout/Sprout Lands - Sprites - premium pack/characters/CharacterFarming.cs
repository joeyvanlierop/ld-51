using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterFarming : MonoBehaviour
{
  public List<TileBase> blacklistedTiles = new List<TileBase>();
  public Tile tilledTile;
  public Tile tilledPreviewTile;
  public Tilemap groundTilemap;
  public Tilemap hillTilemap;
  public Tilemap tilledTilemap;
  public Tilemap bridgeTilemap;
  public Tilemap tilledPreviewTilemap;
  private CharacterMovement characterMovement;
  private CharacterInventory characterInventory;
  public PlantManager plantManager;
  public GameStateManager gameStateManager;
  private Animator animator;

  public InputAction action;

  private Vector3Int previousPos;
  private const float REACH = 0.65f;
  public GameObject milk;

  void Awake()
  {
    action.performed += _ => PerformAction();
    characterMovement = gameObject.GetComponent<CharacterMovement>();
    characterInventory = gameObject.GetComponent<CharacterInventory>();
    animator = gameObject.GetComponent<Animator>();
  }

  void Update()
  {
    if (gameStateManager.gameState == GameState.MAIN_MENU)
      return;

    var target = GetTarget();
    if (!target.Equals(previousPos))
    {
      tilledPreviewTilemap.SetTile(previousPos, null);
      if (IsValidTarget(target))
        tilledPreviewTile.color = Color.white;
      else
        tilledPreviewTile.color = Color.red;
      tilledPreviewTilemap.SetTile(target, tilledPreviewTile);
      previousPos = target;
    }
  }

  bool IsValidTarget(Vector3Int target)
  {
    var groundTile = groundTilemap.GetTile(target);
    var hillTile = hillTilemap.GetTile(target);
    var bridgeTile = bridgeTilemap.GetTile(target);
    if (bridgeTile && hillTile)
      return false;
    if (!hillTile && !groundTile)
      return false;
    if (hillTile && !blacklistedTiles.Contains(hillTile))
      return true;
    if (!hillTile && !blacklistedTiles.Contains(groundTile))
      return true;
    return false;
  }

  void PerformAction()
  {
    if (gameStateManager.gameState == GameState.MAIN_MENU)
      return;

    var target = GetTarget();
    var heldItem = characterInventory.heldItem?.GetComponent<Item>();
    var heldPlant = characterInventory.heldItem?.GetComponent<IPlant>();
    var isTilled = tilledTilemap.HasTile(target);
    var isPlanted = plantManager.HasPlant(target);

    Debug.Log("TEST1");
    if (!heldItem)
    {
      var colliders = Physics2D.OverlapCircleAll(transform.position, REACH);
      foreach (var collider in colliders)
      {
        if (collider.tag == "Cow")
        {
          var milkObj = Instantiate(milk, transform).GetComponent<Item>();
          characterInventory.AttachItem(milkObj);
          return;
        }
      }
    }

    if (heldItem && heldItem.consumable)
    {
      heldItem.Consume(target);
      characterInventory.heldItem = null;
    }
    else if (heldPlant && isTilled && Plant(heldPlant, target))
      return;
    else if (!isTilled && !isPlanted)
      Till(target);
    else if (isPlanted)
      Harvest(target);
  }

  void Till(Vector3Int target)
  {
    if (!IsValidTarget(target))
      return;

    animator.SetTrigger("tilling");
  }

  void TillCallback()
  {
    var target = GetTarget();
    tilledTilemap.SetTile(target, tilledTile);
  }

  bool Plant(IPlant plant, Vector3Int target)
  {
    var hillTile = hillTilemap.GetTile(target);
    Debug.Log(hillTile);
    Debug.Log(plant.GetComponent<Bush>());
    if (plant.GetComponent<Bush>() && !hillTile)
      return false;

    if (plantManager.Plant(plant, target))
    {
      characterInventory.heldItem = null;
      plant.gameObject.SetActive(false);
      tilledTilemap.SetTile(target, null);
      return true;
    }
    return false;
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
