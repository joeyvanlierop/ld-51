using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatSoliciting : MonoBehaviour
{

    public Dictionary<GameObject, int> wantedItems = new Dictionary<GameObject, int>();
    public Animator animator;
    public List<GameObject> RequestedIemsBubbles = new List<GameObject>();
    public GameObject RequestedIemsBubble;
    bool soliciting = false;

    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {   
        rb = gameObject.GetComponent<Rigidbody2D>();
        SetWantBubble();
    }

    void SetWantBubble() {
        int distinctItems = wantedItems.Count;
        Vector2 size = RequestedIemsBubble.GetComponent<SpriteRenderer>().bounds.size;
        int i = 0;
        foreach (var wantedItemPair in wantedItems) {
            var wantedItem = wantedItemPair.Key;
            wantedItem.transform.SetParent(RequestedIemsBubble.transform);
            if (distinctItems == 1) {
                wantedItem.transform.localPosition =  new Vector2(0, 0);
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetBool("isCruisin")) {
            if (!soliciting) {
                soliciting = true;
                StartSoliciting();
            }
        } else {
            if (soliciting) {
                soliciting = false;
                StopSoliciting();
            }
        }

        
    }

    void StartSoliciting() {
        RequestedIemsBubble.GetComponent<SpriteRenderer>().enabled = true;
        foreach (var wantedItemPair in wantedItems) {
            wantedItemPair.Key.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void StopSoliciting() {
        RequestedIemsBubble.GetComponent<SpriteRenderer>().enabled = false;
        foreach (var wantedItemPair in wantedItems) {
            wantedItemPair.Key.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Item") {
            bool found = false;
            GameObject key = new GameObject();

            foreach (var wantedItemPair in wantedItems) {
                if (true) {
                    found = true;
                    key = wantedItemPair.Key;
                }

                Destroy(collision.gameObject);
            }
            if (found){
                wantedItems[key]--;
                if (wantedItems[key] == 0) {
                    
                    wantedItems.Remove(key);
                }
                SetWantBubble();
            }
            Debug.Log("Here");
        }
    }

}
