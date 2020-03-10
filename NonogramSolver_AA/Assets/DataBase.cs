using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    private int rows;
    private int columns;
    private int[][] rowsArray;
    private int[][] columnsArray;
    private int[][] gameBoard;
    private bool pasoAPaso;

    public GridDrawer grid;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    
    
    public bool PasoAPaso
    {
        get => pasoAPaso;
        set => pasoAPaso = value;
    }
    public int Columns
    {
        get => columns;
        set => columns = value;
    }

    public int Rows
    {
        get => rows;
        set => rows = value;
    }

    public int[][] ColumnsArray
    {
        get => columnsArray;
        set => columnsArray = value;
    }

    public int[][] RowsArray
    {
        get => rowsArray;
        set => rowsArray = value;
    }

    public int[][] GameBoard
    {
        get => gameBoard;
        set => gameBoard = value;
    }

    public void printBoard()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                //print("Aiuda");
                print(gameBoard[i][j]);
            }
        }
    }

    public void printNonogram()
    {
        string res = "";

        for (int i = 0; i < rows; i++)
        {
            
            res += "\n";
            for (int j = 0; j < columns; j++)
            {
                res += gameBoard[i][j];
                res += "  ";
                
            }
        }

        print(res);
    }
    
    public void printNonogramBonito()
    {
        string res = "";

        for (int i = 0; i < rows; i++)
        {
            
            res += "\n";
            for (int j = 0; j < columns; j++)
            {
                if (gameBoard[i][j] == 1)
                {
                    res += "▓";
                }
                else

                {
                    res += "▒"; 
                }

            }
        }

        print(res);
    }

    public int[][] transponer()
    {
        int[][] res = new int[columns][];
        for (int i = 0; i < res.Length; i++)
        {
            res[i]= new int[rows];
        }
        for (int a = 0; a < Rows; a++)
        {
            for (int b = 0; b < columns; b++)
            {
                res[b][a] = gameBoard[a][b];
               
            }
        }

        return res;
    }
    
    
    
}
