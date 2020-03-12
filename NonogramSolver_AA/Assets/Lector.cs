using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Lector : MonoBehaviour
{
    private string file = "";
    static string path;
    public DataBase dataBase;
    
    // Start is called before the first frame update
    void Start()
    {
        /*
        readFile(""); */

    }


    public void OpenExplorer()
    {
        path = EditorUtility.OpenFilePanel("Overwrite with txt", "", "txt");
    }
    
    public void readFile(string name)
    {
        if (path == null)
            path = "Nonogram.txt";
        using (StreamReader fr = File.OpenText(path))
        {
            file = fr.ReadLine();
            setCounter(file);

            file = fr.ReadLine();
            //print("####################Array de filas ####################");
            for (int i = 0; i < dataBase.Rows; i++)
            {
                file =  fr.ReadLine();
               // print(file);
                dataBase.RowsArray[i] = obtainArray(file);
                
                for (int j = 0; j < dataBase.RowsArray[i].Length; j++)
                {
                   // print(dataBase.RowsArray[i][j]);
                }
            }
            
            file = fr.ReadLine();
            //print("####################Array de Columnas ####################");
            for (int i = 0; i < dataBase.Columns; i++)
            {
                file =  fr.ReadLine();
                //print(file);
                dataBase.ColumnsArray[i] = obtainArray(file);
                
                for (int j = 0; j < dataBase.ColumnsArray[i].Length; j++)
                {
                    //print(dataBase.ColumnsArray[i][j]);
                }
            }
            dataBase.grid._init_gridDrawer(dataBase.GameBoard);
        }

    }

    public void setCounter(string line)
    {
        string value = "";
        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] != ',')
            {
                value += line[i];
            }
            else
            {
                dataBase.Rows = Convert.ToInt32(value);
                dataBase.RowsArray = new int[Convert.ToInt32(value)][];
                i++;
                value = "";
            }
        }
        dataBase.Columns = Convert.ToInt32(value);
        dataBase.ColumnsArray = new int[Convert.ToInt32(value)][];
        dataBase.GameBoard = new int [dataBase.Rows][];
        for (int i = 0; i < dataBase.GameBoard.Length; i++)
        {
            dataBase.GameBoard[i] = new int[dataBase.Columns];
        }
        //dataBase.printBoard();
        
    }

    public int[] obtainArray(string line)
    {
        int[] res = new int[cantValores(line)];
        string value = "";
        int pos = 0; 
        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] != ',')
            {
                value += line[i];
            }
            else
            {
                res[pos]= Convert.ToInt32(value);
                i++;
                value = "";
                pos++;
            }
        }
        res[pos]= Convert.ToInt32(value);
        

        return res;
    }

    public int cantValores(string line)
    {
        string value = "";
        int res = 1;
        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == ',')
            {
                res++;
            }

        }

        return res;
    }







    // Update is called once per frame
    void Update()
    {
        
    }
}
