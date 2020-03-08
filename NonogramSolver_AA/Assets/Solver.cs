using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Solver : MonoBehaviour
{
    public DataBase dataBase;


    void Start()
    {

    }

    public bool solve()
    {

        Point find = getEmpty();
        if (find == Point.Empty)
        {
            return validRows() && validColumns();
        }

        find.X = find.X - 1;
        find.Y = find.Y - 1;
        

        if (validPosition(find,1))
        {
            for (int i = 1; i < 3; i++)
            {
                dataBase.GameBoard[find.X][find.Y] = i;
                if (solve())
                {
                    return true;
                }

                dataBase.GameBoard[find.X][find.Y] = 0;
            }
        }
        else
        {
            
            dataBase.GameBoard[find.X][find.Y] = 2;
            if (solve())
            {
                return true;
            }

            dataBase.GameBoard[find.X][find.Y] = 0;
        }


        return false;
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
                if (dataBase.GameBoard[i][j] == 0)
                {
                    return new Point(i + 1, j + 1);
                }
            }
        }

        return Point.Empty;
    }


    public bool validPosition(Point point, int n)
    {
        return validRow(point) && validColumn(point);
    }
    


    public bool validRows()
    {
        for (int i = 0; i < dataBase.Rows; i++)
        {
            if (!validArrayA(dataBase.RowsArray[i], dataBase.GameBoard[i]))
            {
                return false;
            }

        }

        return true;
    }


    public bool validRow(Point point)
    {
        return validArrayA(dataBase.RowsArray[point.X], dataBase.GameBoard[point.X]);
    }




    public bool validColumns()
    {
        bool res = true;
        for (int i = 0; i < dataBase.Columns; i++)
        {
            int[] arr = new int[dataBase.Rows];
            for (int j = 0; j < dataBase.Rows; j++)
            {
                arr[j] = dataBase.GameBoard[j][i];
            }

            if (!validArrayA(dataBase.ColumnsArray[i], arr))
            {
                return false;
            }
        }

        return true;

    }


    public bool validColumn(Point point)
    {
        int[] arr = new int[dataBase.Rows];
        for (int i = 0; i < dataBase.Rows; i++)
        {
            arr[i] = dataBase.GameBoard[i][point.Y];
        }

        arr[point.X] = 1;
        return validArrayA(dataBase.ColumnsArray[point.Y], arr);

    }

    public int cantGrupos(int[] arr)
    {
        int res = 0;
        bool agrego = false;

        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i]==1 && !agrego)
            {
                agrego = true;
                res++;
            }
            if(arr[i]!= 1)
            {
                agrego = false;
            }
            
        }
        
        
        return res;

    }

    public bool arrayFull(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == 0)
            {
                return false;
            }   
        }
        

        return true;
    }

    int[] arrToTraks(int[] arr, int c)
    {
        int[] res = new int[c];
        int count = 0;
        int n = 0;
        bool encontro = false;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == 1 )
            {
                encontro = true;
                n++;
            }
            else
            {
                if (encontro)
                {
                    res[count] = n;
                    count++;
                    n = 0;
                    encontro = false;                   
                }
            }
        }

        if (n>0)
        {
            res[count] = n;
        }

        return res;
    }


public bool validArrayA(int[] tracks, int[] arr)
{

        int traksLen = tracks.Length;

        if (traksLen == 0)
        {
            return false;
        }
        if (tracks[0] == arr.Length)
        {
            return true;
        }
        
        bool arrFull = arrayFull(arr);
        int cantGr = cantGrupos(arr);
        
        if (cantGr  > traksLen)
        {
            return false;
        }

        int[] arrT = arrToTraks(arr,traksLen);

        for (int i = 0; i < traksLen; i++)
        {

            if (arrT[i] > tracks[i])
            {
                return false;
            }

            if (arrFull && arrT[i] != tracks[i])
            {
                return false;
            }
        }

        return true;
    }
    
    
    
    
    public bool validArrayB(int[] tracks, int[] arr)
    {


        int trackPos = 0;
        int empty = emptySpacesA(arr);
        int c = 0;
        bool found = false;

        for (int i = 0; i < arr.Length; i++)
        {
            if (trackPos > tracks.Length)
            {

                return false;
            }

            if ((arr[i] == 0 || arr[i] == 2) && found)
            {
                if (empty > 0)
                {
                    if (trackPos < tracks.Length && tracks[trackPos] >= c)
                    {
                        trackPos++;
                        found = false;
                        c = 0;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    if (trackPos < tracks.Length && tracks[trackPos] == c)
                    {
                        trackPos++;
                        found = false;
                        c = 0;
                    }
                    else
                    {
                        return false;
                    }
                }

            }

            if (arr[i] == 1)
            {

                c++;
                found = true;
            }
        }

        if (c > 0)
        {
            if (empty > 0)
            {
                if (trackPos < tracks.Length && !(tracks[trackPos] >= c))
                {
                    return false;
                }
            }
            else
            {
                if (trackPos < tracks.Length && !(tracks[trackPos] == c))
                {
                    return false;
                }
            }

            trackPos++;
            if (trackPos > tracks.Length)
            {
                return false;
            }
        }

        return true;
    }
    

    public int emptySpacesA( int[] arr)
    {
        int res = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i]==0)
            {
                res++;
            } 
        }
        return res;

    }
    
}
