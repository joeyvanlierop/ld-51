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

    public List<Item> ItemsPrefab = new List<Item>();

    List<GameObject> CurrentPossibleChoices = new List<GameObject>();

    List<GameObject> CurrentPossibleSolicitingItems = new List<GameObject>();

    float betweenBoatTime = 10f;

    public int MaxBoats = 1;

    public float MinBoatX = -18f;

    bool boatTimerStarted = false;
    public GameObject BetweenBoatTimer;
    GameObject TimerRef;
    float startTime = 11f;

    int rounds = 0;



    public Vector3 SpawnLocation = new Vector3(13.29f, -2.36f, 0.02834536f);
    // Start is called before the first frame update
    void Start()
    {

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
    }

    void SpawnDeliveryBoat() {
        GameObject newBoat = Instantiate(DeliveryBoatPrefab, SpawnLocation, Quaternion.identity);
        foreach (GameObject choice in ChoicesPrefab) {
            newBoat.GetComponent<boatDelivery>().choicesPrefab.Add(choice);
            // newBoat.GetComponent<boatDelivery>().Items.Add();
        }
        Boats.Add(newBoat);
    }

    void SpawnNormalBoat() {
        GameObject newBoat = Instantiate(BoatPrefab, SpawnLocation, Quaternion.identity);

        foreach (GameObject PossibleItem in PossibleSolicitingItems) {
            newBoat.GetComponent<boatSoliciting>().wantedItems.Add(Instantiate(PossibleItem), Random.Range(0, 2));
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

    void GetChoice(GameObject boat) {
        if (boat.GetComponent<boatSoliciting>() == null) {

        } else {

        }
    }
    
    void FixedUpdate() {
        if (Boats.Count == MaxBoats) {
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
