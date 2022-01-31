using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Game
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, isGameOver;
        int jumpSpeed;
        int score = 0;
        int force;
        int playerSpeed = 7;

        int horizontalSpeed = 5;
        int verticalSpeed = 3;

        int enemyOneSpeed = 5;
        int enemyTwoSpeed = 3;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void txt_Click_1(object sender, EventArgs e)
        {

        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;
            player.Top += jumpSpeed;
            if (goLeft == true)
            {
                player.Left -= playerSpeed;
            }

            if (goRight == true)
            {
                player.Left += playerSpeed;
            }

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height; //So player won't fall thru the platform


                            if ((string)x.Name == "horizontalPlatform" && goLeft == false || (string)x.Name == "horizontalPlatform" && goRight == false) //move the player with the horizontal platform
                            {
                                player.Left -= horizontalSpeed;
                            }

                        }
                        x.BringToFront(); //Keep the platforms in front of everything else 

                    }

                    //When player interacts with coins, increase the score and set coins invisible
                    if ((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;

                        }
                    }

                    //If the player touches enemies, stop the game
                    if ((string)x.Tag == "enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            txtScore.Text = "Score: " + score + Environment.NewLine + "You were killed!";
                            textBox.Text = "You were killed!";

                            gameTimer.Stop();
                            isGameOver = true;
                        }
                    }
                }
            }
        


            horizontalPlatform.Left -= horizontalSpeed;

            //The horizontal platform won't move out of the window, and reverse its' speed when it touches the border of the window
            if (horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }

            verticalPlatform.Top -= verticalSpeed;

            //Move the vertical platform up and down
            if (verticalPlatform.Top < 78 || verticalPlatform.Top > 500)
            {
                verticalSpeed = -verticalSpeed;
            }

            enemyOne.Left -= enemyOneSpeed;

            //Move the enemy one on the platform
            if (enemyOne.Left < pictureBox5.Left || enemyOne.Left + enemyOne.Width > pictureBox5.Left + pictureBox5.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }

            enemyTwo.Left += enemyTwoSpeed;

            //Move the enemy two on the platform
            if (enemyTwo.Left < pictureBox2.Left || enemyTwo.Left + enemyTwo.Width > pictureBox2.Left + pictureBox2.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            //If the player falls out of the game, stop the game
            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "You fell to your death";
                textBox.Text = "You fell to your death";

            }

            //If the player has collected all the coins and is at the door, stop the game
            if (player.Bounds.IntersectsWith(door.Bounds) && score == 26)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "Your quest is complete";
                textBox.Text = "Your quest is complete";

            }
            else
            {
                txtScore.Text = "Score: " + score + Environment.NewLine + "Collect all the coins";

            }
        }


        private void player_Click(object sender, EventArgs e)
        {

        }

        private void door_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void horizontalPlatform_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {

        }

        private void textBox_Click(object sender, EventArgs e)
        {

        }

        //Making it possible to move the character
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;

            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (jumping == true)
            {
                jumping = false;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();
            }
        }

        //Restarts the game and resets stats
        private void RestartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;
            txtScore.Text = "Score: " + score;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            // reset the location of player, enemies and platforms
            player.Left = 94;
            player.Top = 540;

            enemyOne.Left = 561;
            enemyTwo.Left = 381;

            horizontalPlatform.Left = 242;
            verticalPlatform.Top = 787;

            gameTimer.Start();
        }
    }
}
