using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetPos : MonoBehaviour
{
	// target
	private GameObject target;
	private GameObject[] slider = new GameObject[2];  // slider �z��̒ǉ�
	private float[] sliderVal = new float[2];

	// Start is called before the first frame update
	void Start()
	{
		target = GameObject.Find("Target");
		for (int i = 0; i < slider.Length; i++)
		{
			slider[i] = GameObject.Find("Slider_" + i.ToString());  // Slider�I�u�W�F�N�g���擾
			sliderVal[i] = slider[i].GetComponent<Slider>().value;  // Slider�̒l���擾
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

		// Slider�̒l�Ɋ�Â��ă^�[�Q�b�g�̈ʒu���X�V
		target.transform.position = new Vector3(sliderVal[0], sliderVal[1], 0);
	}
}
