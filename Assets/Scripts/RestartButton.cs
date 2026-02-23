using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
	void Awake()
	{
		Timer.OnGameEnded += Show;
		gameObject.SetActive(false);
	}

	void OnDestroy()
	{
		Timer.OnGameEnded -= Show;
	}

	void Show()
	{
		gameObject.SetActive(true);
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
