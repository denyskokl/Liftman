using UnityEngine;
using System.Collections;

public class GuestGenerate : MonoBehaviour
{
    public static GuestGenerate instance;
   
    public GameObject _guest;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreateGuest(float lifeTime, Stage stage)
    {
        var guest = Instantiate(_guest);
        guest.transform.SetParent(gameObject.transform);
        guest.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
        guest.transform.position = new Vector3(guest.transform.position.x, stage.Offset, guest.transform.position.z);
    }
}