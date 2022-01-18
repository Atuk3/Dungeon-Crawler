using System;
using System.IO;
using System.Text;
using static System.Console;
using System.Linq;
using System.Collections.Generic;

namespace Crawler
{
    /**
     * The main class of the Dungeon Crawler Application
     * 
     * You may add to your project other classes which are referenced.
     * Complete the templated methods and fill in your code where it says "Your code here".
     * Do not rename methods or variables which already exist or change the method parameters.
     * You can do some checks if your project still aligns with the spec by running the tests in UnitTest1
     * 
     * For Questions do contact us!
     */
    public class CMDCrawler
    {
        /**
         * use the following to store and control the next movement of the yser
         */
        public enum PlayerActions {NOTHING, NORTH, EAST, SOUTH, WEST, PICKUP, ATTACK, QUIT };
        private PlayerActions action = PlayerActions.NOTHING;
       
        
        private char[][] originMap = new char[0][]; //making originMap global so it can be used in other methods//
        private char[][] currentMap = new char[0][];//making currentMap global so it can be used in other methods//
        private int Damage = 0;
        private bool gameRun = false;// to show if game actually runs
        private int Kills = 0;
        private int Gold = 0;
        
        

        

        /**
         * tracks if the game is running
         */
        private bool active = true;//made it true to make the game start runnung//

        /**
         * Reads user input from the Console
         * 
         * Please use and implement this method to read the user input.
         * 
         * Return the input as string to be further processed
         * 
         */
        private string ReadUserInput()
        {
            string inputRead = string.Empty;
           
            
                inputRead = Console.ReadLine(); //reads the user input and stores it in inputRead
           
            
            // Your code here
            
            return inputRead;
        }

        /**
         * Processed the user input string
         * 
         * takes apart the user input and does control the information flow
         *  * initializes the map ( you must call InitializeMap)
         *  * starts the game when user types in Play
         *  * sets the correct playeraction which you will use in the GameLoop
         */
        public void ProcessUserInput(string input)
        {
            // Your Code here
            input = input.ToLower(); // converts all user input to small letters 
            if (input == "load simple.map")
            {
                Console.WriteLine("Loading Map");
                InitializeMap("Simple.map"); // called initialise map and carries out the function on simple.map
            }
            if (input == "load advanced.map")
            {
                Console.WriteLine("Loading Map");
                InitializeMap("Advanced.map"); // called initialise map and carries out the function on advanced.map
            }
            if (input == "play")
            {
                Console.WriteLine("Start");
                gameRun = true; //made gameRun true
            }
            if (gameRun == true)
            {
                if (input == "d" || input == "D")
                {
                    action = PlayerActions.EAST;
                }
                else if (input == "w" || input == "W")
                {
                    action = PlayerActions.NORTH;
                }
                else if (input == "a" || input == "A")
                {
                    action = PlayerActions.WEST;
                }
                else if (input == "s" || input == "S")
                {
                    action = PlayerActions.SOUTH;
                }

                else if (input == "e" || input == "E")
                {
                    action = PlayerActions.PICKUP;
                }
                else if (input == " ") // input = Spacebar
                {
                    action = PlayerActions.ATTACK;
                }
                else if (input == "q" || input == "Q")
                {
                    action = PlayerActions.QUIT;
                }
                else
                {
                    action = PlayerActions.NOTHING;
                }
            }
            else { action = PlayerActions.NOTHING; }

            
        }


        /**
         * The Main Game Loop. 
         * It updates the game state.
         * 
         * This is the method where you implement your game logic and alter the state of the map/game
         * use playeraction to determine how the character should move/act
         * the input should tell the loop if the game is active and the state should advance
         */
        public void GameLoop(bool active)
        {


            // Your code here
            try
            {
               
                if (gameRun == true)
                {
                    int[] playerPosition = GetPlayerPosition();// created a new int that stores the players position
                    //Console.Clear();
                 
                    if (action == PlayerActions.NORTH && currentMap[playerPosition[1] - 1][playerPosition[0]] != '#' && currentMap[playerPosition[1] - 1][playerPosition[0]] != 'M')//if moving up is not # or M 
                    {
                        

                        if (currentMap[playerPosition[1] - 1][playerPosition[0]] == 'E')// if moving up = E
                        {
                            currentMap[playerPosition[1]][playerPosition[0]] = '.';// replace the position the player left with "."
                            currentMap[playerPosition[1] - 1][playerPosition[0]] = '@';
                            action = PlayerActions.QUIT;// made action quit to end game
                            gameRun = false;// made gameRun false to stop the game from running
                        }
                        else
                        {
                            currentMap[playerPosition[1]][playerPosition[0]] = '.';// replace the position the player left with "."
                            currentMap[playerPosition[1] - 1][playerPosition[0]] = '@'; // replace the position the player went to with "@"

                        }


                    }
                    else if (action == PlayerActions.SOUTH && currentMap[playerPosition[1] + 1][playerPosition[0]] != '#' && currentMap[playerPosition[1] + 1][playerPosition[0]] != 'M')//if moving down is not # or M 
                    {
                       
                        if (currentMap[playerPosition[1] + 1][playerPosition[0]] == 'E')// if moving down = E
                        {

                            currentMap[playerPosition[1]][playerPosition[0]] = '.';// replace the position the player left with "."
                            currentMap[playerPosition[1] + 1][playerPosition[0]] = '@';// replace the position the player went to with "@"
                            action = PlayerActions.QUIT;// made action quit to end game
                        }
                        else
                        {

                            currentMap[playerPosition[1]][playerPosition[0]] = '.';// replace the position the player left with "."
                            currentMap[playerPosition[1] + 1][playerPosition[0]] = '@';// replace the position the player went to with "@"
                        }

                    }
                    else if (action == PlayerActions.EAST && currentMap[playerPosition[1]][playerPosition[0] + 1] != '#' && currentMap[playerPosition[1] + 1][playerPosition[0] + 1] != 'M')//if moving right is not # or M 
                    {
                       
                        if (currentMap[playerPosition[1]][playerPosition[0] + 1] == 'E')// if moving right = E
                        {
                            currentMap[playerPosition[1]][playerPosition[0]] = '.';// replace the position the player left with "."
                            currentMap[playerPosition[1]][playerPosition[0] + 1] = '@';// replace the position the player went to with "@"
                            action = PlayerActions.QUIT;// made action quit to end game
                        }
                        else
                        {
                            currentMap[playerPosition[1]][playerPosition[0]] = '.';
                            currentMap[playerPosition[1]][playerPosition[0] + 1] = '@';// replace the position the player went to with "@"
                        }
                    }
                    else if (action == PlayerActions.WEST && currentMap[playerPosition[1]][playerPosition[0] - 1] != '#' && currentMap[playerPosition[1]][playerPosition[0] - 1] != 'M')//if moving left is not # or M 
                    {
                       
                        if (currentMap[playerPosition[1]][playerPosition[0] - 1] == 'E')// if moving left = E
                        {
                            currentMap[playerPosition[1]][playerPosition[0]] = '.';// replace the position the player left with "."
                            currentMap[playerPosition[1]][playerPosition[0] - 1] = '@';// replace the position the player went to with "@"
                            action = PlayerActions.QUIT;// made action quit to end game
                        }
                        else
                        {

                            currentMap[playerPosition[1]][playerPosition[0]] = '.';// replace the position the player left with "."
                            currentMap[playerPosition[1]][playerPosition[0] - 1] = '@';// replace the position the player went to with "@"
                        }
                        
                      

                    }
                    else if (action == PlayerActions.ATTACK)
                    {
                        if (currentMap[playerPosition[1] - 1][playerPosition[0]] == 'M')// if moving up = M
                        {
                            currentMap[playerPosition[1] - 1][playerPosition[0]] = '@';// replace the position the player went to with "@"
                            currentMap[playerPosition[1]][playerPosition[0]] = '.';// replace the position the player left with "."
                            Damage = Damage - 1;// for every monster do damage of 1
                            if (Damage <= 0)
                            {
                                
                                Console.WriteLine("Damage done:" + Damage);
                            }
                            Kills = Kills + 1;
                        }
                        if (currentMap[playerPosition[1] + 1][playerPosition[0]] == 'M')// if moving down = M
                        {
                            currentMap[playerPosition[1] + 1][playerPosition[0]] = '@';
                            currentMap[playerPosition[1]][playerPosition[0]] = '.';
                            Damage = Damage - 1;// for every monster do damage of 1
                            if (Damage <= 0)
                            {
                                
                                Console.WriteLine("Damage done:"+ Damage);
                            }
                            Kills = Kills + 1;

                        }
                        if (currentMap[playerPosition[1]][playerPosition[0]-1] == 'M')// if moving left = M
                        {
                            currentMap[playerPosition[1]][playerPosition[0]-1] = '@';
                            currentMap[playerPosition[1]][playerPosition[0]] = '.';
                            Damage = Damage - 1;// for every monster do damage of 1
                            if (Damage <= 0)
                            {
                               
                                
                                Console.WriteLine("Damage done:"+ Damage);
                            }
                            Kills = Kills + 1;
                        }
                        if (currentMap[playerPosition[1]][playerPosition[0]+1] == 'M')// if moving right = M
                        {
                            currentMap[playerPosition[1]][playerPosition[0]+1] = '@';
                            currentMap[playerPosition[1]][playerPosition[0]] = '.';
                            Damage = Damage - 1;// for every monster do damage of 1
                            if (Damage <= 0)
                            {
                                Console.WriteLine("Damage done:" + Damage);
                            }
                            
                            Kills = Kills + 1;

                        }
                    }
                    else if(action == PlayerActions.PICKUP)
                    {
                        if (currentMap[playerPosition[1] - 1][playerPosition[0]] == 'G')
                        {
                            currentMap[playerPosition[1] - 1][playerPosition[0]] = '@';
                            currentMap[playerPosition[1]][playerPosition[0]] = '.';
                            Gold = Gold + 1;// for every Gold add 1


                        }
                        if (currentMap[playerPosition[1] + 1][playerPosition[0]] == 'G')
                        {
                            currentMap[playerPosition[1] + 1][playerPosition[0]] = '@';
                            currentMap[playerPosition[1]][playerPosition[0]] = '.';

                            Gold = Gold + 1;// for every Gold add 1

                        }
                        if (currentMap[playerPosition[1]][playerPosition[0] - 1] == 'G')
                        {
                            currentMap[playerPosition[1]][playerPosition[0] - 1] = '@';
                            currentMap[playerPosition[1]][playerPosition[0]] = '.';
                            Gold = Gold + 1;// for every Gold add 1
                        }
                        if (currentMap[playerPosition[1]][playerPosition[0] + 1] == 'G')// if next player position =G
                        {
                            currentMap[playerPosition[1]][playerPosition[0] + 1] = '@';
                            currentMap[playerPosition[1]][playerPosition[0]] = '.';
                            Gold = Gold + 1;// for every Gold add 1
                        }

                        }
                    else if (action == PlayerActions.QUIT)// if input = q
                    {
                        Console.WriteLine("Game ended");
                    }
                    GetCurrentMapState();


                }

            }
            catch (IOException)
            {
                Console.WriteLine("Movement not rendering");
            }






        }

        /**
        * Map and GameState get initialized
        * mapName references a file name 
        * 
        * Create a private object variable for storing the map in Crawler and using it in the game.
        */
        public bool InitializeMap(String mapName)
        {
            bool initSuccess = false;

            // Your code here

            string path = @"maps/" + mapName; //getting the string path of the map
            
            List<string> map = new List<string>();// creating a map list
            map = File.ReadAllLines(path).ToList();// converting all lines to a list and reading all the lines of path into map

            currentMap = map.Select(a => a.ToArray()).ToArray();// converting map into an array 
            originMap = map.Select(a => a.ToArray()).ToArray();// converting map into an array
           
            initSuccess = true;
            for (int y = 0; y < currentMap.Length; y++)// loop to check for S and to make it '@' instead
            {
                for (int x = 0; x < currentMap[y].Length; x++)
                {
                    if (currentMap[y][x] == 'S')
                    {
                        currentMap[y][x] ='@';
                        originMap[y][x] ='@';
                        
                    }
                }
            }


            return initSuccess;
        }

        /**
         * Returns a representation of the currently loaded map
         * before any move was made.
         */
        public char[][] GetOriginalMap()
        {

            char[][] map = new char[0][];

            // Your code here
            for (int y = 0; y < originMap.Length; y++)
            {
                for (int x = 0; x < originMap[y].Length; x++)
                {

                   
                   
                    Console.Write(originMap[y][x]);// loop through original map and displayed the original map
                }
                Console.WriteLine();
            }


            return originMap;
        }

        /*
         * Returns the current map state 
         * without altering it 
         */
        public char[][] GetCurrentMapState()
        {
            // the map should be map[y][x]
            char[][] map = new char[0][];
            

            // Your code here
            for (int y = 0; y < currentMap.Length; y++)
            {
                for (int x = 0; x < currentMap[y].Length; x++) 
                {
                    Console.Write(currentMap[y][x]);// loop through the current map and displayed the current map
                }
                Console.WriteLine();
            }
             Console.WriteLine("Gold :" + Gold);// displays the amount of Gold collected
            Console.WriteLine("Monsters killed :" + Kills);// displays the amount of monsters killed


            return currentMap;
        }

        /**
         * Returns the current position of the player on the map
         * 
         * The first value is the x corrdinate and the second is the y coordinate on the map
         */
        public int[] GetPlayerPosition()
        {
            int[] position = { 0, 0 };

            // Your code here
            for (int y = 0; y < currentMap.Length; y++)
            {
                for (int x = 0; x < currentMap[y].Length; x++)
                {
                    if (currentMap[y][x] == '@')// loop through current map and look for '@' to find out where it is located in both the x and the y axis
                    {
                        position[0] = x;
                        position[1] = y;
                    }
                }
            }
           

            return position;
        }

        /**
        * Returns the next player action
        * 
        * This method does not alter any internal state
        */
        public int GetPlayerAction()
        {
           

            // Your code here
         
            
            return (int)action;// made action return an integer
        }


        public bool GameIsRunning()
        {
            bool running = false;
            // Your code here 
            if (gameRun == true)
            {
                
                running = true; 
                active = true;// active= true if gameRun is true

            }
            if(active == false)
            {
                running = false;
            }
            

            return running;
        }

        /**
         * Main method and Entry point to the program
         * ####
         * Do not change! 
        */
        static void Main(string[] args)
        {
            CMDCrawler crawler = new CMDCrawler();
            string input = string.Empty;
            Console.WriteLine("Welcome to the Commandline Dungeon!" +Environment.NewLine+ 
                "May your Quest be filled with riches!"+Environment.NewLine);
            
            // Loops through the input and determines when the game should quit
            while (crawler.active && crawler.action != PlayerActions.QUIT)
            {
                Console.WriteLine("Your Command: ");
                input = crawler.ReadUserInput();
                Console.WriteLine(Environment.NewLine);

                crawler.ProcessUserInput(input);
            
                crawler.GameLoop(crawler.active);
            }

            Console.WriteLine("See you again" +Environment.NewLine+ 
                "In the CMD Dungeon! ");


        }


    }
}
