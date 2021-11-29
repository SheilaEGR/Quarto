using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Pool pool;
    public Board board;
    public GUI gui;

    enum GameState
    {
        START,
        PLAYER1_SELECT,
        PLAYER1_PLACE,
        PLAYER2_SELECT,
        PLAYER2_PLACE,
        FIND_WINNER,
        GAME_OVER
    }

    private GameReferee gameReferee;
    private GameState state = GameState.START;
    private int selectedPiece = -1;
    private int player = 1;

    private void Start()
    {
        state = GameState.START;
        gameReferee = FindObjectOfType<GameReferee>();
    }

    private void Update()
    {
        switch(state)
        {
            case GameState.START:
                state = GameState.PLAYER1_SELECT;
                pool.StartSelection();
                gui.SetPlayerSelect(1);
                gui.HideResult();
                selectedPiece = -1;
                player = 1;
                break;

            case GameState.PLAYER1_SELECT:
                selectedPiece = pool.SelectedPiece();
                if(selectedPiece >= 0)
                {
                    state = GameState.PLAYER2_PLACE;
                    board.StartTurn(selectedPiece, 2);
                    gui.SetPlayerPlace(2);
                    player = 2;
                }
                break;

            case GameState.PLAYER2_PLACE:
                if(board.PieceWasPlaced())
                {
                    state = GameState.FIND_WINNER;
                    pool.StartSelection();
                    gui.SetPlayerSelect(2);
                    selectedPiece = -1;
                }
                break;

            case GameState.PLAYER2_SELECT:
                selectedPiece = pool.SelectedPiece();
                if(selectedPiece >= 0)
                {
                    state = GameState.PLAYER1_PLACE;
                    board.StartTurn(selectedPiece, 1);
                    gui.SetPlayerPlace(1);
                }
                break;

            case GameState.PLAYER1_PLACE:
                if(board.PieceWasPlaced())
                {
                    state = GameState.FIND_WINNER;
                    pool.StartSelection();
                    gui.SetPlayerSelect(1);
                    selectedPiece = -1;
                    player = 1;
                }
                break;

            case GameState.FIND_WINNER:
                if(gameReferee.IsWinner())
                {
                    state = GameState.GAME_OVER;
                    Debug.Log("Player " + player.ToString() + " win!");
                    gui.ShowResult(player);
                }
                else
                {
                    if (player == 1)
                        state = GameState.PLAYER1_SELECT;
                    else
                        state = GameState.PLAYER2_SELECT;
                }
                break;

            default:
                break;
        }
    }
}
