using UnityEngine;
using UnityEngine.UI;

public class WallController : MonoBehaviour
{
	private static readonly Color[] Colors;

	private Vector3[] _endP;

	private int                       _generated;
	private ParticleSystem.MainModule _particleSystemMain;

	private PlayerController _playerController;

	private Color        _pSysColor;
	private Text         _scoreText;
	private GameObject[] _walls;
	public  float        acceleration;

	public ParticleSystem ParticleSystem;

	public GameObject player;

	public float speed;

	private float[] x, z;

	static WallController()
	{
		Colors = new[]
		{
			new Color(255f / 255, 204f / 255, 153f / 255),
			new Color(204f / 255, 255f / 255, 153f / 255),
			new Color(204f / 255, 153f / 255, 255f / 255),
			new Color(255f / 255, 153f / 255, 204f / 255),
			new Color(153f / 255, 204f / 255, 255f / 255),
			new Color(153f / 255, 255f / 255, 204f / 255)
		};
	}

	private void Awake()
	{
		_particleSystemMain = ParticleSystem.main;
		_playerController   = player.GetComponent<PlayerController>();
		_scoreText          = GameObject.Find("ScoreText").GetComponent<Text>();

		_walls = GameObject.FindGameObjectsWithTag("Wall");
		_endP  = new Vector3[_walls.Length];
		x      = new float[_walls.Length];
		z      = new float[_walls.Length];

		Generate();
	}

	private static int GetRndPos()
	{
		return Random.Range(-1, 2) * 3;
	}

	private static float RndFrom(float? axis = null)
	{
		var outp = GetRndPos();

		if (axis == null)
			return outp;

		while (axis == outp)
			outp = GetRndPos();

		return outp;
	}

	private void Generate()
	{
		_pSysColor              =  Colors[Random.Range(0, Colors.Length)];
		speed                   += acceleration;
		_playerController.speed += 1;

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
			for (var i = 0; i < _endP.Length; i++)
				_endP[i] = new Vector3(-x[i], 2, z[i]);
		}
		else
		{
			for (var i = 0; i < z.Length; i++)
				z[i] = 50 * ((int) dir / 2);

			x[0] = RndFrom();
			x[1] = RndFrom(x[0]);

			for (var i = 0; i < _endP.Length; i++)
				_endP[i] = new Vector3(x[i], 2, -z[i]);
		}

		for (var i = 0; i < _walls.Length; i++)
		{
			_walls[i].transform.position = new Vector3(x[i], 2, z[i]);
			_walls[i].transform.rotation = Quaternion.identity;
		}
	}

	private void Update()
	{
		if (_walls[0].transform.position == _endP[0] && Global.IsAlive)
		{
			Generate();
			_scoreText.text = "Score: " + ++_generated;
			if (_generated > Global.Record)
				Global.Record = _generated;
		}

		for (var i = 0; i < _walls.Length; i++)
			_walls[i].transform.position =
				Vector3.MoveTowards(_walls[i].transform.position, _endP[i], speed * Time.deltaTime);

		_particleSystemMain.startColor =
			Color.Lerp(_particleSystemMain.startColor.color, _pSysColor, 3 * Time.deltaTime);
	}
}