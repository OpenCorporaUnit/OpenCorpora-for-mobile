using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;

public class Registration : MonoBehaviour {
    public InputField login;
    public InputField pass;
    public InputField passcheck;
    public InputField email;
    public Toggle licenzi;
    public GameObject avtoriz;
	// Use this for initialization
	void Awake () {
        transform.localScale = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick() {
        if (login.text.Length == 0 || pass.text.Length == 0 || passcheck.text.Length == 0)
            MessageError.instance.MessageErrorInfo("Имеются пустые поля");
        else if (string.Compare(pass.text, passcheck.text) != 0)
            MessageError.instance.MessageErrorInfo("Введённые пароли не совпадают");
        else if (!licenzi.isOn)
            MessageError.instance.MessageErrorInfo("Для завершения регистрации необходимо согласиться с лицензионным соглашением");
        else {

        string json = @"
          {
              ""login""   : """ + login.text + @""",
              ""passwd"" : """ + pass.text + @""",
              ""passwd_re"" : """ + passcheck.text + @""",
              ""email"" : """ + email.text + @"""
          }
        ";


            API.SendRequest("register", JsonMapper.ToObject(json), RegistrationComplete);

        }
    }

    void RegistrationComplete(JsonData data)
    {
        Menu.instance.RegistrationInAPI(login.text, pass.text, licenzi.isOn, false);
    }

    public void Close()
    {
        LeanTween.scale(avtoriz, Vector3.one, 0f);
        LeanTween.scale(gameObject, Vector3.zero, 0f);
    }
}
