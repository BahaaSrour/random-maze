using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    [SerializeField] public GridType[,] itemToSpwan;
    [SerializeField] GameObject unWalkableGrid;
    [SerializeField] int unWalkingGridNumber;
    [SerializeField] GameObject walkableGrid;
    [SerializeField] GameObject PathSolutionGrid;
    [SerializeField] public int gridX = 5;
    [SerializeField] public int gridZ = 6;
    [SerializeField] Vector3 gridOrigin;
    [SerializeField] float gridOffset;
    [SerializeField] int Test_x;
    [SerializeField] int Test_z;
     int unWalkableCellsinRaw=4;
    [SerializeField] int diffaultValue;


    [HideInInspector] public int[,] GridValue;
    void Start()
    {
        diffaultValue = unWalkableCellsinRaw;
        GridValue = new int[gridX, gridZ];
        itemToSpwan = new GridType[gridX, gridZ];
        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridZ; j++)
            {
                itemToSpwan[i, j] = new GridType();
            }
        }
        SpawnGrid();
    }
    bool playerOrGoal=false;
    void SpawnGrid()
    {
        for (int i = 0; i < gridX; i++)
        {
            unWalkableCellsinRaw = diffaultValue;
            for (int j = 0; j < gridZ; j++)
            {
                Vector3 spawnObjPosition = new Vector3(i * gridOffset, .5f, j * gridOffset) + gridOrigin;
                if ((i == BFS.player.x && j == BFS.player.y) || (i == BFS.goal.x && j == BFS.goal.y)) playerOrGoal = true;
                spawnObj(spawnObjPosition, unWalkableGrid, Quaternion.identity, i, j,ref unWalkableCellsinRaw,(j%2==0 ||i%2==1 ));
                playerOrGoal = false;
            }
        }
    }
    private void spawnObj(Vector3 objPos, GameObject spawnPrefab, Quaternion objRotation, int X_Index, int Z_Index,ref int unwalks,bool mod)
    {
        int x = UnityEngine.Random.Range(1, 3);
        GameObject cloneItem;
        if (unWalkingGridNumber > 0 && (x == 2) && unWalkableCellsinRaw>0 && unwalks>0&& mod&&!playerOrGoal )
        {
            unWalkingGridNumber--;
            cloneItem = GameObject.Instantiate(walkableGrid, objPos, objRotation, this.transform);
            cloneItem.name = string.Format("[{0},{1}]", X_Index, Z_Index);
            itemToSpwan[X_Index, Z_Index].AddValues(cloneItem, GridWalkabliliyState.unwalkable, X_Index,Z_Index);
            unwalks--;
        }
        else
        {
            cloneItem = GameObject.Instantiate(unWalkableGrid, objPos, objRotation, this.transform);
            cloneItem.name = string.Format("[{0},{1}]", X_Index, Z_Index);
            itemToSpwan[X_Index, Z_Index].AddValues(cloneItem, GridWalkabliliyState.walkable, X_Index, Z_Index);
        }
    }
}
public enum GridWalkabliliyState
{
    walkable = 1,
    unwalkable = 2
}
public class GridType
{
    public int weight;
    public bool Visited;
    public GameObject CellType;
    public GridWalkabliliyState gridWalkabliliyState;
    public GridType parentInGrid =null;
    public  int Xindex, Zindex;
    public void AddValues(GameObject obj, GridWalkabliliyState state,int x,int z)
    {
        Xindex = x;
        Zindex = z;
        Visited = false;
        CellType = obj;
        gridWalkabliliyState = state;
    }

}