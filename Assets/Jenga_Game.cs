using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using System.Collections;

public class Jenga_Game : MonoBehaviour {

	public class Player {
		public int id;
		public bool enabled;

		public Player(int ID) { id = ID; enabled = false; }
	};

	public List<Player> Players;

	public int PlayerNumber = 2;
	public int ComputerPlayers = 0;

	public Jenga_Control_Block jcb = null;
	public Jenga_Controller jc = null;

	private string	strPlayer = "", 
					strPlayerBegin_Human = "Player ",
					strPlayerBegin_Computer = "Computer ";
	private string	strGame = "",
					strGame_Moving = "Who is moving?",
					strGame_GameOver = "Game Over! And the loser is : ";

	// Use this for initialization
	void Start () {
		if (ComputerPlayers > PlayerNumber)
		{
			ComputerPlayers = PlayerNumber;
		}
		jc.canMove = true;

		Players = new List<Player> ();

		for (int i = 0; i < PlayerNumber; ++i)
		{
			Players.Add(new Player(i));
			//Players[i].id = i;
		}

		strPlayer = strPlayerBegin_Human + "0";
		strGame = strGame_Moving;

	}
	
	// Update is called once per frame
	void Update () {

		if (jcb.controler >= jcb.treshold)
		{
			jc.canMove = false;

			int temp = jc.moveIterator;
			if (jc.blockPicked == false) {
				temp -= 1;
			}
			temp %= PlayerNumber;

			strPlayer = "L : " + PlayerRace(temp) + temp.ToString();
			strGame = strGame_GameOver;
		} 
		else 
		{
			jc.canMove = true;
			strGame = strGame_Moving;
			strPlayer = (PlayerRace(jc.moveIterator % PlayerNumber)) + (jc.moveIterator % PlayerNumber).ToString ();
		}


	}

	void OnGUI() {
		GUI.Box (new Rect (10,10,200,90), strGame);
		GUI.TextField (new Rect (20, 40, 160, 20), strPlayer);

	}

	string PlayerRace(int x){

		if ( x < (PlayerNumber - ComputerPlayers) )
		{
			return strPlayerBegin_Human;
		}

		return strPlayerBegin_Computer;
	}
}
