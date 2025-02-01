using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Finishing : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out _))
        {
            SceneManager.LoadScene("Final");
        }
    }
}
