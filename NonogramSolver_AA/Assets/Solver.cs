using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Solver : MonoBehaviour
{
    public DataBase dataBase;
    
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Point getEmpty()
    {
        for (int i = 0; i < dataBase.Rows; i++)
        {
            for (int j = 0; j < dataBase.Columns; j++)
            {
                if (dataBase.GameBoard[i, j] == 0)
                {
                    return new Point(i,j);
                }
            }
        }

        return Point.Empty;
    }


    public bool validPosition(Point point)
    {


        return true;
    }


    public bool validRow(Point point )
    {
        int[] arr = new int[dataBase.Columns];
        for (int i = 0; i < dataBase.Columns; i++)
        {
            arr[i] = dataBase.GameBoard[point.X, i];
        }
        return validArray(dataBase.RowsArray[point.X], point.Y, arr);
    }
    
    
    public bool validColumn(Point point )
    {
        int[] arr = new int[dataBase.Rows];
        for (int i = 0; i < dataBase.Rows; i++)
        {
            arr[i] = dataBase.GameBoard[i, point.Y];
        }
        return validArray(dataBase.ColumnsArray[point.Y], point.X, arr);

    }


    public bool validArray(int[] tracks, int position , int[] arr)
    {
        int empty = emptySpaces(tracks,arr);
        int occupated = arr.Length - empty;
        int max = maxO(tracks);
        if (occupated > max)
        {
            return false;
        }
        
        
        
        

        return true;
    }


    public bool validSeparation(int[] tracks, int[] arr)
    {

        return true;
    }


    public int maxO(int[] tracks)
    {
        int res = 0;
        for (int i = 0; i < tracks.Length; i++)
        {
            res += tracks[i];
        }

        return res;
    }
    

    public int emptySpaces(int[] tracks, int[] arr)
    {
        int res = maxO(tracks);
        res = arr.Length - res;
        return res;

    }
    
}
