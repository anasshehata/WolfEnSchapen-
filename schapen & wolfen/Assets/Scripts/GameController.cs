using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject gridArea;
	public GameObject gridSpaceButton;
	private List<GridSpace> gridSpaces = new List<GridSpace>();
    private int foxPos = 60;
    private int sheep1Pos = 1;
    private int sheep2Pos = 3;
    private int sheep3Pos = 5;
    private int sheep4Pos = 7;
    private int sheep1buffer = 1;
    private int sheep2buffer = 3;
    private int sheep3buffer = 5;
    private int sheep4buffer = 7;

    private GridSpaceStatus AIGoToStatus = GridSpaceStatus.EMPTY;


    /*
	 * Initialise the buttons for the grid.
	 * */
    void Awake() {
		int k = 0;
		for(int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				GameObject tempButton = Instantiate (gridSpaceButton);
				tempButton.transform.SetParent(gridArea.transform);
				tempButton.transform.localPosition = new Vector2((-256 + (64 * j)), (256 - (64 * i)));
				tempButton.transform.GetComponentInChildren<Text>().text = k.ToString();
         

				GridSpace tempGridSpace = tempButton.GetComponent<GridSpace> ();
				tempGridSpace.SetGridSpaceNumber(k);


                tempGridSpace.SetSheep1Position(1);
                tempGridSpace.SetSheep2Position(3);
                tempGridSpace.SetSheep3Position(5);
                tempGridSpace.SetSheep4Position(7);
                if (PieceOnLeftEdge(tempGridSpace.GetGridSpaceNumber()) != true && PieceOnRightEdge(tempGridSpace.GetGridSpaceNumber()) != true && PieceOnBottomEdge(tempGridSpace.GetGridSpaceNumber()) != true && PieceOnCorner(tempGridSpace.GetGridSpaceNumber()) != true)

                {

                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() + 7);
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() + 9);
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() - 7);
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() - 9);


                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() - 7);
                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() - 9);
                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber());

                    //print(openGridSpaceses[0]);
                    //print(openGridSpaceses[1]);
                }
                else if (PieceOnLeftEdge(tempGridSpace.GetGridSpaceNumber()) == true && PieceOnCorner(tempGridSpace.GetGridSpaceNumber()) != true)
                {
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() - 7);
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() + 9);

                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() - 7);
                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber());
                    //tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() - 9);

                    //print(openGridSpaceses[0]);
                }
                else if(PieceOnBottomEdge(tempGridSpace.GetGridSpaceNumber()) == true)
                {
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() - 7);
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() - 9);

                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() - 7);
                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() - 9);
                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber());

                }
                else if (PieceOnRightEdge(tempGridSpace.GetGridSpaceNumber()) == true && PieceOnCorner(tempGridSpace.GetGridSpaceNumber()) != true)
                {
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() - 9);
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() + 7);

                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() - 9);
                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber());
                    //tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() - 7);
                    //print(openGridSpaceses[0]);
                }else if (PieceOnCorner(tempGridSpace.GetGridSpaceNumber()) == true)
                {
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() - 7);

                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() + 7);
                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber());
                }
                else 
                {
                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() - 9);
                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() );
                    //tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() + 7);
                }
               


            int gridNumber = tempGridSpace.GetGridSpaceNumber();


                //* color gridspaces
                if (gridNumber == 0 || gridNumber == 2 ||
                    gridNumber == 4 || gridNumber == 6 ||
                    gridNumber == 9 || gridNumber == 11 ||
                    gridNumber == 13 || gridNumber == 15 ||
                    gridNumber == 16 || gridNumber == 18 ||
                    gridNumber == 20 || gridNumber == 22 ||
                    gridNumber == 25 || gridNumber == 27 ||
                    gridNumber == 29 || gridNumber == 31 ||
                    gridNumber == 32 || gridNumber == 34 ||
                    gridNumber == 36 || gridNumber == 38 ||
                    gridNumber == 41 || gridNumber == 43 ||
                    gridNumber == 45 || gridNumber == 47 ||
                    gridNumber == 48 || gridNumber == 50 ||
                    gridNumber == 52 || gridNumber == 54 ||
                    gridNumber == 57 || gridNumber == 59 ||
                    gridNumber == 61 || gridNumber == 63 )
                {
                    ColorBlock cbA = tempGridSpace.GetComponent<Button>().colors;
                    cbA.normalColor = new Color(0.4392157f, 0.4f, 0.4666667f, 1f);
                    tempGridSpace.GetComponent<Button>().colors = cbA;

                    tempGridSpace.GetComponent<Image>().color = new Color(0.8392157f, 0.4f, 0.4666667f, 1f);

                }
                

                tempGridSpace.SetGridSpaceStatus(GridSpaceStatus.EMPTY);
				tempButton.GetComponent<Button> ().onClick.AddListener (tempButton.GetComponent<GridSpace>().ButtonClick);



                gridSpaces.Add (tempButton.GetComponent<GridSpace>());
				k++;
			}
		}

		StartGame ();
	}

    // LeftEdge
    public bool PieceOnLeftEdge(int position)
    {
        if (
            position == 0 || //
            position == 8 ||
            position == 16 || //
            position == 24 ||
            position == 32 || //
            position == 40 ||
            position == 48 || //
            position == 56
        )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // RightEdge
    public bool PieceOnRightEdge(int position)
    {
        if (
            position == 7 ||
            position == 15 ||//
            position == 23 ||
            position == 31 ||//
            position == 39 ||
            position == 47 || //
            position == 55 ||
            position == 63    //
        )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // bottomEdge
    public bool PieceOnBottomEdge(int position)
    {
        if (position > 56)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PieceOnTopEdge(int position)
    {
        if (position < 7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PieceOnCorner(int position)
    {
        if (position == 7 || position == 56)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*public void MinMAx( Node node , int depth)
    {
       if (isLeafe(node) || depth <= 0)
        {
            return heuristic value of node // Heuristic tactic is to check if the node results in a win and then check on depth
        }       
       

        α = -∞    
        foreach child in node:          
            α = max(a, -minimax(child, depth - 1));

        return α
    }*/

    public void AIMoveWolf()
    {
        //AILogic

        gridSpaces[foxPos].SetGridSpaceStatus(GridSpaceStatus.EMPTY);
        if(gridSpaces[foxPos].GetAlowedGridSpaces().Count == 2 )
        {
            gridSpaces[gridSpaces[foxPos].GetAlowedGridSpaces()[0]].SetGridSpaceStatus(GridSpaceStatus.WOLF);
            print(gridSpaces[gridSpaces[foxPos].GetAlowedGridSpaces()[0]].GetGridSpaceStatus());
            print("RV");
            print(foxPos);
        }
        if (gridSpaces[foxPos].GetAlowedGridSpaces().Count == 4)
        {
            
            gridSpaces[gridSpaces[foxPos].GetAlowedGridSpaces()[3]].SetGridSpaceStatus(GridSpaceStatus.WOLF);
            print(gridSpaces[gridSpaces[foxPos].GetAlowedGridSpaces()[3]].GetGridSpaceStatus());
            print("LV");
            print(foxPos);
        }



    }
    void Update()
    {
        Player player = GameObject.Find("/Player").GetComponent<Player>();
        for (int i = 0; i < 64; i++)
        {
            // print((gridSpaces[sheep1buffer].GetGridSpaceStatus()));
            gridSpaces[i].SetSheep1OldPos((gridSpaces[sheep1buffer].GetGridSpaceStatus()));
            gridSpaces[i].SetSheep2OldPos((gridSpaces[sheep2buffer].GetGridSpaceStatus()));
            gridSpaces[i].SetSheep3OldPos((gridSpaces[sheep3buffer].GetGridSpaceStatus()));
            gridSpaces[i].SetSheep4OldPos((gridSpaces[sheep4buffer].GetGridSpaceStatus()));

            //AI
            /*if (GridSpaceStatus.SHEEP == player.playerRole && (gridSpaces[i].GetAITurn() == true))
            {
                print("AI IS IN TURN NOW!");
                if(gridSpaces[gridSpaces[foxPos].GetAlowedGridSpaces()[0]].GetGridSpaceStatus() == GridSpaceStatus.EMPTY)
                {
                    AIMoveWolf();
                   
                    //adjust new foxPosition and end AIturn
                    foxPos = gridSpaces[i].GetGridSpaceNumber();
                    if (gridSpaces[foxPos].GetAlowedGridSpaces().Count == 4)
                    {
                        AIGoToStatus = gridSpaces[gridSpaces[foxPos].GetAlowedGridSpaces()[3]].GetGridSpaceStatus();
                    }
                    gridSpaces[i].SetAITurn(false);
                }
                
            }*/

            {

                if (GridSpaceStatus.WOLF == gridSpaces[i].GetGridSpaceStatus())
                {
                    foxPos = gridSpaces[i].GetGridSpaceNumber();
                    //print("****************************************" + (foxPos));
                }
                //foxCanOnlyMoveOnOneTiles
                if (gridSpaces[foxPos].GetAlowedGridSpaces().Count == 1
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep1Pos) == true
                    || gridSpaces[foxPos].GetAlowedGridSpaces().Count == 1 
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep2Pos) == true
                    || gridSpaces[foxPos].GetAlowedGridSpaces().Count == 1
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep3Pos) == true
                    || gridSpaces[foxPos].GetAlowedGridSpaces().Count == 1
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep4Pos) == true
                    )
                {
                    print("**************************SHEEEEEEP WIN!!!!*************************");
                    ResetGridSpaces();
                }

                //foxCanOnlyMoveOnTwoTiles
                if (gridSpaces[foxPos].GetAlowedGridSpaces().Count == 2  
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep1Pos) == true 
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep2Pos) == true
                    || gridSpaces[foxPos].GetAlowedGridSpaces().Count == 2
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep3Pos) == true
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep4Pos) == true
                    || gridSpaces[foxPos].GetAlowedGridSpaces().Count == 2
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep1Pos) == true
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep3Pos) == true
                    || gridSpaces[foxPos].GetAlowedGridSpaces().Count == 2
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep2Pos) == true
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep4Pos) == true
                    || gridSpaces[foxPos].GetAlowedGridSpaces().Count == 2
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep1Pos) == true
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep4Pos) == true
                    || gridSpaces[foxPos].GetAlowedGridSpaces().Count == 2
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep2Pos) == true
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep3Pos) == true

                    )
                {
                    print("**************************SHEEEEEEP WIN!!!!*************************");
                    ResetGridSpaces();
                }

                //foxCanOnlyMoveOnfourTiles
                if (gridSpaces[foxPos].GetAlowedGridSpaces().Count == 4
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep1Pos) == true
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep2Pos) == true
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep3Pos) == true
                    && gridSpaces[foxPos].GetAlowedGridSpaces().Contains(sheep4Pos) == true
                    )
                {
                    print("**************************SHEEEEEEP WIN!!!!*************************");
                    ResetGridSpaces();
                } 

                //Sheep 1,2,3,4
                if (player.playerRole == gridSpaces[i].GetGridSpaceStatus())
                {
                    
                    if(gridSpaces[i].GetAlowedGridSpacesSheep().Count == 2)
                    {

                        //sheep1
                        if (gridSpaces[i].GetAlowedGridSpacesSheep()[0] == sheep1buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[1] == sheep1buffer)
                        {
                            if ((gridSpaces[sheep1buffer].GetGridSpaceStatus()) == GridSpaceStatus.EMPTY)
                            {
                                if (gridSpaces[i].GetGridSpaceNumber() != sheep2Pos && gridSpaces[i].GetGridSpaceNumber() != sheep3Pos && gridSpaces[i].GetGridSpaceNumber() != sheep4Pos)
                                {
                                    print("!!!!!!!!!!!SHEEpOne" + i);
                                    sheep1Pos = gridSpaces[i].GetGridSpaceNumber();

                                    sheep1buffer = gridSpaces[i].GetGridSpaceNumber();
                                }
                            }    
                        }
                        
                        // sheep2 
                        if (gridSpaces[i].GetAlowedGridSpacesSheep()[0] == sheep2buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[1] == sheep2buffer)
                        {
                            if ((gridSpaces[sheep2buffer].GetGridSpaceStatus()) == GridSpaceStatus.EMPTY)
                            {
                                if (gridSpaces[i].GetGridSpaceNumber() != sheep1Pos && gridSpaces[i].GetGridSpaceNumber() != sheep3Pos && gridSpaces[i].GetGridSpaceNumber() != sheep4Pos)
                                {
                                    print("!!!!!!!!!!!SheepTwo" + i);
                                    sheep2Pos = gridSpaces[i].GetGridSpaceNumber();

                                    sheep2buffer = gridSpaces[i].GetGridSpaceNumber();
                                }
                            }
                        }

                        //sheep3
                        if (gridSpaces[i].GetAlowedGridSpacesSheep()[0] == sheep3buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[1] == sheep3buffer)
                        {
                            if ((gridSpaces[sheep3buffer].GetGridSpaceStatus()) == GridSpaceStatus.EMPTY)
                            {
                                if (gridSpaces[i].GetGridSpaceNumber() != sheep1Pos && gridSpaces[i].GetGridSpaceNumber() != sheep2Pos && gridSpaces[i].GetGridSpaceNumber() != sheep4Pos)
                                {
                                    print("!!!!!!!!!!!SheepThree" + i);
                                    sheep3Pos = gridSpaces[i].GetGridSpaceNumber();

                                    sheep3buffer = gridSpaces[i].GetGridSpaceNumber();
                                }
                            }
                        }

                        //sheep4
                        if (gridSpaces[i].GetAlowedGridSpacesSheep()[0] == sheep4buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[1] == sheep4buffer)
                        {
                            if ((gridSpaces[sheep4buffer].GetGridSpaceStatus()) == GridSpaceStatus.EMPTY)
                            {
                                if (gridSpaces[i].GetGridSpaceNumber() != sheep1Pos && gridSpaces[i].GetGridSpaceNumber() != sheep2Pos && gridSpaces[i].GetGridSpaceNumber() != sheep3Pos)
                                {
                                    print("!!!!!!!!!!!SheepFour" + i);
                                    sheep4Pos = gridSpaces[i].GetGridSpaceNumber();

                                    sheep4buffer = gridSpaces[i].GetGridSpaceNumber();
                                }
                            }
                        }
                    }
                    if (gridSpaces[i].GetAlowedGridSpacesSheep().Count == 3)
                    {

                        //sheep1
                        if (gridSpaces[i].GetAlowedGridSpacesSheep()[0] == sheep1buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[1] == sheep1buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[2] == sheep1buffer)
                        {
                                if ((gridSpaces[sheep1buffer].GetGridSpaceStatus()) == GridSpaceStatus.EMPTY)
                            {
                                if (gridSpaces[i].GetGridSpaceNumber() != sheep2Pos && gridSpaces[i].GetGridSpaceNumber() != sheep3Pos && gridSpaces[i].GetGridSpaceNumber() != sheep4Pos)
                                {
                                    print("**SheepOne**" + i);
                                    sheep1Pos = gridSpaces[i].GetGridSpaceNumber();
                                    sheep1buffer = gridSpaces[i].GetGridSpaceNumber();
                                }
                               
                            }
                        }
                        
                        // sheep 2 
                        if (gridSpaces[i].GetAlowedGridSpacesSheep()[0] == sheep2buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[1] == sheep2buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[2] == sheep2buffer)
                        {
                           
                            if ((gridSpaces[sheep2buffer].GetGridSpaceStatus()) == GridSpaceStatus.EMPTY)
                            {
                                if (gridSpaces[i].GetGridSpaceNumber() != sheep1Pos && gridSpaces[i].GetGridSpaceNumber() != sheep3Pos && gridSpaces[i].GetGridSpaceNumber() != sheep4Pos)
                                {
                                    print("SheepTwo" + i);
                                    sheep2Pos = gridSpaces[i].GetGridSpaceNumber();

                                    sheep2buffer = gridSpaces[i].GetGridSpaceNumber();
                                }
                            }
                               
                        }

                        //sheep3
                        if (gridSpaces[i].GetAlowedGridSpacesSheep()[0] == sheep3buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[1] == sheep3buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[2] == sheep3buffer)
                        {

                            if ((gridSpaces[sheep3buffer].GetGridSpaceStatus()) == GridSpaceStatus.EMPTY)
                            {
                                if (gridSpaces[i].GetGridSpaceNumber() != sheep1Pos && gridSpaces[i].GetGridSpaceNumber() != sheep2Pos && gridSpaces[i].GetGridSpaceNumber() != sheep4Pos)
                                {
                                    print("SheepThree" + i);

                                    sheep3Pos = gridSpaces[i].GetGridSpaceNumber();

                                    sheep3buffer = gridSpaces[i].GetGridSpaceNumber();
                                }
                            }

                        }

                        //sheep4
                        if (gridSpaces[i].GetAlowedGridSpacesSheep()[0] == sheep4buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[1] == sheep4buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[2] == sheep4buffer)
                        {

                            if ((gridSpaces[sheep4buffer].GetGridSpaceStatus()) == GridSpaceStatus.EMPTY)
                            {
                                if (gridSpaces[i].GetGridSpaceNumber() != sheep1Pos && gridSpaces[i].GetGridSpaceNumber() != sheep2Pos && gridSpaces[i].GetGridSpaceNumber() != sheep3Pos)
                                {
                                    print("SheepFour" + (gridSpaces[sheep4buffer].GetGridSpaceStatus()));
                                    sheep4Pos = gridSpaces[i].GetGridSpaceNumber();

                                    sheep4buffer = gridSpaces[i].GetGridSpaceNumber();
                                }
                            }

                        }
                    }



                }
                
                if (
                    
                    foxPos == 0 ||
                    foxPos == 1 ||
                    foxPos == 2 ||
                    foxPos == 4 ||
                    foxPos == 5 ||
                    foxPos == 6 ||
                    foxPos == 7 
                    )
                {
                    ResetGridSpaces();
                }
            }
            
            gridSpaces[i].SetFoxPosition(foxPos);
            
            if(player.playerRole == GridSpaceStatus.SHEEP && sheep1Pos != sheep2Pos)
            {
                gridSpaces[i].SetSheep1Position(sheep1Pos);
                gridSpaces[i].SetSheep2Position(sheep2Pos);
                gridSpaces[i].SetSheep3Position(sheep3Pos);
                gridSpaces[i].SetSheep4Position(sheep4Pos);
            }
        }
       
    }

    void StartGame() {
		ResetGridSpaces ();
        
    }

	void ResetGridSpaces() {
		for(int i = 0; i < 64; i++) {
			gridSpaces [i].SetGridSpaceStatus (GridSpaceStatus.EMPTY);


            gridSpaces[i].SetSheep1OldPos(GridSpaceStatus.SHEEP);
            gridSpaces[i].SetSheep2OldPos(GridSpaceStatus.SHEEP);
            gridSpaces[i].SetSheep3OldPos(GridSpaceStatus.SHEEP);
            gridSpaces[i].SetSheep4OldPos(GridSpaceStatus.SHEEP);
        }


        // 1, 3, 5 and 7 are sheep
        // 60 is a wolf

        sheep1Pos = 1;
        sheep2Pos = 3;
        sheep3Pos = 5;
        sheep4Pos = 7;
        sheep1buffer = 1;
        sheep2buffer = 3;
        sheep3buffer = 5;
        sheep4buffer = 7;
        gridSpaces[1].SetGridSpaceStatus(GridSpaceStatus.SHEEP);
		gridSpaces[3].SetGridSpaceStatus(GridSpaceStatus.SHEEP);
		gridSpaces[5].SetGridSpaceStatus(GridSpaceStatus.SHEEP);
		gridSpaces[7].SetGridSpaceStatus(GridSpaceStatus.SHEEP);
		gridSpaces[60].SetGridSpaceStatus(GridSpaceStatus.WOLF);
	}

}
