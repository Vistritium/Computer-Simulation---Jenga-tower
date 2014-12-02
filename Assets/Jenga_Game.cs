using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Assets
{
    public class Jenga_Game : MonoBehaviour {
		
		public enum AIMode {
			random = 0,
			heurystic,
			other, 
			Length
		}
		
        public abstract class Player {
            public static int idCounter;
            public int id = idCounter++;
            public bool enabled;

            public Player() { enabled = false; }

            abstract public void Turn ();
        };

        public class AiPlayer : Player {
            public AiPlayer() : base(){}

            public override void Turn ()
            {
                GameObject.Find ("AI").GetComponent<AiRunner> ().Turn ();
                GameObject.Find ("Jenga_Blocks_Controller").GetComponent<Jenga_Controller> ().moveIterator++;
            }
        }

        public class HumanPlayer : Player {
            public HumanPlayer(): base(){}

            public override void Turn ()
            {

            }
        }

        public List<Player> Players;

        public int PlayerNumber = 2;
        public int ComputerPlayers = 0;

        public Jenga_Control_Block jcb = null;
        public Jenga_Controller jc = null;
		
		public List<AIMode> AIModes;

        private string	strPlayer = "", 
            strPlayerBegin_Human = "Player ",
            strPlayerBegin_Computer = "Computer ";
        private string	strGame = "",
            strGame_Moving = "Who is moving?",
            strGame_GameOver = "Game Over!\nAnd the loser is : ";

        private string	strPlayerNumber = "";

        private bool	editingEnded = false, firstUpdate = true;

        // Use this for initialization
        void Start () {
            if (ComputerPlayers > PlayerNumber)
            {
                ComputerPlayers = PlayerNumber;
            }
            jc.canMove = true;

            Players = new List<Player> ();

            strPlayer = strPlayerBegin_Human + "0";
            strGame = strGame_Moving;

            jc.toInvokeOnNewTurn.Add (this.newTurn);
		
        }


        // Update is called once per frame
        void Update () {

            if (editingEnded == false)
            {
                jc.canMove = false;
                return;
            }
            else
            {
                if (firstUpdate == true) {
                    firstUpdate = false;
                    for (int i = 0; i < PlayerNumber - ComputerPlayers; ++i)
                    {
                        Players.Add(new HumanPlayer());
                    }
				
                    for (int i=0; i<ComputerPlayers; i++)
                    {
                        Players.Add(new AiPlayer());
                    }
                }
            }

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
                Jenga_Controller.GameFinished = true;
            } 
            else 
            {
                Jenga_Controller.GameFinished = false;
                jc.canMove = true;
                strGame = strGame_Moving;
                strPlayer = (PlayerRace(jc.moveIterator % PlayerNumber)) + (jc.moveIterator % PlayerNumber).ToString ();
            }


        }

        public void newTurn(){
            var currentPlayerNumber = jc.moveIterator % PlayerNumber;
            var currentPlayer = Players[currentPlayerNumber];
            currentPlayer.Turn ();
        }

		void OnGUI() 
		{
			
			if (editingEnded == false) 
			{
				
				if ( AIModes.ToArray().Length < ComputerPlayers ) {
					for (int i = AIModes.ToArray().Length; i < ComputerPlayers; ++i)
					{
						AIModes.Add(new AIMode());
					}
				}
				
				if ( AIModes.ToArray().Length > ComputerPlayers ) {
					for (int i = AIModes.ToArray().Length; i > ComputerPlayers; --i)
					{
						AIModes.RemoveAt(i-1);
					}
				}
				
				int oversize = 0;
				int oversizeDelta = 25;
				
				if ( ComputerPlayers > 0 )
				{
					oversize += 10 + oversizeDelta * ComputerPlayers;
				}
				
				GUI.Box (new Rect (10,10,140,190 + oversize), "Preparing game");
				
				#region PlatyerNumber
				
				GUI.Box ( new Rect (15,45,130,50), "Number of players" );
				GUI.TextField (new Rect (20, 70, 40, 20), PlayerNumber.ToString());
				if (GUI.Button (new Rect (65, 70, 30, 20), "-"))
				{
					--PlayerNumber;
				}
				if (GUI.Button (new Rect (100, 70, 30, 20), "+"))
				{
					++PlayerNumber;
				}
				#endregion
				
				#region Computer Players
				
				GUI.Box ( new Rect (15,95,130,50), "Number of AI" );
				GUI.TextField (new Rect (20, 120, 40, 20), ComputerPlayers.ToString());
				
				//Debug.Log (strPlayerNumber);
				if (GUI.Button (new Rect (65, 120, 30, 20), "-"))
				{
					if (ComputerPlayers > 0) 
					{
						--ComputerPlayers;
					}
				}
				if (GUI.Button (new Rect (100, 120, 30, 20), "+"))
				{
					if (ComputerPlayers < PlayerNumber) 
					{
						++ComputerPlayers;
					}
				}
				#endregion
				
				int sY = 160;
				
				for (int i = 0; i < ComputerPlayers; ++i) 
				{
					GUI.TextField (new Rect (45, sY + i * oversizeDelta, 70, 20), AIModes[i].ToString());
					
					if (GUI.Button (new Rect (20, sY + i * oversizeDelta, 20, 20), "<"))
					{
						if (AIModes[i] > 0) 
						{
							AIModes[i] -= 1;
						}
					}
					
					if (GUI.Button (new Rect (120, sY + i * oversizeDelta, 20, 20), ">"))
					{
						if (AIModes[i] < AIMode.Length - 1) 
						{
							AIModes[i] += 1;
						}
					}
				}
				
				if (GUI.Button (new Rect (20, 160 + oversize, 120, 20), "Start Game"))
				{
					editingEnded = true;
					string str = "";
					foreach ( var i in AIModes ){
						str += i.ToString() + " # ";
					}
				//	Debug.Log (str);
				}
			}
			else
			{
				GUI.Box (new Rect (10,10,120,70), strGame);
				GUI.TextField (new Rect (20, 50, 100, 20), strPlayer);
			}
			
		}



		string PlayerRace(int x){
		
			if ( x < (PlayerNumber - ComputerPlayers) )
			{
				return strPlayerBegin_Human;
			}
		
			return strPlayerBegin_Computer;
		}
	}
}
