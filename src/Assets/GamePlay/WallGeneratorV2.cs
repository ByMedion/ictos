using UnityEngine;
using UnityEngine.UI;

public class WallGeneratorV2 : MonoBehaviour
{
    // Use this for initialization
    public GameObject[] _walls;

    private Color colorr;

    private Vector3[] endP;

    public int Generated;

    public GameObject Player;

    //private Vector3[] prevEndP;
    public ParticleSystem Psys;

    private Color pSysColor;

    public float Speed;
    public float SpeedInc;

    private float[] x, z;

    private void Start()
    {
        _walls = GameObject.FindGameObjectsWithTag("Wall");
        endP = new Vector3[_walls.Length];
        x = new float[_walls.Length];
        z = new float[_walls.Length];
        //prevEndP = new Vector3[_walls.Length];
        Generate();
    }

    private static int GetRndPos()
    {
        return Random.Range(-1, 2) * 3;
    }

    private float RndFrom(float? axis = null)
    {
        var outp = GetRndPos();
        if (axis == null)
            return outp;
        while (axis == outp)
            outp = GetRndPos();

        return outp;
    }

    private static Color GetRndColor()
    {
        switch (Random.Range(0, 6))
        {
            case 0: return new Color(255f / 255, 204f / 255, 153f / 255); // "ffcc99";
            case 1: return new Color(204f / 255, 255f / 255, 153f / 255); //"ccff99";
            case 2: return new Color(204f / 255, 153f / 255, 255f / 255); // "cc99ff";
            case 3: return new Color(255f / 255, 153f / 255, 204f / 255); //"ff99cc";
            case 4: return new Color(153f / 255, 204f / 255, 255f / 255); // "99ccff";
            case 5: return new Color(153f / 255, 255f / 255, 204f / 255); // "99ffcc";
        }
        return new Color();
    }

    private void Generate()
    {
        pSysColor = GetRndColor();
        Speed += SpeedInc;
        Player.GetComponent<PlayerMove>().Speed += 1;

        
        Direction dir;
        do
            dir = (Direction) Random.Range(-2, 3); while (dir == 0);

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

        for (var i = 0; i < _walls.Length; i++)
        {
            _walls[i].transform.position = new Vector3(x[i], 2, z[i]);
            _walls[i].transform.rotation = Quaternion.identity;
        }
    }

    private void Update()
    {
        if (_walls[0].transform.position == endP[0] && Global.IsAlive)
        {
            Generate();
            GameObject.Find("ScoreText").GetComponent<Text>().text = "Score: " + ++Generated;
            if (Generated > Global.Record)
                Global.Record = Generated;
        }

        for (var i = 0; i < _walls.Length; i++)
            _walls[i].transform.position =
                Vector3.MoveTowards(_walls[i].transform.position, endP[i], Speed * Time.deltaTime);

        Psys.startColor = Color.Lerp(Psys.startColor, pSysColor, 3 * Time.deltaTime);
    }
}

internal enum Direction
{
    LeftToRight = -1,
    RightToLeft = 1,
    BottomToTop = -2,
    TopToBottom = 2
}