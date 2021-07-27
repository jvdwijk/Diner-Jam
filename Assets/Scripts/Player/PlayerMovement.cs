using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridPosition))]
public class PlayerMovement : MonoBehaviour
{
    private GridPosition gridPosition;
    private GameobjectMap map;
    private Animator animator;

    public Vector2Int LookingAt { get; private set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gridPosition = GetComponent<GridPosition>();
    }

    public void SetMap(GameobjectMap map)
    {
        this.map = map;
    }

    public void Move(MoveDirection direction)
    {
        LookingAt = GetDesiredChange(direction);
        Vector2Int desiredPosition = gridPosition.Position + LookingAt;
        
        transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, LookingAt));

        if (map.GetCell(desiredPosition) == null)
        {
            animator.SetTrigger("Move");
            gridPosition.SetPosition(desiredPosition);
        }
    }

    private Vector2Int GetDesiredChange(MoveDirection direction)
    {
        return direction switch
        {
            MoveDirection.UP => Vector2Int.up,
            MoveDirection.DOWN => Vector2Int.down,
            MoveDirection.LEFT => Vector2Int.left,
            MoveDirection.RIGHT => Vector2Int.right,
            _ => Vector2Int.zero,
        };
    }

}

public enum MoveDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT,

    NONE
}
