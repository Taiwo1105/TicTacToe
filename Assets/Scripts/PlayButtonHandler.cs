using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonHandler : MonoBehaviour
{
    // Name of the scene to load when Play is clicked
    public string TicTacToe = "GameScene";

    // Call this from the UI Button's OnClick event
    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene(TicTacToe);
    }
}
