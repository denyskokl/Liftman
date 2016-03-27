using DG.Tweening;
using UnityEngine;

public class Guest : MonoBehaviour
{
    public int StageNumber { get; set; }
    public int Destination { get; set; }
    public float LifeTime { get; set; }

    public bool IsClaimed;
    private float lifeDuration;
    private Rigidbody2D _rigidbody;

    public Guest(int stageNumber, int destination, float lifeTime)
    {
        StageNumber = stageNumber;
        Destination = destination;
        LifeTime = lifeTime;
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        MoveGuest();
    }


    // Temporal coordinates. Fix it
    public void MoveIn()
    {
        GetComponent<Animator>().Play("Moving");
        transform.DOMoveX(-0.3f, 2f).OnComplete(StopAnimation);
        IsClaimed = true;
    }

    // Temporal coordinates Fix it
    public void MoveOut()
    {
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