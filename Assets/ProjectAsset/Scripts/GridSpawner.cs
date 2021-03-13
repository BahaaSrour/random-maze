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

    [HideInInspector] public int[,] GridValue;
    void Start()
    {
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
    //For testing porpose
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.S))
    //        for (int i = 0; i < gridX; i++)
    //        {
    //            for (int j = 0; j < gridZ; j++)
    //            {
    //                Debug.Log(string.Format("item [{0},{1}]", i, j, itemToSpwan[i, j]));
    //            }
    //        }
    //    if (Input.GetKeyDown(KeyCode.D))
    //        Debug.Log(" value x " + itemToSpwan.Length.ToString());
    //    if (Input.GetKeyDown(KeyCode.A))
    //        Debug.Log(" value x " + itemToSpwan[Test_x, Test_z].gridWalkabliliyState.ToString()+ " ( "+ itemToSpwan[Test_x, Test_z] .Xindex+ " , "+ itemToSpwan[Test_x, Test_z] .Zindex+ " )");
    //}
    void SpawnGrid()
    {
        for (int i = 0; i < gridX; i++)
            for (int j = 0; j < gridZ; j++)
            {
                Vector3 spawnObjPosition = new Vector3(i * gridOffset, .5f, j * gridOffset) + gridOrigin;
                spawnObj(spawnObjPosition, unWalkableGrid, Quaternion.identity, i, j);
            }
    }
    private void spawnObj(Vector3 objPos, GameObject spawnPrefab, Quaternion objRotation, int X_Index, int Z_Index)
    {
        int x = UnityEngine.Random.Range(1, 3);
        GameObject cloneItem;
        if (unWalkingGridNumber > 0 && (x == 2))
        {
            unWalkingGridNumber--;
            cloneItem = GameObject.Instantiate(walkableGrid, objPos, objRotation, this.transform);
            cloneItem.name = string.Format("[{0},{1}]", X_Index, Z_Index);
            itemToSpwan[X_Index, Z_Index].AddValues(cloneItem, GridWalkabliliyState.unwalkable, X_Index,Z_Index);
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