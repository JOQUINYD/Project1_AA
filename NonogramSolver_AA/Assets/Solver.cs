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
        int[] p1 = {3};
        int[] a = {2, 1, 2, 2, 2};


    }

    public bool solve()
    {

        Point find = getEmpty();
        if (find == Point.Empty)
        {

            return  validRows() && validColumns();
        }
        
        
        find.X = find.X - 1;
        find.Y = find.Y - 1;
        if (validPosition(find))
        {
            for (int i = 1; i < 3; i++)
            {
                dataBase.GameBoard[find.X, find.Y] = i;
                if (solve())
                {
                    return true;
                }    
                dataBase.GameBoard[find.X, find.Y] = 0;
            }
        }
        else
        {
            dataBase.GameBoard[find.X, find.Y] = 2;
            if (solve())
            {
                return true;
            }    
            dataBase.GameBoard[find.X, find.Y] = 0;
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
                if (dataBase.GameBoard[i, j] == 0)
                {
                  //  print("Retorno un punto");
                    return new Point(i+1,j+1);
                }
            }
        }

        return Point.Empty;
    }


    public bool validPosition(Point point)
    {
        return validRow(point) && validColumn(point);
    }


    public bool validRows()
    {
        bool res = true;
        for (int i = 0; i < dataBase.Rows; i++)
        {
          /*  string text = "";
            text += "Se validara la fila " + i + " : ";
*/

            int[] arr = new int[dataBase.Columns];
            for (int j = 0; j < dataBase.Columns; j++)
            {
                arr[j] = dataBase.GameBoard[i, j];
                //text += arr[j] + ", ";
            }
            //print(text);


           /* string text2 = "Las pistas son: ";
            for (int j = 0; j < dataBase.RowsArray[i].Length; j++)
            {
                text2 += dataBase.RowsArray[i][j];
                text2 += ", ";

            }

            print(text2);*/

           if (!validArrayA(dataBase.RowsArray[i], arr))
           {
               return false;
           }
            

            
           // print("Esta fila es " + rest);
           

        }

        return true;

    }
    
    
    public bool validRow(Point point )
    {
        int[] arr = new int[dataBase.Columns];
        for (int i = 0; i < dataBase.Columns; i++)
        {
            arr[i] = dataBase.GameBoard[point.X, i];
        }
        arr[ point.Y] = 1;
        return validArrayA(dataBase.RowsArray[point.X],  arr);
    }
    
    
    
    
    public bool validColumns()
    {
        bool res = true;
        for (int i = 0; i < dataBase.Columns; i++)
        {
            int[] arr = new int[dataBase.Rows];
            for (int j = 0; j < dataBase.Rows; j++)
            {
                arr[j] = dataBase.GameBoard[j,i];
            }

            if (!validArrayA(dataBase.ColumnsArray[i], arr))
            {
                return false;
            }
        }
        return true;

    }
    
    
    public bool validColumn(Point point )
    {
        int[] arr = new int[dataBase.Rows];
        for (int i = 0; i < dataBase.Rows; i++)
        {
            arr[i] = dataBase.GameBoard[i, point.Y];
        }

        arr[ point.X] = 1;
        return validArrayA(dataBase.ColumnsArray[point.Y], arr);

    }


    public bool validArrayA(int[] tracks, int[] arr)
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

            if ((arr[i] == 0 ||arr[i] == 2) && found)
            {
                if (empty>0)
                {
                    if (trackPos<tracks.Length && tracks[trackPos] >= c  )
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
                    if (trackPos<tracks.Length && tracks[trackPos] == c )
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
                if (trackPos < tracks.Length && !(tracks[trackPos] >= c)){
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
    
    
    
    
    
    public bool validArray(int[] tracks, int position , int[] arr)
    {
        arr[position] = 1;
        int empty = emptySpaces(tracks,arr);
        int occupated = arr.Length - empty;
        int max = maxO(tracks);
        if (occupated > max)
        {
            return false;
        }

        if (groupsCounter(arr) > tracks.Length)
        {
            return false;
        }

        for (int i = 0; i < tracks.Length; i++)
        {
            if (!findGroup(tracks[i], arr))
            {
                return false;
            }
        }
        return true;
    }

    public int groupsCounter(int[] arr)
    {
        bool found = false;
        int res = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if(arr[i] == 0&& found)
            {
                res++;
                found = false;
            }
            if(arr[i] == 1)
            {
                found = true;
            }
            
        }

        return res;
    }
    
    public bool findGroup(int c, int[] arr)
    {
        int counter =0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == 0)
            {
                if (counter==c)
                {
                    return true;
                }
                counter = 0;
            }
            else
            {
                counter++;
            }
        }
        if (counter==c)
        {
            return true;
        }
        return false;

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
        int res = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[1]==0)
            {
                res++;
            } 
        }
        return res;

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
