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

  [SerializeField] private List<Floor> _stage;

  private const float GuestLifeTime = 15;

  [SerializeField] private GameObject _guest;


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
    Debug.Log("Random stage is " + stage.Number);
    CreateGuest(GuestLifeTime, stage.PositionY);
  }

  public void CreateGuest(float lifeTime, float stagePos)
  {
    var guest = Instantiate(_guest);
    guest.transform.SetParent(gameObject.transform);
    guest.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
    guest.transform.position = new Vector3(0.65f, stagePos, guest.transform.position.z);
    Guests.Add(guest);
  }

  private void SetGuestValues(Guest guest, Stage stage)
  {
    guest.StageNumber = stage.Number;
    guest.Destination = Random.Range(1, 10);
    stage.GuestNumber += 1;
  }

  private Floor GetRandomStage()
  {
    var currentStage = _stage[Random.RandomRange(0, 10)];
    return _stage.FirstOrDefault(f => f.Number.Equals(currentStage.Number));


//    if (count > MaxStage) return _stages[0];
//    count++;
//    var stage = _stages[Random.Range(0, 10)];
//    if (stage.GuestNumber < 3)
//    {
//
//      return stage;
//    }
//    GetRandomStage(count);
//    return _stages[0];
  }

//  public static void RemoveGuest(GameObject guest)
//  {
//    if (Guests.Count > 0)
//      Guests.Remove(guest);
//  }
}

