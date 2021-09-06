
public class GameBoard {
    
    public int[] board;
    public int playerScore = 0;
    public int AIScore = 0;
    public const int playersNum = 2;
    public static int holesNum = 5;
    bool[] movable = new bool[10];

    public int measure() => AIScore - playerScore;

    public GameBoard() {
        board = new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        for (int i = 0; i < movable.Length; ++i) {
            movable[i] = true;
        }
    }

    public GameBoard(int[] initial, int ai = 0, int player = 0) {
        board = (int[])initial.Clone();
        playerScore = player;
        AIScore = ai;
        for(int i = 0; i < movable.Length; ++i) {
            movable[i] = true;
        }
    }

    public void endGame() {
        for (int i = 0; i < holesNum; ++i) {
            AIScore += board[i];
            playerScore += board[holesNum + i];
        }
    }

    public bool[] canMove(int player) {
        bool[] res = new bool[holesNum];
        for (int i = 0; i < holesNum; ++i) {
            res[i] = (board[i + holesNum * player] != 0);
            if (!movable[i + holesNum * player]) {
                res[i] = false;
                movable[i + holesNum * player] = true;
            }
        }
        return res;
    }

    public bool[] turns(int player, int hole) => (hole + (player * holesNum)) switch
    {
        int x when
        x == 3 ||
        x == 4 ||
        x == 8 ||
        x == 9 => new bool[] { true },

        int x when
        x == 2 ||
        x == 7 => new bool[] { true, false },

        int x when
        x == 0 ||
        x == 1 ||
        x == 5 ||
        x == 6 => new bool[] { false },

        _ => new bool[] { }
    };


    public void makeTurn(int player, int button, bool forward) {
        int position = player * holesNum + button;
        int counter = board[position];
        board[position] = 0;
        int deltaPosition = forward ? 1 : (-1);

        // deadlock check
        if(counter == 1 && (button == 0 || button == holesNum - 1 || button == holesNum || button == holesNum * playersNum - 1)) {
            int blockPosition = position + deltaPosition;
            if (blockPosition < 0) blockPosition = holesNum * playersNum - 1;
            if (blockPosition >= holesNum * playersNum) blockPosition = 0;
            if (board[blockPosition] == 0) {
                movable[blockPosition] = false;
                board[blockPosition] = 1;
                return;
            }
        }

        // move the stones
        for (int i = 0; i < counter; ++i) {
            position += deltaPosition;
            if (position < 0) position = holesNum * playersNum - 1;
            if (position >= holesNum * playersNum) position = 0;
            board[position] += 1;
        }

        // update the score if needed
        while (board[position] == 2 || board[position] == 4) {
            if (player == 0) {
                AIScore += board[position];
            } else {
                playerScore += board[position];
            }
            board[position] = 0;
            position -= deltaPosition;
            if (position < 0) position = holesNum * playersNum - 1;
            if (position >= holesNum * playersNum) position = 0;
        }
    }


    public GameBoard makeTheorTurn(int player, int hole, bool forward) {
        GameBoard res = new GameBoard(board, AIScore, playerScore);
        res.makeTurn(player, hole, forward);
        return res;
    }

    public override string ToString() {
        return board.ToString();
    }
}
