using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Media;
using WMPLib;

namespace SnakeGamePlatform
{
    
    public class GameEvents:IGameEvents
    {
        //Define game variables here! for example...
        GameObject[] snake;
        TextLabel lblScore;
        GameObject right, left, down, up;
        int snakeSize;

        //This function is called by the game one time on initialization!
        //Here you should define game board resolution and size (x,y).
        //Here you should initialize all variables defined above and create all visual objects on screen.
        //You could also start game background music here.
        //use board Object to add game objects to the game board, play background music, set interval, etc...
        public int SnakeSize(GameObject[] arr)
        {
            int snakeSize = 0;
            while (arr[snakeSize] != null)
            {
                snakeSize++;
            }
            return snakeSize;
        }
        public void GameInit(Board board)
        {

            snake = new GameObject[24000];
            snakeSize = SnakeSize(snake);

            //Setup board size and resolution!
            Board.resolutionFactor = 1;
            board.XSize = 800;
            board.YSize = 800;

            //Adding a text label to the game board.
            Position labelPosition = new Position(50, 20);
            lblScore = new TextLabel("מספר הנקודות:", labelPosition);
            lblScore.SetFont("Ariel", 14);
            board.AddLabel(lblScore);


            //Adding Game Object
            Position snakePosition = new Position(300, 300);
            snake[0] = new GameObject(snakePosition, 20, 20);
            snake[0].SetImage(Properties.Resources.food);
            snake[0].direction = GameObject.Direction.RIGHT;
            board.AddGameObject(snake[0]);

            //artificial snake
            Position snakePosition1 = new Position(320, 300);
            snake[1] = new GameObject(snakePosition1, 20, 20);
            snake[1].SetImage(Properties.Resources.food);
            board.AddGameObject(snake[1]);

            Position snakePosition2 = new Position(340, 300);
            snake[2] = new GameObject(snakePosition2, 20, 20);
            snake[2].SetImage(Properties.Resources.food);
            board.AddGameObject(snake[2]);

            Position snakePosition3 = new Position(360, 300);
            snake[3] = new GameObject(snakePosition3, 20, 20);
            snake[3].SetImage(Properties.Resources.food);
            board.AddGameObject(snake[3]);


            //adding misgeret
            Position boarddown = new Position(740,0) ;
            down = new GameObject(boarddown, 800, 25);
            down.SetBackgroundColor(Color.Black);
            board.AddGameObject(down);

            Position boardup = new Position(175, 0);
            up = new GameObject(boardup, 800, 25);
            board.AddGameObject(up);
            up.SetBackgroundColor(Color.Black);

            Position boardright = new Position(200, 760);
            right = new GameObject(boardright, 25, 600);
            board.AddGameObject(right);
            right.SetBackgroundColor(Color.Black);

            Position boardleft = new Position(200, 0);
            left = new GameObject(boardleft, 25, 600);
            board.AddGameObject(left);
            left.SetBackgroundColor(Color.Black);

            //Play file in loop!
            board.PlayBackgroundMusic(@"\Images\gameSound.wav");
            //Play file once!
            board.PlayShortMusic(@"\Images\eat.wav");


            //Start game timer!
            board.StartTimer(50);
        }
        
        
        //This function is called frequently based on the game board interval that was set when starting the timer!
        //Use this function to move game objects and check collisions
        public void GameClock(Board board)
        {
            for (int i = 1; i < snakeSize; i++)
            {
                int p = snakeSize;
                snake[p - 1].SetPosition(snake[p].GetPosition());
                p--;
            }

            Position snakePosition = snake[0].GetPosition();
            if (snake[0].direction == GameObject.Direction.RIGHT)
                snakePosition.Y = snakePosition.Y + 5;
            if (snake[0].direction == GameObject.Direction.LEFT)
                snakePosition.Y = snakePosition.Y - 5;
            if (snake[0].direction == GameObject.Direction.DOWN)
                snakePosition.X = snakePosition.X + 5;
            if (snake[0].direction == GameObject.Direction.UP)
                snakePosition.X = snakePosition.X - 5;


            snake[0].SetPosition(snakePosition);
        }

        //This function is called by the game when the user press a key down on the keyboard.
        //Use this function to check the key that was pressed and change the direction of game objects acordingly.
        //Arrows ascii codes are given by ConsoleKey.LeftArrow and alike
        //Also use this function to handle game pause, showing user messages (like victory) and so on...
        public void KeyDown(Board board, char key)
        {
            if (key == (char)ConsoleKey.LeftArrow)
                snake[0].direction = GameObject.Direction.LEFT;
            if (key == (char)ConsoleKey.RightArrow)
                snake[0].direction = GameObject.Direction.RIGHT;
            if (key == (char)ConsoleKey.UpArrow)
                snake[0].direction = GameObject.Direction.UP;
            if (key == (char)ConsoleKey.DownArrow)
                snake[0].direction = GameObject.Direction.DOWN;
        }
    }
}
