using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainButton : MonoBehaviour
{
    public void ReloadGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
