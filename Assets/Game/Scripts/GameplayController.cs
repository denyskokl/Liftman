using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameplayController : MonoBehaviour
{
  public static List<GameObject> Guests;

  private static List<Stage> _stages;
  private const int MaxStage = 10;
  private const float GuestLifeTime = 15;

  [SerializeField] private float _startPosition;
  [SerializeField] private float _stageOfSet;
  [SerializeField] private GameObject _guest;


  public void Init()
  {
    _stages = new List<Stage>();
    for (int i = 1; i <= MaxStage; i++)
    {
      _stages.Add(new Stage(i));
    }
  }

  void Awake()
  {
    LiftController.OnStageStay = GuestLanding;
  }

  void Start()
  {
    Guests = new List<GameObject>();
    Init();
    InvokeRepeating("InitGuest", 0, 10);
  }


  void GuestLanding(int stageNumber)
  {
    foreach (var item in Guests)
    {
      var guest = item.GetComponent<Guest>();
      if (guest.Destination == stageNumber)
        guest.MoveOut();
      if (guest.StageNumber == stageNumber)
        guest.MoveIn();
    }
  }

  private void InitGuest()
  {
    Debug.Log("Init Guest Sheduled!");
    var stage = GetRandomStage();
    Debug.Log("Random stage is " + stage.Number);
    CreateGuest(GuestLifeTime, stage);
  }

  public void CreateGuest(float lifeTime, Stage stage)
  {
    var guest = Instantiate(_guest);
    var guestItem = guest.GetComponent<Guest>();
    SetGuestValues(guestItem, stage);

    guest.transform.SetParent(gameObject.transform);
    guest.transform.localPosition = new Vector3(240f, stage.Offset + _startPosition, guest.transform.localPosition.z);
    Guests.Add(guest);
  }

  private void SetGuestValues(Guest guest, Stage stage)
  {
    guest.StageNumber = stage.Number;
    guest.Destination = Random.Range(1, 10);
    stage.GuestNumber += 1;
  }

  private Stage GetRandomStage(int count = 1)
  {
    if (count > MaxStage) return _stages[0];
    count++;
    var stage = _stages[Random.Range(0, 10)];
    if (stage.GuestNumber < 3)
    {
      stage.Offset = _stageOfSet*stage.Number;
      Debug.Log("Stage offSet is " + stage.Offset);
      return stage;
    }
    GetRandomStage(count);
    return _stages[0];
  }

  public static void RemoveGuest(GameObject guest)
  {
    if (Guests.Count > 0)
      Guests.Remove(guest);
  }
}