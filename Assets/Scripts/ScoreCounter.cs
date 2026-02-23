using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    public static int Score { get; private set; }

    void Start()
    {
        Score = 0;
        if (text != null)
            text.text = "Score: 0";
    }

    void OnEnable()
    {
        Target.OnTargetHit += OnTargetHit;
    }

    void OnDisable()
    {
        Target.OnTargetHit -= OnTargetHit;
    }

    void OnTargetHit()
    {
        Score++;
        if (text != null)
            text.text = $"Score: {Score}";
    }
}