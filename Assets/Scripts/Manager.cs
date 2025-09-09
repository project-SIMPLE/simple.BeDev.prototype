using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] int seed = 42;
    [SerializeField] InputActionReference resetButton;


    void Awake()
    {
        Random.InitState(seed);
        Debug.Log("Random seed initialized to: " + seed);
    }

    void Update()
    {
        Reset();
    }

    void Reset()
    {
        if (resetButton.action.IsPressed())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}