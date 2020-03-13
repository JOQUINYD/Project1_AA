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
        
    }

    private void drawRowsHints()
    {
        // Get a reference to an existing TextMeshPro component or Add one if needed.
        TextMeshPro m_Text;
        TextContainer m_TextContainer;
        m_Text = Instantiate(GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>());
 
        // Get a reference to the text container. Alternatively, you can now use the RectTransform on the text object instead.
        m_TextContainer = Instantiate(GetComponent<TextContainer>());
        m_TextContainer.width = 25f;
        m_TextContainer.height = 3f;
 
        // Set the point size
        m_Text.fontSize = 24;
 
        // Set the text
        m_Text.text = "A simple line of text.";
    }
}
