using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int _playerScore;
    private int _AIScore;

    public Text playerScoreText;
    public Text AIScoreText;

    public Ball ball;
    public Paddle playerPaddle;
    public Paddle AIPaddle;

    public void PlayerScores()
    {
        _playerScore++;

        this.playerScoreText.text = _playerScore.ToString();

        ResetRound();
    }

    public void AIScores()
    {
        _AIScore++;

        this.AIScoreText.text = _AIScore.ToString();

        ResetRound();
    }

    private void ResetRound()
    {
        this.playerPaddle.ResetPosition();
        this.AIPaddle.ResetPosition();
        this.ball.ResetBallPosition();
        this.ball.AddStartingForce();
    }

    
}
