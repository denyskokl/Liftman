using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayGameMoving : MonoBehaviour {

	[SerializeField]
	private Camera mainCamera;

	[SerializeField]
	private Image leftDor;

	[SerializeField]
	private Image rightDor;

	[SerializeField]
	private GameObject button;


	public void PlayGame()
	{

		button.SetActive (false);
		DOTween.To ( () => mainCamera.orthographicSize, x => mainCamera.orthographicSize = x, 0.87f, 1.5f).OnComplete(LoadGameScene);
		leftDor.transform.DOLocalMoveX(-44f, 1f);
		rightDor.transform.DOLocalMoveX(44f, 1f);
	
	}

	private void LoadGameScene()
	{
		SceneManager.LoadScene ("GameScene");

	}
}
