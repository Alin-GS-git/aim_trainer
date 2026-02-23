using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] Transform cameraHolder;
	[SerializeField] float mouseSensitivity = 1;

	float verticalLookRotation;

	void Start()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void OnEnable()
	{
		Timer.OnGameEnded += OnGameEnded;
	}

	void OnDisable()
	{
		Timer.OnGameEnded -= OnGameEnded;
	}

	void OnGameEnded()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	void Update()
	{
		if(Timer.GameEnded)
			return;

		transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

		verticalLookRotation -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
		cameraHolder.localEulerAngles = new Vector3(verticalLookRotation, 0, 0);
	}
}
