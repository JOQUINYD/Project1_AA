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
        //db.grid.drawHint("1"+"\n"+"2"+"\n"+"3"+"\n"+"4");
        lector.readFile("");
        db.printNonogram();
        // startSolving grafica en "tiempo real"
        //solver.startSolving();

        db.printNonogram();
        db.printNonogramBonito();

        //db.grid.drawAllBoard();
    }

    // Update is called once per frame
    void Update()
    {
       // db.grid.drawAllBoard();
    }

    public void startSolve()
    {
        UnityMainThread.wkr.clearJobs();
        db.grid.cleanGraphBoard();
        lector.readFile("");
        solver.startSolving();
    }
}
