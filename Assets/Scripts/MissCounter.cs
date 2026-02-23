using TMPro;
using UnityEngine;

public class MissCounter : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    public static int Misses { get; private set; }

    void Awake()
    {
        Misses = 0;
        if (text != null)
            text.text = "Misses: 0";
    }

    void OnEnable()
    {
        Gun.OnTargetMissed += OnTargetMissed;
    }

    void OnDisable()
    {
        Gun.OnTargetMissed -= OnTargetMissed;
    }

    void OnTargetMissed()
    {
        Misses++;
        if (text != null)
            text.text = $"Misses: {Misses}";
        Debug.Log($"Miss counted! Total: {Misses}");
    }
}