using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	public static Action OnTargetHit;

	void Start()
	{
		RandomizePosition();
	}

	public void Hit()
	{
		RandomizePosition();
		OnTargetHit?.Invoke();
	}

	void RandomizePosition()
	{
		transform.position = TargetBounds.Instance.GetRandomPosition();
	}
}
