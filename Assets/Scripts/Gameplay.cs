using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{
    public GameObject[] swords;
    public GameObject[] holes;
    public GameObject pirate;
    int holeCount = 0;
    int rndHole = 0;
    bool isGameOver = false;

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Start()
    {
        holeCount = holes.Length;
        rndHole = Random.Range(0, holeCount);
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Holes"))
                {
                    for (int i = 0; i < holes.Length; i++)
                    {
                        if (holes[i].gameObject == hit.collider.gameObject)
                        {
                            holes[i].SetActive(false);
                            swords[i].SetActive(true);
                            if (i == rndHole) isGameOver = true;
                            break;
                        }
                    }
                }
            }
        }
        Vector2 scrollValue = Mouse.current.scroll.ReadValue();
        scrollValue.y *= 200;
        transform.Rotate(Vector3.up, 20 * scrollValue.y * Time.deltaTime);

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    void FixedUpdate()
    {
        if (isGameOver)
        {
            Rigidbody rb = GetComponentInChildren<Rigidbody>();
            rb.AddForce(new Vector3(17,1000,15), ForceMode.Force);
            isGameOver = false;
            pirate.transform.SetParent(null);
        }
    }
}
