using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private Ball _ball;

    private bool _isStarted;

    #endregion


    #region Unity lifecycle

    private void Update()
    {
        if (_isStarted)
            return;


        _ball.MoveWithPad();

        if (Input.GetMouseButtonDown(0))
        {
            StartBall();
        }
    }

    #endregion


    public void GameOver()
    {
        SceneManager.LoadScene("SampleScene");
    }


    #region Private methods

    private void StartBall()
    {
        _isStarted = true;
        _ball.StartMove();
    }

    #endregion
}