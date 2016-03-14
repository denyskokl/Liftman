using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GuesController : MonoBehaviour
{
	void Start()
	{
		MoveGues ();

	}

	private void MoveGues()
	{
		ChageFlip(true);
		transform.DOMoveX (-7.8f, 6f, false).OnComplete(Pause);
	}

	private void Pause()
	{
		Invoke ("MoveBack", 1f);
	}

	private void MoveBack()
	{
		ChageFlip(false);
		transform.DOMoveX (-2f, 6f, false).OnComplete(MoveGues);
	}

	private bool ChageFlip(bool status)
	{
		transform.GetComponent<SpriteRenderer> ().flipX = status;
		return status;
	}

}