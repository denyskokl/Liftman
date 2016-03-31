using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Guest : MonoBehaviour
{
    public int StageNumber { get; set; }
    public int Destination { get; set; }
    public float LifeTime { get; set; }


    public float StayPosition;
    public bool IsClaimed;

    [SerializeField]
    private Text destinationText;
    [SerializeField]
    private Image _patienceBar;
    private Rigidbody2D _rigidbody;
    private GameObject _lift;
    private GameplayController _gamePlayController;

    public Guest(int stageNumber, int destination, float lifeTime)
    {
        StageNumber = stageNumber;
        Destination = destination;
        LifeTime = lifeTime;
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _lift = FindObjectOfType<LiftController>().gameObject;
        _gamePlayController = FindObjectOfType<GameplayController>();
         StartCoroutine(TimeTrigger(() => MoveOut()));
    }

    void Start()
    {
        destinationText.text = Destination.ToString();
        MoveGuest();
    }


    IEnumerator TimeTrigger(Action callback)
    {
        yield return new WaitForSeconds(1);
        while (LifeTime >= 0)
        {
            
            LifeTime--;
            PatienceBar();
             yield return new WaitForSeconds(1);
        }
        callback();
    }

    private void PatienceBar()
    {
        if(IsClaimed)
        {
          //todo need logic to update time patience guest
            LifeTime = _gamePlayController.GuestLifeTime;
        }
        var amount = LifeTime / _gamePlayController.GuestLifeTime;
        _patienceBar.fillAmount = amount;
    }

    
    // Temporal coordinates. Fix it
    public void MoveIn(float position)
    {
        transform.SetParent(_lift.transform);
        _rigidbody.isKinematic = true;
        GetComponent<Animator>().Play("Moving");
        transform.DOMoveX(position, 2f).OnComplete(StopAnimation);
        IsClaimed = true;
    }

    // Temporal coordinates Fix it
    public void MoveOut()
    {
        _rigidbody.isKinematic = false;
        transform.SetParent(_gamePlayController.gameObject.transform);
        GetComponent<SpriteRenderer>().flipX = false;
        GetComponent<Animator>().Play("Moving");
        transform.DOMoveX(0.6f, 3).OnComplete(StopAnimation).OnComplete(Destroy);
    }

    private void MoveGuest()
    {
        GetComponent<Animator>().Play("Moving");
        transform.DOLocalMoveX(-0.1f, 3).OnComplete(StopAnimation);
    }


    private void StopAnimation()
    {
        GetComponent<Animator>().Play("Idle");
    }

    public void Destroy()
    {
        GameplayController.RemoveGuest(gameObject);
        Destroy(gameObject);
    }
}