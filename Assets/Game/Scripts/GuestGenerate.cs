using UnityEngine;
using System.Collections;

public class GuestGenerate : MonoBehaviour
{
  public static GuestGenerate ins;

  void Awake()
  {
    if (ins == null)
    {
      ins = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  [SerializeField] private float _stageOfSet;
  public GameObject _guest;

  public void GuestCreated()
  {
    for (int i = 0; i < 3; i ++)
    {
      CreateGuest();
    }
  }

  public void CreateGuest()
  {
    var guest = Instantiate(_guest);
    guest.transform.SetParent(gameObject.transform);
    guest.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
    guest.transform.position = new Vector3(guest.transform.position.x, GenerateStages(), guest.transform.position.z);
  }

  private float GenerateStages()
  {
    int[] stages = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
    var currentStage = stages[Random.RandomRange(0, 10)];
    var offset = 2.16f;
    return _stageOfSet + offset*currentStage;
  }
}