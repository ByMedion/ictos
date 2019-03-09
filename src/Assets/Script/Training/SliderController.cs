using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
	public Text              progressText;
	public SlideController[] slides;

	private void Start()
	{
		progressText.text = string.Format("{0}/{1}", 1, slides.Length);
		SlideIn(slides[0].transform);
		for (var i = 0; i < slides.Length - 1; i++)
		{
			var i1 = i;
			slides[i]
				.onClick.AddListener(sender =>
				{
					progressText.text = string.Format("{0}/{1}", i1 + 2, slides.Length);
					SlideOut(sender, () => Destroy(sender.gameObject));
					SlideIn(slides[i1 + 1].transform);
				});
		}

		slides[slides.Length - 1]
			.onClick.AddListener(sender => SlideOut(sender, () => SceneManager.LoadScene(Scenes.Game)));
	}

	private void FixedUpdate()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			SceneManager.LoadScene(0);
	}

	private static IEnumerator SlidePanel(Transform current, Vector3 to, float speed, Action onFinish = null)
	{
		var currPos = current.position;
		while (currPos != to)
		{
			currPos          = Vector3.MoveTowards(currPos, to, speed * Time.deltaTime);
			current.position = currPos;

			yield return null;
		}

		if (onFinish != null)
			onFinish.Invoke();
	}

	private void SlideIn(Transform panel)
	{
		var to = panel.position;
		to.x = 0;
		StartCoroutine(SlidePanel(panel, to, 30));
	}

	private void SlideOut(Transform panel, Action onFinish = null)
	{
		var to = panel.position;
		to.x = -14;
		StartCoroutine(SlidePanel(panel, to, 45, onFinish));
	}
}