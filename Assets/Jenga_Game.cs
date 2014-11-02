using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class Jenga_Game : MonoBehaviour {

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
            } 
            else 
            {
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

        void OnGUI() {

            if (editingEnded == false) 
            {
                GUI.Box (new Rect (10,10,140,190), "Preparing game");

                #region PlayerNumber

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
			
                if (GUI.Button (new Rect (65, 120, 30, 20), "-"))
                {
                    --ComputerPlayers;
                }
                if (GUI.Button (new Rect (100, 120, 30, 20), "+"))
                {
                    ++ComputerPlayers;
                }
                #endregion

                if (GUI.Button (new Rect (20, 160, 120, 20), "Start Game"))
                {
                    editingEnded = true;
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
