using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    public Lector lector;
    public Solver solver;
    public DataBase db;
    
    void Start()
    {
        int[] r = {3, 4, 5};
        //print("EL TAMAÑO ES ");
        //print(r.Length);
        lector.readFile("");
        /* for (int i = 0; i < db.ColumnsArray.Length; i++)
        {
            print("Columna");
            string r = "";
            for (int j = 0; j < db.ColumnsArray[i].Length; j++)
            {
                r += db.ColumnsArray[i][j];
            }
            print(r);
        }*/
        
        db.printNonogram();
        solver.solve();
        db.printNonogram();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
