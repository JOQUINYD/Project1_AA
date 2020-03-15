using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GridDrawer : MonoBehaviour
{
    private GameObject prefabBlackTile;
    private GameObject prefabDarkBlueTile;
    public TextMeshProUGUI timeStamp;
    private TextMeshPro tmpScore;
    public RectTransform hintsContainer;
    
    private int numCols;
    private int numRows;

    private int maxSize;
    private float space;
    private float maxSizeX;
    private float maxSizeY;
    private float tileSize;
    private float initPosX;
    private float initPosY;
    private float gridSpacing;
    private int[][] mainBoard;
    private GameObject[][] graphBoard;
    
    private int[][] rowsHints;
    private int[][] columnsHints;
    
    
    public GridDrawer(int numCols, int numRows, int[][] mainBoard)
    {
        this.numCols = numCols;
        this.numRows = numRows;
        this.mainBoard = mainBoard;
        setValues();
    }

    public GridDrawer(int[][] mainBoard)
    {
        this.mainBoard = mainBoard;
        this.numRows = this.mainBoard.Length;
        this.numCols = this.mainBoard[0].Length;
        setValues();
    }

    public GridDrawer()
    {
    }

    public void _init_gridDrawer(int[][] _mainBoard, int[][] _rowsHints, int[][] _columnsHints)
    {
        this.mainBoard = _mainBoard;
        this.numRows = this.mainBoard.Length;
        this.numCols = this.mainBoard[0].Length;
        this.rowsHints = _rowsHints;
        this.columnsHints = _columnsHints;
        setValues();
        initGraphBoard();
        drawFrame();
        drawHints();
    }

    public void cleanGraphBoard()
    {
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                drawWhiteTile(i,j);
            }
        }
    }
    
    void setValues()
    {
        maxSize = 280;
        space = 4f*15;
        if (numCols <= numRows)
        {
            maxSizeY = (float) ((maxSize - space) * 200);
            maxSizeX = (maxSizeY / numRows) * numCols;
        }
        else
        {
            maxSizeX = (float) ((maxSize - space) * 200); 
            maxSizeY = (maxSizeX / numCols) * numRows;
        }

        initPosX = -maxSizeX / 200;
        initPosY = maxSizeY / 200;
        gridSpacing = maxSizeX / (numCols * 100);
        tileSize = maxSizeX / numCols;
        
        prefabBlackTile = Resources.Load<GameObject>("black_pixel");
        prefabDarkBlueTile = Resources.Load<GameObject>("dark_blue_pixel");
    }

    void initGraphBoard()
    {
        graphBoard = new GameObject[numRows][];
        for (int i = 0; i < numRows; i++)
        {
            graphBoard[i] = new GameObject[numCols];
            for (int j = 0; j < numCols; j++)
            {
                float posX = initPosX + (gridSpacing * j) + (gridSpacing / 2);
                float posY = initPosY - (gridSpacing * i) - (gridSpacing / 2);
        
                GameObject blackTile = Instantiate(this.prefabDarkBlueTile);
                blackTile.transform.localScale = new Vector3(tileSize,tileSize);
                blackTile.transform.position = new Vector3(posX, posY);
                blackTile.SetActive(false);

                graphBoard[i][j] = blackTile;
            }
        }
    }
    
    void drawFrame()
    {

        GameObject yFrame = Instantiate(prefabBlackTile);
        GameObject xFrame = Instantiate(prefabBlackTile);
        
        // leftmost and upper bold frame 
        yFrame.transform.localScale = new Vector3(20*(maxSize/15),maxSizeY);
        xFrame.transform.localScale = new Vector3(maxSizeX,20*(maxSize/15));
        
        yFrame.transform.position = new Vector3(initPosX,0);
        xFrame.transform.position = new Vector3(0,initPosY);
        
        // rightmost and lower bold frame
        yFrame = Instantiate(prefabBlackTile);
        xFrame = Instantiate(prefabBlackTile);
        yFrame.transform.localScale = new Vector3(20*(maxSize/15),maxSizeY);
        xFrame.transform.localScale = new Vector3(maxSizeX,20*(maxSize/15));
        
        yFrame.transform.position = new Vector3((maxSizeX/200),0);
        xFrame.transform.position = new Vector3(0,(-maxSizeY/200));

        // draw vertical lines
        float nextPosX = initPosX;
        for (int i = 1; i < numCols; i++)
        {
            nextPosX += gridSpacing;
            yFrame = Instantiate(prefabBlackTile);

            if (i % 5 == 0)
            {
                yFrame.transform.localScale = new Vector3(20*(maxSize/15),maxSizeY);
            }
            else
            {
                yFrame.transform.localScale = new Vector3(5*(maxSize/15), maxSizeY);
            }

            yFrame.transform.position = new Vector3(nextPosX,0);
        }
        
        //draw horizontal lines
        float nextPosY = initPosY;
        for (int i = 1; i < numRows; i++)
        {
            nextPosY -= gridSpacing;
            xFrame = Instantiate(prefabBlackTile);

            if (i % 5 == 0)
            {
                xFrame.transform.localScale = new Vector3(maxSizeX,20*(maxSize/15));
            }
            else
            {
                xFrame.transform.localScale = new Vector3(maxSizeX, 5*(maxSize/15));
            }

            xFrame.transform.position = new Vector3(0,nextPosY);
        }
    }
    
    // draws a single black tile
    public void drawBlackTile(float posRow, float posCol)
    {
        graphBoard[(int) posRow][(int) posCol].SetActive(true);
    }

    public void drawWhiteTile(float posRow, float posCol)
    {
        graphBoard[(int) posRow][(int) posCol].SetActive(false);
    }

    public void drawAllBoard()
    {
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                if (mainBoard[i][j] == 1)
                {
                    drawBlackTile(i,j);
                }
                else
                {
                    drawWhiteTile(i,j);
                }
            }
        }
    }

    int[][] generateRandomBoard()
    {
        int[][] board = new int[numRows][];
        for (int i = 0; i < numRows; i++)
        {
            board[i] = new int[numCols];
            for (int j = 0; j < numCols; j++)
            {
                board[i][j] = Random.Range(0, 3);
            }
        }

        return board;
    }

    void printBoard(int[][] board)
    {
        string strBoard = "";
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                strBoard += board[i][j].ToString();
            }
            strBoard += "\n";
        }
        Debug.Log(strBoard);
    }

    public void drawTimeStamp(long time)
    {
        timeStamp.text = time.ToString();
    }

    public void drawHints()
    {
        drawRowsHints();
        drawColsHints();
    }

    private void drawRowsHints()
    {
        int fontSize;
        float SizeDelta = defineMaxSizeDelta(findMaxNumOfHints(rowsHints));
        float posX;
        float posY;
        string hints;
        int numOfHints; 
        
        for (int i = 0; i < numRows; i++)
        {
            numOfHints = rowsHints[i].Length;
            fontSize = defineFontSize(numOfHints);
            //SizeDelta = defineMaxSizeDelta(numOfHints);
            float[] pos = defineHintPosRows(i, SizeDelta);
            posX = pos[0];
            posY = pos[1];
            hints = RowHintsToString(rowsHints[i]);
            drawHint(hints,fontSize,SizeDelta,50, posX, posY, true);
        }
    }

    private void drawColsHints()
    {
        int fontSize;
        float SizeDelta = space;
        float posX;
        float posY;
        string hints;
        int numOfHints;
        for (int i = 0; i < numCols; i++)
        {
            numOfHints = columnsHints[i].Length;
            fontSize = defineFontSize(numOfHints);
            float[] pos = defineHintPosCols(i, SizeDelta);
            posX = pos[0];
            posY = pos[1];
            hints = ColHintsToString(columnsHints[i]);
            drawHint(hints,fontSize, tileSize, SizeDelta, posX, posY, false);
        }
    }


    private int defineFontSize(int numOfHints)
    {
        if (numOfHints > 5)
        {
            return 12 - (numOfHints / 4);
        }

        return 12;
    }

    private float defineMaxSizeDelta(int maxNumOfHints)
    {
        float spaceBtHints = 25;
        return maxNumOfHints * spaceBtHints;
    }

    private int findMaxNumOfHints(int[][] hints)
    {
        int max = 0;
        for (int i = 0; i < hints.Length; i++)
        {
            if (hints[i].Length > max)
                max = hints[i].Length;
        }

        return max;
    }

    private float[] defineHintPosCols(int posCol, float maxSizeDelta)
    {
        
        float posX = initPosX + (gridSpacing * posCol) + (gridSpacing / 2);
        float posY = initPosY + (maxSizeDelta / 1.8f);

        float[] position = new[] {posX, posY};

        return position;
    }

    private float[] defineHintPosRows(int posRow, float maxSizeDelta)
    {
        float posX = initPosX - (maxSizeDelta/1.8f);
        float posY = initPosY - (gridSpacing * posRow) - (gridSpacing / 2);
        
        float[] position = new[] {posX, posY};

        return position;
    }

    private string RowHintsToString(int[] hints)
    {
        string text = "";
        for (int i = 0; i < hints.Length; i++)
        {
            text += hints[i].ToString() + " ";
        }

        return text;
    }
    
    private string ColHintsToString(int[] hints)
    {
        string text = "";
        for (int i = 0; i < hints.Length; i++)
        {
            text += hints[i].ToString() + "\n";
        }

        return text;
    }


    public void drawHint(string text, int fontSize, float maxWidth, float maxHeight, float posX, float posY, bool rightAlignment)
    {
        fontSize = 12;
        GameObject textContainer = new GameObject("HintTextMesh", typeof(TextMeshProUGUI));
        textContainer.transform.SetParent(hintsContainer.transform, false);
        textContainer.GetComponent<TextMeshProUGUI>().text = text;
        if (rightAlignment)
        {
            textContainer.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
        }
        else
        {
            textContainer.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            textContainer.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Bottom;
        }
        textContainer.GetComponent<TextMeshProUGUI>().fontSize = fontSize;
        textContainer.GetComponent<TextMeshProUGUI>().color = Color.black;

        RectTransform rectTransform = textContainer.GetComponent<RectTransform>();
        
        rectTransform.sizeDelta = new Vector2(maxWidth,maxHeight);
        rectTransform.position = new Vector3(posX,posY);
    }
    
    
}
