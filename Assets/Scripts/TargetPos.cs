using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetPos : MonoBehaviour
{
	// target
	private GameObject target;
	private GameObject[] slider = new GameObject[2];  // slider 配列の追加
	private float[] sliderVal = new float[2];

	// Start is called before the first frame update
	void Start()
	{
		target = GameObject.Find("Target");
		for (int i = 0; i < slider.Length; i++)
		{
			slider[i] = GameObject.Find("Slider_" + i.ToString());  // Sliderオブジェクトを取得
			sliderVal[i] = slider[i].GetComponent<Slider>().value;  // Sliderの値を取得
		}
	}

	// Update is called once per frame
	void Update()
	{
		// get slider value
		for (int i = 0; i < slider.Length; i++)
		{
			sliderVal[i] = slider[i].GetComponent<Slider>().value;
		}

		// Sliderの値に基づいてターゲットの位置を更新
		target.transform.position = new Vector3(sliderVal[0], sliderVal[1], 0);
	}
}
