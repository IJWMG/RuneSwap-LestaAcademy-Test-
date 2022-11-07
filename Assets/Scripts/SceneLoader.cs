using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        RuneVew.GetRuneVew().NewGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
