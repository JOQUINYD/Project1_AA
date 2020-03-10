using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Solver : MonoBehaviour
{
    public DataBase dataBase;

    public int cantxd = 0;

    void Start()
    {

    }

    public bool solve()
    {
       // print(cantxd);
        //cantxd++;
        Point find = getEmpty();
        if (find == Point.Empty)
        {
            return true;
            return validRows() && validColumns();
        }

        find.X = find.X - 1;
        find.Y = find.Y - 1;
        


        for (int i = 1; i < 3; i++)
        {
            if (validPosition(find, i))
            {
                dataBase.GameBoard[find.X][find.Y] = i;
                if (solve())
                {
                    return true;
                }

                dataBase.GameBoard[find.X][find.Y] = 0; 
            }
        }
        
        
        
        
        /*
         Este funciona con el for de 1 y 2 arriba
         else
        {
            if (validPosition(find,2)) 
            {
                dataBase.GameBoard[find.X][find.Y] = 2;
                if (solve())
                {
                    return true;
                }

                dataBase.GameBoard[find.X][find.Y] = 0;         
            }

        }*/


        /*for (int i = 1; i < 2; i++)
        {
            if (validPosition(find,i))
            {
                dataBase.GameBoard[find.X][find.Y] = i;
                if (solve())
                {
                    return true;
                }
                dataBase.GameBoard[find.X][find.Y] = 0;   
            }
        }*/
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
        return validRow(point, n) && validColumn(point, n);
    }
    


    public bool validRows()
    {
        for (int i = 0; i < dataBase.Rows; i++)
        {
            if (!validArrayA(dataBase.RowsArray[i], dataBase.GameBoard[i], 1))
            {
                return false;
            }

        }

        return true;
    }


    public bool validRow(Point point, int n)
    {
        int[] arr = new int[dataBase.Columns];
        for (int i = 0; i < dataBase.Columns; i++)
        {
            arr[i] = dataBase.GameBoard[point.X][i];
        }
        arr[point.Y] = n;
        return validArrayA(dataBase.RowsArray[point.X], arr, n);
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

            if (!validArrayA(dataBase.ColumnsArray[i], arr, 1))
            {
                return false;
            }
        }

        return true;

    }


    public bool validColumn(Point point, int n)
    {
        int[] arr = new int[dataBase.Rows];
        for (int i = 0; i < dataBase.Rows; i++)
        {
            arr[i] = dataBase.GameBoard[i][point.Y];
        }

        arr[point.X] = n;
        return validArrayA(dataBase.ColumnsArray[point.Y], arr,n);

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

    public int cantPistas(int[] traks)
    {
        int r = 0;
        for (int i = 0; i < traks.Length; i++)
        {
            r += traks[i];
        }
        return r;
    }

    public int cantLlenas(int[] arr)
    {
        int res = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == 1)
            {
                res += 1;
            }
        }

        return res;
    }
    
    public int cantDisponibles(int[] arr)
    {
        int res = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == 0)
            {
                res += 1;
            }
        }

        return res;
    }
    
    public bool validArrayA2(int[] tracks, int[] arr, int n)
    {
        if (cantDisponibles(arr) < cantPistas(tracks) - cantLlenas(arr))
        {
            return false;
        }
        int[] arrT = arrToTraks(arr,tracks.Length);
        for (int i = 0; i < arrT.Length; i++)
        {

            if (arrT[i]!=0 && arrT[i] < tracks[i])
            {
                return false;
            }

        }
        
        return true;
    }

    public bool validArrayA(int[] tracks, int[] arr,int n)
    {
        if (n == 2)
        {
            return validArrayA2(tracks, arr, n);
        }
        

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
