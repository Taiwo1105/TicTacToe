using UnityEngine;

public class TicTacToeAI : MonoBehaviour
{
    public TicTacToeManager manager;

    public void MakeAIMove()
    {
        Debug.Log("AI is making a move...");

        if (manager.gameOver) return;

        int move = GetBestMove();

        if (move != -1)
        {
            manager.OnGridClick(move);
        }
    }

    int GetBestMove()
    {
        string ai = manager.currentPlayer;
        string[] board = manager.board;
        int[][] winConditions = manager.winConditions;

        // 1. Try to win
        foreach (int[] wc in winConditions)
        {
            int move = FindWinningMove(wc, ai);
            if (move != -1) return move;
        }

        // 2. Block opponent
        string opponent = (ai == "X") ? "O" : "X";
        foreach (int[] wc in winConditions)
        {
            int move = FindWinningMove(wc, opponent);
            if (move != -1) return move;
        }

        // 3. Pick random empty cell
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == "")
            {
                return i;
            }
        }

        return -1; // No move possible
    }

    int FindWinningMove(int[] line, string player)
    {
        string[] board = manager.board;

        int count = 0;
        int emptyIndex = -1;

        foreach (int index in line)
        {
            if (board[index] == player)
            {
                count++;
            }
            else if (board[index] == "")
            {
                emptyIndex = index;
            }
        }

        return (count == 2 && emptyIndex != -1) ? emptyIndex : -1;
    }
}
