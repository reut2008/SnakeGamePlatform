using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Media;
using WMPLib;
using System.Runtime.InteropServices;

namespace SnakeGamePlatform
{
    
    public class GameEvents:IGameEvents
    {
        //Define game variables here! for example...
        GameObject[] snake, misgeret;
        TextLabel lblScore, score;
        GameObject right, left, down, up, food;
        int snakeSize, snakeSpeed, points;
        bool gameEnded;

        //This function is called by the game one time on initialization!
        //Here you should define game board resolution and size (x,y).
        //Here you should initialize all variables defined above and create all visual objects on screen.
        //You could also start game background music here.
        //use board Object to add game objects to the game board, play background music, set interval, etc...
        public void Lose(GameObject[] snake, GameObject[] misgeret, Board Board, int snakeSize, Board board)
        {
            if(snake[0].IntersectWith(misgeret[0]) || snake[0].IntersectWith(misgeret[1]) || snake[0].IntersectWith(misgeret[2]) || snake[0].IntersectWith(misgeret[3]))
            {
               Board.StopTimer();
                gameEnded = true;
            }
            for (int i=1; i < snakeSize; i++)
            {
                if (snake[0].IntersectWith(snake[i]))
                {
                    Board.StopTimer();
                    gameEnded = true;
                }
            }
            if (gameEnded)
            {
                board.PlayBackgroundMusic(@"\Images\");
                board.PlayShortMusic(@"\Images\lose.wav");
            }

        }
        public void Points ()
        {
                points++;

            score.SetText(points.ToString());
        }


        //מוסיף אוכל במקום רנדומלי ומוחק את האוכל הקודם
        public void AddFood (Board board)
        {
            if (food != null)
            {
                board.RemoveGameObject(food);
            }
            Random rnd = new Random();
            int y = rnd.Next(25,750);
            int x = rnd.Next(200, 755);
            
            Position snakePosition = new Position(x, y);
            food = new GameObject(snakePosition, 20, 20);
            food.SetImage(Properties.Resources.food);
            board.AddGameObject(food);
        }

        public void AddSnake(Board board, GameObject[] snake)
        {
            
            Position pos = snake[snakeSize - 1].GetPosition();
            snake[snakeSize] = new GameObject(pos, 20, 20);
            snake[snakeSize].SetImage(Properties.Resources.food);
            board.AddGameObject(snake[snakeSize]);

            snakeSize++;
        }
        public void ChangeSpeed (Board board)
        {
            if (snakeSpeed > 0)
            {
                snakeSpeed -= 5;
                board.StopTimer();
                board.StartTimer(snakeSpeed);
            }

        }
        public void Reset (Board board)
        {
            board.StopTimer();
            for (int i = 0; i < snakeSize; i++)
            {
                board.RemoveGameObject(snake[i]);
                snake[i] = null;
            }
            board.RemoveGameObject(food);
            points = 0;
            snakeSpeed = 181;
            board.PlayBackgroundMusic(@"\Images\backgroundmusic.mp4");
            GameInit(board);
        }
        
        public void GameInit(Board board)
        {
            points = 0;
            snakeSpeed = 181;
            AddFood(board);
            snake = new GameObject[24000];
            misgeret = new GameObject[4];

            //Setup board size and resolution!
            Board.resolutionFactor = 1;
            board.XSize = 800;
            board.YSize = 800;

            //Adding a text label to the game board.
            Position labelPosition = new Position(50, 20);
            lblScore = new TextLabel("מספר הנקודות:", labelPosition);
            lblScore.SetFont("Ariel", 14);
            board.AddLabel(lblScore);

            Position pointsPosition = new Position(75, 20);
            score = new TextLabel("0", pointsPosition);
            score.SetFont("Ariel", 14);
            board.AddLabel(score);



            //Adding Game Object
            Position snakePosition = new Position(300, 300);
            snake[0] = new GameObject(snakePosition, 20, 20);
            snake[0].SetImage(Properties.Resources.food);
            snake[0].direction = GameObject.Direction.RIGHT;
            board.AddGameObject(snake[0]);
            snakeSize = 1;
            #region artificial snake
            //Position snakePosition1 = new Position(320, 300);
            //snake[1] = new GameObject(snakePosition1, 20, 20);
            //snake[1].SetImage(Properties.Resources.food);
            //board.AddGameObject(snake[1]);

            //Position snakePosition2 = new Position(340, 300);
            //snake[2] = new GameObject(snakePosition2, 20, 20);
            //snake[2].SetImage(Properties.Resources.food);
            //board.AddGameObject(snake[2]);

            //Position snakePosition3 = new Position(360, 300);
            //snake[3] = new GameObject(snakePosition3, 20, 20);
            //snake[3].SetImage(Properties.Resources.food);
            //board.AddGameObject(snake[3]);

            //Position snakePosition4 = new Position(360, 300);
            //snake[4] = new GameObject(snakePosition3, 20, 20);
            //snake[4].SetImage(Properties.Resources.food);
            //board.AddGameObject(snake[4]);

            //Position snakePosition5 = new Position(360, 300);
            //snake[5] = new GameObject(snakePosition3, 20, 20);
            //snake[5].SetImage(Properties.Resources.food);
            //board.AddGameObject(snake[5]);

            //Position snakePosition6 = new Position(360, 300);
            //snake[6] = new GameObject(snakePosition3, 20, 20);
            //snake[6].SetImage(Properties.Resources.food);
            //board.AddGameObject(snake[6]);

            //Position snakePosition7 = new Position(360, 300);
            //snake[7] = new GameObject(snakePosition3, 20, 20);
            //snake[7].SetImage(Properties.Resources.food);
            //board.AddGameObject(snake[7]);

            //Position snakePosition8 = new Position(360, 300);
            //snake[8] = new GameObject(snakePosition3, 20, 20);
            //snake[8].SetImage(Properties.Resources.food);
            //board.AddGameObject(snake[8]);

            //snakeSize =9;
            #endregion

            //adding misgeret array

            Position boarddown = new Position(775,0) ;
            misgeret[0] = new GameObject(boarddown, 800, 25);
            misgeret[0].SetBackgroundColor(Color.Black);
            board.AddGameObject(misgeret[0]);

            Position boardup = new Position(175, 0);
            misgeret[1] = new GameObject(boardup, 800, 25);
            board.AddGameObject(misgeret[1]);
            misgeret[1].SetBackgroundColor(Color.Black);

            Position boardright = new Position(200, 775);
            misgeret[2] = new GameObject(boardright, 25, 600);
            board.AddGameObject(misgeret[2]);
            misgeret[2].SetBackgroundColor(Color.Black);

            Position boardleft = new Position(200, 0);
            misgeret[3] = new GameObject(boardleft, 25, 600);
            board.AddGameObject(misgeret[3]);
            misgeret[3].SetBackgroundColor(Color.Black);

            //Play file in loop!
            board.PlayBackgroundMusic(@"\Images\backgroundmusic.mp4");


            //Start game timer!
            board.StartTimer(snakeSpeed);
        }
        
        
        //This function is called frequently based on the game board interval that was set when starting the timer!
        //Use this function to move game objects and check collisions
        public void GameClock(Board board)
        {
            int p = snakeSize-1;
            for (int i = 1; i < snakeSize; i++)
            {
                
                snake[p].SetPosition(snake[p-1].GetPosition());
                p--;
            }

            Position snakePosition = snake[0].GetPosition();
            if (snake[0].direction == GameObject.Direction.RIGHT)
                snakePosition.Y = snakePosition.Y + 20;
            if (snake[0].direction == GameObject.Direction.LEFT)
                snakePosition.Y = snakePosition.Y - 20;
            if (snake[0].direction == GameObject.Direction.DOWN)
                snakePosition.X = snakePosition.X + 20;
            if (snake[0].direction == GameObject.Direction.UP)
                snakePosition.X = snakePosition.X - 20;


            snake[0].SetPosition(snakePosition);
            
            Lose(snake, misgeret, board, snakeSize, board);
            if (food.OnScreen(board)) { }
            else
            {
                AddFood(board);
            }
            if (snake[0].IntersectWith(food))
            {
                AddFood(board);
                AddSnake(board, snake);
                ChangeSpeed(board);
                Points();
            }

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

            if (gameEnded == true)
            {
                if (key == (char)ConsoleKey.Spacebar)
                {
                    Reset(board);
                }
            }
        }
    }
}
