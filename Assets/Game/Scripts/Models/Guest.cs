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


    public bool IsClaimed;
    [SerializeField]
    private Text destinationText;
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
        _lift =  FindObjectOfType<LiftController>().gameObject;
        _gamePlayController = FindObjectOfType<GameplayController>();
      
        StartCoroutine(TimeTrigger(() => Destroy()));

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
            yield return new WaitForSeconds(1);
        }
        callback();
    }

    // Temporal coordinates. Fix it
    public void MoveIn()
    {
        transform.SetParent(_lift.transform);
        _rigidbody.isKinematic = true;
        GetComponent<Animator>().Play("Moving");
        transform.DOMoveX(-0.3f, 2f).OnComplete(StopAnimation);
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
        transform.DOLocalMoveX(-0.1f, 3).OnComplete(StopAnimation);
    }


    private void StopAnimation()
    {
        GetComponent<Animator>().StopPlayback();
    }

    private void StopMoving()
    {
        Invoke("MoveBack", 1f);
    }


    private void MoveBack()
    {
        transform.rotation = Quaternion.Euler(Vector3.back);
        transform.DOLocalMoveX(160f, 8f).OnComplete(Destroy);
    }


    public void Destroy()
    {
        GameplayController.RemoveGuest(gameObject);
        Destroy(gameObject);
    }
}