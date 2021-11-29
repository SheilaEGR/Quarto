using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private const float xOffset = 9.5f;
    private const float yOffset = -3.0f;
    private const float xStep = 1.6f;
    private const float yStep = 1.6f;

    private BlockSet blockSet = null;

    private int selectedIndex = 0;

    // ========================================================================
    // =====    PUBLIC METHODS
    // ========================================================================

    public void StartSelection()
    {
        selectedIndex = -1;
    }

    public int SelectedPiece()
    {
        return selectedIndex;
    }

    public void Restart()
    {
        for (int index = 0; index < blockSet.numBlocks; index++)
            blockSet.blocks[index].inPool = true;
        ShowBlocks();
    }

    // ========================================================================
    // =====    
    // ========================================================================
    private void ShowBlocks()
    {
        if(blockSet == null)
            blockSet = FindObjectOfType<BlockSet>();

        int x = 0, y = 0;
        for(int index=0; index<blockSet.numBlocks; index++)
        {
            if(blockSet.blocks[index].inPool)
            {
                Vector2 pos = new Vector2(
                    x * xStep + xOffset,
                    y * yStep + yOffset
                    );
                blockSet.SetPosition(index, pos);
                blockSet.Deselect(index);

                x++;
                if(x >= 4)
                {
                    x = 0; 
                    y++;
                }
            }
        }

        // No piece is selected
        selectedIndex = -1;
    }

    private void Start()
    {
        ShowBlocks();
    }

    private void Update()
    {
        if (selectedIndex >= 0) return;

        if(Input.GetMouseButtonDown(0))
        {
            for (int index = 0; index < blockSet.numBlocks; index++)
            {
                if (blockSet.blocks[index].inPool && blockSet.IsSelected(index))
                {
                    selectedIndex = index;
                    return;
                }
            }
        }
    }


}
