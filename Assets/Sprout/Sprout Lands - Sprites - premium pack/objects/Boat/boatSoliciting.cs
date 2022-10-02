using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class boatSoliciting : MonoBehaviour
{

    public Dictionary<GameObject, int> wantedItems = new Dictionary<GameObject, int>();
    Dictionary<GameObject, List<GameObject>> displayedItems = new Dictionary<GameObject, List<GameObject>>();
    public Animator animator;
    public List<GameObject> RequestedIemsBubbles = new List<GameObject>();
    public GameObject RequestedIemsBubble;
    bool soliciting = false;


    public Rigidbody2D rb;

    public GameObject Timer;

    public Event myTrigger;

    float timeout = 2f;
    float time = 0;

    GameObject timerRef;
    public enum SolicitingState {
        SUCCESS,
        FAIL
    }

    public delegate void EndSolicitingCallbackType(SolicitingState state);


    public EndSolicitingCallbackType EndSolicitingCallback;

    void Awake() {
        myTrigger = new Event();
        Event.Instance.EatCallbacks.Add(ResetTimer);
    }

    void ResetTimer() {
        if (timerRef != null) {
            timerRef.GetComponent<Animator>().Play("time", -1, 0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {   
        rb = gameObject.GetComponent<Rigidbody2D>();
        foreach (var wantedItem in wantedItems) {
            List<GameObject> items = new List<GameObject>();
            for(int i = 0; i < wantedItem.Value; i++) {
                items.Add(Instantiate(wantedItem.Key));
            }
            displayedItems.Add(wantedItem.Key ,items);
        }
        SetWantBubble();
    }

    void SetWantBubble() {
        int totalItems = 0;
        Vector2 iconSize = new Vector2(0,0);
        foreach (var wantedItemPair in wantedItems) {
            iconSize = wantedItemPair.Key.GetComponent<SpriteRenderer>().bounds.size;
            totalItems += wantedItemPair.Value;
        }
        float size_X = iconSize.x * totalItems;
        Vector2 newSize = new Vector2(size_X, RequestedIemsBubbles[0].GetComponent<SpriteRenderer>().bounds.size.y);
        Vector2 betterSize = new Vector2(newSize.x + iconSize.x, newSize.y);
        RequestedIemsBubble.GetComponent<SpriteRenderer>().size = betterSize;
        RequestedIemsBubble.transform.localPosition = new Vector2(betterSize.x / 2 + iconSize.x / 1.5f, 0);

        int i = 0;
        foreach (var pair in displayedItems) {
            var wantedItemList = pair.Value;
            foreach (var wantedItem in wantedItemList) {
                wantedItem.transform.SetParent(RequestedIemsBubble.transform);
                wantedItem.transform.localPosition =  new Vector2((newSize.x / 2) - i - iconSize.x / 2, 0);
                i++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (time < timeout) {
            time += Time.deltaTime;
        }
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
        foreach (var pair in displayedItems) {
            foreach (var item in pair.Value) {
                item.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        timerRef = Instantiate(Timer, new Vector2(rb.transform.position.x, rb.transform.position.y - 1.5f), Quaternion.identity);
    }

    void StopSoliciting() {
        RequestedIemsBubble.GetComponent<SpriteRenderer>().enabled = false;
        foreach (var pair in displayedItems) {
            foreach (var item in pair.Value) {
                item.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        Destroy(timerRef);
        if (time < timeout) {
            return;
        }
        if (displayedItems.Count == 0) {
            EndSolicitingCallback(SolicitingState.SUCCESS);
        } else {
            EndSolicitingCallback(SolicitingState.FAIL);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Item") {
            bool found = false;
            GameObject key = new GameObject();

            foreach (var wantedItemPair in wantedItems) {
                if (collision.gameObject.name == wantedItemPair.Key.name) {
                    found = true;
                    key = wantedItemPair.Key;
                }

                Destroy(collision.gameObject);
            }
            if (found){
                wantedItems[key]--;
                RemoveItem(key);
                SetWantBubble();
            }
        }
    }

    void RemoveItem(GameObject key) {
        var remove = displayedItems[key].Last();
        remove.GetComponent<SpriteRenderer>().enabled = false;
        displayedItems[key].RemoveAt(displayedItems[key].Count - 1);
        Destroy(remove);
        if (wantedItems[key] == 0) {
            displayedItems.Remove(key);
            wantedItems.Remove(key);
        }
    }

    public void DeleteAll() {
        foreach (var pair in displayedItems) {
            foreach (var item in pair.Value) {
                Destroy(item);
            }
        }

        foreach (var pair in wantedItems)
        {
            Destroy(pair.Key);
        }
    }

}
