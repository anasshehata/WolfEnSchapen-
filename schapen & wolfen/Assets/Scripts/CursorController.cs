﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour {

	public Sprite sheep;
	public Sprite wolf;
	private Vector3 mousePosition;

	public GameObject cursorPrefab;
	private GameObject cursor;

	private GameObject playerObject;
	private Player player;

	private CursorStatus cursorStatus;
	public CursorStatus GetCursorStatus() {
		return this.cursorStatus;
	}
	public void SetCursorStatus(CursorStatus cursorStatus) {
		this.cursorStatus = cursorStatus;

		if (this.cursorStatus == CursorStatus.EMPTY) {
			cursor.SetActive (true);
		} else {
			cursor.SetActive (true);
		}
	}

	void Awake() {
		cursorStatus = CursorStatus.EMPTY;

		cursor = Instantiate(cursorPrefab, GameObject.Find("/Canvas/Board").transform);
		cursor.transform.localScale = new Vector3 (50, 50, 0);
		playerObject = GameObject.Find ("/Player");
		player = playerObject.GetComponent<Player> ();
		if (player.playerRole == GridSpaceStatus.SHEEP) {
			cursor.GetComponent<SpriteRenderer> ().sprite = sheep;
		} else {
			cursor.GetComponent<SpriteRenderer> ().sprite = wolf;
        }

        cursor.SetActive (true);
	}

	void Update() {
		if (this.cursorStatus == CursorStatus.OCCUPIED) {
			mousePosition = Input.mousePosition;
			cursor.transform.position = mousePosition;
		}
	}

	public override string ToString() {
		return "Cursor Status: " + cursorStatus.ToString();
	}

}
