using UnityEngine;
using UnityEngine.SceneManagement;

public class goBack : MonoBehaviour
{
    public void goback()
    {
        SceneManager.LoadScene(0);
    }
}