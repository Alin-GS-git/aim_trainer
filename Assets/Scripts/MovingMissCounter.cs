using TMPro;
using UnityEngine;

public class MovingMissCounter : MonoBehaviour
{
    [SerializeField] TMP_Text missText;
    public static int Misses { get; private set; }

    void Start()
    {
        Misses = 0;
        if (missText != null)
            missText.text = "Misses: 0";
    }

    public static void AddMiss()
    {
        Misses++;
        MovingMissCounter instance = FindObjectOfType<MovingMissCounter>();
        if (instance != null && instance.missText != null)
        {
            instance.missText.text = $"Misses: {Misses}";
        }
    }
}