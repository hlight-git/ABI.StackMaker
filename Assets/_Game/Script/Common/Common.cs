using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameTag
{
    public const string PLAYER = "Player";
    public const string BRICK = "Brick";
    public const string UNBRICK = "UnBrick";
    public const string BRICK_TAKER = "BrickTaker";
    public const string WINZONE = "WinZone";
    public const string FINISH_LINE = "FinishLine";
}
public static class GameAnim
{
    public static class Duration
    {
        public static class Player
        {
            public const float IDLE = 1.1f;
            public const float JUMP = 0.17f;
            public const float CHEER = 6.14f;
        }
        public static class Pusher
        {
            public const float BOUNCE = 0.5f;
        }
        public static class SceneTransition
        {
            public const float START_SCENE = 1.5f;
            public const float END_SCENE = 1.5f;
        }
    }
}

public static class GameConstant
{
    public const float TILESIZE = 1f;
    public static class Player
    {
        public const float DEFAULT_MOVE_SPEED = 10f;
        public const float RAYCAST_MAX_RANGE = 100f;  // Mathf.Infinity;
    }
    public static class Pusher
    {
        public static readonly Vector3 ELASTIC_PLANE_NORMAL = new Vector3(1, 0, -1);
        public const float PUSH_FORCE = 12f;
    }
    public static class Bridge
    {
        //public const string LAYER_NAME = "Brick Taker";
        public static readonly Color[] WINZONE_COLORS =
        {
            Color.red,
            Color.yellow,
            Color.green,
            Color.cyan,
            Color.blue,
            Color.magenta,
            Color.gray,
            Color.white
        };
    }
    public static class Brick
    {
        public const float THICKNESS = 0.3f;
        public static class UnCollectable
        {
            public const string LAYER_NAME = "UnBrick";
        }
    }
    public static class Level
    {
        public static class Reward
        {
            public const int GOLD = 50;
        }
    }
}
public static class GameData
{
    public const int DEFAULT_ASSET = 0;
    public const int DEFAULT_LEVEL = 1;
    public static class Keys
    {
        public const string GOLD = "gold";
        public const string LEVEL = "level";
    }
}

public static class VectorUtils
{
    public static readonly Vector3[] CardinalDirectionVectors =
    {
        Vector3.zero,
        Vector3.forward,
        Vector3.right,
        Vector3.back,
        Vector3.left,
        Vector3.zero
    };
    public static Vector3 CardinalDirectionVectorOf(CardinalDirection cardinalDirection)
    {
        return CardinalDirectionVectors[(int)cardinalDirection];
    }
    public static CardinalDirection CardinalDirectionOf(Vector3 vector)
    {
        foreach (CardinalDirection direction in (CardinalDirection[])Enum.GetValues(typeof(CardinalDirection)))
        {
            if (Approximately(vector, CardinalDirectionVectorOf(direction)))
            {
                return direction;
            }
        }

        return CardinalDirection.Unknown;
    }

    public static bool Approximately(Vector3 vectorA, Vector3 vectorB, float allowedDifference = 0.01f)
    {
        Vector3 subVector = vectorA - vectorB;
        return subVector.magnitude < allowedDifference;
    }
}