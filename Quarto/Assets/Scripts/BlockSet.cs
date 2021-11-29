using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public GameObject gameObject;
    public Transform transform;
    public Piece piece;
    public bool inPool;
    public int positionInBoard;
}

public class BlockSet : MonoBehaviour
{
    public GameObject[] piecePrefabs;

    [HideInInspector]
    public int numBlocks = 16;
    [HideInInspector]
    public Block[] blocks;

    private static BlockSet _instance = null;
    public static BlockSet Instance { get { return _instance; } }

    private Vector2 moveTo;
    private int blockToMove;
    private bool isMoving;
    private float speed = 10f;

    // ========================================================================
    // =====    PUBLIC METHODS
    // ========================================================================
    public void SetPosition(int blockIndex, Vector2 position)
    {
        if (!ValidBlock(blockIndex)) return;

        blocks[blockIndex].transform.position = position;
    }

    public void MoveToPosition(int blockIndex, Vector2 position)
    {
        if (!ValidBlock(blockIndex)) return;

        //blocks[blockIndex].transform.position = position;
        blockToMove = blockIndex;
        moveTo = position;
        isMoving = true;
    }

    public void Deselect(int blockIndex)
    {
        if (!ValidBlock(blockIndex)) return;

        blocks[blockIndex].piece.Deselect();
    }

    public bool IsSelected(int blockIndex)
    {
        if (!ValidBlock(blockIndex)) return false;

        return blocks[blockIndex].piece.Selected();
    }

    public void SetInBoard(int blockIndex, int positionInBoard)
    {
        if (!ValidBlock(blockIndex)) return;

        blocks[blockIndex].inPool = false;
        blocks[blockIndex].positionInBoard = positionInBoard;
    }

    public int GetPositionInBoard(int blockIndex)
    {
        if (!ValidBlock(blockIndex)) return -1;
        return blocks[blockIndex].positionInBoard;
    }

    public bool BlocksMoving()
    {
        return isMoving;
    }

    // ========================================================================
    // =====    INITIALIZATION
    // ========================================================================
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
        CreateBlocks();
    }

    private void CreateBlocks()
    {
        blocks = new Block[numBlocks];
        for (int index = 0; index < numBlocks; index++)
        {
            GameObject go = Instantiate(piecePrefabs[index]);

            blocks[index] = new Block();
            blocks[index].gameObject = go;
            blocks[index].transform = go.transform;
            blocks[index].piece = go.transform.GetComponent<Piece>();
            blocks[index].inPool = true;
            blocks[index].positionInBoard = -1;
        }
    }

    bool ValidBlock(int blockIndex)
    {
        if (blockIndex < 0 || blockIndex >= numBlocks) return false;
        return true;
    }


    private void Update()
    {
        if (!isMoving) return;

        Vector2 posNow = blocks[blockToMove].transform.position;
        Vector2 dPos = moveTo - posNow;
        if (dPos.magnitude < 0.1f)
        {
            blocks[blockToMove].transform.position = moveTo;
            isMoving = false;
        }
        else
        {
            blocks[blockToMove].transform.position = Vector2.Lerp(
                posNow,
                moveTo,
                Time.deltaTime * speed
                );
        }
    }
}
