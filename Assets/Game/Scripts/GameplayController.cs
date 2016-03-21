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
}