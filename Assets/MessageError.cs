using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageError : MonoBehaviour {
	public Text Label;

	string Methotod;
	// Use this for initialization
	
	private static MessageError _instance;
	float startPos, finishPos;


	public static MessageError instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<MessageError>();
			}
			return _instance;
		}
	}
	
	void Awake() {
		startPos = transform.position.y;
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void MessageErrorInfo(string LabelInfo) {
		LeanTween.moveY(GetComponent<RectTransform>(), 0f, 0.5f).onComplete = delegate
		{
			LeanTween.alpha(gameObject, 1, 2).onComplete = delegate {
				MessageErrorClose();
		};
		};
		Label.text = LabelInfo;

	}
	
	public void MessageErrorClose() {
		LeanTween.moveY(GetComponent<RectTransform>(), 280f, 0.5f);
	}
	
	public void Replay() {
		
	}
}
