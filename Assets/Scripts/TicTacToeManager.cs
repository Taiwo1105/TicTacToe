using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TicTacToeManager : MonoBehaviour
{
    public Button[] buttons;
    public TMP_Text resultText;
    public GameObject[] strikeLines;
    public Button resetButton;
    public TMP_Text turnText;
    public TicTacToeAI ai;
    public Toggle aiToggle; // ✅ Toggle UI for AI mode

    [HideInInspector] public string[] board = new string[9];
    [HideInInspector] public string currentPlayer = "X";
    [HideInInspector] public bool gameOver = false;

    public bool playAgainstAI = true;

    public readonly int[][] winConditions = new int[][]
    {
        new int[] { 0, 1, 2 },
        new int[] { 3, 4, 5 },
        new int[] { 6, 7, 8 },
        new int[] { 0, 3, 6 },
        new int[] { 1, 4, 7 },
        new int[] { 2, 5, 8 },
        new int[] { 0, 4, 8 },
        new int[] { 2, 4, 6 }
    };

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnGridClick(index));
        }

        if (resetButton != null)
            resetButton.onClick.AddListener(ResetGame);

        if (aiToggle != null)
        {
            playAgainstAI = aiToggle.isOn;
            aiToggle.onValueChanged.AddListener(delegate { ToggleAI(); });
        }

        ResetGame();
    }

    void ToggleAI()
    {
        playAgainstAI = aiToggle.isOn;
        ResetGame(); // Optional: Reset game when toggle changes
    }

    public void OnGridClick(int index)
    {
        if (gameOver || !string.IsNullOrEmpty(board[index]))
            return;

        board[index] = currentPlayer;
        TMP_Text buttonText = buttons[index].GetComponentInChildren<TMP_Text>();
        if (buttonText != null)
            buttonText.text = currentPlayer;

        buttons[index].interactable = false;

        CheckWinCondition();

        if (!gameOver)
        {
            currentPlayer = currentPlayer == "X" ? "O" : "X";

            if (turnText != null)
                turnText.text = "Turn: Player " + currentPlayer;

            if (playAgainstAI && currentPlayer == "O" && ai != null)
                Invoke(nameof(TriggerAIMove), 0.5f);
        }
    }

    void TriggerAIMove()
    {
        if (!gameOver)
            ai.MakeAIMove();
    }

    void CheckWinCondition()
    {
        for (int i = 0; i < winConditions.Length; i++)
        {
            int a = winConditions[i][0];
            int b = winConditions[i][1];
            int c = winConditions[i][2];

            if (!string.IsNullOrEmpty(board[a]) && board[a] == board[b] && board[a] == board[c])
            {
                gameOver = true;
                resultText.text = "Player " + board[a] + " Wins!";
                if (strikeLines[i] != null)
                    strikeLines[i].SetActive(true);
                return;
            }
        }

        bool isDraw = true;
        foreach (string cell in board)
        {
            if (string.IsNullOrEmpty(cell))
            {
                isDraw = false;
                break;
            }
        }

        if (isDraw)
        {
            gameOver = true;
            resultText.text = "It's a Draw!";
        }
    }

    public void ResetGame()
    {
        if (buttons == null || buttons.Length != 9 || resultText == null || strikeLines == null)
        {
            Debug.LogError("Missing UI references.");
            return;
        }

        for (int i = 0; i < board.Length; i++)
        {
            board[i] = "";
            TMP_Text buttonText = buttons[i].GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
                buttonText.text = "";

            buttons[i].interactable = true;
        }

        foreach (GameObject line in strikeLines)
        {
            if (line != null)
                line.SetActive(false);
        }

        currentPlayer = "X";
        gameOver = false;
        resultText.text = "";

        if (turnText != null)
            turnText.text = "Turn: Player " + currentPlayer;

        if (playAgainstAI && currentPlayer == "O" && ai != null)
            Invoke(nameof(TriggerAIMove), 0.5f);
    }
}
