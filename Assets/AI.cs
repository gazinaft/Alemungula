
public class AI {

    public enum Difficulty {
        Easy = 4,
        Normal = 6,
        Hard = 8
    }

    Difficulty difficulty;

    public AI(Difficulty difficulty) {
        this.difficulty = difficulty;
    }

    public void makeTurn(ref GameBoard game) {
        GameBoard local = new GameBoard(game.board, game.AIScore, game.playerScore);
        Node root = new Node(local, (int)difficulty);
        game = root.bestTurn();
        foreach (bool elem in game.canMove(1)) {
            if (elem) return;
        }
        game.endGame();
    }
}
