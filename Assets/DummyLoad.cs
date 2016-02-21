using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DummyLoad : MonoBehaviour {
    public static DummyLoad inst;
    // Use this for initialization
    public Image[] dm;
    private float delay;
	public bool isLoading = true;
    void Start () {
        inst = this;

        StartCoroutine(Pulse(dm[0].GetComponent<RectTransform>(), 0.6f));
        StartCoroutine(Pulse(dm[1].GetComponent<RectTransform>(), 0.3f));
        StartCoroutine(Pulse(dm[2].GetComponent<RectTransform>(), 0f));



        /*foreach (Image d in dm) 
                {
                    d.GetComponent<RectTransform>().localScale = Vector2.zero;
                }*/

    }


    IEnumerator Pulse(RectTransform rect, float offset)
    {
        float scale = 0f;
        while (true)
        {
            rect.gameObject.SetActive(isLoading);
            rect.localScale = new Vector3(scale, scale, 1f);
            scale = Mathf.Clamp01(Mathf.PingPong(offset + Time.timeSinceLevelLoad * 1.5f, 1.5f) - 0.5f);
            yield return new WaitForSeconds(0.001f);
        }
    }
    /*
        public void Loop() {
            Loading();
            InvokeRepeating("Loading", 0, 2);
        }

        public void Loading()
        {
            //bool isLoading = true;
            if (isLoading) {
                gameObject.SetActive (true);
                LeanTween.scale (dm [0].GetComponent<RectTransform> (), Vector2.one, 0.5f).setDelay (0).setLoopPingPong(1);
                LeanTween.scale (dm [1].GetComponent<RectTransform> (), Vector2.one, 0.5f).setDelay (0.2f).setLoopPingPong(1);
                LeanTween.scale (dm [2].GetComponent<RectTransform> (), Vector2.one, 0.5f).setDelay (0.4f).setLoopPingPong(1);
            } else if (!isLoading) {
                gameObject.SetActive(false);
            }
        }
        */


    // Update is called once per frame
    void Update () {
	}
}
