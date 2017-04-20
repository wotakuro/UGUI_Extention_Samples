using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public float p = 0.5f;
    public ImageFillEffect bar;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        bar.fillAmount = (Mathf.Cos(Time.realtimeSinceStartup) + 1.0f )* 0.5f;
	}
}
