using System;
using Script.Game;
using UnityEngine;
using Random = UnityEngine.Random;

public class WallController : MonoBehaviour
{
    public float acceleration;

    private Vector3[] endP;

    private int generated;
    private PlayerController playerController;

    private Color pSysColor;
    public float speed;
    private GameObject[] walls;

    private float[] x, z;

    public event Action wallsGenerated;

    private void Awake()
    {
        playerController = GameManager.instance.playerController;

        walls = GameObject.FindGameObjectsWithTag("Wall");
        endP = new Vector3[walls.Length];
        x = new float[walls.Length];
        z = new float[walls.Length];

        Generate();
    }

    private static int GetRndPos()
    {
        return Random.Range(-1, 2) * 3;
    }

    private static float RndFrom(float? axis = null)
    {
        var result = GetRndPos();

        if (axis == null)
            return result;

        while (axis == result)
            result = GetRndPos();

        return result;
    }

    private void Generate()
    {
        speed += acceleration;
        playerController.speed += 1;

        Direction dir;
        do
        {
            dir = (Direction) Random.Range(-2, 3);
        } while (dir == 0);

        if (dir == Direction.LeftToRight || dir == Direction.RightToLeft)
        {
            for (var i = 0; i < x.Length; i++)
                x[i] = 50 * (int) dir;

            z[0] = RndFrom();
            z[1] = RndFrom(z[0]);
            for (var i = 0; i < endP.Length; i++)
                endP[i] = new Vector3(-x[i], 2, z[i]);
        }
        else
        {
            for (var i = 0; i < z.Length; i++)
                z[i] = 50 * ((int) dir / 2);

            x[0] = RndFrom();
            x[1] = RndFrom(x[0]);

            for (var i = 0; i < endP.Length; i++)
                endP[i] = new Vector3(x[i], 2, -z[i]);
        }

        for (var i = 0; i < walls.Length; i++)
        {
            walls[i].transform.position = new Vector3(x[i], 2, z[i]);
            walls[i].transform.rotation = Quaternion.identity;
        }

        wallsGenerated?.Invoke();
    }

    private void Update()
    {
        if (walls[0].transform.position == endP[0] && Global.IsAlive) Generate();

        for (var i = 0; i < walls.Length; i++)
            walls[i].transform.position =
                Vector3.MoveTowards(walls[i].transform.position, endP[i], speed * Time.deltaTime);
    }
}