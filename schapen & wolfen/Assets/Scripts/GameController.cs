using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour {

    public GameObject gridArea;
    public GameObject gridSpaceButton;
    private List<GridSpace> gridSpaces = new List<GridSpace>();
    private int foxPos = 60 ;
    private int sheep1Pos = 1;
    private int sheep2Pos = 3;
    private int sheep3Pos = 5;
    private int sheep4Pos = 7;
    private int sheep1buffer = 1;
    private int sheep2buffer = 3;
    private int sheep3buffer = 5;
    private int sheep4buffer = 7;
    private int foxPosClone;
    private List<int> sheepList = new List<int>(){ 1, 3, 5, 7 };
    private List<int> sheepListClone = new List<int>(){ 1, 3, 5, 7 };
    private int sheepturnBuffer = 0;
    private int lastclickedBuffer = -1;
    private int lastDroppedBuffer = -1;
    private List<int> lastclickedBufferLog = new List<int>(){};


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
                tempButton.transform.GetComponentInChildren<Text>().color = Color.red;
                // Position gridNumbers on button
                tempButton.transform.GetComponentInChildren<Text>().transform.position = new Vector3(tempButton.transform.GetComponentInChildren<Text>().transform.position.x + 4   ,tempButton.transform.GetComponentInChildren<Text>().transform.position.y -4,tempButton.transform.GetComponentInChildren<Text>().transform.position.z);
         

                GridSpace tempGridSpace = tempButton.GetComponent<GridSpace> ();
                tempGridSpace.SetGridSpaceNumber(k);
                //tempGridSpace.SetAITurn(true);
                Player player = GameObject.Find("/Player").GetComponent<Player>();
                if(GridSpaceStatus.SHEEP == player.playerRole){
                    tempGridSpace.SetAITurn(true);
                }
                

                tempGridSpace.SetSheep1Position(1);
                tempGridSpace.SetSheep2Position(3);
                tempGridSpace.SetSheep3Position(5);
                tempGridSpace.SetSheep4Position(7);
                if (PieceOnLeftEdge(tempGridSpace.GetGridSpaceNumber()) != true && PieceOnRightEdge(tempGridSpace.GetGridSpaceNumber()) != true && PieceOnBottomEdge(tempGridSpace.GetGridSpaceNumber()) != true && PieceOnCorner(tempGridSpace.GetGridSpaceNumber()) != true && PieceOnTopEdge(tempGridSpace.GetGridSpaceNumber()) != true)

                {
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() + 7);
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() + 9);
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() - 7);
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() - 9);


                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() - 7);
                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() - 9);
                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber());


                    tempGridSpace.SetMmAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() + 7);
                    tempGridSpace.SetMmAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() + 9);
                    //tempGridSpace.SetMmAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber())

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


                    tempGridSpace.SetMmAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() + 9);
                    //print(openGridSpaceses[0]);
                }
                else if (PieceOnBottomEdge(tempGridSpace.GetGridSpaceNumber()) == true)
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

                    tempGridSpace.SetMmAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() + 7);
                    //print(openGridSpaceses[0]);
                } else if (PieceOnCorner(tempGridSpace.GetGridSpaceNumber()) == true && PieceOnTopEdge(tempGridSpace.GetGridSpaceNumber()))
                {
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() - 7);

                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() + 7);
                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber());

                    tempGridSpace.SetMmAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() + 9);
                }
                else if (PieceOnCorner(tempGridSpace.GetGridSpaceNumber()) == true && PieceOnBottomEdge(tempGridSpace.GetGridSpaceNumber()))
                {
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() - 7);

                    //tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() + 7);
                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber());
                }
               else if (PieceOnTopEdge(tempGridSpace.GetGridSpaceNumber()) == true && PieceOnCorner(tempGridSpace.GetGridSpaceNumber()) != true)
                {
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() + 7);
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() + 9);

                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber());

                    tempGridSpace.SetMmAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() + 7);
                    tempGridSpace.SetMmAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() + 9);

                }else if (tempGridSpace.GetGridSpaceNumber() == 56)
                {
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() - 7);
                }
                else if (tempGridSpace.GetGridSpaceNumber() == 7)
                {
                    tempGridSpace.SetAlowedGridSpaces(tempGridSpace.GetGridSpaceNumber() + 7);

                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber());
                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() + 7);


                    tempGridSpace.SetMmAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() + 7);
                }

                else
                {
                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber() - 9);
                    tempGridSpace.SetAlowedGridSpacesSheep(tempGridSpace.GetGridSpaceNumber());
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

    public  bool AreListsEqual(IList<int> list1, IList<int> list2)
    {
        var areListsEqual = true;

        if (list1.Count != list2.Count)
            return false;

        for (var i = 0; i < list1.Count; i++)
        {
            if (list2[i] != list1[i])
            {
                areListsEqual = false;
            }
        }

        return areListsEqual;
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

    public List<int> nonSheepAlowedGridspace(int pos){
        List<int> totalNoSheep = new List<int>(){};
        for (int i = 0; i < gridSpaces[pos].GetAlowedGridSpaces().Count; i++)
            {
                if(gridSpaces[gridSpaces[pos].GetAlowedGridSpaces()[i]].GetGridSpaceStatus() != GridSpaceStatus.SHEEP){
                    totalNoSheep.Add(gridSpaces[pos].GetAlowedGridSpaces()[i]);
                }
            }
        return totalNoSheep;
    } 

    public TreeNode<int> createTreeWolf(TreeNode<int> root, int depth, bool maximizingPlayer)
    {
        

        ArrayList row0 = new ArrayList();
        row0.Add(56);
        row0.Add(58);
        row0.Add(60);
        row0.Add(62);

        ArrayList row1 = new ArrayList();
        row1.Add(49);
        row1.Add(51);
        row1.Add(53);
        row1.Add(55);

        ArrayList row2 = new ArrayList();
        row2.Add(40);
        row2.Add(42);
        row2.Add(44);
        row2.Add(46);

        ArrayList row3 = new ArrayList();
        row3.Add(33);
        row3.Add(35);
        row3.Add(37);
        row3.Add(39);

        ArrayList row4 = new ArrayList();
        row4.Add(24);
        row4.Add(26);
        row4.Add(28);
        row4.Add(30);

        ArrayList row5 = new ArrayList();
        row5.Add(17);
        row5.Add(19);
        row5.Add(21);
        row5.Add(23);

        ArrayList row6 = new ArrayList();
        row6.Add(8);
        row6.Add(10);
        row6.Add(12);
        row6.Add(14);

        ArrayList row7 = new ArrayList();
        row7.Add(1);
        row7.Add(3);
        row7.Add(5);
        row7.Add(7);


        bool isTrapped = true;
        int counter = 0;
        TreeNode<int> returnValue = new TreeNode<int>(int.MinValue);


        
        
        // print("Debug2: " + sheepList[1]);
        // print("Debug3: " + sheepList[2]);
        // print("Debug4: " + sheepList[3]);
     
        //print("@@" + foxPosClone);
        if (depth == 1)
        {
            // print("sheepListCLone [0]" + sheepListClone[0]);
            // print("sheepListCLone [1]" + sheepListClone[1]);
            // print("sheepListCLone [2]" + sheepListClone[2]);
            // print("sheepListCLone [3]" + sheepListClone[3]);
            root.maximizingNode = true;
            //print("!!!!!!!!!!!!!!!!!!! DEPTH == 1 !!!!!!!!!!!!!!!!!!" + foxPosClone);
           // print("FOXPOSCLONE"+foxPosClone);
            if( foxPos == 1 ||
                foxPos == 3 ||
                foxPos == 5 ||
                foxPos == 7)
            {
                ResetGridSpaces();
            }

            for (int i = 0; i < gridSpaces[foxPosClone].GetAlowedGridSpaces().Count; i++)
            {
                
                
                

                if (row0.Contains(gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]))
                {

                    if( row0.Contains(sheepList.Min()) && gridSpaces[gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]].GetGridSpaceStatus() == GridSpaceStatus.EMPTY){
                        root.AddChild(1001);
                        // root.setData(1);
                        root._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                    
                        root[i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];

                        root[i].maximizingNode = false; 
                    }else{
                        root.AddChild(1);
                        // root.setData(1);
                        root._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                    
                        root[i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];

                        root[i].maximizingNode = false;
                    }

                    
                }
                if (row1.Contains(gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]))
                {

                    if( row1.Contains(sheepList.Min()) && gridSpaces[gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]].GetGridSpaceStatus() == GridSpaceStatus.EMPTY){
                        root.AddChild(1002);
                        // root.setData(2);
                        root._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i].maximizingNode = false;
                    }
                    else{
                        root.AddChild(2);
                        // root.setData(2);
                        root._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i].maximizingNode = false;
                    }
                    
                }
                if (row2.Contains(gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]))
                {
                    if( row2.Contains(sheepList.Min()) && gridSpaces[gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]].GetGridSpaceStatus() == GridSpaceStatus.EMPTY){
                        root.AddChild(1003);
                        // root.setData(2);
                        root._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i].maximizingNode = false;
                    }
                    else{
                        root.AddChild(3);
                        // root.setData(2);
                        root._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i].maximizingNode = false;
                    }
                }
                if (row3.Contains(gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]))
                {
                    if( row3.Contains(sheepList.Min()) && gridSpaces[gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]].GetGridSpaceStatus() == GridSpaceStatus.EMPTY){
                        root.AddChild(1004);
                        // root.setData(2);
                        root._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i].maximizingNode = false;
                    }
                    else{
                        root.AddChild(4);
                        // root.setData(2);
                        root._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i].maximizingNode = false;
                    }
                }
                if (row4.Contains(gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]))
                {
                    if( row4.Contains(sheepList.Min()) && gridSpaces[gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]].GetGridSpaceStatus() == GridSpaceStatus.EMPTY){
                        root.AddChild(1005);
                        // root.setData(2);
                        root._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i].maximizingNode = false;
                    }
                    else{
                        root.AddChild(5);
                        // root.setData(2);
                        root._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i].maximizingNode = false;
                    }

                }
                if (row5.Contains(gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]))
                {
                   if( row5.Contains(sheepList.Min()) && gridSpaces[gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]].GetGridSpaceStatus() == GridSpaceStatus.EMPTY){
                        root.AddChild(1006);
                        // root.setData(2);
                        root._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i].maximizingNode = false;
                    }
                    else{
                        root.AddChild(6);
                        // root.setData(2);
                        root._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i].maximizingNode = false;
                    }


                }
                if (row6.Contains(gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]))
                {
                    if( row6.Contains(sheepList.Min()) && gridSpaces[gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]].GetGridSpaceStatus() == GridSpaceStatus.EMPTY){
                        root.AddChild(1007);
                        // root.setData(2);
                        root._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i].maximizingNode = false;
                    }
                    else{
                        root.AddChild(7);
                        // root.setData(2);
                        root._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                        root[i].maximizingNode = false;
                    }

                }


                if (row7.Contains(gridSpaces[foxPosClone].GetAlowedGridSpaces()[i])  )
                {
                    
                    //print("DEBUg" + gridSpaces[foxPosClone].GetAlowedGridSpaces().Count);
                    root.AddChild(int.MaxValue);
                    //root.setData(int.MaxValue);
                    root._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                    root[i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                    root[i].maximizingNode = false;

                }
               
                // print("***************************" + nonSheepAlowedGridspace(root[0]._gridSpaceValue).Count);

                //print(largestHeuristicValue);
                //print("!!!!!!!!!!!!!!!!!!!!!!!!!!!--------------------!!!!!!!!!!!!!!!!!!!!!!!!!" + root.Data);
            }

            //If sheep is holding a straight line
            if(row7.Contains(sheepListClone[0]) && row7.Contains(sheepListClone[1]) && row7.Contains(sheepListClone[2])&& row7.Contains(sheepListClone[3]) 
                || row6.Contains(sheepListClone[0]) && row6.Contains(sheepListClone[1]) && row6.Contains(sheepListClone[2])&& row6.Contains(sheepListClone[3])
                || row5.Contains(sheepListClone[0]) && row5.Contains(sheepListClone[1]) && row5.Contains(sheepListClone[2])&& row5.Contains(sheepListClone[3])
                || row4.Contains(sheepListClone[0]) && row4.Contains(sheepListClone[1]) && row4.Contains(sheepListClone[2])&& row4.Contains(sheepListClone[3])
                || row3.Contains(sheepListClone[0]) && row3.Contains(sheepListClone[1]) && row3.Contains(sheepListClone[2])&& row3.Contains(sheepListClone[3])
                || row2.Contains(sheepListClone[0]) && row2.Contains(sheepListClone[1]) && row2.Contains(sheepListClone[2])&& row2.Contains(sheepListClone[3])
                || row1.Contains(sheepListClone[0]) && row1.Contains(sheepListClone[1]) && row1.Contains(sheepListClone[2])&& row1.Contains(sheepListClone[3])
                || row0.Contains(sheepListClone[0]) && row0.Contains(sheepListClone[1]) && row0.Contains(sheepListClone[2])&& row0.Contains(sheepListClone[3]))
            {
                // print("----> straightLine <----");
                for (int sheepNr = 0; sheepNr < sheepListClone.Count; sheepNr++)
                {
                    //Als schaap in een hoek zit; geef hem de hoogste score;
                    if(gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep().Count == 1 && sheepListClone.Contains(gridSpaces[gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0]].GetGridSpaceNumber()) == false){
                        // print("corner! -----------------");
                        for (int rootChildren = 0; rootChildren < root.Count; rootChildren++)
                        {
                            if(root[rootChildren]._gridSpaceValue != gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0] ){

                                if(nonSheepAlowedGridspace(root[rootChildren]._gridSpaceValue).Count == 1 && nonSheepAlowedGridspace(root[rootChildren]._gridSpaceValue)[0] == gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0]){
                                    root[rootChildren].AddChild(int.MinValue);
                                    root[rootChildren][root[rootChildren].Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0];
                                    root[rootChildren][root[rootChildren].Count-1].maximizingNode = true;
                                    root[rootChildren][root[rootChildren].Count-1].sheepNumbr = sheepNr;


                                    // print("--Debug--");
                                }
                                else{
                                    root[rootChildren].AddChild(-10);
                                    root[rootChildren][root[rootChildren].Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0];
                                    root[rootChildren][root[rootChildren].Count-1].maximizingNode = true;
                                    root[rootChildren][root[rootChildren].Count-1].sheepNumbr = sheepNr;

                                    // print("--Debug--");

                                    
                                }
                                
                               
                            }
                        }
                    }else{

                        for (int rootChildren = 0; rootChildren < root.Count; rootChildren++)
                        {
                            for(int alowedgSpacesSheep = 0; alowedgSpacesSheep < gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep().Count; alowedgSpacesSheep++ ){
                                if(root[rootChildren]._gridSpaceValue != gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep] ){

                                    if(nonSheepAlowedGridspace(root[rootChildren]._gridSpaceValue).Count == 1 && nonSheepAlowedGridspace(root[rootChildren]._gridSpaceValue)[0] == gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep]){
                                        root[rootChildren].AddChild(int.MinValue);
                                        root[rootChildren][root[rootChildren].Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep];
                                        root[rootChildren][root[rootChildren].Count-1].maximizingNode = true;
                                        root[rootChildren][root[rootChildren].Count-1].sheepNumbr = sheepNr;

                                        // print("--Debug--");
                                    }
                                    else{
                                        root[rootChildren].AddChild(-1);
                                        root[rootChildren][root[rootChildren].Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep];
                                        root[rootChildren][root[rootChildren].Count-1].maximizingNode = true;
                                        root[rootChildren][root[rootChildren].Count-1].sheepNumbr = sheepNr;

                                        // print("--Debug--");
                                    }
                                    
                                }
                                
                                //print(gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep]);
                            }
                        }
                    }

                }
            }else{
                for (int sheepNr = 0; sheepNr < sheepListClone.Count; sheepNr++){       
                    if(gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep().Count == 2){
                        
                        for (int rootChildren = 0; rootChildren < root.Count; rootChildren++){
                            int totalSheepInAlowedgSPace = 0;
                            for(int alowedgSpacesSheep = 0; alowedgSpacesSheep < gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep().Count; alowedgSpacesSheep++ ){
                                //Juist als er een is die niet empty is dan is dat de stap die schaap het meest punten hoort op te leveren;
                                if( sheepListClone.Contains(gridSpaces[gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep]].GetGridSpaceNumber()) == true){
                                    totalSheepInAlowedgSPace +=1;
                                    
                                }
                                if(totalSheepInAlowedgSPace == 1 && alowedgSpacesSheep == 1){
                                    if(root[rootChildren]._gridSpaceValue != gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep-1] ){
                                        if(sheepListClone.Contains(gridSpaces[gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep-1]].GetGridSpaceNumber()) == false){
                                            if(nonSheepAlowedGridspace(root[rootChildren]._gridSpaceValue).Count == 1 && nonSheepAlowedGridspace(root[rootChildren]._gridSpaceValue)[0] == gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep-1]){
                                                root[rootChildren].AddChild(int.MinValue);
                                                root[rootChildren][root[rootChildren].Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep-1];
                                                root[rootChildren][root[rootChildren].Count-1].maximizingNode = true;
                                                root[rootChildren][root[rootChildren].Count-1].sheepNumbr = sheepNr;

                                                // print("--Debug--");
                                            }
                                            else{
                                                root[rootChildren].AddChild(-10);
                                                root[rootChildren][root[rootChildren].Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep-1];
                                                root[rootChildren][root[rootChildren].Count-1].maximizingNode = true;
                                                root[rootChildren][root[rootChildren].Count-1].sheepNumbr = sheepNr;

                                                // print("--Debug--");
                                            }

                                            
                                        }
                                    }
                                }
                                if(totalSheepInAlowedgSPace == 1 && alowedgSpacesSheep == 0){
                                    if(root[rootChildren]._gridSpaceValue != gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep+1] ){
                                        if(sheepListClone.Contains(gridSpaces[gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep+1]].GetGridSpaceNumber()) == false){
                                            if(nonSheepAlowedGridspace(root[rootChildren]._gridSpaceValue).Count == 1 && nonSheepAlowedGridspace(root[rootChildren]._gridSpaceValue)[0] == gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep+1]){
                                                root[rootChildren].AddChild(int.MinValue);
                                                root[rootChildren][root[rootChildren].Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep+1];
                                                root[rootChildren][root[rootChildren].Count-1].maximizingNode = true;
                                                root[rootChildren][root[rootChildren].Count-1].sheepNumbr = sheepNr;

                                                // print("--Debug--");
                                            }
                                            else{
                                                root[rootChildren].AddChild(-10);
                                                root[rootChildren][root[rootChildren].Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep+1];
                                                root[rootChildren][root[rootChildren].Count-1].maximizingNode = true;
                                                root[rootChildren][root[rootChildren].Count-1].sheepNumbr = sheepNr;

                                                // print("--Debug--");
                                            }
                                            
                                        }   
                                    }
                                }
                            }
                            if(sheepListClone.Contains(gridSpaces[gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0]].GetGridSpaceNumber()) == false && sheepListClone.Contains(gridSpaces[gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[1]].GetGridSpaceNumber()) == false){
                                if(root[rootChildren]._gridSpaceValue != gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0] ){
                                    if(nonSheepAlowedGridspace(root[rootChildren]._gridSpaceValue).Count == 1 && nonSheepAlowedGridspace(root[rootChildren]._gridSpaceValue)[0] == gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0]){
                                        root[rootChildren].AddChild(int.MinValue);
                                        root[rootChildren][root[rootChildren].Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0];
                                        root[rootChildren][root[rootChildren].Count-1].maximizingNode = true;
                                        root[rootChildren][root[rootChildren].Count-1].sheepNumbr = sheepNr;

                                        // print("--Debug--");
                                    }

                                    else{

                                        root[rootChildren].AddChild(-1);
                                        root[rootChildren][root[rootChildren].Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0];
                                        root[rootChildren][root[rootChildren].Count-1].maximizingNode = true;
                                        root[rootChildren][root[rootChildren].Count-1].sheepNumbr = sheepNr;
                                        


                                        // print("--Debug--");
                                    }
                                    
                                }
                                if(root[rootChildren]._gridSpaceValue != gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[1] ){
                                    if(nonSheepAlowedGridspace(root[rootChildren]._gridSpaceValue).Count == 1 && nonSheepAlowedGridspace(root[rootChildren]._gridSpaceValue)[0] == gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[1]){
                                        root[rootChildren].AddChild(int.MinValue);
                                        root[rootChildren][root[rootChildren].Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[1];
                                        root[rootChildren][root[rootChildren].Count-1].maximizingNode = true;
                                        root[rootChildren][root[rootChildren].Count-1].sheepNumbr = sheepNr;

                                        // print("--Debug--");
                                    }
                                    else{
                                        root[rootChildren].AddChild(-1);
                                        root[rootChildren][root[rootChildren].Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[1];
                                        root[rootChildren][root[rootChildren].Count-1].maximizingNode = true;
                                        root[rootChildren][root[rootChildren].Count-1].sheepNumbr = sheepNr;

                                        // print("--Debug--");
                                    }
                                    
                                }
                            }  
                        }  
                    }
                }
            }
            
            int lowestHeuristicValue = int.MaxValue;
            int posBuffer = 0;
            int negBuffer = 0;
            // append correct value to leafNode.Parrent
            for (int y = 0; y < root.Count; y++)
            {
                if(root[y].Data >= 0 && root[y].Data != posBuffer){
                    posBuffer = root[y].Data;
                }
                for(int z = 0; z < root[y].Count; z++){
                    if (root[y].Data > root[y][z].Data)
                    {

                        if(root[y][z].Data < 0 && root[y][z].Data != negBuffer){
                            negBuffer = root[y][z].Data;
                        }

                        lowestHeuristicValue = root[y][z].Data;
                        // print("---------------------------------------" + posBuffer );

                        
                        root[y].setData(  root[y][z].Data + posBuffer);
                        

                    }

                    // if(root[y][z].IsLeaf == false){
                    //     for(int x = 0; x < root[y][z].Count; x++){
                    //         if(root[y][z].Data < root [y][z][x].Data){
                    //             root[y][z].setData(  root[y][z][x].Data + negBuffer);
                    //         }
                    //     }
                    // }
                }
            }


            
            //print("******************* WHATSROOTS DATA???? ************** " + root[y]);
            int largestHeuristicValue = int.MinValue;
            int blockedSpaces = 0;
            for (int y = 0; y < root.Count; y++)
            {

                // asign root best move... Do this in MoveWolfAI
                // if (root.Data < root[y].Data && gridSpaces[root[y]._gridSpaceValue].GetGridSpaceStatus() != GridSpaceStatus.SHEEP)
                // {
                //     largestHeuristicValue = root[y].Data;
                //     root._bestMove = root[y]._gridSpaceValue;
                //     root.setData(root[y].Data);

                // }
                if(gridSpaces[root[y]._gridSpaceValue].GetGridSpaceStatus() == GridSpaceStatus.SHEEP){
                    blockedSpaces+=1;    
                }
                
                if(blockedSpaces == root.Count){
                    //print("PRERESET!!!!!!!!!!!" + root._bestMove);
                    //gridSpaces [root._bestMove].SetGridSpaceStatus(GridSpaceStatus.EMPTY);
                    root.gameOver = true;
                    ResetGridSpaces();
                }

            }
            
            return root;
        }
        else
        {

            TreeNode<int> baseCaseNode = createTreeWolf(root, 1, true);
            int sheep1Posbuff = sheepListClone[0];
            int sheep2Posbuff = sheepListClone[1];
            int sheep3Posbuff = sheepListClone[2];
            int sheep4Posbuff = sheepListClone[3];

            int previousValueBuff =0;
            int maximizingBuff = -1000;
            
            for (int i = 0; i < root.Count; i++)
            {
                // print("foxPOSCLONEAlgorithm " + root._children[i]._gridSpaceValue);
                if (root._children[i]._gridSpaceValue > -1)
                {
                    foxPosClone = root._children[i]._gridSpaceValue;
                }
                //sheepNr count from 0 to 3 (sheep1 to sheep4);
                for (int y = 0; y < root._children[i].Count; y++)
                {
                    if(root._children[i]._children[y]._gridSpaceValue > -1){
                        if(root._children[i]._children[y].sheepNumbr == 0){
                            sheepListClone[0] = root._children[i]._children[y]._gridSpaceValue;
                            sheepListClone[1] = sheep2Posbuff;
                            sheepListClone[2] = sheep3Posbuff;
                            sheepListClone[3] = sheep4Posbuff;
                        }
                        if(root._children[i]._children[y].sheepNumbr == 1){
                            sheepListClone[1] = root._children[i]._children[y]._gridSpaceValue;


                            sheepListClone[0] = sheep1Posbuff;
                            sheepListClone[2] = sheep3Posbuff;
                            sheepListClone[3] = sheep4Posbuff;
                        }
                        if(root._children[i]._children[y].sheepNumbr == 2){
                            sheepListClone[2] = root._children[i]._children[y]._gridSpaceValue;

                            sheepListClone[0] = sheep1Posbuff;
                            sheepListClone[1] = sheep2Posbuff;
                            sheepListClone[3] = sheep4Posbuff;
                        }
                        if(root._children[i]._children[y].sheepNumbr == 3){
                            sheepListClone[3] = root._children[i]._children[y]._gridSpaceValue;

                            sheepListClone[0] = sheep1Posbuff;
                            sheepListClone[1] = sheep2Posbuff;
                            sheepListClone[2] = sheep3Posbuff;
                        }
                    }




                    returnValue = createTreeWolf(root._children[i]._children[y], depth - 1, false);

                   
                    // print( "------------------------/////////////////////////////"+ returnValue._gridSpaceValue);
                    // print( "000000000000000000000000:  " + returnValue.Data);



                    // for(int p = 0; p < returnValue.Count; p++){


                    //     // print( "------------------------/////////////////////////////[2]"+ returnValue[p]._gridSpaceValue);
                    //     // print( "000000000000000000000000:  [2]" + returnValue[p].Data);

                    //     if( maximizingBuff < returnValue[p].Data){
                    //         maximizingBuff = returnValue[p].Data;
                            
                    //     }

                    //     previousValueBuff = returnValue.Data;
                        
                        
                        
                        
                    // }
                    // returnValue.setData((maximizingBuff + previousValueBuff));
                    // print("DEBUGGGGGGG" + returnValue.Data);

                    // print("returnValue.Parent.Data " + returnValue.Parent.Data);
                    // print("previousValueBuff" +yyyyyyyyyyyyyyyyyyyyyyyyy)
                    // print("previousValueBuff " + (previousValueBuff * -1) + returnValue.Parent.Data);
                    // returnValue.Parent.setData(previousValueBuff * -1);
                    
                    // maximizingBuff = -1000;
                    // previousValueBuff = 0;
                    


                }


                
                //createTreeWolf(root._children[i], depth - 1);
                
                // if(returnValue.Data < returnValue.Parent.Data && returnValue.Parent.maximizingNode == false)
                // {
                //     returnValue.Parent.setData(99);
                // }
                // if(returnValue.Data > returnValue.Parent.Data && returnValue.Parent.maximizingNode == true)
                // {
                //     returnValue.Parent.setData(99);
                // }
            }

            //--start
            
            for(int x = 0; x < returnValue.Parent.Parent.Count; x++){
                int posssBuffer = + 1000;
                int oldValue = -1000;
                for(int i = 0; i < returnValue.Parent.Parent[x].Count; i++){

                    int neggBuffer = -1000;
                    
                    for(int y = 0; y < returnValue.Parent.Parent[x][i].Count; y++){

                        
                        if(returnValue.Parent.Parent[x][i][y].Data > neggBuffer && returnValue.Parent.Parent[x][i][y].Data != neggBuffer){
                            //print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" + returnValue.Parent.Parent[x][i][y].Data );
                            neggBuffer = returnValue.Parent.Parent[x][i][y].Data;
                        }
                    }

                    if(returnValue.Parent.Parent[x][i].Data < posssBuffer && returnValue.Parent.Parent[x][i].Data != posssBuffer){
                        oldValue = returnValue.Parent.Parent[x][i].Data;
                    }

                    // print("*Debug[x][i]" + returnValue.Parent.Parent[x][i].Data);
                    // print("negBuffer" + neggBuffer);
                    returnValue.Parent.Parent[x][i].setData(neggBuffer + returnValue.Parent.Parent[x][i].Data);

                    if(returnValue.Parent.Parent[x][i].Data < posssBuffer && returnValue.Parent.Parent[x][i].Data != posssBuffer){
                        posssBuffer = returnValue.Parent.Parent[x][i].Data;
                    }

                }
                // print("posBuffer" + posssBuffer);
                // print("oldValue" + (((oldValue * -1) + returnValue.Parent.Parent[x].Data) + posssBuffer)) ;
                returnValue.Parent.Parent[x].setData((((oldValue * -1) + returnValue.Parent.Parent[x].Data) + posssBuffer));
            }

            // for(int i = 0; i < returnValue.Parent.Count; i++){
            //     //print("---i" + returnValue.Parent[i].Data);
            //     for(int y = 0; y < returnValue.Parent[i].Count; y++){
            //         if(returnValue.Parent[i].Data < returnValue.Parent[i][y].Data && returnValue.Parent[i][y].maximizingNode == false){
            //             returnValue.Parent[i].setData(-20);
            //             print("***************************************************************************" + returnValue.Parent[i].Data);
            //             print("********************************************************************Gspace" + returnValue.Parent[i]._gridSpaceValue);
            //             print("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++" + returnValue.Parent[i][y].Data);



            //         }
            //         //print(returnValue.Parent[i].Data < returnValue.Parent[i][y].Data && returnValue.Parent[i][y].maximizingNode == false);
            //     }
                
            // }
            // ---end

            /*for (int i = 0; i < gridSpaces[foxPos].GetAlowedGridSpaces().Count; i++)
            {


                //root._bestMove = possibleMove;
                //createTreeWolf(root[i], depth - 1);
                //print("COUNTER!!" + i);
                //counter++;
            }*/
            return returnValue;
        }
        
            
            //return root;
    }



    public TreeNode<int> createTreeSheep(TreeNode<int> root, int depth, bool maximizingPlayer)
    {
        

        ArrayList row0 = new ArrayList();
        row0.Add(56);
        row0.Add(58);
        row0.Add(60);
        row0.Add(62);

        ArrayList row1 = new ArrayList();
        row1.Add(49);
        row1.Add(51);
        row1.Add(53);
        row1.Add(55);

        ArrayList row2 = new ArrayList();
        row2.Add(40);
        row2.Add(42);
        row2.Add(44);
        row2.Add(46);

        ArrayList row3 = new ArrayList();
        row3.Add(33);
        row3.Add(35);
        row3.Add(37);
        row3.Add(39);

        ArrayList row4 = new ArrayList();
        row4.Add(24);
        row4.Add(26);
        row4.Add(28);
        row4.Add(30);

        ArrayList row5 = new ArrayList();
        row5.Add(17);
        row5.Add(19);
        row5.Add(21);
        row5.Add(23);

        ArrayList row6 = new ArrayList();
        row6.Add(8);
        row6.Add(10);
        row6.Add(12);
        row6.Add(14);

        ArrayList row7 = new ArrayList();
        row7.Add(1);
        row7.Add(3);
        row7.Add(5);
        row7.Add(7);


        bool isTrapped = true;
        int counter = 0;
        TreeNode<int> returnValue = new TreeNode<int>(int.MinValue);


        
        
        // print("Debug2: " + sheepList[1]);
        // print("Debug3: " + sheepList[2]);
        // print("Debug4: " + sheepList[3]);
     
        //print("@@" + foxPosClone);
        if (depth == 1)
        {
            // print("sheepListCLone [0]" + sheepListClone[0]);
            // print("sheepListCLone [1]" + sheepListClone[1]);
            // print("sheepListCLone [2]" + sheepListClone[2]);
            // print("sheepListCLone [3]" + sheepListClone[3]);
            root.maximizingNode = false;
            //print("!!!!!!!!!!!!!!!!!!! DEPTH == 1 !!!!!!!!!!!!!!!!!!" + foxPosClone);
           // print("FOXPOSCLONE"+foxPosClone);
            if( foxPos == 1 ||
                foxPos == 3 ||
                foxPos == 5 ||
                foxPos == 7)
            {
                ResetGridSpaces();
            }

            

            //If sheep is holding a straight line
            if(row7.Contains(sheepListClone[0]) && row7.Contains(sheepListClone[1]) && row7.Contains(sheepListClone[2])&& row7.Contains(sheepListClone[3]) 
                || row6.Contains(sheepListClone[0]) && row6.Contains(sheepListClone[1]) && row6.Contains(sheepListClone[2])&& row6.Contains(sheepListClone[3])
                || row5.Contains(sheepListClone[0]) && row5.Contains(sheepListClone[1]) && row5.Contains(sheepListClone[2])&& row5.Contains(sheepListClone[3])
                || row4.Contains(sheepListClone[0]) && row4.Contains(sheepListClone[1]) && row4.Contains(sheepListClone[2])&& row4.Contains(sheepListClone[3])
                || row3.Contains(sheepListClone[0]) && row3.Contains(sheepListClone[1]) && row3.Contains(sheepListClone[2])&& row3.Contains(sheepListClone[3])
                || row2.Contains(sheepListClone[0]) && row2.Contains(sheepListClone[1]) && row2.Contains(sheepListClone[2])&& row2.Contains(sheepListClone[3])
                || row1.Contains(sheepListClone[0]) && row1.Contains(sheepListClone[1]) && row1.Contains(sheepListClone[2])&& row1.Contains(sheepListClone[3])
                || row0.Contains(sheepListClone[0]) && row0.Contains(sheepListClone[1]) && row0.Contains(sheepListClone[2])&& row0.Contains(sheepListClone[3]))
            {
                // print("----> straightLine <----");
                for (int sheepNr = 0; sheepNr < sheepListClone.Count; sheepNr++)
                {
                    //Als schaap in een hoek zit; geef hem de hoogste score;
                    if(gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep().Count == 1 && sheepListClone.Contains(gridSpaces[gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0]].GetGridSpaceNumber()) == false){
                        // print("corner! -----------------");
                        
                            if(root._gridSpaceValue != gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0] ){

                                if(nonSheepAlowedGridspace(foxPosClone).Count == 1 && nonSheepAlowedGridspace(foxPosClone)[0] == gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0]){
                                    root.AddChild(int.MinValue);
                                    root[root.Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0];
                                    root[root.Count-1].maximizingNode = true;
                                    root[root.Count-1].sheepNumbr = sheepNr;


                                    // print("--Debug--");
                                }
                                else{
                                    root.AddChild(-10);
                                    root[root.Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0];
                                    root[root.Count-1].maximizingNode = true;
                                    root[root.Count-1].sheepNumbr = sheepNr;

                                    // print("--Debug--");

                                    
                                }
                                
                               
                            }
                        
                    }else{

                        
                            for(int alowedgSpacesSheep = 0; alowedgSpacesSheep < gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep().Count; alowedgSpacesSheep++ ){
                                if(root._gridSpaceValue != gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep] ){

                                    if(nonSheepAlowedGridspace(foxPosClone).Count == 1 && nonSheepAlowedGridspace(foxPosClone)[0] == gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep]){
                                        root.AddChild(int.MinValue);
                                        root[root.Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep];
                                        root[root.Count-1].maximizingNode = true;
                                        root[root.Count-1].sheepNumbr = sheepNr;

                                        // print("--Debug--");
                                    }
                                    else{
                                        root.AddChild(-1);
                                        root[root.Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep];
                                        root[root.Count-1].maximizingNode = true;
                                        root[root.Count-1].sheepNumbr = sheepNr;

                                        // print("--Debug--");
                                    }
                                    
                                }
                                
                                //print(gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep]);
                            }
                        
                    }

                }
            }else{
                for (int sheepNr = 0; sheepNr < sheepListClone.Count; sheepNr++){       
                    if(gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep().Count == 2){
                        
                        
                            int totalSheepInAlowedgSPace = 0;
                            for(int alowedgSpacesSheep = 0; alowedgSpacesSheep < gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep().Count; alowedgSpacesSheep++ ){
                                //Juist als er een is die niet empty is dan is dat de stap die schaap het meest punten hoort op te leveren;
                                if( sheepListClone.Contains(gridSpaces[gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep]].GetGridSpaceNumber()) == true){
                                    totalSheepInAlowedgSPace +=1;
                                    
                                }
                                if(sheepNr == 1){
                                    print("::: "+ sheepList[0]);
                                    print("::: " + sheepList[1]);
                                    print("::: " + sheepList[2]);
                                    print("::: " + sheepList[3]);

                                    print(sheepListClone.Contains(gridSpaces[gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep]].GetGridSpaceNumber()) == true);
                                }
                                
                                if(totalSheepInAlowedgSPace == 1 && alowedgSpacesSheep == 1){
                                    // print("!!!!!!!!!!"+ root[root.Count-1]._gridSpaceValue);
                                    if(root._gridSpaceValue != gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep-1] ){
                                        if(sheepListClone.Contains(gridSpaces[gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep-1]].GetGridSpaceNumber()) == false){
                                            if(nonSheepAlowedGridspace(foxPosClone).Count == 1 && nonSheepAlowedGridspace(foxPosClone)[0] == gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep-1]){
                                                root.AddChild(int.MinValue);
                                                root[root.Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep-1];
                                                root[root.Count-1].maximizingNode = true;
                                                root[root.Count-1].sheepNumbr = sheepNr;

                                                // print("--Debug--");
                                            }
                                            else{
                                                // print("!!!!!!!!!!"+ root[root.Count-1]._gridSpaceValue); // ???
                                                root.AddChild(-10);
                                                root[root.Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep-1];
                                                root[root.Count-1].maximizingNode = true;
                                                root[root.Count-1].sheepNumbr = sheepNr;

                                                // print("--Debug--");
                                            }

                                            
                                        }
                                    }
                                }
                                if(totalSheepInAlowedgSPace == 1 && alowedgSpacesSheep == 0){
                                    if(root._gridSpaceValue != gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep+1] ){
                                        if(sheepListClone.Contains(gridSpaces[gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep+1]].GetGridSpaceNumber()) == false){
                                            if(nonSheepAlowedGridspace(foxPosClone).Count == 1 && nonSheepAlowedGridspace(foxPosClone)[0] == gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep+1]){
                                                root.AddChild(int.MinValue);
                                                root[root.Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep+1];
                                                root[root.Count-1].maximizingNode = true;
                                                root[root.Count-1].sheepNumbr = sheepNr;

                                                // print("--Debug--");
                                            }
                                            else{
                                                root.AddChild(-10);
                                                root[root.Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[alowedgSpacesSheep+1];
                                                root[root.Count-1].maximizingNode = true;
                                                root[root.Count-1].sheepNumbr = sheepNr;

                                                // print("--Debug--");
                                            }
                                            
                                        }   
                                    }
                                }
                            }
                            if(sheepListClone.Contains(gridSpaces[gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0]].GetGridSpaceNumber()) == false && sheepListClone.Contains(gridSpaces[gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[1]].GetGridSpaceNumber()) == false){
                                if(root._gridSpaceValue != gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0] ){
                                    if(nonSheepAlowedGridspace(foxPosClone).Count == 1 && nonSheepAlowedGridspace(foxPosClone)[0] == gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0]){
                                        root.AddChild(int.MinValue);
                                        root[root.Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0];
                                        root[root.Count-1].maximizingNode = true;
                                        root[root.Count-1].sheepNumbr = sheepNr;

                                        // print("--Debug--");
                                    }

                                    else{

                                        root.AddChild(-1);
                                        root[root.Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[0];
                                        root[root.Count-1].maximizingNode = true;
                                        root[root.Count-1].sheepNumbr = sheepNr;
                                        


                                        // print("--Debug--");
                                    }
                                    
                                }
                                if(root._gridSpaceValue != gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[1] ){
                                    if(nonSheepAlowedGridspace(foxPosClone).Count == 1 && nonSheepAlowedGridspace(foxPosClone)[0] == gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[1]){
                                        root.AddChild(int.MinValue);
                                        root[root.Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[1];
                                        root[root.Count-1].maximizingNode = true;
                                        root[root.Count-1].sheepNumbr = sheepNr;

                                        // print("--Debug--");
                                    }
                                    else{
                                        root.AddChild(-1);
                                        root[root.Count-1]._gridSpaceValue = gridSpaces[sheepListClone[sheepNr]].GetMmAlowedGridSpacesSheep()[1];
                                        root[root.Count-1].maximizingNode = true;
                                        root[root.Count-1].sheepNumbr = sheepNr;

                                        // print("--Debug--");
                                    }
                                    
                                }
                            }  
                          
                    }
                }
            }
            
            


            for (int rootChildren = 0; rootChildren < root.Count; rootChildren++){
                for (int i = 0; i < gridSpaces[foxPosClone].GetAlowedGridSpaces().Count; i++)
                    {
                        
                        
                        

                        if (row0.Contains(gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]))
                        {

                            if( row0.Contains(sheepList.Min()) && gridSpaces[gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]].GetGridSpaceStatus() == GridSpaceStatus.EMPTY){
                                root[rootChildren].AddChild(1001);
                                // root.setData(1);
                                root[rootChildren]._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                            
                                root[rootChildren][i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];

                                root[rootChildren][i].maximizingNode = false; 
                            }else{
                                root[rootChildren].AddChild(1);
                                // root.setData(1);
                                root[rootChildren]._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                            
                                root[rootChildren][i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];

                                root[rootChildren][i].maximizingNode = false;
                            }

                            
                        }
                        if (row1.Contains(gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]))
                        {

                            if( row1.Contains(sheepList.Min()) && gridSpaces[gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]].GetGridSpaceStatus() == GridSpaceStatus.EMPTY){
                                root.AddChild(1002);
                                // root.setData(2);
                                root[rootChildren]._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i].maximizingNode = false;
                            }
                            else{
                                root[rootChildren].AddChild(2);
                                // root.setData(2);
                                root[rootChildren]._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i].maximizingNode = false;
                            }
                            
                        }
                        if (row2.Contains(gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]))
                        {
                            if( row2.Contains(sheepList.Min()) && gridSpaces[gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]].GetGridSpaceStatus() == GridSpaceStatus.EMPTY){
                                root[rootChildren].AddChild(1003);
                                // root.setData(2);
                                root[rootChildren]._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i].maximizingNode = false;
                            }
                            else{
                                root[rootChildren].AddChild(3);
                                // root.setData(2);
                                root[rootChildren]._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i].maximizingNode = false;
                            }
                        }
                        if (row3.Contains(gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]))
                        {
                            if( row3.Contains(sheepList.Min()) && gridSpaces[gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]].GetGridSpaceStatus() == GridSpaceStatus.EMPTY){
                                root[rootChildren].AddChild(1004);
                                // root.setData(2);
                                root[rootChildren]._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i].maximizingNode = false;
                            }
                            else{
                                root[rootChildren].AddChild(4);
                                // root.setData(2);
                                root[rootChildren]._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i].maximizingNode = false;
                            }
                        }
                        if (row4.Contains(gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]))
                        {
                            if( row4.Contains(sheepList.Min()) && gridSpaces[gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]].GetGridSpaceStatus() == GridSpaceStatus.EMPTY){
                                root[rootChildren].AddChild(1005);
                                // root.setData(2);
                                root[rootChildren]._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i].maximizingNode = false;
                            }
                            else{
                                root[rootChildren].AddChild(5);
                                // root.setData(2);
                                root[rootChildren]._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i].maximizingNode = false;
                            }

                        }
                        if (row5.Contains(gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]))
                        {
                           if( row5.Contains(sheepList.Min()) && gridSpaces[gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]].GetGridSpaceStatus() == GridSpaceStatus.EMPTY){
                                root[rootChildren].AddChild(1006);
                                // root.setData(2);
                                root[rootChildren]._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i].maximizingNode = false;
                            }
                            else{
                                root[rootChildren].AddChild(6);
                                // root.setData(2);
                                root[rootChildren]._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i].maximizingNode = false;
                            }


                        }
                        if (row6.Contains(gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]))
                        {
                            if( row6.Contains(sheepList.Min()) && gridSpaces[gridSpaces[foxPosClone].GetAlowedGridSpaces()[i]].GetGridSpaceStatus() == GridSpaceStatus.EMPTY){
                                root[rootChildren].AddChild(1007);
                                // root.setData(2);
                                root[rootChildren]._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i].maximizingNode = false;
                            }
                            else{
                                root[rootChildren].AddChild(7);
                                // root.setData(2);
                                root[rootChildren]._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                                root[rootChildren][i].maximizingNode = false;
                            }

                        }


                        if (row7.Contains(gridSpaces[foxPosClone].GetAlowedGridSpaces()[i])  )
                        {
                            
                            //print("DEBUg" + gridSpaces[foxPosClone].GetAlowedGridSpaces().Count);
                            root[rootChildren].AddChild(int.MaxValue);
                            //root.setData(int.MaxValue);
                            root[rootChildren]._bestMove = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                            root[rootChildren][i]._gridSpaceValue = gridSpaces[foxPosClone].GetAlowedGridSpaces()[i];
                            root[rootChildren][i].maximizingNode = false;

                        }
                       
                        // print("***************************" + nonSheepAlowedGridspace(root[0]._gridSpaceValue).Count);

                        //print(largestHeuristicValue);
                        //print("!!!!!!!!!!!!!!!!!!!!!!!!!!!--------------------!!!!!!!!!!!!!!!!!!!!!!!!!" + root.Data);
                    }
                }

            int lowestHeuristicValue = int.MaxValue;
            int posBuffer = 0;
            int negBuffer = 0;
            // append correct value to leafNode.Parrent
            for (int y = 0; y < root.Count; y++)
            {
                if(root[y].Data <= 0 && root[y].Data != posBuffer){
                    posBuffer = root[y].Data;
                }
                print("---------------------------------------" + posBuffer );

                for(int z = 0; z < root[y].Count; z++){
                    //print( root[y].Data < root[y][1].Data );
                    if (root[y].Data < root[y][z].Data)
                    {

                        if(root[y][z].Data < 0 && root[y][z].Data != negBuffer){
                            negBuffer = root[y][z].Data;
                        }

                        lowestHeuristicValue = root[y][z].Data;
                        // print("---------------------------------------" + posBuffer );

                        
                        root[y].setData(  root[y][z].Data + posBuffer);
                        

                    }

                
                }
            }


            
            
            int largestHeuristicValue = int.MinValue;
            int blockedSpaces = 0;
            for (int y = 0; y < root.Count; y++)
            {

                // asign root best move... Do this in MoveWolfAI
                // if (root.Data < root[y].Data && gridSpaces[root[y]._gridSpaceValue].GetGridSpaceStatus() != GridSpaceStatus.SHEEP)
                // {
                //     largestHeuristicValue = root[y].Data;
                //     root._bestMove = root[y]._gridSpaceValue;
                //     root.setData(root[y].Data);

                // }
                if(gridSpaces[root[y]._gridSpaceValue].GetGridSpaceStatus() == GridSpaceStatus.SHEEP){
                    blockedSpaces+=1;    
                }
                
                if(blockedSpaces == root.Count){
                    //print("PRERESET!!!!!!!!!!!" + root._bestMove);
                    //gridSpaces [root._bestMove].SetGridSpaceStatus(GridSpaceStatus.EMPTY);
                    root.gameOver = true;
                    ResetGridSpaces();
                }

            }

            return root;
        }
        else
        {

            TreeNode<int> baseCaseNode = createTreeSheep(root, 1, true);
            int sheep1Posbuff = sheepListClone[0];
            int sheep2Posbuff = sheepListClone[1];
            int sheep3Posbuff = sheepListClone[2];
            int sheep4Posbuff = sheepListClone[3];

            int previousValueBuff =0;
            int maximizingBuff = -1000;
            
            for (int i = 0; i < root.Count; i++)
            {
                // print("foxPOSCLONEAlgorithm " + root._children[i]._gridSpaceValue);
                if (root._children[i]._gridSpaceValue > -1)
                {
                    if(root._children[i]._gridSpaceValue > -1){
                        if(root._children[i].sheepNumbr == 0){
                            sheepListClone[0] = root._children[i]._gridSpaceValue;
                            sheepListClone[1] = sheep2Posbuff;
                            sheepListClone[2] = sheep3Posbuff;
                            sheepListClone[3] = sheep4Posbuff;
                        }
                        if(root._children[i].sheepNumbr == 1){
                            sheepListClone[1] = root._children[i]._gridSpaceValue;


                            sheepListClone[0] = sheep1Posbuff;
                            sheepListClone[2] = sheep3Posbuff;
                            sheepListClone[3] = sheep4Posbuff;
                        }
                        if(root._children[i].sheepNumbr == 2){
                            sheepListClone[2] = root._children[i]._gridSpaceValue;

                            sheepListClone[0] = sheep1Posbuff;
                            sheepListClone[1] = sheep2Posbuff;
                            sheepListClone[3] = sheep4Posbuff;
                        }
                        if(root._children[i].sheepNumbr == 3){
                            sheepListClone[3] = root._children[i]._gridSpaceValue;

                            sheepListClone[0] = sheep1Posbuff;
                            sheepListClone[1] = sheep2Posbuff;
                            sheepListClone[2] = sheep3Posbuff;
                        }
                    }
                    
                }
                //sheepNr count from 0 to 3 (sheep1 to sheep4);
                for (int y = 0; y < root._children[i].Count; y++)
                {
                    foxPosClone = root._children[i]._children[y]._gridSpaceValue;
                    returnValue = createTreeSheep(root._children[i]._children[y], depth - 1, false);
                }
            }

            //--start
            //// Todo ------- refactor to sheepAI
            for(int x = 0; x < returnValue.Parent.Parent.Count; x++){
                int posssBuffer = - 1000;
                int oldValue = +1000;
                for(int i = 0; i < returnValue.Parent.Parent[x].Count; i++){

                    int neggBuffer = +1000;
                    
                    for(int y = 0; y < returnValue.Parent.Parent[x][i].Count; y++){

                        
                        if(returnValue.Parent.Parent[x][i][y].Data < neggBuffer && returnValue.Parent.Parent[x][i][y].Data != neggBuffer){
                            //print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" + returnValue.Parent.Parent[x][i][y].Data );
                            neggBuffer = returnValue.Parent.Parent[x][i][y].Data;
                        }
                    }

                    if(returnValue.Parent.Parent[x][i].Data > posssBuffer && returnValue.Parent.Parent[x][i].Data != posssBuffer){
                        oldValue = returnValue.Parent.Parent[x][i].Data;
                    }


                    returnValue.Parent.Parent[x][i].setData(neggBuffer + returnValue.Parent.Parent[x][i].Data);

                    if(returnValue.Parent.Parent[x][i].Data > posssBuffer && returnValue.Parent.Parent[x][i].Data != posssBuffer){
                        posssBuffer = returnValue.Parent.Parent[x][i].Data;
                    }

                }
                
                returnValue.Parent.Parent[x].setData((((oldValue * -1) + returnValue.Parent.Parent[x].Data) + posssBuffer));
            }

       
            return returnValue;
        }
    }


 

    public void AIMoveWolf()
    {
        //AILogic


        

        foxPosClone = foxPos;
        sheepListClone[0] = sheepList[0];
        sheepListClone[1] = sheepList[1];
        sheepListClone[2] = sheepList[2];
        sheepListClone[3] = sheepList[3];

        TreeNode<int> roots = new TreeNode<int>(int.MinValue);
        
        TreeNode<int> minMaxNode = createTreeWolf(roots, 2, true);

        // print("#####" + minMaxNode.Parent.Parent[1][6]._gridSpaceValue);
        // print("#####" + minMaxNode.Parent.Parent[1][6].Data);
        // print("#" + minMaxNode.Parent.Parent[0][6][2]._gridSpaceValue);
        // print("#" + minMaxNode.Parent.Parent[0][6][2].Data);
        //print(minMaxNode.Data < minMaxNode[2].Data && minMaxNode[0].maximizingNode == false);



        while(minMaxNode.IsRoot == false){
            minMaxNode = minMaxNode.Parent;
        }

        // print("MinMax::::root.Data " + minMaxNode.Data);
        // print("MinMax::::root._bestMove " + minMaxNode._bestMove);
        // print("MinMax::::root._bestMove " + minMaxNode.maximizingNode);
        
        // print("MinMax::::[0]_gridSpaceValue " + minMaxNode[0]._gridSpaceValue);
        // print("MinMax::::[0]Data " + minMaxNode[0].Data);

        // print("MinMax::::[1]_gridSpaceValue " + minMaxNode[1]._gridSpaceValue);
        // print("MinMax::::[1]Data " + minMaxNode[1].Data);


        // print("---->[0].Count: " + minMaxNode[0].Count  +  "<----");
        // print("---->[1].Count: " + minMaxNode[1].Count  +  "<----");
        
        // for(int i = 0; i <minMaxNode.Count; i++){
        //     print("MinMax::::["+ i + "]_gridSpaceValue " + minMaxNode[i]._gridSpaceValue);
        //     print("MinMax::::["+ i + "]Data " + minMaxNode[i].Data);
        // }

        // if(minMaxNode.Count > 2){
        //     for(int i = 0; i <minMaxNode[2].Count; i++){
        //             print("MinMax::::[0]["+ i + "]_gridSpaceValue " + minMaxNode[2][i]._gridSpaceValue);
        //             print("MinMax::::[0]["+ i + "]Data " + minMaxNode[2][i].Data);
        //             print("MinMax::::[0]["+ i + "]sheepNr " + minMaxNode[2][i].sheepNumbr);
        //     }

        // }




        int largestHeuristicValue = int.MinValue;
        int blockedSpaces = 0;

        for (int y = 0; y < minMaxNode.Count; y++)
        {
            if (minMaxNode.Data < minMaxNode[y].Data && gridSpaces[minMaxNode[y]._gridSpaceValue].GetGridSpaceStatus() != GridSpaceStatus.SHEEP)
            {
                // largestHeuristicValue = root[y].Data;
                minMaxNode._bestMove = minMaxNode[y]._gridSpaceValue;
                minMaxNode.setData(minMaxNode[y].Data);

            }
        }
        print("------ debug ------");

        print("MinMax::::root._gridSpaceValue " + minMaxNode._gridSpaceValue);
        print("MinMax::::root._bestMove " + minMaxNode._bestMove);
        print("MinMax::::root.Data " + minMaxNode.Data);

        print("---end of root---");
        if(minMaxNode.Count > 2){
            print("MinMax::::root._gridSpaceValue[0]._gridSpaceValue " + minMaxNode[0]._gridSpaceValue);
            print("MinMax::::root._gridSpaceValue[0]._gridSpaceValue " + minMaxNode[0].Data);

            print("MinMax::::root._gridSpaceValue[1]._gridSpaceValue " + minMaxNode[1]._gridSpaceValue);
            print("MinMax::::root._gridSpaceValue[1]._gridSpaceValue " + minMaxNode[1].Data);

            print("MinMax::::root._gridSpaceValue[2]._gridSpaceValue " + minMaxNode[2]._gridSpaceValue);
            print("MinMax::::root._gridSpaceValue[2]._gridSpaceValue " + minMaxNode[2].Data);

            print("MinMax::::root._gridSpaceValue[3]._gridSpaceValue " + minMaxNode[3]._gridSpaceValue);
            print("MinMax::::root._gridSpaceValue[3]._gridSpaceValue " + minMaxNode[3].Data);


            print("MinMax::::root._gridSpaceValue[0][0]._gridSpaceValue " + minMaxNode[0][0]._gridSpaceValue);
            print("MinMax::::root._gridSpaceValue[0][0]._gridSpaceValue " + minMaxNode[0][0].Data);

           
            print("MinMax::::root._gridSpaceValue[0][1]._gridSpaceValue " + minMaxNode[0][1]._gridSpaceValue);
            print("MinMax::::root._gridSpaceValue[0][1]._gridSpaceValue " + minMaxNode[0][1].Data);


        }
        

        // print("MinMax::::root._gridSpaceValue[0]._gridSpaceValue " + minMaxNode[0]._gridSpaceValue);
        // print("MinMax::::root._gridSpaceValue[0]._gridSpaceValue " + minMaxNode[0].Data);
        // print("MinMax::::root._gridSpaceValue[0][0]._gridSpaceValue " + minMaxNode[0][0]._gridSpaceValue);
        // print("MinMax::::root._gridSpaceValue[0][0]._gridSpaceValue " + minMaxNode[0][0].Data);

        // print("---");
        // print("MinMax::::root._gridSpaceValue[0][6][2]._gridSpaceValue " + minMaxNode[0][6][2]._gridSpaceValue);
        // print("MinMax::::root._gridSpaceValue[0][6][2].Data " + minMaxNode[0][6][2].Data);

        // print("MinMax::::root._gridSpaceValue[0][6][2][4]._gridSpaceValue " + minMaxNode[0][6][2][1]._gridSpaceValue);
        // print("MinMax::::root._gridSpaceValue[0][6][2][4].Data " + minMaxNode[0][6][2][1].Data);

        // print("MinMax::::root._gridSpaceValue[0][6][2][4][3]._gridSpaceValue " + minMaxNode[0][6][2][4][3]._gridSpaceValue);
        // print("MinMax::::root._gridSpaceValue[0][6][2][4][3].Data " + minMaxNode[0][6][2][4][3].Data);
        // print("MinMax::::root._gridSpaceValue[0][6].TestSheepNr " + minMaxNode[0][6].sheepNumbr);
        // print("MinMax::::root._gridSpaceValue[0][5][0].TestSheepNr " + minMaxNode[0][6][2][4].debugBuff);

        // print("MinMax::::root._gridSpaceValue[0][1][0].Count " + minMaxNode[0][1][0].Count);

        //print(minMaxNode.Parent.Parent.IsRoot);

        // if(minMaxNode.Count > 2){
        //     if(minMaxNode[2].Count > 2){
        //         for(int i = 0; i <minMaxNode[2][6].Count; i++){
        //                 print("MinMax::::[2][6]["+ i + "]_gridSpaceValue " + minMaxNode[2][6][i]._gridSpaceValue);
        //                 print("MinMax::::[2][6]["+ i + "]Data " + minMaxNode[2][6][i].Data);
        //                 print("MinMax::::[2][6]["+ i + "]sheepNr " + minMaxNode[2][6][i].sheepNumbr);
        //         }
        //     }
        // }

        // if(minMaxNode.Count > 2){
        //     if(minMaxNode[2].Count > 2){
        //         if(minMaxNode[2][6].Count > 2){
        //             for(int i = 0; i <minMaxNode[2][6].Count; i++){
        //                 print("MinMax::::[2][6][3]["+ i + "]_gridSpaceValue " + minMaxNode[2][6][3][i]._gridSpaceValue);
        //                 print("MinMax::::[2][6][3]["+ i + "]Data " + minMaxNode[2][6][3][i].Data);
        //                 print("MinMax::::[2][6][3]["+ i + "]sheepNr " + minMaxNode[2][6][3][i].sheepNumbr);
        //             }
        //         }
                
        //     }
        // }
        
        // print(minMaxNode[2][6].Count);


        // print("-----");
        // if(minMaxNode.Count > 2)
        // {
        //     print("MinMax::::[2]_gridSpaceValue " + minMaxNode[2]._gridSpaceValue);
        //     print("MinMax::::[2]Data " + minMaxNode[2].Data);
        //     print("MinMax::::[3]_gridSpaceValue " + minMaxNode[3]._gridSpaceValue);
        //     print("MinMax::::[3]Data " + minMaxNode[3].Data);
        // }
        
        // print("##minMax::[0][0]._gridSpaceValue" + minMaxNode[0]._gridSpaceValue);
        // print("##minMax::[0][0].Data" + minMaxNode[0].Data);
        
        //print("----------------------------------------------->" + minMaxNode.gameOver);
        if(minMaxNode.gameOver != true){
            
            gridSpaces[foxPos].SetGridSpaceStatus(GridSpaceStatus.EMPTY);
            gridSpaces[minMaxNode._bestMove].SetGridSpaceStatus(GridSpaceStatus.WOLF);
        }else{
            print("GAMEOVER!");
        }
        if(minMaxNode._bestMove == 1 ||
            minMaxNode._bestMove == 3 ||
            minMaxNode._bestMove == 5 ||
            minMaxNode._bestMove == 7)
        {
            ResetGridSpaces();
        }
        
        
    }
    public void AIMoveSheep(){
        foxPosClone = foxPos;
        
        TreeNode<int> roots = new TreeNode<int>(int.MaxValue);
        
        TreeNode<int> minMaxNode = createTreeSheep(roots, 1, true);

        while(minMaxNode.IsRoot == false){
            minMaxNode = minMaxNode.Parent;
        }

        // print("root._gridSpaceValue" + minMaxNode._gridSpaceValue);
        // print("root.Data" + minMaxNode.Data);
        // print("root.Count" + minMaxNode.Count);

        // print("root[0]._gridSpaceValue" + minMaxNode[6]._gridSpaceValue);
        // print("root[0].Data" + minMaxNode[6].Data);
        // print("root[0].Count" + minMaxNode[6].Count);

        // print("root[0][0]._gridSpaceValue" + minMaxNode[6][2]._gridSpaceValue);
        // print("root[0][0].Data" + minMaxNode[6][2].Data);
        // print("root[0][0].Count" + minMaxNode[6][2].Count);

        // print("root[0][0][0]._gridSpaceValue" + minMaxNode[6][2][4]._gridSpaceValue);
        // print("root[0][0][0].Data" + minMaxNode[6][2][4].Data);
        // print("root[0][0][0].Count" + minMaxNode[6][2][4].Count);

        // print("root[0][0][0][0]._gridSpaceValue" + minMaxNode[6][2][4][0]._gridSpaceValue);
        // print("root[0][0][0][0].Data" + minMaxNode[6][2][4][0].Data);
        // print("root[0][0][0][0].Count" + minMaxNode[6][2][4][0].Count);

        // print("minMaxNode[6]._gridSpaceValue" + minMaxNode[0]._gridSpaceValue);
        // print("minMaxNode[6].Data" + minMaxNode[0].Data);
        // print("minMaxNode[6].Count" + minMaxNode[0].Count);

        // print("minMaxNode[6]._gridSpaceValue" + minMaxNode[1]._gridSpaceValue);
        // print("minMaxNode[6].Data" + minMaxNode[1].Data);
        // print("minMaxNode[6].Count" + minMaxNode[1].Count);

        // print("minMaxNode[6]._gridSpaceValue" + minMaxNode[2]._gridSpaceValue);
        // print("minMaxNode[6].Data" + minMaxNode[2].Data);
        // print("minMaxNode[6].Count" + minMaxNode[2].Count);

        // print("minMaxNode[6]._gridSpaceValue" + minMaxNode[3]._gridSpaceValue);
        // print("minMaxNode[6].Data" + minMaxNode[3].Data);
        // print("minMaxNode[6].Count" + minMaxNode[3].Count);

        //  print("minMaxNode[6]._gridSpaceValue" + minMaxNode[4]._gridSpaceValue);
        // print("minMaxNode[6].Data" + minMaxNode[4].Data);
        // print("minMaxNode[6].Count" + minMaxNode[4].Count);

        //  print("minMaxNode[6]._gridSpaceValue" + minMaxNode[5]._gridSpaceValue);
        // print("minMaxNode[6].Data" + minMaxNode[5].Data);
        // print("minMaxNode[6].Count" + minMaxNode[5].Count);

        //  print("minMaxNode[6]._gridSpaceValue" + minMaxNode[6]._gridSpaceValue);
        // print("minMaxNode[6].Data" + minMaxNode[6].Data);
        // print("minMaxNode[6].Count" + minMaxNode[6].Count);

        // print("minMaxNode[6][0]._gridSpaceValue" + minMaxNode[6][0]._gridSpaceValue);
        // print("minMaxNode[6][0].Data" + minMaxNode[6][0].Data);
        // print("minMaxNode[6][0].Count" + minMaxNode[6][0].Count);



        
        int largestHeuristicValue = int.MaxValue;
        int blockedSpaces = 0;

        for (int y = 0; y < minMaxNode.Count; y++)
        {
            if (minMaxNode.Data > minMaxNode[y].Data && gridSpaces[minMaxNode[y]._gridSpaceValue].GetGridSpaceStatus() != GridSpaceStatus.WOLF)
            {
                // largestHeuristicValue = root[y].Data;
                minMaxNode._bestMove = minMaxNode[y]._gridSpaceValue;
                minMaxNode.sheepNumbr = minMaxNode[y].sheepNumbr;
                minMaxNode.setData(minMaxNode[y].Data);

            }
        }

        print("********_bestMove" + minMaxNode._bestMove);
        print("********sheepNumbr" + minMaxNode.sheepNumbr);

        //print("********" + sheepList[1]);




            
        gridSpaces[sheepList[minMaxNode.sheepNumbr]].SetGridSpaceStatus(GridSpaceStatus.EMPTY);
        gridSpaces[minMaxNode._bestMove].SetGridSpaceStatus(GridSpaceStatus.SHEEP);
        sheepList[minMaxNode.sheepNumbr] = minMaxNode._bestMove;


        print("sheep0Pos" + sheepList[0]);
        print("sheep1Pos" + sheepList[1]);
        print("sheep2Pos" + sheepList[2]);
        print("sheep3Pos" + sheepList[3]);

        sheepListClone[0] = sheepList[0];
        sheepListClone[1] = sheepList[1];
        sheepListClone[2] = sheepList[2];
        sheepListClone[3] = sheepList[3];

        

      
    }

    public void SheepturnCalculator(){
        if(gridSpaces[0].sheep1OldPos != GridSpaceStatus.SHEEP){
            for (int i = 0; i < 64; i++){
                gridSpaces[i].sheep1 = true;
            }            
        }else{
            for (int i = 0; i < 64; i++){
                gridSpaces[i].sheep1 = false;
            }   
        }
        if(gridSpaces[0].sheep2OldPos != GridSpaceStatus.SHEEP){
            for (int i = 0; i < 64; i++){
                gridSpaces[i].sheep2 = true;
            }            
        }else{
            for (int i = 0; i < 64; i++){
                gridSpaces[i].sheep2 = false;
            }   
        }
        if(gridSpaces[0].sheep3OldPos != GridSpaceStatus.SHEEP){
            for (int i = 0; i < 64; i++){
                gridSpaces[i].sheep3 = true;
            }            
        }else{
            for (int i = 0; i < 64; i++){
                gridSpaces[i].sheep3 = false;
            }   
        }
        if(gridSpaces[0].sheep4OldPos != GridSpaceStatus.SHEEP){
            for (int i = 0; i < 64; i++){
                gridSpaces[i].sheep4 = true;
            }            
        }else{
            for (int i = 0; i < 64; i++){
                gridSpaces[i].sheep4 = false;
            }   
        }
        if(gridSpaces[0].sheep1 == true){
            sheepturnBuffer = 1;
        }
        if(gridSpaces[0].sheep2 == true){
            sheepturnBuffer = 2;
        }
        
        if(gridSpaces[0].sheep3 == true){
            sheepturnBuffer = 3;
        }
        
        if(gridSpaces[0].sheep4 == true){
            sheepturnBuffer = 4;
        }


        if(sheepturnBuffer == 1){
            for( int i = 0; i < gridSpaces[sheep1Pos].GetMmAlowedGridSpacesSheep().Count; i++){
                    gridSpaces[gridSpaces[sheep1Pos].GetMmAlowedGridSpacesSheep()[i]].isAlowedGridSpace = true;
                    //print(gridSpaces[gridSpaces[sheep1Pos].GetMmAlowedGridSpacesSheep()[i]].isAlowedGridSpace);
                    //print(gridSpaces[10].isAlowedGridSpace);
            }
        }
        if(sheepturnBuffer == 2){
            for( int i = 0; i < gridSpaces[sheep2Pos].GetMmAlowedGridSpacesSheep().Count; i++){
                    gridSpaces[gridSpaces[sheep2Pos].GetMmAlowedGridSpacesSheep()[i]].isAlowedGridSpace = true;
            }
        }
        if(sheepturnBuffer == 3){
            for( int i = 0; i < gridSpaces[sheep3Pos].GetMmAlowedGridSpacesSheep().Count; i++){
                    gridSpaces[gridSpaces[sheep3Pos].GetMmAlowedGridSpacesSheep()[i]].isAlowedGridSpace = true;
            }
        }
        if(sheepturnBuffer == 4){
            for( int i = 0; i < gridSpaces[sheep4Pos].GetMmAlowedGridSpacesSheep().Count; i++){
                    gridSpaces[gridSpaces[sheep4Pos].GetMmAlowedGridSpacesSheep()[i]].isAlowedGridSpace = true;
            }
        }

        // for (int i = 0; i < 64; i++){
        //     if(gridSpaces[i].lastpickedUp  != lastclickedBuffer && gridSpaces[i].lastpickedUp != 0 && !lastclickedBufferLog.Contains(gridSpaces[i].lastpickedUp)){
        //         lastclickedBuffer = gridSpaces[i].lastpickedUp;
        //         lastclickedBufferLog.Add(gridSpaces[i].lastpickedUp);
        //     }
        //     if(gridSpaces[i].signalDroped == true){
        //         gridSpaces[lastclickedBuffer].clearSignalAlowedGspace = true;
        //     }
        //     // print(lastclickedBuffer);
        //     if(lastclickedBuffer != -1){
        //         if(gridSpaces[lastclickedBuffer].clearSignalAlowedGspace == true){
        //         //  print("---------------------------------------------------------------->>>");
        //         for (int y = 0; y < 64; y++){
        //             gridSpaces[y].isAlowedGridSpace = false;
        //         }
        //     }
        //     }
            

        // }


        
        
        if(gridSpaces[sheep1Pos].GetGridSpaceStatus() != GridSpaceStatus.SHEEP){
            lastclickedBuffer = sheep1Pos;
            for (int i = 0; i < 64; i++){
                if(gridSpaces[i].GetGridSpaceStatus() == GridSpaceStatus.SHEEP && i != sheep1Pos && i != sheep2Pos && i != sheep3Pos && i != sheep4Pos){
                    lastDroppedBuffer = i;
                    if(lastDroppedBuffer != sheepList[0]){
                        sheepList[0] = lastDroppedBuffer;
                    }
                }
            }

        }
        if(gridSpaces[sheep2Pos].GetGridSpaceStatus() != GridSpaceStatus.SHEEP){
            lastclickedBuffer = sheep2Pos;
            for (int i = 0; i < 64; i++){
                if(gridSpaces[i].GetGridSpaceStatus() == GridSpaceStatus.SHEEP && i != sheep1Pos && i != sheep2Pos && i != sheep3Pos && i != sheep4Pos){
                    lastDroppedBuffer = i;
                    if(lastDroppedBuffer != sheepList[1]){
                        sheepList[1] = lastDroppedBuffer;
                    }
                }
            }
        }
        if(gridSpaces[sheep3Pos].GetGridSpaceStatus() != GridSpaceStatus.SHEEP){
            lastclickedBuffer = sheep3Pos;
            for (int i = 0; i < 64; i++){
                if(gridSpaces[i].GetGridSpaceStatus() == GridSpaceStatus.SHEEP && i != sheep1Pos && i != sheep2Pos && i != sheep3Pos && i != sheep4Pos){
                    lastDroppedBuffer = i;
                    if(lastDroppedBuffer != sheepList[2]){
                        sheepList[2] = lastDroppedBuffer;
                    }
                }
            }
        }
        if(gridSpaces[sheep4Pos].GetGridSpaceStatus() != GridSpaceStatus.SHEEP){
            lastclickedBuffer = sheep4Pos;
            for (int i = 0; i < 64; i++){
                if(gridSpaces[i].GetGridSpaceStatus() == GridSpaceStatus.SHEEP && i != sheep1Pos && i != sheep2Pos && i != sheep3Pos && i != sheep4Pos){
                    lastDroppedBuffer = i;
                    if(lastDroppedBuffer != sheepList[3]){
                        sheepList[3] = lastDroppedBuffer;
                    }
                }
            }
        }
        
        if(lastclickedBuffer != -1){
            for (int i = 0; i < 64; i++){
                if( AreListsEqual(gridSpaces[i].currentAlowedGridSpacesSheep, gridSpaces[lastclickedBuffer].GetMmAlowedGridSpacesSheep() )){
                    //DoNothing
                }
                else{
                    gridSpaces[i].currentAlowedGridSpacesSheep.Clear();
                    for(int y = 0; y < gridSpaces[lastclickedBuffer].GetMmAlowedGridSpacesSheep().Count; y++){
                        gridSpaces[i].currentAlowedGridSpacesSheep.Add(gridSpaces[lastclickedBuffer].GetMmAlowedGridSpacesSheep()[y]);
                    }
                    
                }
            }
        }
        

        

    }

    void Update()
    {
        // if(gridSpaces[0].currentAlowedGridSpacesSheep.Count == 2){
        //     print(gridSpaces[0].currentAlowedGridSpacesSheep[1]);
        // }


        Player player = GameObject.Find("/Player").GetComponent<Player>();
         if (GridSpaceStatus.SHEEP == player.playerRole)
        {
            SheepturnCalculator();
        }
        for (int i = 0; i < 64; i++)
        {
            // print((gridSpaces[sheep1buffer].GetGridSpaceStatus()));
            gridSpaces[i].SetSheep1OldPos((gridSpaces[sheep1buffer].GetGridSpaceStatus()));
            gridSpaces[i].SetSheep2OldPos((gridSpaces[sheep2buffer].GetGridSpaceStatus()));
            gridSpaces[i].SetSheep3OldPos((gridSpaces[sheep3buffer].GetGridSpaceStatus()));
            gridSpaces[i].SetSheep4OldPos((gridSpaces[sheep4buffer].GetGridSpaceStatus()));
            //print(gridSpaces[i].GetAITurn());
            
            if (GridSpaceStatus.WOLF == gridSpaces[i].GetGridSpaceStatus())
                {
                    foxPos = gridSpaces[i].GetGridSpaceNumber();
                    //print("****************************************" + (foxPos));
                }



            //AI
            if (GridSpaceStatus.SHEEP == player.playerRole && (gridSpaces[i].GetAITurn() == true))
            {
                print("AI IS IN TURN NOW!");
                
                
                    AIMoveWolf();
                
                   
                    //adjust new  Position and end AIturn
                    // foxPos = gridSpaces[i].GetGridSpaceNumber();
                    // if (gridSpaces[foxPos].GetAlowedGridSpaces().Count == 4)
                    // {
                    //     AIGoToStatus = gridSpaces[gridSpaces[foxPos].GetAlowedGridSpaces()[3]].GetGridSpaceStatus();
                    // }
                    for (int y = 0; y < 64; y++)
                    {
                        gridSpaces[y].SetAITurn(false);
                    }
                
            }

             if (GridSpaceStatus.WOLF == player.playerRole && (gridSpaces[i].GetAITurn() == true))
            {
                print("AISheep IS IN TURN NOW!");
                
                
                    AIMoveSheep();
                
                   
                    
                    for (int y = 0; y < 64; y++)
                    {
                        gridSpaces[y].SetAITurn(false);
                    }
                
            }

            {

                //Sheep 1,2,3,4
                if (player.playerRole == gridSpaces[i].GetGridSpaceStatus())
                {
                    
                    if(gridSpaces[i].GetAlowedGridSpacesSheep().Count == 2)
                    {

                        //sheep1
                        if (gridSpaces[i].GetAlowedGridSpacesSheep()[0] == sheep1buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[1] == sheep1buffer)
                        {
                            if ((gridSpaces[sheep1buffer].GetGridSpaceStatus()) != GridSpaceStatus.SHEEP)
                            {
                                if (gridSpaces[i].GetGridSpaceNumber() != sheep2Pos && gridSpaces[i].GetGridSpaceNumber() != sheep3Pos && gridSpaces[i].GetGridSpaceNumber() != sheep4Pos)
                                {
                                   // print("!!!!!!!!!!!SHEEpOne" + i);
                                    sheep1Pos = gridSpaces[i].GetGridSpaceNumber();

                                    sheep1buffer = gridSpaces[i].GetGridSpaceNumber();
                                    sheepList[0] = sheep1Pos;
                                }
                            }    
                        }
                        
                        // sheep2 
                        if (gridSpaces[i].GetAlowedGridSpacesSheep()[0] == sheep2buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[1] == sheep2buffer)
                        {
                            if ((gridSpaces[sheep2buffer].GetGridSpaceStatus()) != GridSpaceStatus.SHEEP)
                            {
                                if (gridSpaces[i].GetGridSpaceNumber() != sheep1Pos && gridSpaces[i].GetGridSpaceNumber() != sheep3Pos && gridSpaces[i].GetGridSpaceNumber() != sheep4Pos)
                                {
                                    //print("!!!!!!!!!!!SheepTwo" + i);
                                    sheep2Pos = gridSpaces[i].GetGridSpaceNumber();

                                    sheep2buffer = gridSpaces[i].GetGridSpaceNumber();
                                    sheepList[1] = sheep2Pos;
                                }
                            }
                        }

                        //sheep3
                        if (gridSpaces[i].GetAlowedGridSpacesSheep()[0] == sheep3buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[1] == sheep3buffer)
                        {
                            if ((gridSpaces[sheep3buffer].GetGridSpaceStatus()) != GridSpaceStatus.SHEEP)
                            {
                                if (gridSpaces[i].GetGridSpaceNumber() != sheep1Pos && gridSpaces[i].GetGridSpaceNumber() != sheep2Pos && gridSpaces[i].GetGridSpaceNumber() != sheep4Pos)
                                {
                                    //print("!!!!!!!!!!!SheepThree" + i);
                                    sheep3Pos = gridSpaces[i].GetGridSpaceNumber();

                                    sheep3buffer = gridSpaces[i].GetGridSpaceNumber();
                                    sheepList[2] = sheep3Pos;
                                }
                            }
                        }

                        //sheep4
                        if (gridSpaces[i].GetAlowedGridSpacesSheep()[0] == sheep4buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[1] == sheep4buffer)
                        {
                            if ((gridSpaces[sheep4buffer].GetGridSpaceStatus()) != GridSpaceStatus.SHEEP)
                            {
                                if (gridSpaces[i].GetGridSpaceNumber() != sheep1Pos && gridSpaces[i].GetGridSpaceNumber() != sheep2Pos && gridSpaces[i].GetGridSpaceNumber() != sheep3Pos)
                                {
                                    //print("!!!!!!!!!!!SheepFour" + i);
                                    sheep4Pos = gridSpaces[i].GetGridSpaceNumber();

                                    sheep4buffer = gridSpaces[i].GetGridSpaceNumber();
                                    sheepList[3] = sheep4Pos;
                                }
                            }
                        }
                    }
                    if (gridSpaces[i].GetAlowedGridSpacesSheep().Count == 3)
                    {

                        //sheep1
                        if (gridSpaces[i].GetAlowedGridSpacesSheep()[0] == sheep1buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[1] == sheep1buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[2] == sheep1buffer)
                        {
                                if ((gridSpaces[sheep1buffer].GetGridSpaceStatus()) != GridSpaceStatus.SHEEP)
                            {
                                if (gridSpaces[i].GetGridSpaceNumber() != sheep2Pos && gridSpaces[i].GetGridSpaceNumber() != sheep3Pos && gridSpaces[i].GetGridSpaceNumber() != sheep4Pos)
                                {
                                    //print("**SheepOne**" + i);
                                    sheep1Pos = gridSpaces[i].GetGridSpaceNumber();
                                    sheep1buffer = gridSpaces[i].GetGridSpaceNumber();
                                    sheepList[0] = sheep1Pos;
                                }
                               
                            }
                        }
                        
                        // sheep 2 
                        if (gridSpaces[i].GetAlowedGridSpacesSheep()[0] == sheep2buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[1] == sheep2buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[2] == sheep2buffer)
                        {
                           
                            if ((gridSpaces[sheep2buffer].GetGridSpaceStatus()) != GridSpaceStatus.SHEEP)
                            {
                                if (gridSpaces[i].GetGridSpaceNumber() != sheep1Pos && gridSpaces[i].GetGridSpaceNumber() != sheep3Pos && gridSpaces[i].GetGridSpaceNumber() != sheep4Pos)
                                {
                                   // print("SheepTwo" + i);
                                    sheep2Pos = gridSpaces[i].GetGridSpaceNumber();

                                    sheep2buffer = gridSpaces[i].GetGridSpaceNumber();
                                    sheepList[1] = sheep2Pos;
                                }
                            }
                               
                        }

                        //sheep3
                        if (gridSpaces[i].GetAlowedGridSpacesSheep()[0] == sheep3buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[1] == sheep3buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[2] == sheep3buffer)
                        {

                            if ((gridSpaces[sheep3buffer].GetGridSpaceStatus()) != GridSpaceStatus.SHEEP)
                            {
                                if (gridSpaces[i].GetGridSpaceNumber() != sheep1Pos && gridSpaces[i].GetGridSpaceNumber() != sheep2Pos && gridSpaces[i].GetGridSpaceNumber() != sheep4Pos)
                                {
                                    //print("SheepThree" + i);

                                    sheep3Pos = gridSpaces[i].GetGridSpaceNumber();

                                    sheep3buffer = gridSpaces[i].GetGridSpaceNumber();
                                    sheepList[2] = sheep3Pos;
                                }
                            }

                        }

                        //sheep4
                        if (gridSpaces[i].GetAlowedGridSpacesSheep()[0] == sheep4buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[1] == sheep4buffer || gridSpaces[i].GetAlowedGridSpacesSheep()[2] == sheep4buffer)
                        {

                            if ((gridSpaces[sheep4buffer].GetGridSpaceStatus()) != GridSpaceStatus.SHEEP)
                            {
                                if (gridSpaces[i].GetGridSpaceNumber() != sheep1Pos && gridSpaces[i].GetGridSpaceNumber() != sheep2Pos && gridSpaces[i].GetGridSpaceNumber() != sheep3Pos)
                                {
                                    //print("SheepFour" + (gridSpaces[sheep4buffer].GetGridSpaceStatus()));
                                    sheep4Pos = gridSpaces[i].GetGridSpaceNumber();

                                    sheep4buffer = gridSpaces[i].GetGridSpaceNumber();
                                    sheepList[3] = sheep4Pos;
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
                    print("GAMEOVER! FOXES WIN!!");
                    ResetGridSpaces();
                }
            }

            int gSpacesCounter = 0;
            for (int o = 0; o < gridSpaces[foxPos].GetAlowedGridSpaces().Count; o++)
            {
                if(gridSpaces[gridSpaces[foxPos].GetAlowedGridSpaces()[o]].GetGridSpaceStatus() == GridSpaceStatus.SHEEP){
                    gSpacesCounter++;
                }
                if(gSpacesCounter == gridSpaces[foxPos].GetAlowedGridSpaces().Count){
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
        Player player = GameObject.Find("/Player").GetComponent<Player>();
        for(int i = 0; i < 64; i++) {
            gridSpaces [i].SetGridSpaceStatus (GridSpaceStatus.EMPTY);
            gridSpaces[i].SetSheep1OldPos(GridSpaceStatus.SHEEP);
            gridSpaces[i].SetSheep2OldPos(GridSpaceStatus.SHEEP);
            gridSpaces[i].SetSheep3OldPos(GridSpaceStatus.SHEEP);
            gridSpaces[i].SetSheep4OldPos(GridSpaceStatus.SHEEP);
            gridSpaces[i].SetGridSpaceStatus(GridSpaceStatus.EMPTY);


            if(GridSpaceStatus.SHEEP == player.playerRole){
                gridSpaces[i].SetAITurn(true);
            }else{
                gridSpaces[i].SetAITurn(false);
            }
        }
        sheepList[0] = 1;
        sheepList[1] = 3;
        sheepList[2] = 5;
        sheepList[3] = 7;

        sheepListClone[0] = 1;
        sheepListClone[1] = 3;
        sheepListClone[2] = 5;
        sheepListClone[3] = 7;

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
