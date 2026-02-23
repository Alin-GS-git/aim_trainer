using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
	CanvasGroup canvasGroup;

	void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		if (canvasGroup == null)
			canvasGroup = gameObject.AddComponent<CanvasGroup>();

		HidePanel();
	}

	void OnEnable()
	{
		Timer.OnGameEnded += OnGameEnded;
	}

	void OnDisable()
	{
		Timer.OnGameEnded -= OnGameEnded;
	}

	void HidePanel()
	{
		canvasGroup.alpha = 0f;
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;
	}

	void OnGameEnded()
	{
		canvasGroup.alpha = 1f;
		canvasGroup.interactable = true;
		canvasGroup.blocksRaycasts = true;

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
}
