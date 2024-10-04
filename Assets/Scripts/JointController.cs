using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace InverseKinematics
{
    public class JointController : MonoBehaviour
    {  
        // robot
        private GameObject[] joint = new GameObject[2];
        private GameObject[] arm = new GameObject[2];
        private float[] armL = new float[2];
        private Vector3[] angle = new Vector3[2];

        // UI
        private GameObject[] slider = new GameObject[2];
        private float[] sliderVal = new float[2];
        private GameObject[] angText = new GameObject[2];
        private GameObject[] posText = new GameObject[2];

        // Start is called before the first frame update
        void Start()
        {
            // robot
            for (int i = 0; i < joint.Length; i++)
            {
                joint[i] = GameObject.Find("Joint_" + i.ToString());
                arm[i] = GameObject.Find("Arm_" + i.ToString());
                armL[i] = arm[i].transform.localScale.x;    // get arm length
            }
            // UI settings
            for (int i = 0; i < joint.Length; i++)
            {
                slider[i] = GameObject.Find("Slider_" + i.ToString());
                sliderVal[i] = slider[i].GetComponent<Slider>().value;
                posText[i] = GameObject.Find("Ref_" + i.ToString());
                angText[i] = GameObject.Find("Ang_" + i.ToString());
            }
            // 中村俊幸.Unityで学ぶ ロボットアームの逆運動学(MRブックス) (pp.25 - 26). Kindle 版. 
        }

        // Update is called once per frame
        void Update()
        {
            // get slider value
            for (int i = 0; i < joint.Length; i++)
            {
                sliderVal[i] = slider[i].GetComponent<Slider>().value;
            }

            // memo
            /*{
                余弦定理を使う
                ボーンの長さをl1,l2,第一ルートの始点の内角をα外角をｑ1、第二ルートの始点の内角をβ外角をｑ2とすると、
                l1^2 + l2^2 - 2 * l1 * l2 * cos(θ) = x^2 + y^2となり、
                l2^2 = l1^2+ x^2 + y^2 - 2 * l1 * √(x^2 + y^2) * cos(θ)となる。
                そのため
                α = cos^-1 * (l1^2 + l2^2 - x^2 - y^2
                    / 2 * l1^2 * l2^2)
                β = cos^-1 * (x^2 + y^2 + l1^2 - l2^2
                    / 2 * l1 * √(x^2 + y^2))
                山折りで曲げる場合
                q1 = tan^-1 * (y/x) - β
                q2 = π -\+ α
                谷折りで曲げる場合
                q1 = tan^-1 * (y/x) - β
                q2 = π - α
            }*/

            // inverse kinematics
            float x = sliderVal[0];
            float y = sliderVal[1];

            // calculate angle
            float a = Mathf.Acos((armL[0] * armL[0] + armL[1] * armL[1] - x * x - y * y) 
                / (2f * armL[0] * armL[1]));
            float b = Mathf.Acos((armL[0] * armL[0] + x * x + y * y - armL[1] * armL[1])
                / (2f * armL[0] * Mathf.Pow((x * x + y * y), 0.5f)));

            angle[1].z = -Mathf.PI + a;
            angle[0].z = Mathf.Atan2(y, x) + b;

            // set angle
            for (int i = 0; i < joint.Length; i++)
            {
                joint[i].transform.localEulerAngles = angle[i] * Mathf.Rad2Deg;
                posText[i].GetComponent<Text>().text = sliderVal[i].ToString("f2");
            }

            angText[0].GetComponent<Text>().text = (angle[0].z * Mathf.Rad2Deg).ToString("f2");
            angText[1].GetComponent<Text>().text = (angle[1].z * Mathf.Rad2Deg).ToString("f2");

           // 中村俊幸.Unityで学ぶ ロボットアームの逆運動学(MRブックス) (pp.26 - 27). Kindle 版. 

        }
    }
}
