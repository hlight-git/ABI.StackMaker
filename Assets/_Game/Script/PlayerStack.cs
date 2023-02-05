using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStack : MonoBehaviour
{
    public const float BRICK_HEIGHT = 0.3f;

    [SerializeField] private GameObject collectedBrickPrefab;

    private Stack<GameObject> bricks;
    public int Count => bricks.Count;
    void Start()
    {
        OnInit();
    }

    void OnInit()
    {
        bricks = new Stack<GameObject>();
        GameObject collectedBrick = Instantiate(collectedBrickPrefab, transform.position, Quaternion.identity);
        collectedBrick.transform.parent = this.transform;
        bricks.Push(collectedBrick);
    }

    public void AddBrick(GameObject positiveBrick)
    {
        Destroy(positiveBrick);
        Vector3 topCollectedBrickPos = bricks.Peek().transform.position;
        Vector3 newCollectedBrickPos = new Vector3(topCollectedBrickPos.x, topCollectedBrickPos.y + BRICK_HEIGHT, topCollectedBrickPos.z);
        GameObject collectedBrick = Instantiate(collectedBrickPrefab, newCollectedBrickPos, Quaternion.identity);
        collectedBrick.transform.parent = this.transform;
        bricks.Push(collectedBrick);
    }

    public void RemoveBrick()
    {
        Destroy(bricks.Pop());
    }
}
