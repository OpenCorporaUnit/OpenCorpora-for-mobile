using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Statistic : MonoBehaviour {

	// Use this for initialization

	public Text all_score_p, check_p, mistake_p;

	void Start () {
		//AllStatistic (all_score_p.text, check_p.text, mistake_p.text);
	}

	public void AllStatistic(string all_score, string check, string mistake)
	{
		all_score_p.text = all_score;
		check_p.text = check;
		mistake_p.text = mistake;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
