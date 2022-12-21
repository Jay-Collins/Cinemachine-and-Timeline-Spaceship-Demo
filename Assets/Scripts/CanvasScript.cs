using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] private bool _gameOver;
    [SerializeField] private GameObject _gameOverButton;
    
    public void RestartGame()
    {
        if (!_gameOver) return;
        SceneManager.LoadScene(0);

    }

    public void GameOver()
    {
        _gameOverButton.SetActive(true);
        _gameOver = true;
    }
    
}
