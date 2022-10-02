using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoatManager : MonoBehaviour
{
    List<GameObject> Boats = new List<GameObject>();
    public List<GameObject> PossibleSolicitingItems = new List<GameObject>();
    public GameObject BoatPrefab;
    public GameObject DeliveryBoatPrefab;

    public List<GameObject> ChoicesPrefab = new List<GameObject>();

    public List<Item> ChoicesItemsPrefab = new List<Item>();

    Dictionary<GameObject, Item> CurrentPossibleChoices = new Dictionary<GameObject, Item>();

    List<GameObject> CurrentPossibleSolicitingItems = new List<GameObject>();

    float betweenBoatTime = 10f;

    public int MaxBoats = 1;

    public float MinBoatX = -18f;

    bool boatTimerStarted = false;
    public GameObject BetweenBoatTimer;
    GameObject TimerRef;
    float startTime = 11f;

    int rounds = 0;

    int maxItems = 5;



    public Vector3 SpawnLocation = new Vector3(13.29f, -2.36f, 0.02834536f);
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ChoicesPrefab.Count; i++) {
            CurrentPossibleChoices.Add(ChoicesPrefab[i], ChoicesItemsPrefab[i]);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBoat() {
        if (rounds % 3 == 0) {
            SpawnDeliveryBoat();
        } else {
            SpawnNormalBoat();
        }
        rounds++;
    }

    void SpawnDeliveryBoat() {
        GameObject newBoat = Instantiate(DeliveryBoatPrefab, SpawnLocation, Quaternion.identity);
        if (CurrentPossibleSolicitingItems.Count == 0) {
            newBoat.GetComponent<boatDelivery>().choicesPrefab.Add(ChoicesPrefab[0]);
            newBoat.GetComponent<boatDelivery>().Items.Add(ChoicesItemsPrefab[0]);
            newBoat.GetComponent<boatDelivery>().choicesPrefab.Add(ChoicesPrefab[1]);
            newBoat.GetComponent<boatDelivery>().Items.Add(ChoicesItemsPrefab[1]);
            newBoat.GetComponent<boatDelivery>().ChoiceCallback = GetChoice;
            Boats.Add(newBoat);
            return;
        }
        var addedChoice = 0;
        while (addedChoice != 2) {
            foreach (var pair in CurrentPossibleChoices) {
                bool toAdd = Random.Range(0, 2) == 1;
                if (toAdd) {
                    newBoat.GetComponent<boatDelivery>().choicesPrefab.Add(pair.Key);
                    newBoat.GetComponent<boatDelivery>().Items.Add(pair.Value);
                    addedChoice++;
                    if (addedChoice == 2) {
                        break;
                    }
                }
            }
        }
        newBoat.GetComponent<boatDelivery>().ChoiceCallback = GetChoice;
        Boats.Add(newBoat);
    }

    void SpawnNormalBoat() {
        GameObject newBoat = Instantiate(BoatPrefab, SpawnLocation, Quaternion.identity);
        var choseItems = 0;
        foreach (GameObject PossibleItem in CurrentPossibleSolicitingItems) {
            var thisChoice = Random.Range(1, maxItems - choseItems);
            choseItems += thisChoice;
            newBoat.GetComponent<boatSoliciting>().wantedItems.Add(Instantiate(PossibleItem), 2);
        }
        Boats.Add(newBoat);
    }

    void RemoveBoat(GameObject boat) {
        if (boat.GetComponent<boatSoliciting>() == null) {
             boat.GetComponent<boatDelivery>().DeleteAll();
        } else {
            boat.GetComponent<boatSoliciting>().DeleteAll();
        }
        
        Boats.RemoveAt(0);
        Destroy(boat);
    }

    void GetChoice(GameObject choice) {
        var choiceIndex = 0;
        Debug.Log(choice.name);
        // int choiceIndex = CurrentPossibleChoices.Keys.ToList().IndexOf(choice);
        bool found = false;
        for (int i = 0; i < ChoicesPrefab.Count; i++) {
            if (choice.name.Split()[0] == ChoicesPrefab[i].name.Split()[0]) {
                choiceIndex = i;
                found = true;
            }
        }   

        if (!found) {
            Debug.Log("Panic!");
            Debug.Log(choiceIndex);
        } else {
            CurrentPossibleSolicitingItems.Add(PossibleSolicitingItems[choiceIndex]);
            CurrentPossibleChoices.Remove(choice);
        }
    }
    
    void FixedUpdate() {
        if (Boats.Count >= 1) {
            if (Boats[0].transform.position.x < MinBoatX) {
                RemoveBoat(Boats[0]);
            }
            return;
        }

        if (startTime > betweenBoatTime) {
            startTime = 0;
            SpawnBoat();
            if (TimerRef != null) {
                Destroy(TimerRef);
                boatTimerStarted = false;
            }
        } else {
            if (!boatTimerStarted) {
                TimerRef = Instantiate(BetweenBoatTimer, new Vector2(8, -2.5f), Quaternion.identity);
                boatTimerStarted = true;
                Debug.Log(TimerRef);
            }
            startTime += Time.deltaTime;
        }
    }
}
