using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    public Lector lector;
    public Solver solver;
    public DataBase db;
    private GridDrawer grid;

    void Start()
    {
        lector.readFile("");
        db.printNonogram();
        //
        var watch = System.Diagnostics.Stopwatch.StartNew();
        solver.solve();
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        print(elapsedMs);
        
        db.printNonogram();
        db.printNonogramBonito();

        db.grid.drawAllBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
