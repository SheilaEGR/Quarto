
using UnityEngine;
using UnityEngine.UI;

public class ResultGUI : MonoBehaviour
{
    public Text resultText;
    public Text winLineText;
    public Text winStateText;

    private GameReferee gameReferee;

    public void ShowResult(int player)
    {
        if (gameReferee == null)
            gameReferee = FindObjectOfType<GameReferee>();

        resultText.text = "Player " + player.ToString() + " win!";
        PrintWinLine();
        PrintWinState();
    }

    private void PrintWinLine()
    {
        GameReferee.WIN_LINE winLine = gameReferee.GetWinLine(); 
        switch(winLine)
        {
            case GameReferee.WIN_LINE.HORIZ_0:
                winLineText.text = "Horizontal line 0";
                break;
            case GameReferee.WIN_LINE.HORIZ_1:
                winLineText.text = "Horizontal line 1";
                break;
            case GameReferee.WIN_LINE.HORIZ_2:
                winLineText.text = "Horizontal line 2";
                break;
            case GameReferee.WIN_LINE.HORIZ_3:
                winLineText.text = "Horizontal line 3";
                break;

            case GameReferee.WIN_LINE.VERT_0:
                winLineText.text = "Vertical line 0";
                break;
            case GameReferee.WIN_LINE.VERT_1:
                winLineText.text = "Vertical line 1";
                break;
            case GameReferee.WIN_LINE.VERT_2:
                winLineText.text = "Vertical line 2";
                break;
            case GameReferee.WIN_LINE.VERT_3:
                winLineText.text = "Vertical line 3";
                break;

            case GameReferee.WIN_LINE.DIAG_1:
                winLineText.text = "Diagonal line 0";
                break;
            case GameReferee.WIN_LINE.DIAG_2:
                winLineText.text = "Diagonal line 1";
                break;
        }
    }

    private void PrintWinState()
    {
        Piece winState = gameReferee.GetWinState();
        winStateText.text = "";
        if (winState.white)
            winStateText.text += "COLOR\n";
        if (winState.rect)
            winStateText.text += "SHAPE\n";
        if (winState.solid)
            winStateText.text += "SOLID/HOLLOW\n";
        if (winState.large)
            winStateText.text += "SIZE\n";
    }
}
