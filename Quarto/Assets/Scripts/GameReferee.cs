using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReferee : MonoBehaviour
{
    public enum WIN_LINE
    {
        NONE,
        HORIZ_0,
        HORIZ_1,
        HORIZ_2,
        HORIZ_3,
        VERT_0,
        VERT_1,
        VERT_2,
        VERT_3,
        DIAG_1,
        DIAG_2
    }

    private BlockSet blockSet = null;
    private int[] boardRep = new int[16];
    private Piece winState = new Piece();
    private WIN_LINE winLine;

    private static GameReferee _instance = null;
    public static GameReferee Instance { get { return _instance; } }

    public void SetPiece(int boardPosition, int pieceIndex)
    {
        boardRep[boardPosition] = pieceIndex;
    }

    public bool IsWinner()
    {
        if (blockSet == null)
            blockSet = FindObjectOfType<BlockSet>();

        ClearWinState();
        winLine = WIN_LINE.NONE;

        // Horizontal
        if (CheckLine(0, 1, 2, 3))
        {
            winLine = WIN_LINE.HORIZ_0;
            return true;
        }
        if (CheckLine(4, 5, 6, 7))
        {
            winLine = WIN_LINE.HORIZ_1;
            return true;
        }
        if (CheckLine(8, 9, 10, 11))
        {
            winLine = WIN_LINE.HORIZ_2;
            return true;
        }
        if (CheckLine(12, 13, 14, 15))
        {
            winLine = WIN_LINE.HORIZ_3;
            return true;
        }

        // Vertical
        if (CheckLine(0, 4, 8, 12))
        {
            winLine = WIN_LINE.VERT_0;
            return true;
        }
        if (CheckLine(1, 5, 9, 13))
        {
            winLine = WIN_LINE.VERT_1;
            return true;
        }
        if (CheckLine(2, 6, 10, 14))
        {
            winLine = WIN_LINE.VERT_2;
            return true;
        }
        if (CheckLine(3, 7, 11, 15))
        {
            winLine = WIN_LINE.VERT_3;
            return true;
        }

        // Diagonal
        if (CheckLine(0, 5, 10, 15))
        {
            winLine = WIN_LINE.DIAG_1;
            return true;
        }
        if (CheckLine(3, 6, 9, 12))
        {
            winLine = WIN_LINE.DIAG_2;
            return true;
        }

        return false;
    }

    public Piece GetWinState()
    {
        return winState;
    }

    public WIN_LINE GetWinLine()
    {
        return winLine;
    }

    public void Restart()
    {
        if (blockSet == null)
            blockSet = FindObjectOfType<BlockSet>();

        for (int i = 0; i < blockSet.numBlocks; i++)
            boardRep[i] = -1;
    }

    // ========================================================================
    // ========================================================================
    private void Start()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
        Restart();
    }

    // ========================================================================
    // ========================================================================
    private void ClearWinState()
    {
        winState.white = false;
        winState.rect = false;
        winState.solid = false;
        winState.large = false;
    }

    private bool CheckWinState()
    {
        if (winState.white || winState.rect || winState.large || winState.solid)
            return true;
        return false;
    }

    bool CheckLine(int elem1, int elem2, int elem3, int elem4)
    {
        // Row 0
        if(boardRep[elem1] >= 0 && boardRep[elem2] >= 0 && 
            boardRep[elem3] >= 0 && boardRep[elem4] >= 0)
        {
            winState.white = CheckColor(elem1, elem2, elem3, elem4);
            winState.rect = CheckShape(elem1, elem2, elem3, elem4);
            winState.large = CheckSize(elem1, elem2, elem3, elem4);
            winState.solid = CheckSolid(elem1, elem2, elem3, elem4);
            return CheckWinState();
        }

        return false;
    }

    bool CheckColor(int elem1, int elem2, int elem3, int elem4)
    {
        bool allFalse = !blockSet.blocks[boardRep[elem1]].piece.white &
                !blockSet.blocks[boardRep[elem2]].piece.white &
                !blockSet.blocks[boardRep[elem3]].piece.white &
                !blockSet.blocks[boardRep[elem4]].piece.white;
        bool allTrue = blockSet.blocks[boardRep[elem1]].piece.white &
                blockSet.blocks[boardRep[elem2]].piece.white &
                blockSet.blocks[boardRep[elem3]].piece.white &
                blockSet.blocks[boardRep[elem4]].piece.white;
        if (allTrue || allFalse)
            return true;
        return false;
    }

    bool CheckShape(int elem1, int elem2, int elem3, int elem4)
    {
        bool allFalse = !blockSet.blocks[boardRep[elem1]].piece.rect &
                !blockSet.blocks[boardRep[elem2]].piece.rect &
                !blockSet.blocks[boardRep[elem3]].piece.rect &
                !blockSet.blocks[boardRep[elem4]].piece.rect;
        bool allTrue = blockSet.blocks[boardRep[elem1]].piece.rect &
                blockSet.blocks[boardRep[elem2]].piece.rect &
                blockSet.blocks[boardRep[elem3]].piece.rect &
                blockSet.blocks[boardRep[elem4]].piece.rect;
        if (allTrue || allFalse)
            return true;
        return false;
    }

    bool CheckSize(int elem1, int elem2, int elem3, int elem4)
    {
        bool allFalse = !blockSet.blocks[boardRep[elem1]].piece.large &
                !blockSet.blocks[boardRep[elem2]].piece.large &
                !blockSet.blocks[boardRep[elem3]].piece.large &
                !blockSet.blocks[boardRep[elem4]].piece.large;
        bool allTrue = blockSet.blocks[boardRep[elem1]].piece.large &
                blockSet.blocks[boardRep[elem2]].piece.large &
                blockSet.blocks[boardRep[elem3]].piece.large &
                blockSet.blocks[boardRep[elem4]].piece.large;
        if (allTrue || allFalse)
            return true;
        return false;
    }

    bool CheckSolid(int elem1, int elem2, int elem3, int elem4)
    {
        bool allFalse = !blockSet.blocks[boardRep[elem1]].piece.solid &
                !blockSet.blocks[boardRep[elem2]].piece.solid &
                !blockSet.blocks[boardRep[elem3]].piece.solid &
                !blockSet.blocks[boardRep[elem4]].piece.solid;
        bool allTrue = blockSet.blocks[boardRep[elem1]].piece.solid &
                blockSet.blocks[boardRep[elem2]].piece.solid &
                blockSet.blocks[boardRep[elem3]].piece.solid &
                blockSet.blocks[boardRep[elem4]].piece.solid;
        if (allTrue || allFalse)
            return true;
        return false;
    }
}
