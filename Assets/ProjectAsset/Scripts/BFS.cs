using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BFS : MonoBehaviour
{
    [SerializeField] GridSpawner gs;
    [SerializeField] List<GridType> visitedGridsList;
    [SerializeField] int playerPosX;
    [SerializeField] int playerPosZ;
    [SerializeField] int X_goal;
    [SerializeField] int Z_goal;
    [SerializeField] GameObject Pathprefab;
    [SerializeField] GameObject Goalprefab;
    [SerializeField] Text text;

    bool tergetFound;
    public Queue<GridType> Visited;
    GridType tmp;
    public static Vector2Int player;
    public static Vector2Int goal;
    public static int unWalkingGridNumber;

    public bool LoadSettingsFromFile= true;
    private void Awake()
    {
        if (LoadSettingsFromFile)
        {
            XMLSaverAndLoader.LoadSettings(this);
        }
        else
        {
            player.x = playerPosX;
            player.y = playerPosZ;
            goal.x = X_goal;
            goal.y = Z_goal;
            unWalkingGridNumber = gs.unWalkingGridNumber;
        }
    }
    void Start()
    {
        
        Visited = new Queue<GridType>();
        gs.itemToSpwan[X_goal, Z_goal].CellType.GetComponent<MeshRenderer>().material.color = Color.yellow;
        gs.itemToSpwan[playerPosX, playerPosZ].CellType.GetComponent<MeshRenderer>().material.color = Color.green;
        gs.itemToSpwan[playerPosX, playerPosZ].gridWalkabliliyState = GridWalkabliliyState.walkable;
        // gs.itemToSpwan[X_goal, Z_goal].gridWalkabliliyState = GridWalkabliliyState.walkable;
    }


    public void bfs( )
    {
        Visited.Enqueue(gs.itemToSpwan[playerPosX, playerPosZ]);
        if (gs.itemToSpwan[playerPosX, playerPosZ].gridWalkabliliyState == GridWalkabliliyState.unwalkable)
        {
            text.text = "Player can't move, There is no soulutoin";
            Debug.Log("Cant Move");
            return;
        }
        while (Visited.Count>0)
        {
            if (tergetFound) break;
            tmp = Visited.Dequeue();

            Solve(X_goal, Z_goal);
        }
         text.text = "There is no soulutoin";
        Debug.Log("no soulutoin");
        if (tergetFound)
        {
            text.text = string.Format("Number of Steps = {0}", tmp.weight);
              DrawPath(tmp);
        }
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

    void Solve(/*int X_index, int Z_index,*/ int Xgoal, int Zgoal)
    {
        if (!(tmp.Xindex == Xgoal && tmp.Zindex == Zgoal))
        {
            tmp.Visited = true;
            //Check up
            if (tmp.Xindex > 0 && !gs.itemToSpwan[tmp.Xindex - 1, tmp.Zindex].Visited && gs.itemToSpwan[tmp.Xindex - 1, tmp.Zindex].gridWalkabliliyState == GridWalkabliliyState.walkable)
            {
                //if (tmp.Xindex - 1 == Xgoal && tmp.Zindex == Zgoal) goalHasFound = true;
                Visited.Enqueue(gs.itemToSpwan[tmp.Xindex - 1, tmp.Zindex]);
                gs.itemToSpwan[tmp.Xindex - 1, tmp.Zindex].parentInGrid = tmp;
                gs.itemToSpwan[tmp.Xindex - 1, tmp.Zindex].weight = tmp.weight + 1;
                gs.itemToSpwan[tmp.Xindex - 1, tmp.Zindex].Visited = true;
            }
            //check Down 
            if (tmp.Xindex < gs.gridX - 1 && !gs.itemToSpwan[tmp.Xindex + 1, tmp.Zindex].Visited && gs.itemToSpwan[tmp.Xindex + 1, tmp.Zindex].gridWalkabliliyState == GridWalkabliliyState.walkable)
            {
                Visited.Enqueue(gs.itemToSpwan[tmp.Xindex + 1, tmp.Zindex]);
                gs.itemToSpwan[tmp.Xindex + 1, tmp.Zindex].parentInGrid = tmp;
                gs.itemToSpwan[tmp.Xindex + 1, tmp.Zindex].weight = tmp.weight + 1;
                gs.itemToSpwan[tmp.Xindex + 1, tmp.Zindex].Visited = true;
            }
            //check Right
            if (tmp.Zindex < gs.gridZ - 1 && !gs.itemToSpwan[tmp.Xindex, tmp.Zindex + 1].Visited && gs.itemToSpwan[tmp.Xindex, tmp.Zindex + 1].gridWalkabliliyState == GridWalkabliliyState.walkable)
            {
                Visited.Enqueue(gs.itemToSpwan[tmp.Xindex, tmp.Zindex + 1]);
                gs.itemToSpwan[tmp.Xindex, tmp.Zindex + 1].parentInGrid = tmp;
                gs.itemToSpwan[tmp.Xindex, tmp.Zindex + 1].weight = tmp.weight + 1;
                gs.itemToSpwan[tmp.Xindex, tmp.Zindex + 1].Visited = true;
            }
            //Check Left
            if (tmp.Zindex > 0 && !gs.itemToSpwan[tmp.Xindex, tmp.Zindex - 1].Visited && gs.itemToSpwan[tmp.Xindex, tmp.Zindex - 1].gridWalkabliliyState == GridWalkabliliyState.walkable)
            {
                Visited.Enqueue(gs.itemToSpwan[tmp.Xindex, tmp.Zindex - 1]);
                gs.itemToSpwan[tmp.Xindex, tmp.Zindex - 1].parentInGrid = tmp;
                gs.itemToSpwan[tmp.Xindex, tmp.Zindex - 1].weight = tmp.weight + 1;
                gs.itemToSpwan[tmp.Xindex, tmp.Zindex - 1].Visited = true;
            }

        }
        else { Debug.Log("goalHasFound"); tergetFound = true; }
        return;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

   public void SetSettings(Settings settings)
   {
        if (settings != null)
        {
            player = settings.player;
            playerPosX = player.x;
            playerPosZ = player.y;
            goal = settings.goal;
            X_goal = goal.x;
            Z_goal = goal.y;
            unWalkingGridNumber = settings.maxNumberOfBlock;
            gs.unWalkingGridNumber = settings.maxNumberOfBlock;
        }
        else 
        {
            player.x = playerPosX;
            player.y = playerPosZ;
            goal.x = X_goal;
            goal.y = Z_goal;
            unWalkingGridNumber = gs.unWalkingGridNumber;
        }
   }
   public static Settings GetSettings ()
   {
        return new Settings { player = player,goal = goal, maxNumberOfBlock= unWalkingGridNumber};
   }
}

