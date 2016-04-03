using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using Random = UnityEngine.Random;
using System.Collections;

public class GameplayController : MonoBehaviour
{
    public static List<GameObject> Guests;
    public static List<GameObject> LiftGuests;
    public const int MAX_LIFT_GUESTS = 3;


    //  private static List<Stage> _stages;

    [SerializeField]
    private List<Floor> _stages;
    public float GuestLifeTime = 25;

    [SerializeField]
    private List<GameObject> _liftStayPositions;

    [SerializeField]
    private GameObject _guest;

    void Awake()
    {
        LiftController.OnStageStay += GuestLanding;
    }

    void Start()
    {
        LiftGuests = new List<GameObject>();
        Guests = new List<GameObject>();
        InvokeRepeating("InitGuest", 0, 10);
    }


    void GuestLanding(int stageNumber)
    {
        foreach (var item in Guests)
        {
            var guest = item.GetComponent<Guest>();
            if (guest.Destination == stageNumber && guest.IsClaimed || guest.IsClaimed && guest.IsTimeLeft)
            {
                var stayObj = _liftStayPositions.Where(obj => obj.transform.position.x == guest.StayPosition).First();
                stayObj.SetActive(false);
                LiftGuests.Remove(item);
                guest.MoveOut();
            }

            if (guest.StageNumber == stageNumber && !guest.IsClaimed && LiftGuests.Count < 3)
            {
                var stayObj = _liftStayPositions.Where(obj => obj.activeSelf == false).First();
                stayObj.SetActive(true);
                var stayPosition = stayObj.transform.position.x;
                guest.StayPosition = stayPosition;
                LiftGuests.Add(item);
                guest.MoveIn(stayPosition);
            }

        }
    }

    private void InitGuest()
    {
        Debug.Log("Init Guest Sheduled!");
        var stage = GetRandomStage();
        if (stage == null) return;
        Debug.Log("Random stage is " + stage.Number);
        CreateGuest(GuestLifeTime, stage);
    }

    public void CreateGuest(float lifeTime, Floor floor)
    {
        var guest = Instantiate(_guest);
        var itemScript = guest.GetComponent<Guest>();
        SetGuestValues(itemScript, floor);
        guest.transform.SetParent(gameObject.transform);
        guest.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        guest.transform.position = new Vector3(0.65f, floor.PositionY, guest.transform.position.z);
        Guests.Add(guest);
    }

    private void SetGuestValues(Guest guest, Floor floor)
    {
        guest.StageNumber = floor.Number;
        int destination = Random.Range(1, 11);
        if (destination == floor.Number)
        {
            while (destination == floor.Number)
            {
                destination = Random.Range(1, 11);
            }
        }

        guest.Destination = destination;
        guest.LifeTime = GuestLifeTime;
        floor.GuestNumber += 1;
    }

    private Floor GetRandomStage(int count = 1)
    {
        if (count > 11) return null;
        count++;
        var currentStage = _stages[Random.RandomRange(0, 10)];
        if (currentStage.GuestNumber < 3)
        {

            return _stages.FirstOrDefault(f => f.Number.Equals(currentStage.Number));
        }
        GetRandomStage(count);
        return null;
    }
    public static void RemoveGuest(GameObject guest)
    {
        if (Guests.Count > 0)
            Guests.Remove(guest);
    }

}

