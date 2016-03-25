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
        guest.transform.SetParent(gameObject.transform);
        guest.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        guest.transform.position = new Vector3(0.8f, stage.Offset + _startPosition, guest.transform.position.z);
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

/*


using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameplayController : MonoBehaviour
{
    public static  List<GameObject> guests;

    private static  List<Stage> stages;
    private const int MAX_STAGE = 10;
    private const float GUEST_LIFE_TIME = 15;

    [SerializeField]
    private float startPosition;
    [SerializeField]
    private float _stageOfSet;
    [SerializeField]
    private GameObject _guest;

    public void Init()
    {
        stages = new List<Stage>();
        for (int i = 1; i <= MAX_STAGE; i++)
        {
            stages.Add(new Stage(i));
        }
    }

    void Awake()
    {
        LiftController.OnStageStay = EnterTheLift;
    }
    void Start()
    {
        guests = new List<GameObject>();
        Init();
        InvokeRepeating("InitGuest", 0, 10);
    }


    void EnterTheLift(int stageNumber)
    {

    }

    private void InitGuest()
    {
        Debug.Log("Init Guest Sheduled!");
        var stage = GetRandomStage();
        Debug.Log("Random stage is " + stage.Number);
        CreateGuest(GUEST_LIFE_TIME, stage);
    }

    public void CreateGuest(float lifeTime, Stage stage)
    {
        var guest = Instantiate(_guest);
        guest.transform.SetParent(gameObject.transform);
        guest.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
        guest.transform.position = new Vector3(guest.transform.position.x, stage.Offset + startPosition, guest.transform.position.z);
        guests.Add(guest);
    }

    private Stage GetRandomStage(int count = 1)
    {
        if (count <= MAX_STAGE)
        {
            count++;
            var stage = stages[Random.Range(0, 10)];
            if (stage.GuestNumber < 3)
            {
                stage.Offset = _stageOfSet * stage.Number;
                Debug.Log("Stage offSet is " + stage.Offset);
                return stage;
            }
            else GetRandomStage(count);
        }
        return stages[0];
    }

    public static void RemoveGuest(GameObject guest)
    {
        if(guests.Count > 0)
            guests.Remove(guest);
    }
}*/