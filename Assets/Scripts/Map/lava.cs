using UnityEngine;
using UnityEngine.SceneManagement;

public class lava : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(3);
        }
    }
}
