using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Message : MonoBehaviour {
    public Text Label;

    public Button Btn1;
    public Button Btn2;
    public Button Btn3;

    string Methotod;
    // Use this for initialization

    private static Message _instance;

    public static Message instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Message>();
            }
            return _instance;
        }
    }

    void Awake() {
        transform.localScale = Vector3.zero;
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MessageInfo(string LabelInfo, int TypeInfo) {
        LeanTween.scale(gameObject, Vector3.one, 0f);

        System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
        System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;

        Label.text = textInfo.ToUpper(LabelInfo);
        switch (TypeInfo) {
            case 1: Btn3.gameObject.SetActive(false); Btn2.gameObject.SetActive(true); break;
            case 2: Btn2.gameObject.SetActive(false); Btn3.gameObject.SetActive(false); break;
            default: Btn2.gameObject.SetActive(true); Btn3.gameObject.SetActive(true); break;
        }
    }

    public void MessageClose() {
        LeanTween.scale(gameObject, Vector3.zero, 0f);
    }

    public void Replay() {

    }
}
