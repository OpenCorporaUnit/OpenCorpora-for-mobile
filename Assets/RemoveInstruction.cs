using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RemoveInstruction : MonoBehaviour {

	// Use this for initialization

	public Scrollbar sb;
	float startpos;
	void Start () {
		startpos = GetComponent<RectTransform> ().anchoredPosition.x;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Close()
    {

        LeanTween.moveX(GetComponent<RectTransform>(), startpos, 0.5f).onComplete = delegate
        {
            sb.value = 1;
        };
    }

    public void Open()
    {
        LeanTween.moveX(GetComponent<RectTransform>(), 0, 0.5f);
    }
}
