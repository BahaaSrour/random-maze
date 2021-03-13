using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS : MonoBehaviour
{
    [SerializeField] GridSpawner gs;
    [SerializeField] List<GridType> visitedGridsList;
    [SerializeField] int playerPosX;
    [SerializeField] int playerPosZ;
    public int gridLength;
    [SerializeField] int X_goal;
    [SerializeField] int Z_goal;
    [SerializeField] GameObject Pathprefab;
    void Start()
    { 
        Visited = new Queue<GridType>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            for (int i = 0; i < gs.gridX; i++)
            {
                for (int j = 0; j < gs.gridZ; j++)
                {
                    Debug.Log(string.Format("item [{0},{1}]", i, j, gs.itemToSpwan[i, j]));
                }
            }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(gs.itemToSpwan[0, 0]);
            bfs();
      
        }
    }

    bool tergetFound;
    public void bfs( )
    {
        Visited.Enqueue(gs.itemToSpwan[playerPosX, playerPosZ]);
        if (gs.itemToSpwan[playerPosX, playerPosZ].gridWalkabliliyState == GridWalkabliliyState.unwalkable)
        {
            Debug.Log("Cant Move");
            return;
        }
        while (Visited.Count>0)
        {
            if (tergetFound) break;
            Debug.Log("Visited "+Visited.Count);
            tmp = Visited.Dequeue();

            Solve(X_goal, Z_goal);
        }
        Debug.Log("no soulutoin");
        if (tergetFound) DrawPath(tmp);
    }

    void DrawPath(GridType Target)
    {
        while (tmp.parentInGrid != null)
        {
            GameObject.Instantiate(Pathprefab, tmp.CellType.transform.position, Quaternion.identity);
            tmp = tmp.parentInGrid;
        }
            GameObject.Instantiate(Pathprefab, gs.itemToSpwan[playerPosX, playerPosZ].CellType.transform.position, Quaternion.identity);

    }
    bool goalHasFound;
    public Queue<GridType> Visited;
    GridType tmp;

    //search if i can reach the nood or not
    void Solve(/*int X_index, int Z_index,*/ int Xgoal, int Zgoal)
    {
        if (!(tmp.Xindex == Xgoal && tmp.Zindex == Zgoal))
        {
            tmp.Visited = true;
            //Check up
            if (tmp.Xindex > 0 && !gs.itemToSpwan[tmp.Xindex - 1, tmp.Zindex].Visited && gs.itemToSpwan[tmp.Xindex - 1, tmp.Zindex].gridWalkabliliyState == GridWalkabliliyState.walkable)
            {
                if (tmp.Xindex - 1 == Xgoal && tmp.Zindex == Zgoal) goalHasFound = true;
                Visited.Enqueue(gs.itemToSpwan[tmp.Xindex - 1, tmp.Zindex]);
                gs.itemToSpwan[tmp.Xindex - 1, tmp.Zindex].parentInGrid = tmp;
                gs.itemToSpwan[tmp.Xindex - 1, tmp.Zindex].weight = tmp.weight + 1;
                gs.itemToSpwan[tmp.Xindex - 1, tmp.Zindex].Visited = true;
            }

            //check Down 
            if (tmp.Xindex < gs.gridX - 1 && !gs.itemToSpwan[tmp.Xindex + 1, tmp.Zindex].Visited && gs.itemToSpwan[tmp.Xindex + 1, tmp.Zindex].gridWalkabliliyState == GridWalkabliliyState.walkable)
            {
                if (tmp.Xindex + 1 == Xgoal && tmp.Zindex == Zgoal) goalHasFound = true;
                Visited.Enqueue(gs.itemToSpwan[tmp.Xindex + 1, tmp.Zindex]);
                gs.itemToSpwan[tmp.Xindex + 1, tmp.Zindex].parentInGrid = tmp;
                gs.itemToSpwan[tmp.Xindex + 1, tmp.Zindex].weight = tmp.weight + 1;
                gs.itemToSpwan[tmp.Xindex + 1, tmp.Zindex].Visited = true;
            }

            //check Right
            if (tmp.Zindex < gs.gridZ - 1 && !gs.itemToSpwan[tmp.Xindex, tmp.Zindex + 1].Visited && gs.itemToSpwan[tmp.Xindex, tmp.Zindex + 1].gridWalkabliliyState == GridWalkabliliyState.walkable)
            {
                if (tmp.Xindex == Xgoal && tmp.Zindex + 1 == Zgoal) goalHasFound = true;
                Visited.Enqueue(gs.itemToSpwan[tmp.Xindex, tmp.Zindex + 1]);
                gs.itemToSpwan[tmp.Xindex, tmp.Zindex + 1].parentInGrid = tmp;
                gs.itemToSpwan[tmp.Xindex, tmp.Zindex + 1].weight = tmp.weight + 1;
                gs.itemToSpwan[tmp.Xindex, tmp.Zindex + 1].Visited = true;
            }

            //Check Left
            if (tmp.Zindex > 0 && !gs.itemToSpwan[tmp.Xindex, tmp.Zindex - 1].Visited && gs.itemToSpwan[tmp.Xindex, tmp.Zindex - 1].gridWalkabliliyState == GridWalkabliliyState.walkable)
            {
                if (tmp.Xindex == Xgoal && tmp.Zindex - 1 == Zgoal) goalHasFound = true;
                Visited.Enqueue(gs.itemToSpwan[tmp.Xindex, tmp.Zindex - 1]);
                gs.itemToSpwan[tmp.Xindex, tmp.Zindex - 1].parentInGrid = tmp;
                gs.itemToSpwan[tmp.Xindex, tmp.Zindex - 1].weight = tmp.weight + 1;
                gs.itemToSpwan[tmp.Xindex, tmp.Zindex - 1].Visited = true;
            }

        }
        else { Debug.Log("goalHasFound"); tergetFound = true; }
        return;
    }

}
