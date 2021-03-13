using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] itemToSpwan;
    [SerializeField] GameObject unWalkableGrid;
    [SerializeField] int unWalkingGridNumber;
    [SerializeField] GameObject walkableGrid;
    [SerializeField] GameObject PathSolutionGrid;
    [SerializeField] int gridX =5;
    [SerializeField] int gridZ=6;
    [SerializeField] Vector3 gridOrigin;
    [SerializeField] float gridOffset;

    [HideInInspector] public int[,] GridValue; 

    // Start is called before the first frame update
    void Start()
    {
        GridValue = new int[gridX, gridZ];
        SpawnGrid();
    }

    void SpawnGrid()
    {
        Debug.Log(" value x "+GridValue.Length.ToString() );
        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridZ; j++)
            {
                Vector3 spawnObjPosition = new Vector3(i * gridOffset, .5f, j * gridOffset) + gridOrigin;
                Debug.Log(" pos " + spawnObjPosition);
                spawnObj(spawnObjPosition , unWalkableGrid,Quaternion.identity , i,j);

            }
        }
    }

    private void spawnObj(Vector3 objPos,GameObject spawnPrefab,Quaternion objRotation, int X_Index, int Z_Index)
    {
        int x = UnityEngine.Random.Range(1, 3);
        GameObject cloneItem;
        if (x == 1)
        {
            cloneItem = GameObject.Instantiate(unWalkableGrid, objPos, objRotation, this.transform);
        }
        else if (x == 2)
            cloneItem = GameObject.Instantiate(walkableGrid, objPos, objRotation, this.transform);
        else
        { cloneItem = GameObject.Instantiate(spawnPrefab, objPos, objRotation, this.transform); ; }
        Debug.Log(x);
        cloneItem.name = string.Format("[{0},{1}]", X_Index, Z_Index);
    }
}

public enum GridWalkabliliyState
{
    walkable,
    unwalkable
}

class GridType :MonoBehaviour
{
    public GameObject CellType;
    public GridWalkabliliyState gridWalkabliliyState;

}
