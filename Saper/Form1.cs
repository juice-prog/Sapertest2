using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NfSaper6



{
    public partial class NFSaper6 : Form
    {
        Game game;
        Settings settings;

        public NFSaper6()
        {
            InitializeComponent();
            SetColor();

                 settings = new Settings(9, 9, 9);
                     game = new NfSaper6.Game(pictureBox1, settings, this);
               

            }

            private void SetColor()
        {
            this.BackColor = System.Drawing.Color.White;
        }

            private void button1_Click(object sender, EventArgs e)
        {
        }


            private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
                 if (game.gameOngoing == false)
                return;

                    if(e.Button == MouseButtons.Right)
            {
                Input.BombFieldSetFlag(Convert.ToInt32(e.X), Convert.ToInt32(e.Y), game);
                    }
            else if(e.Button == MouseButtons.Left)
                {
                Input.BombFieldClicked(Convert.ToInt32(e.X), Convert.ToInt32(e.Y), game);
            }
            else if(e.Button == MouseButtons.Middle)
            {
            }



                    if (game.gameLost == true)
                labelFlags.Text = "Ты проиграл!";
                     else if (game.gameWin == true)
                labelFlags.Text = "Ты выиграл";
            else
                labelFlags.Text = "Flag: " + Convert.ToString(game.field.flagNumber) + "/" + Convert.ToString(game.field.bombNumber);

            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            game.renderAll(e.Graphics);
        }

            private void pictureBox1_CursorChanged(object sender, EventArgs e)
            {
            
            }

          private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            



                    if(Convert.ToInt32(e.X) / game.field.squarePixelSize != game.mouseX || Convert.ToInt32(e.Y) / game.field.squarePixelSize != game.mouseY)
            {
                Input.MouseOnUnvisibleField(Convert.ToInt32(e.X), Convert.ToInt32(e.Y), game);
                  pictureBox1.Refresh();
              }
           }

          private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
                 game.MouseLeaveArea();
                pictureBox1.Refresh();
        }

                    private void timerGame_Tick(object sender, EventArgs e)
                 {
                     if(game.gameOngoing == true)
            {
  


                game.gameTime.AddSeconds(1);

                labelTime.Text = game.gameTime.ToString();
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            game = new Game(pictureBox1, settings, this);
            game.StartGame();
            timerGame.Start();

               buttonReset.Enabled = true;


            buttonStart.Enabled = false;

             labelFlags.Text = "Flag: " + Convert.ToString(game.field.flagNumber) + "/" + Convert.ToString(game.field.bombNumber);

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = true;
            buttonReset.Enabled = false;

            game = new Game(pictureBox1, settings, this);
            labelTime.Text = game.gameTime.ToString();

            labelFlags.Text = "Flag: " + Convert.ToString(game.field.flagNumber) + "/" + Convert.ToString(game.field.bombNumber);
            Refresh();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings(settings, game, pictureBox1);
            formSettings.Show();
            buttonStart.Enabled = true;
            buttonReset.Enabled = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.Show();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void labelFlags_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
