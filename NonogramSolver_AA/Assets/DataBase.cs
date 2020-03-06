using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    private int rows;
    private int columns;
    private int[][] rowsArray;
    private int[][] columnsArray;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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


    
    
    
}
