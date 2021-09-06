using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandle : MonoBehaviour {
    public TextMeshProUGUI hole1;
    public TextMeshProUGUI hole2;
    public TextMeshProUGUI hole3;
    public TextMeshProUGUI hole4;
    public TextMeshProUGUI hole5;
    public TextMeshProUGUI hole6;
    public TextMeshProUGUI hole7;
    public TextMeshProUGUI hole8;
    public TextMeshProUGUI hole9;
    public TextMeshProUGUI hole10;

    public TextMeshProUGUI AIScore;
    public TextMeshProUGUI HumanScore;

    public void backwards1() {
        if (board.board[5] == 0) return;
        board.makeTurn(1, 0, false);
        opponent.makeTurn(ref board);
    }

    public void backwards2() {
        if (board.board[6] == 0) return;
        board.makeTurn(1, 1, false);
        opponent.makeTurn(ref board);
    }

    public void backwards3() {
        if (board.board[7] == 0) return;
        board.makeTurn(1, 2, false);
        opponent.makeTurn(ref board);
    }

    public void forward3() {
        if (board.board[7] == 0) return;
        board.makeTurn(1, 2, true);
        opponent.makeTurn(ref board);
    }

    public void forward4() {
        if (board.board[8] == 0) return;
        board.makeTurn(1, 3, true);
        opponent.makeTurn(ref board);
    }

    public void forward5() {
        if (board.board[9] == 0) return;
        board.makeTurn(1, 4, true);
        opponent.makeTurn(ref board);
    }

    public AI opponent;
    public GameBoard board;
    public AI.Difficulty difficulty;
    // Start is called before the first frame update
    void Start() {
        board = new GameBoard();
        opponent = new AI(difficulty);
    }
    // Update is called once per frame
    void Update() {
        hole1.text = board.board[0].ToString();
        hole2.text = board.board[1].ToString();
        hole3.text = board.board[2].ToString();
        hole4.text = board.board[3].ToString();
        hole5.text = board.board[4].ToString();
        hole6.text = board.board[5].ToString();
        hole7.text = board.board[6].ToString();
        hole8.text = board.board[7].ToString();
        hole9.text = board.board[8].ToString();
        hole10.text = board.board[9].ToString();

        AIScore.text = board.AIScore.ToString();
        HumanScore.text = board.playerScore.ToString();
    }
}
