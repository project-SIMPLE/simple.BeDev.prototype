using UnityEngine;

public class Mosquito : MonoBehaviour
{
    [Header("Orbit Settings")]
    [SerializeField] float orbitSpeed = 1f;
    [SerializeField] float xRadius = 2f;
    [SerializeField] float zRadius = 2f;

    [Header("Vertical Bobbing")]
    [SerializeField] float bobSpeed = 3f;
    [SerializeField] float bobHeight = 0.2f;

    [Header("Random Noise")]
    [SerializeField] float noiseSpeed = 2f;
    [SerializeField] float noiseMagnitude = 0.5f;

    [Header("On Hit Effects")]
    [SerializeField] ParticleSystem electricEffect;

    Vector3 orbitCenter;
    float orbitProgress = 0f;
    float noiseOffsetX;
    float noiseOffsetY;
    float noiseOffsetZ;
    bool isHit = false;
    int direction;

    Racket racket;

    void Start()
    {
        racket = Racket.Instance;

        orbitCenter = transform.position;

        orbitProgress = Random.Range(0f, 2f * Mathf.PI);

        noiseOffsetX = Random.Range(0f, 1000f);
        noiseOffsetY = Random.Range(0f, 1000f);
        noiseOffsetZ = Random.Range(0f, 1000f);

        direction = Random.Range(0, 2) * 2 - 1;
    }

    void Update()
    {
        if (isHit) return;
        
        FlyMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("racket"))
        {
            Instantiate(electricEffect, transform.position, Quaternion.identity);
            racket.PlayHaptic();
            Destroy(gameObject, 0.1f);
        }
    }

    

    void FlyMovement()
    {
        orbitProgress += Time.deltaTime * orbitSpeed * direction;

        float x = Mathf.Cos(orbitProgress) * xRadius;
        float z = Mathf.Sin(orbitProgress) * zRadius;

        float yBob = Mathf.Sin(Time.time * bobSpeed) * bobHeight;

        Vector3 orbitPosition = new Vector3(x, yBob, z);

        float time = Time.time * noiseSpeed;
        float noiseX = (Mathf.PerlinNoise(time, noiseOffsetX) - 0.5f) * 2f;
        float noiseY = (Mathf.PerlinNoise(time, noiseOffsetY) - 0.5f) * 2f;
        float noiseZ = (Mathf.PerlinNoise(time, noiseOffsetZ) - 0.5f) * 2f;

        Vector3 noiseOffset = new Vector3(noiseX, noiseY, noiseZ) * noiseMagnitude;

        Vector3 finalPosition = orbitCenter + orbitPosition + noiseOffset;

        Vector3 lookDirection = finalPosition - transform.position;
        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(lookDirection),
                Time.deltaTime * 5f
            );
        }

        transform.position = finalPosition;
    }
}

