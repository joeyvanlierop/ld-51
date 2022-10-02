using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class boatDelivery : MonoBehaviour
{

    public List<GameObject> RequestedIemsBubbles = new List<GameObject>();
    public Animator animator;
    bool delivering = false;
    public List<GameObject> choicesPrefab = new List<GameObject>();
    List<GameObject> choices = new List<GameObject>();

    public List<Item> Items = new List<Item>();

    public GameObject Timer;

    GameObject timerRef;

    Rigidbody2D rb;

    public BoatControls boatControls;

    bool chose = false;

    public float ChooseDistance = 2f;

    GameObject player;


    public delegate void ChoiceCallbackType(GameObject choice);


    public ChoiceCallbackType ChoiceCallback;

    void Awake() {
        boatControls = new BoatControls();
    }

    void OnEnable() {
        boatControls.Enable();
    }

    void OnDisable() {
        boatControls.Disable();
    }


    void PerformAction() {

    }




    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        foreach(var prefab in choicesPrefab) {
            choices.Add(Instantiate(prefab));
        }
        SetBubbles();

        boatControls.Choose.ChooseLeft.performed += ChooseLeft;
        boatControls.Choose.ChooseRight.performed += ChooseRight;

        player = GameObject.FindGameObjectWithTag("Character");
    }

    void ChooseLeft(InputAction.CallbackContext context) {
        if (!AbleToChoose()) {
            return;
        }

        if (!GiveItem(Items[1])) {
            return;
        }


        

        var remove = choices[1];
        ChoiceCallback(remove);
        choices.RemoveAt(1);
        Destroy(remove);

        chose = true;
        HideItems();
    }

    private void ChooseRight(InputAction.CallbackContext context) {
        if (!AbleToChoose()) {
            return;
        }

        if (!GiveItem(Items[0])) {
            return;
        }

        var remove = choices[0];
        ChoiceCallback(remove);
        choices.RemoveAt(0);
        Destroy(remove);

        chose = true;
        HideItems();
    }


    bool GiveItem(Item item) {
        var inventory = player.GetComponent<CharacterInventory>();
        if (inventory.heldItem != null) {
            return false;
        }
        inventory.AttachItem(Instantiate(item));



        return true;
    }

    void HideItems() {
        for (int i = 0; i < choices.Count; i++) {
            choices[i].GetComponent<SpriteRenderer>().enabled = false;
        }

        for (int i = 0; i < RequestedIemsBubbles.Count; i++) {
            RequestedIemsBubbles[i].GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    bool AbleToChoose() {
        if (!(delivering && !chose)) {
            return false;
        }
        if (Vector2.Distance(player.transform.position, rb.transform.position) > ChooseDistance) {
            return false;
        }

        return true;
    }

    void SetBubbles() {
        for (int i = 0; i < choices.Count; i++) {
            choices[i].transform.SetParent(RequestedIemsBubbles[i].transform);
            choices[i].transform.localPosition = new Vector2(0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetBool("isCruisin")) {
            if (!delivering) {
                delivering = true;
                StartDelivering();
            }
        } else {
            if (delivering) {
                delivering = false;
                StopDelivering();
            }
        }
    }


    void StartDelivering() {
        for (int i = 0; i < choices.Count; i++) {
            choices[i].GetComponent<SpriteRenderer>().enabled = true;
            RequestedIemsBubbles[i].GetComponent<SpriteRenderer>().enabled = true;
        }
        timerRef = Instantiate(Timer, new Vector2(rb.transform.position.x, rb.transform.position.y - 1.5f), Quaternion.identity);
    }

    void StopDelivering() {
        HideItems();
        Destroy(timerRef);
    }

    public void DeleteAll() {
        foreach(var choice in choices) {
            Destroy(choice);
        }
    }
}
