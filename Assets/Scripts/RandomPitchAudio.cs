using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomPitchAudio : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Pitch Variation")]
    [Range(0.5f, 2f)] public float minPitch = 0.9f;
    [Range(0.5f, 2f)] public float maxPitch = 1.1f;

    [Header("Sound Clips")]
    public AudioClip[] clips;

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        PlayRandomSound();
    }

    public void PlayRandomSound()
    {
        if (clips.Length == 0) return; // no clips assigned

        // Pick a random clip
        AudioClip clip = clips[Random.Range(0, clips.Length)];

        // Apply random pitch
        audioSource.pitch = Random.Range(minPitch, maxPitch);

        // Play the chosen clip
        audioSource.clip = clip;
        audioSource.Play();
    }
}
