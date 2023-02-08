using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStack : MonoBehaviour
{
    private Stack<GameObject> bricks;
    public int CollectedBrickCount => bricks.Count - 1;
    void Start()
    {
        OnInit();
    }

    void OnInit()
    {
        bricks = new Stack<GameObject>();
        bricks.Push(transform.GetChild(0).gameObject);
    }

    public void AddBrick(GameObject brick)
    {
        Vector3 topCollectedBrickPos = bricks.Peek().transform.position;
        Vector3 newCollectedBrickPos = new Vector3(topCollectedBrickPos.x, topCollectedBrickPos.y + GameConstant.Brick.THICKNESS, topCollectedBrickPos.z);
        brick.transform.position = newCollectedBrickPos;
        brick.transform.parent = this.transform;
        bricks.Push(brick);
    }

    public void RemoveBrick()
    {
        Destroy(bricks.Pop());
    }
}
