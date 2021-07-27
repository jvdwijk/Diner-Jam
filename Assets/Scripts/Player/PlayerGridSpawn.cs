using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGridSpawn : MonoBehaviour
{

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private GameobjectMap map;

    [SerializeField]
    private Vector2Int spawnCell;

    [SerializeField]
    private GameObject playerPrefab;

    private void Start()
    {
        GameObject player = Instantiate(playerPrefab);
        player.transform.SetParent(transform);

        GridPosition gridPosition = player.GetComponent<GridPosition>();
        gridPosition.Grid = grid;
        gridPosition.SetPosition(spawnCell, true);

        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        movement.SetMap(map);

        CarryObject carryObject = player.GetComponent<CarryObject>();
        carryObject.GameobjectMap = map;
        carryObject.Interaction = map.transform;
}

    private void OnDrawGizmos()
    {
        if (!grid)
            return;

        Vector3 spawnCellPosition = grid.GetCellCenterWorld((Vector3Int)spawnCell);
        Vector3 cellSize = grid.cellSize;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawnCellPosition, Mathf.Min(cellSize.x, cellSize.y) / 4);
    }

}
