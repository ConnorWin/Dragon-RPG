﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	GameObject player;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectsWithTag ("Player") [0];
	}
	
	// LateUpdate is called once per frame
	void LateUpdate ()
	{
		transform.position = player.transform.position;
	}
}