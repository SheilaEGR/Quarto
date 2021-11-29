using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Camera boardCamera;
    public ClickableItem[] boardPlaces = new ClickableItem[16];

    private BlockSet blockSet = null;
    private GameReferee gameReferee = null;

    private Vector2 player1Pos = new Vector2(-9f, -5f);
    private Vector2 player2Pos = new Vector2(-9f, 5f);
    private const float xOffset = -3.6f;
    private const float yOffset = -3.6f;
    private const float xStep = 2.4f;
    private const float yStep = 2.4f;

    private int selectedPiece;
    private bool done;

    // ========================================================================
    // =====    PUBLIC METHODS
    // ========================================================================
    public void StartTurn(int selectedPiece, int player)
    {
        //boardCamera.gameObject.SetActive(true);

        Vector2 pos = player == 1 ? player1Pos : player2Pos;
        ShowSelectedPiece(selectedPiece, pos);
        
        this.selectedPiece = selectedPiece;
        done = false;
    }

    public bool PieceWasPlaced()
    {
        return done;
    }

    public void Restart()
    {
        for (int i = 0; i < blockSet.numBlocks; i++)
        {
            boardPlaces[i].Deselect();
            boardPlaces[i].Enable();
        }
    }

    // ========================================================================
    // ========================================================================
    private void ShowSelectedPiece(int selectedPiece, Vector2 pos)
    {
        if (blockSet == null)
            blockSet = FindObjectOfType<BlockSet>();

        blockSet.MoveToPosition(selectedPiece, pos);
        DeselectBoardPlaces();
    }

    private void DeselectBoardPlaces()
    {
        for (int i = 0; i < blockSet.numBlocks; i++)
        {
            boardPlaces[i].Deselect();
        }
    }

    private int FindSelectedPlace()
    {
        if (blockSet == null)
            blockSet = FindObjectOfType<BlockSet>();

        for (int i=0; i<blockSet.numBlocks; i++)
        {
            if (boardPlaces[i].Selected())
                return i;
        }
        return -1;
    }

    // ========================================================================
    // ========================================================================
    private void Start()
    {
        gameReferee = FindObjectOfType<GameReferee>();    
    }

    private void Update()
    {
        if (done) return;

        int selectedPlace = FindSelectedPlace();
        if(selectedPlace >= 0)
        {
            Vector2 position = new Vector2(
            (selectedPlace % 4) * xStep + xOffset,
            (selectedPlace / 4) * yStep + yOffset
            );
            blockSet.MoveToPosition(selectedPiece, position);
            blockSet.SetInBoard(selectedPiece, selectedPlace);
            gameReferee.SetPiece(selectedPlace, selectedPiece);
            boardPlaces[selectedPlace].Disable();

            done = true;
        }
    }
}
