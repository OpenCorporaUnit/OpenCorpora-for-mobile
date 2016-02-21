using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Authorization : MonoBehaviour {
    public InputField login;
    public InputField password;
    public GameObject Regist;

    bool f = true;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("Login"))
            login.text = PlayerPrefs.GetString("Login");

        if (PlayerPrefs.HasKey("Password"))
           password.text = PlayerPrefs.GetString("Password");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LogIn() {
        if (f)
        {
            f = false;
            Invoke("OnOffF", 1f);
            if (login.text.Length == 0 || password.text.Length == 0)
                MessageError.instance.MessageErrorInfo("Имеются пустые поля");
            else
            {
                Menu.instance.LoginInAPI(login.text, password.text, true);
            }
        }
    }

    void OnOffF() {
        f = true;
    }

    public void Registration() {
        LeanTween.scale(Regist, Vector3.one, 0f);
        LeanTween.scale(gameObject, Vector3.zero, 0f);
    }
}
