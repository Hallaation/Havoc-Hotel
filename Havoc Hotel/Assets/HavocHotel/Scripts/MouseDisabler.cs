﻿using UnityEngine;
using System.Collections;

public class MouseDisabler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
