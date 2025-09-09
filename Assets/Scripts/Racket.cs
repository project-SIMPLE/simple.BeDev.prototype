using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class Racket : MonoBehaviour
{
    [SerializeField] float amplitude = 1f;
    [SerializeField] float duration = 10f;

    public static Racket Instance { get; private set; }
    HapticImpulsePlayer hapticPlayer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        hapticPlayer = GameObject.Find("Right Controller").GetComponent<HapticImpulsePlayer>();

    }

    void Update()
    {
        
    }

    public void PlayHaptic()
    {
        if (hapticPlayer != null)
        {
            Debug.Log(this);
            hapticPlayer.SendHapticImpulse(amplitude, duration);
        }
    }
}
