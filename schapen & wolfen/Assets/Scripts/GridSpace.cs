using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class GridSpace : MonoBehaviour {

    public Sprite emptySprite;
    public Sprite wolfSprite;
    public Sprite sheepSprite;

    public int foxPosition = 0;

    public int currentSheep1Position = 1;
    public int currentSheep2Position = 3;
    public int currentSheep3Position = 5;
    public int currentSheep4Position = 7;


    //if currentsheeppos works, replace with sheeppos.
    public int sheep1Position = 1;
    public int sheep2Position = 3;
    public int sheep3Position = 5;
    public int sheep4Position = 7;
    public GridSpaceStatus sheep1OldPos = GridSpaceStatus.SHEEP;
    public GridSpaceStatus sheep2OldPos = GridSpaceStatus.SHEEP;
    public GridSpaceStatus sheep3OldPos = GridSpaceStatus.SHEEP;
    public GridSpaceStatus sheep4OldPos = GridSpaceStatus.SHEEP;
    public bool AITurn = false;

    public bool sheep1 = false;
    public bool sheep2 = false;
    public bool sheep3 = false;
    public bool sheep4 = false; 

    public bool clearSignalAlowedGspace = true;
    public bool signalDroped = false;
    public bool signalPicked = false;

    private IList<int> alowedGridSpaceses = new List<int>();
    private IList<int> alowedGridSpacesSheep = new List<int>();
    private IList<int> mmAlowedGridSpacesSheep = new List<int>();

    public IList<int> currentAlowedGridSpacesSheep = new List<int>();


    public bool isAlowedGridSpace = false;

    public int lastpickedUp = 0;

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

    public IList<int> GetMmAlowedGridSpacesSheep()
    {
        return this.mmAlowedGridSpacesSheep;
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
        }
    }

    public void SetAlowedGridSpacesSheep(int AlowedgridspaceSheep)
    {
        if (this.alowedGridSpacesSheep.Contains(AlowedgridspaceSheep) == false)
        {
            this.alowedGridSpacesSheep.Add(AlowedgridspaceSheep);
        }
    }

    public void SetMmAlowedGridSpacesSheep(int mmAlowedGridSpacesSheep)
    {
        if (this.mmAlowedGridSpacesSheep.Contains(mmAlowedGridSpacesSheep) == false)
        {
            this.mmAlowedGridSpacesSheep.Add(mmAlowedGridSpacesSheep);
            //this.test2 = mmAlowedGridSpacesSheep[0];
        }
    }


    public void SetFoxPosition(int FoxPosition)
    {
        this.foxPosition = FoxPosition;
    }

    public int getSheep1Position(){
        return sheep1Position;
    }

    public void SetSheep1Position(int Sheep1Position)
    {
        this.sheep1Position = Sheep1Position;
    }

    public int getSheep2Position(){
        return sheep2Position;
    }

    public void SetSheep2Position(int Sheep2Position)
    {
        this.sheep2Position = Sheep2Position;
    }

    public int getSheep3Position(){
        return sheep3Position;
    }

    public void SetSheep3Position(int Sheep3Position)
    {
        this.sheep3Position = Sheep3Position;
    }

    public int getSheep4Position(){
        return sheep4Position;
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
        clearSignalAlowedGspace = false;
        

        CursorStatus cursorStatus = cursorController.GetCursorStatus();
        // The playerRole is a GridSpaceStatus to easily check if the clicked GridSpace can be moved by the player.
        if (cursorStatus == CursorStatus.EMPTY && player.playerRole == this.gridSpaceStatus && !AITurn) {
            // Pick up current piece
            //print("---"+ GetGridSpaceStatus());
           
            cursorController.SetCursorStatus(CursorStatus.OCCUPIED); 
            this.SetGridSpaceStatus (GridSpaceStatus.EMPTY);
            lastpickedUp = this.gridSpaceNumber;
            print("picked up"); 
            if(signalDroped == true){
                signalDroped = false;
                // signalpicked = true;
            }

        }
        //print sheep positions
        /*print("sheep1Position: " + sheep1Position);
        print("sheep2Position: " + sheep2Position);
        print("sheep3Position: " + sheep3Position);
        print("sheep4Position: " + sheep4Position);*/

      


        /*print(alowedGridSpaceses.Count);
        print("0--0");*/
        for (int i = 0; i < alowedGridSpaceses.Count; i++)
        {
            // print("alowedGridSpaceses[" + i + "]: " + alowedGridSpaceses[i]);
        }

        for (int i = 0; i < alowedGridSpacesSheep.Count; i++)
        {
            // print("alowedGridSpacesSheep[" + i + "]: " + alowedGridSpacesSheep[i]);
        }

         for (int i = 0; i < mmAlowedGridSpacesSheep.Count; i++)
        {
            // print("mmAlowedGridSpacesSheep[" + i + "]: " + mmAlowedGridSpacesSheep[i]);
        }



            
        
        for (int i = 0; i < alowedGridSpaceses.Count; i++)
        {
            if (cursorStatus == CursorStatus.OCCUPIED) {
                if (this.gridSpaceStatus == GridSpaceStatus.EMPTY && 
                    alowedGridSpaceses[i] == foxPosition && player.playerRole == GridSpaceStatus.WOLF
                    ) {
                    // Drop Piece if allowed
                    // print("Count = " + i);
                    // print("FoxPOsition = " + foxPosition);
                    // print(this.GetGridSpaceNumber());

                    cursorController.SetCursorStatus(CursorStatus.EMPTY);
                   
                    this.SetGridSpaceStatus (player.playerRole);
                    if(player.playerRole == GridSpaceStatus.WOLF){
                        AITurn = true;
                    }

                    
                    print("Case 0");

                }
            }
        }


        for (int i = 0; i < currentAlowedGridSpacesSheep.Count; i++)
        {
            //print(alowedGridSpacesSheep[i]);
            if (cursorStatus == CursorStatus.OCCUPIED)
            {

                //sheep1
                if (this.gridSpaceStatus == GridSpaceStatus.EMPTY && sheep1 == true)
                {
                    if (sheep1OldPos != GridSpaceStatus.SHEEP)
                    {
                        
                        if(currentAlowedGridSpacesSheep[i] == this.gridSpaceNumber){
                            cursorController.SetCursorStatus(CursorStatus.EMPTY);
                            this.SetGridSpaceStatus(player.playerRole);
                            if(player.playerRole == GridSpaceStatus.SHEEP){
                                AITurn = true;
                            }
                            
                            clearSignalAlowedGspace = true;
                            signalDroped = true;
                            sheep1Position = this.gridSpaceNumber;
                            // print("SHEEEEEEP POSSS1 !" + sheep1Position);
                            // print("SHEEEEEEP POSSS2 !" + sheep2Position);
                            // print("SHEEEEEEP POSSS3 !" + sheep3Position);
                            // print("SHEEEEEEP POSSS4 !" + sheep4Position);
                        }
                    
                    }
                }

                // sheep 2
                if (this.gridSpaceStatus == GridSpaceStatus.EMPTY && sheep2 == true )
                {
                    if (sheep2OldPos != GridSpaceStatus.SHEEP)
                    {
                        if(currentAlowedGridSpacesSheep[i] == this.gridSpaceNumber){
                            cursorController.SetCursorStatus(CursorStatus.EMPTY);
                            this.SetGridSpaceStatus(player.playerRole);
                            if(player.playerRole == GridSpaceStatus.SHEEP){
                                AITurn = true;
                            }
                            clearSignalAlowedGspace = true;
                            signalDroped = true;
                            sheep2Position = this.gridSpaceNumber;
                            // print("SHEEEEEEP POSSS1 !" + sheep1Position);
                            // print("SHEEEEEEP POSSS2 !" + sheep2Position);
                            // print("SHEEEEEEP POSSS3 !" + sheep3Position);
                            // print("SHEEEEEEP POSSS4 !" + sheep4Position);
                        }
                    }
                }

                //sheep 3
                if (this.gridSpaceStatus == GridSpaceStatus.EMPTY && sheep3 == true)
                {
                    if (sheep3OldPos != GridSpaceStatus.SHEEP)
                    {
                        if(currentAlowedGridSpacesSheep[i] == this.gridSpaceNumber){
                            cursorController.SetCursorStatus(CursorStatus.EMPTY);
                            this.SetGridSpaceStatus(player.playerRole);
                            if(player.playerRole == GridSpaceStatus.SHEEP){
                                AITurn = true;
                            }
                            clearSignalAlowedGspace = true;
                            signalDroped = true;
                            sheep3Position = this.gridSpaceNumber;
                            // print("SHEEEEEEP POSSS1 !" + sheep1Position);
                            // print("SHEEEEEEP POSSS2 !" + sheep2Position);
                            // print("SHEEEEEEP POSSS3 !" + sheep3Position);
                            // print("SHEEEEEEP POSSS4 !" + sheep4Position);
                        }
                    }
                }
                //sheep4
                if (this.gridSpaceStatus == GridSpaceStatus.EMPTY && sheep4 == true)
                { 
                    if (sheep4OldPos != GridSpaceStatus.SHEEP)
                    {
                       if(currentAlowedGridSpacesSheep[i] == this.gridSpaceNumber){
                            cursorController.SetCursorStatus(CursorStatus.EMPTY);
                            this.SetGridSpaceStatus(player.playerRole);
                            if(player.playerRole == GridSpaceStatus.SHEEP){
                                AITurn = true;
                            }                            
                            clearSignalAlowedGspace = true;
                            signalDroped = true;
                            sheep4Position = this.gridSpaceNumber;
                            // print("SHEEEEEEP POSSS1 !" + sheep1Position);
                            // print("SHEEEEEEP POSSS2 !" + sheep2Position);
                            // print("SHEEEEEEP POSSS3 !" + sheep3Position);
                            // print("SHEEEEEEP POSSS4 !" + sheep4Position);
                        }
                    }
                }
            }
        }
    }

}
