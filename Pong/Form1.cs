using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Pong
{
    public partial class Form1 : Form
    {
        Rectangle middleLine = new Rectangle(0, 195, 500, 5);
        Rectangle goal1 = new Rectangle(170, 0, 80, 10);
        Rectangle goal2 = new Rectangle(170, 390, 80, 10);
        Rectangle player1 = new Rectangle(180, 20, 20, 20);
        Rectangle player2 = new Rectangle(180, 380, 20, 20);
        Rectangle ball = new Rectangle(295, 195, 10, 10);


        int player1Score = 0;
        int player2Score = 0;

        int playerSpeed = 5;
        int ballXSpeed = 4;
        int ballYSpeed = -4;

        int originalBallX = 0;
        int originalBallY = 0;

        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool rightArrowDown = false;
        bool leftArrowDown = false;

        Random randGen = new Random();

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        Pen whitePen = new Pen(Color.White, 5);

        SoundPlayer goal = new SoundPlayer(Properties.Resources.goal);
        SoundPlayer hit = new SoundPlayer(Properties.Resources.hit);



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(whitePen, 110, -80, 200, 150);
            e.Graphics.DrawEllipse(whitePen, 110, 330, 200, 150);
            e.Graphics.FillRectangle(whiteBrush, middleLine);
            e.Graphics.FillRectangle(redBrush, goal1);
            e.Graphics.FillRectangle(redBrush, goal2);
            e.Graphics.FillRectangle(greenBrush, player1);
            e.Graphics.FillRectangle(blueBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ball.X += ballXSpeed;
            ball.Y += ballYSpeed;
         
            if (player2.IntersectsWith(ball) == false || player1.IntersectsWith(ball) == false)
            {
                originalBallX = ball.X;
                originalBallY = ball.Y;
            }

            // player1 movement
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
            }
            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }
            if (aDown == true && player1.X > 0)
            {
                player1.X -= playerSpeed;
            }
            if (dDown == true && player1.X < this.Width - player1.Width)
            {
                player1.X += playerSpeed;
            }

            // player2 movement
            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
            }
            if (downArrowDown == true && player2.Y < this.Height - player1.Height)
            {
                player2.Y += playerSpeed;
            }
            if (leftArrowDown == true && player2.X > 0)
            {
                player2.X -= playerSpeed;
            }
            if (rightArrowDown == true && player2.X < this.Width - player1.Width)
            {
                player2.X += playerSpeed;
            }

            //check  if  ball  hit  top  or  bottom  wall  
            //check  if  ball  hit  side  walls  
            if (ball.Y < 0 || ball.Y > this.Height - ball.Height)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed; 
            }
            if (ball.X > this.Width - ball.Width)
            {
                ballXSpeed *= -1;  // or: ballYSpeed = -ballYSpeed; 
            }
            if (ball.X < 0 || ball.X > this.Width - ball.Width)
            {
                ballXSpeed *= -1;
            }
            if (ball.X > this.Width - ball.Width)
            {
                ballXSpeed *= -1;
            }

            //check if ball hits the goal
            if (ball.IntersectsWith(goal1) == true)
            {
                goal.Play();
                ball.X = 195;
                ball.Y = 295;

                player1.X = 180;
                player1.Y = 20;

                player2.X = 180;
                player2.Y = 380;

                player2Score += 1;
                p2ScoreLabel.Text = player2Score + "";
            }
            if (ball.IntersectsWith(goal2) == true)
            {
                goal.Play();
                ball.X = 195;
                ball.Y = 295;

                player1.X = 180;
                player1.Y = 20;

                player2.X = 180;
                player2.Y = 380;

                player1Score += 1;
                p1ScoreLabel.Text = player1Score + "";
            }
            //check  if  ball  hits  either  player           
            if (player2.IntersectsWith(ball) == true)
            {
                hit.Play();
                // above
                if (originalBallY > player2.Y)
                {
                    ball.Y = player2.Y + player2.Height;
                    ballYSpeed *= -1;
                    ballXSpeed = randGen.Next(-5, 5);
                }
                // below 
                else if (originalBallY < player2.Y)
                {
                    ball.Y = player2.Y - player2.Height;
                    ballYSpeed *= -1;
                    ballXSpeed = randGen.Next(-5, 5);
                }
                // on right side
                else if (originalBallX > player2.X)
                {
                    ball.X = player2.X + player2.Width;
                    ballXSpeed *= -1;
                    ballYSpeed = randGen.Next(-5, 5);
                }
                // on left side  
                else if (originalBallX < player2.X)
                {
                    ball.X = player2.X - player2.Width;
                    ballXSpeed *= -1;
                    ballYSpeed = randGen.Next(-5, 5);
                }
            }
            if (player1.IntersectsWith(ball))
            {
                hit.Play();
                // above
                if (originalBallY > player1.Y)
                {
                    ball.Y = player1.Y + player1.Height;
                    ballYSpeed *= -1;
                    ballXSpeed = randGen.Next(-5, 5);
                }
                // below 
                else if (originalBallY < player1.Y)
                {
                    ball.Y = player1.Y - player1.Height;
                    ballYSpeed *= -1;
                    ballXSpeed = randGen.Next(-5, 5);
                }
                // on right side
                else if (originalBallX > player1.X)
                {
                    ball.X = player1.X + player1.Width;
                    ballXSpeed *= -1;
                    ballYSpeed = randGen.Next(-5, 5);
                }
                // on left side  
                else if (originalBallX < player1.X)
                {
                    ball.X = player1.X - player1.Width;
                    ballXSpeed *= -1;
                    ballYSpeed = randGen.Next(-5, 5);
                }
            }
            // stop  game  if  a  player  is  at  3 
            if (player1Score == 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 1 Wins!!";
            }
            else if (player2Score == 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 2 Wins!!";
            }

            Refresh();
        }
    }
}
