using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class GridSpace : MonoBehaviour {

	public Sprite emptySprite;
	public Sprite wolfSprite;
	public Sprite sheepSprite;

    public int foxPosition = 0;
    public int sheep1Position = 1;
    public int sheep2Position = 3;
    public int sheep3Position = 5;
    public int sheep4Position = 7;
    public GridSpaceStatus sheep1OldPos = GridSpaceStatus.SHEEP;
    public GridSpaceStatus sheep2OldPos = GridSpaceStatus.SHEEP;
    public GridSpaceStatus sheep3OldPos = GridSpaceStatus.SHEEP;
    public GridSpaceStatus sheep4OldPos = GridSpaceStatus.SHEEP;
    public bool AITurn = false;

    public int test = 0;
    public int test2 = 0;
    private IList<int> alowedGridSpaceses = new List<int>();
    private IList<int> alowedGridSpacesSheep = new List<int>();

    [SerializeField]
	private int gridSpaceNumber;
	[SerializeField]
	private GridSpaceStatus gridSpaceStatus = GridSpaceStatus.EMPTY;
    

    public int GetGridSpaceNumber() {
		return this.gridSpaceNumber;
	}

    public IList<int> GetAlowedGridSpacesSheep()
    {
        return this.alowedGridSpacesSheep;
    }

    public IList<int> GetAlowedGridSpaces()
    {
        return this.alowedGridSpaceses;
    }

    public bool GetAITurn()
    {
        return this.AITurn;
    }
    public void SetGridSpaceNumber(int gridSpaceNumber) {
		this.gridSpaceNumber = gridSpaceNumber;
	}

	public GridSpaceStatus GetGridSpaceStatus() {
		return this.gridSpaceStatus;
	}       

	public void SetGridSpaceStatus(GridSpaceStatus gridSpaceStatus) {
		this.gridSpaceStatus = gridSpaceStatus;
		if (gridSpaceStatus == GridSpaceStatus.WOLF) {
			this.GetComponent<Image>().sprite = wolfSprite;
		} else if (gridSpaceStatus == GridSpaceStatus.SHEEP) { 
			this.GetComponent<Image>().sprite = sheepSprite;
		} else {
			this.GetComponent<Image>().sprite = emptySprite;
		}
	}

    public void SetAlowedGridSpaces(int Alowedgridspace)
    {
        if (this.alowedGridSpaceses.Contains(Alowedgridspace) == false)
        {
            this.alowedGridSpaceses.Add(Alowedgridspace);
            this.test = alowedGridSpaceses[0];
        }
    }

    public void SetAlowedGridSpacesSheep(int AlowedgridspaceSheep)
    {
        if (this.alowedGridSpacesSheep.Contains(AlowedgridspaceSheep) == false)
        {
            this.alowedGridSpacesSheep.Add(AlowedgridspaceSheep);
            this.test2 = alowedGridSpacesSheep[0];
        }
    }

    public void SetFoxPosition(int FoxPosition)
    {
        this.foxPosition = FoxPosition;
    }

    public void SetSheep1Position(int Sheep1Position)
    {
        this.sheep1Position = Sheep1Position;
    }
    public void SetSheep2Position(int Sheep2Position)
    {
        this.sheep2Position = Sheep2Position;
    }
    public void SetSheep3Position(int Sheep3Position)
    {
        this.sheep3Position = Sheep3Position;
    }
    public void SetSheep4Position(int Sheep4Position)
    {
        this.sheep4Position = Sheep4Position;
    }


    public void SetSheep1OldPos(GridSpaceStatus Sheep1OldPos)
    {
        this.sheep1OldPos = Sheep1OldPos;
    }
    public void SetSheep2OldPos(GridSpaceStatus Sheep2OldPos)
    {
        this.sheep2OldPos = Sheep2OldPos;
    }
    public void SetSheep3OldPos(GridSpaceStatus Sheep3OldPos)
    {
        this.sheep3OldPos = Sheep3OldPos;
    }
    public void SetSheep4OldPos(GridSpaceStatus Sheep4OldPos)
    {
        this.sheep4OldPos = Sheep4OldPos;
    }
    public void clearAlowedGridSpaces()
    {
        if(this.alowedGridSpaceses.Count > 2)
        {
            this.alowedGridSpaceses.RemoveAt(0);
            this.alowedGridSpaceses.RemoveAt(1);
        }
    }

    public void SetAITurn( bool aiTurn)
    {
        this.AITurn = aiTurn;
    }





    public override string ToString() {
		return "GridSpaceNumber: " + gridSpaceNumber + " - GridSpaceStatus: " + gridSpaceStatus.ToString();
	}

	public void ButtonClick() {
        GameObject scripts = GameObject.Find ("/Scripts");
		Player player = GameObject.Find("/Player").GetComponent<Player> ();
        CursorController cursorController = scripts.GetComponent<CursorController> ();
        

        CursorStatus cursorStatus = cursorController.GetCursorStatus();
        // The playerRole is a GridSpaceStatus to easily check if the clicked GridSpace can be moved by the player.
        if (cursorStatus == CursorStatus.EMPTY && player.playerRole == this.gridSpaceStatus) {
            // Pick up current piece
            //print("---"+ GetGridSpaceStatus());
			cursorController.SetCursorStatus(CursorStatus.OCCUPIED); 
			this.SetGridSpaceStatus (GridSpaceStatus.EMPTY);

        }

        print("sheep1Position: " + sheep1Position);
        print("sheep2Position: " + sheep2Position);
        print("sheep3Position: " + sheep3Position);
        print("sheep4Position: " + sheep4Position);

        if (alowedGridSpaceses.Count == 4)
        {
            print("pos0:" + alowedGridSpaceses[0]);
            print("pos1:" + alowedGridSpaceses[1]);
            print("pos2:" + alowedGridSpaceses[2]);
            print("pos3:" + alowedGridSpaceses[3]);

   
            print("---");
        }
        if (alowedGridSpaceses.Count == 2 )
        {
            print("pos0:" + alowedGridSpaceses[0]);
            print("pos1:" + alowedGridSpaceses[1]);
 
            print("---");
        }
        if (alowedGridSpaceses.Count == 1 )
        {
            print("pos0:" + alowedGridSpaceses[0]);

            print("---");
        }

        if (alowedGridSpacesSheep.Count == 5)
        {


            print("alowedGridSpacesSheep [0]:" + alowedGridSpacesSheep[0]);
            print("alowedGridSpacesSheep [1]:" + alowedGridSpacesSheep[1]);
            print("alowedGridSpacesSheep [2]:" + alowedGridSpacesSheep[2]);
            print("alowedGridSpacesSheep [3]:" + alowedGridSpacesSheep[3]);
            print("---");
        }
        if (alowedGridSpacesSheep.Count == 3)
        {
            print("alowedGridSpacesSheep[0]:" + alowedGridSpacesSheep[0]);
            print("alowedGridSpacesSheep[1]:" + alowedGridSpacesSheep[1]);
            print("alowedGridSpacesSheep[2]:" + alowedGridSpacesSheep[2]);
            print("---");
        }

        if (alowedGridSpacesSheep.Count == 2)
        {
            print("alowedGridSpacesSheep[0]:" + alowedGridSpacesSheep[0]);
            print("alowedGridSpacesSheep[1]:" + alowedGridSpacesSheep[1]);
            print("---");
        }
        print(alowedGridSpaceses.Count);
        print("0--0");



        if (cursorStatus == CursorStatus.OCCUPIED && alowedGridSpaceses.Count == 4) {
            if (this.gridSpaceStatus == GridSpaceStatus.EMPTY && 
                alowedGridSpaceses[0] == foxPosition || 
                alowedGridSpaceses[1] == foxPosition||
                alowedGridSpaceses[2] == foxPosition ||
                alowedGridSpaceses[3] == foxPosition
                ) {
                // Drop Piece if allowed
                print("Count = 4");
                print("FoxPOsition = " + foxPosition);
                print(this.GetGridSpaceNumber());

                cursorController.SetCursorStatus(CursorStatus.EMPTY);
				this.SetGridSpaceStatus (player.playerRole);

			}
		}
        if(cursorStatus == CursorStatus.OCCUPIED && alowedGridSpaceses.Count == 2)
        {
            if (this.gridSpaceStatus == GridSpaceStatus.EMPTY &&
                alowedGridSpaceses[0] == foxPosition ||
                alowedGridSpaceses[1] == foxPosition)
            {
                // Drop Piece if allowed
                print("Count = 2");
                print("FoxPOsition = " + foxPosition);
                print(this.GetGridSpaceNumber());

                cursorController.SetCursorStatus(CursorStatus.EMPTY);
                this.SetGridSpaceStatus(player.playerRole);
            }
        }
        if (cursorStatus == CursorStatus.OCCUPIED && alowedGridSpaceses.Count == 1)
        {
            if (this.gridSpaceStatus == GridSpaceStatus.EMPTY &&
                alowedGridSpaceses[0] == foxPosition )
            {
                // Drop Piece if allowed
            print("Count = 1");
            print("FoxPOsition = " + foxPosition);
            print(this.GetGridSpaceNumber());

            cursorController.SetCursorStatus(CursorStatus.EMPTY);
            this.SetGridSpaceStatus(player.playerRole);
            }
        }

        /*if (cursorStatus == CursorStatus.OCCUPIED && alowedGridSpacesSheep.Count == 4)
        {
            if (this.gridSpaceStatus == GridSpaceStatus.EMPTY &&
                alowedGridSpacesSheep[0] == sheep1Position ||
                alowedGridSpacesSheep[1] == sheep1Position 
                )
            {
                // Drop Piece if allowed
                print("countSheep = 4");
                print("SheePosition = " + foxPosition);
                print(this.GetGridSpaceNumber());

                cursorController.SetCursorStatus(CursorStatus.EMPTY);
                this.SetGridSpaceStatus(player.playerRole);

            }
        }*/

        //count == 3
        if (cursorStatus == CursorStatus.OCCUPIED && alowedGridSpacesSheep.Count == 3)
        {

            //sheep1
            if (this.gridSpaceStatus == GridSpaceStatus.EMPTY &&
                alowedGridSpacesSheep[0] == sheep1Position ||
                alowedGridSpacesSheep[1] == sheep1Position ||
                alowedGridSpacesSheep[2] == sheep1Position

                )
            {
                if (sheep1OldPos == GridSpaceStatus.EMPTY)
                {
                    // Drop Piece if allowed
                    print("oldPosition" + sheep1OldPos);
                    /*print("CountSheep = 2");
                    print("SheePosition = " + sheep1Position);
                    print(this.GetGridSpaceNumber());*/

                    cursorController.SetCursorStatus(CursorStatus.EMPTY);
                    this.SetGridSpaceStatus(player.playerRole);
                    AITurn = true;
                }
            }

            // sheep 2
            if (this.gridSpaceStatus == GridSpaceStatus.EMPTY &&
                alowedGridSpacesSheep[0] == sheep2Position ||
                alowedGridSpacesSheep[1] == sheep2Position ||
                alowedGridSpacesSheep[2] == sheep2Position

                )
            {
                if (sheep2OldPos == GridSpaceStatus.EMPTY)
                {
                    // Drop Piece if allowed
                    print("oldPosition" + sheep2OldPos);
                    /* print("CountSheep = 2");
                     print("SheePosition = " + sheep2Position);
                     print(this.GetGridSpaceNumber());*/

                    cursorController.SetCursorStatus(CursorStatus.EMPTY);
                    this.SetGridSpaceStatus(player.playerRole);
                    AITurn = true;
                }
            }

            //sheep 3
            if (this.gridSpaceStatus == GridSpaceStatus.EMPTY &&
                alowedGridSpacesSheep[0] == sheep3Position ||
                alowedGridSpacesSheep[1] == sheep3Position ||
                alowedGridSpacesSheep[2] == sheep3Position

                )
            {
                if (sheep3OldPos == GridSpaceStatus.EMPTY)
                {
                    // Drop Piece if allowed
                    print("oldPosition" + sheep3OldPos);
                    /*print("CountSheep = 2");
                    print("SheePosition = " + sheep3Position);
                    print(this.GetGridSpaceNumber());*/

                    cursorController.SetCursorStatus(CursorStatus.EMPTY);
                    this.SetGridSpaceStatus(player.playerRole);
                    AITurn = true;
                }
            }

            //sheep4
            if (this.gridSpaceStatus == GridSpaceStatus.EMPTY &&
               alowedGridSpacesSheep[0] == sheep4Position ||
               alowedGridSpacesSheep[1] == sheep4Position ||
                alowedGridSpacesSheep[2] == sheep4Position

               )
            {
                if (sheep4OldPos == GridSpaceStatus.EMPTY)
                {
                    // Drop Piece if allowed
                    print("oldPosition" + sheep4OldPos);
                    /* print("CountSheep = 2");
                     print("SheePosition = " + sheep4Position);
                     print(this.GetGridSpaceNumber());*/

                    cursorController.SetCursorStatus(CursorStatus.EMPTY);
                    this.SetGridSpaceStatus(player.playerRole);
                    AITurn = true;
                }
            }
        }
        if (cursorStatus == CursorStatus.OCCUPIED && alowedGridSpacesSheep.Count == 2)
        {
            if (this.gridSpaceStatus == GridSpaceStatus.EMPTY &&
                alowedGridSpacesSheep[0] == sheep1Position ||
                alowedGridSpacesSheep[1] == sheep1Position
                )
            {
                if (sheep1OldPos == GridSpaceStatus.EMPTY)
                {
                    // Drop Piece if allowed
                    /* print("GRIDSTATUS" + this.gridSpaceStatus);
                     print("CountSheep = 1");
                     print("SheePosition = " + foxPosition);
                     print(this.GetGridSpaceNumber());*/

                    cursorController.SetCursorStatus(CursorStatus.EMPTY);
                    this.SetGridSpaceStatus(player.playerRole);
                    AITurn = true;
                }
            }

            // sheep 2
            if (this.gridSpaceStatus == GridSpaceStatus.EMPTY &&
                alowedGridSpacesSheep[0] == sheep2Position ||
                alowedGridSpacesSheep[1] == sheep2Position

                )
            {
                if (sheep2OldPos == GridSpaceStatus.EMPTY)
                {
                    // Drop Piece if allowed
                   // print("CountSheep = 1");
                   // print("SheePosition = " + foxPosition);
                   // print(this.GetGridSpaceNumber());

                    cursorController.SetCursorStatus(CursorStatus.EMPTY);
                    this.SetGridSpaceStatus(player.playerRole);
                    AITurn = true;
                }
            }

            //sheep 3
            if (this.gridSpaceStatus == GridSpaceStatus.EMPTY &&
                alowedGridSpacesSheep[0] == sheep3Position ||
                alowedGridSpacesSheep[1] == sheep3Position

                )
            {
                if (sheep3OldPos == GridSpaceStatus.EMPTY)
                {
                    // Drop Piece if allowed
                   // print("CountSheep = 1");
                   // print("SheePosition = " + foxPosition);
                   // print(this.GetGridSpaceNumber());

                    cursorController.SetCursorStatus(CursorStatus.EMPTY);
                    this.SetGridSpaceStatus(player.playerRole);
                    AITurn = true;
                }
            }

            //sheep4
            if (this.gridSpaceStatus == GridSpaceStatus.EMPTY &&
               alowedGridSpacesSheep[0] == sheep4Position ||
                alowedGridSpacesSheep[1] == sheep4Position

               )
            {
                if (sheep4OldPos == GridSpaceStatus.EMPTY)
                {
                    // Drop Piece if allowed
                   // print("CountSheep = 1");
                   // print("SheePosition = " + foxPosition);
                   // print(this.GetGridSpaceNumber());

                    cursorController.SetCursorStatus(CursorStatus.EMPTY);
                    this.SetGridSpaceStatus(player.playerRole);
                    AITurn = true;
                }
            }


            //AI
        }
    }

}
