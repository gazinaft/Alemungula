using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node {

    public GameBoard board;
    public bool isEdge;
    public bool isTerminal;
    Node parent;
    GameBoard bestSolution;
    int level;

    public Node(GameBoard board, int level, Node parent = null) {
        this.board = board;
        if (level <= 1) isEdge = true;
        else isEdge = false;
        this.level = level;
        this.parent = parent;
    }

    Node root() => (parent == null) ? this : parent.root();

    Node afterRoot() => (parent.parent == null) ? this : parent.afterRoot();

    public List<Node> childNodes(int player) {
        List<Node> res = new List<Node>();
        for (int i = 0; i < GameBoard.holesNum; ++i) {
            if (!board.canMove(player)[i]) continue;
            foreach (bool direction in board.turns(player, i)) {
                GameBoard newBoard = board.makeTheorTurn(player, i, direction);
                res.Add(new Node(newBoard, level - 1, this));
            }
        }
        return res;
    }

    public int maxValue(ref int alfa, ref int beta) {
        if (isEdge) return board.measure();
        if (!board.canMove(0).Any()) {
            return board.measure();
        }
        int res = -1000;
        foreach(Node node in childNodes(0)) {

            int temp = node.minValue(ref alfa, ref beta);
            if (temp >= res) {
                res = temp;
                if (parent == null) {
                    bestSolution = node.board;
                }
            }
            if (res >= beta) return res;
            alfa = Mathf.Max(alfa, res);
        }
        return res;
    }

    public int minValue(ref int alfa, ref int beta) {
        if (isEdge) return board.measure();
        if (!board.canMove(1).Any()) {
            return board.measure();
        }
        int res = 1000;
        foreach (Node node in childNodes(1)) {
            res = Mathf.Min(res, node.maxValue(ref alfa, ref beta));
            if (res <= alfa) return res;
            beta = Mathf.Min(beta, res);
        }
        return res;
    }

    public GameBoard bestTurn() {
        int alfa = -1000;
        int beta = 1000;
        int res = maxValue(ref alfa, ref beta);
        Debug.Log(res.ToString());
        if (bestSolution == null) throw new UnityException("durak");
        return bestSolution;
    }
}
