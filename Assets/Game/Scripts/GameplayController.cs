using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using Random = UnityEngine.Random;


public class GameplayController : MonoBehaviour
{
    public static List<GameObject> Guests;


    //  private static List<Stage> _stages;

    [SerializeField]
    private List<Floor> _stages;
    private const float GuestLifeTime = 15;

    [SerializeField]
    private GameObject _guest;


    public void Init()
    {
        //    _stages = new List<Stage>();
        //    for (int i = 1; i <= MaxStage; i++)
        //    {
        //      _stages.Add(new Stage(i));
        //    }
    }

    void Awake()
    {
        LiftController.OnStageStay += GuestLanding;
    }

    void Start()
    {
        Guests = new List<GameObject>();
        //    Init();
        InvokeRepeating("InitGuest", 0, 10);
    }


    void GuestLanding(int stageNumber)
    {
        foreach (var item in Guests)
        {
            var guest = item.GetComponent<Guest>();
            if (guest.Destination == stageNumber && guest.IsClaimed)
                guest.MoveOut();
            if (guest.StageNumber == stageNumber && !guest.IsClaimed)
                guest.MoveIn();
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
        guest.Destination = Random.Range(1, 10);
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

