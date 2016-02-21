using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Profile : MonoBehaviour {
    public static Profile inst;

    public GameObject Avtoriz;
    public GameObject Content;
    public GameObject BtnMenu;

    public Sprite[] menu_spr;

    public GameObject[] Pole;
    float OldPosX;
    float OldPosPoleX;
    bool flag = true;

	// Use this for initialization
	void Start () {
        inst = this;
        OldPosX = Content.GetComponent<RectTransform>().anchoredPosition.x;
        OldPosPoleX = Pole[0].GetComponent<RectTransform>().anchoredPosition.x;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OpenBtnMenu() {
        if (flag)
        {
            LeanTween.moveX(Content.GetComponent<RectTransform>(), 0f, 0.5f).onComplete = delegate
            {
                LeanTween.alpha(BtnMenu.GetComponent<RectTransform>(), 0, 0.2f).onComplete = delegate
                {
                    BtnMenu.GetComponent<Image>().sprite = menu_spr[1];
                    LeanTween.alpha(BtnMenu.GetComponent<RectTransform>(), 1, 0.2f);
                };
                flag = false;
            };
        }
        else {
            LeanTween.moveX(Content.GetComponent<RectTransform>(), OldPosX, 0.5f).onComplete = delegate
            {
                LeanTween.alpha(BtnMenu.GetComponent<RectTransform>(), 0, 0.2f).onComplete = delegate
                {
                    BtnMenu.GetComponent<Image>().sprite = menu_spr[0];
                    LeanTween.alpha(BtnMenu.GetComponent<RectTransform>(), 1, 0.2f);
                };
                flag = true;
            };
        }
    }

    public void OpenMenu() {
        OpenPole(0);
       /* LeanTween.moveX(Content.GetComponent<RectTransform>(), 0f, 0.5f).onComplete = delegate
        {
            LeanTween.scale(BtnMenu, Vector2.one, 0.5f);
            flag = false;
        };*/
    }

    public void CloseMenu() {
        LeanTween.moveX(Content.GetComponent<RectTransform>(), OldPosX, 0.5f).onComplete = delegate
        {
            LeanTween.alpha(BtnMenu.GetComponent<RectTransform>(), 0f, 0.2f).onComplete = delegate
            {
                BtnMenu.GetComponent<Image>().sprite = menu_spr[0];
                LeanTween.alpha(BtnMenu.GetComponent<RectTransform>(), 1f, 0.2f);
            };
          
            flag = true;
        };
    }

    public void ExitProfileAPI() {
        LeanTween.moveX(Content.GetComponent<RectTransform>(), OldPosX, 0.5f).onComplete = delegate
        {
            for (int i = 0; i < Pole.Length; i++)
                LeanTween.moveX(Pole[i].GetComponent<RectTransform>(), OldPosPoleX, 0.5f);

                LeanTween.alpha(BtnMenu.GetComponent<RectTransform>(), 0, 0.2f).onComplete = delegate
                {
                    BtnMenu.GetComponent<Image>().sprite = menu_spr[0];
                    LeanTween.alpha(BtnMenu.GetComponent<RectTransform>(), 1, 0.2f).onComplete = delegate {
                        LeanTween.scale(Avtoriz, Vector3.one, 0).onComplete = delegate {
                            transform.localScale = Vector3.zero;

                            API.Disconnect();
                        }; 
                };

            };
            flag = true;
        };  
    }

    public void OpenPole(int N) {
        print("N: " + N);
        CloseMenu();
        for (int i = 0; i < Pole.Length; i++)
            if (i == N)
            {
                if (!Pole[i].activeSelf && N != 3)      // != 3 -- не равно экрану задания
                {
                    API.Disconnect();
                    DummyLoad.inst.isLoading = false;
                }
                                                                                                                //wdqwdqwewq
                Pole[i].SetActive(true);
                LeanTween.scale(Pole[i], Vector3.one, 0f);
                LeanTween.moveX(Pole[i].GetComponent<RectTransform>(), 0f, 0.5f);
            }
            else
            {
                Pole[i].SetActive(false);
                Pole[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(OldPosPoleX, 0f);
            }
    }
}
