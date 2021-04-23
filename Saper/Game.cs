using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

using System.Diagnostics;


namespace NfSaper6
{
    public class Game
    {
        public Field field { get; set; }


        public int mouseX { get; set; }
        public int mouseY { get; set; }

        public bool gameOngoing { get; set; }
          public bool gameLost { get; set; }
          public bool gameWin { get; set; }

           public Time gameTime { get; set; }


        public Game(PictureBox picture, Settings settings, Form form)
        {
            form.Size = new Size(27 + picture.Location.X + settings.fieldXSize * 20, 50 + picture.Location.Y + settings.fieldYSize * 20);
            Render.setPanelSize(20, settings.fieldXSize, settings.fieldYSize, picture);
            field = new Field(settings.fieldXSize, settings.fieldYSize, 20, settings.bombNumbers);
            mouseX = -1;
            mouseY = -1;

            gameOngoing = false;
            gameLost = false;
            gameWin = false;

            gameTime = new Time(0,0,0);
        }

        public void MouseLeaveArea()
        {
            this.mouseX = -1;
            this.mouseY = -1;
        }

        public void StartGame()
        {
            gameOngoing = true;
            gameLost = false;
            gameWin = false;
        }

        public void LostGame()
        {
            gameOngoing = false;
            gameLost = true;
        }

        public void CheckWin()
        {
            if (field.xFields * field.yFields - field.visibleFields == field.bombNumber)
            {
                Console.WriteLine("Победа!");
                field.AllFieldsVisible();
                gameOngoing = false;
                gameWin = true;
            } 
        }

        public void renderAll(Graphics e)
        {
                    Pen BlackPen = new Pen(Color.Black, 1);
                    Render.AllBombFields(field, e, BlackPen, mouseX, mouseY);
        }

        public void DiscoverField(int xField, int yField)
        {
            Console.WriteLine(  + xField + " " + yField);
            if (xField < 0 || yField < 0 || xField >= field.xFields || yField >= field.yFields) 
            {
                return;
            }

            if (field.bombBoard[xField, yField].isFlag == true)
            {
                field.bombBoard[xField, yField].isFlag = false;
                field.flagNumber--;
            }
            

            if (field.firstDiscover == false)
            {
                field.firstDiscover = true;
                field.GenerateBombs(xField, yField);
            }

            if (field.bombBoard[xField, yField].visible == true) 
            {
                return;
            }
            else if (field.bombBoard[xField, yField].number == 9)
            {
                Console.WriteLine(field.bombBoard[xField, yField].number);


                 Console.WriteLine( xField + " " + yField);
                field.AllFieldsVisible();

                field.flagNumber = 0;
                LostGame();
                return;
            }
            else if (field.bombBoard[xField, yField].number > 0)
            {
                field.bombBoard[xField, yField].visible = true;
                field.visibleFields++;
                Console.WriteLine(field.visibleFields);
                return;
            }
            else
            {
                field.bombBoard[xField, yField].visible = true;
                field.visibleFields++;
                Console.WriteLine(field.visibleFields);

                for (int i = xField - 1; i <= xField + 1; i++) 
                {
                    for (int y = yField - 1; y <= yField + 1; y++)
                    {
                        DiscoverField(i, y);
                    }
                }
            }

            
            
        }

        public void ChangeFlag(int xField, int yField)
        {
            if (xField < 0 || yField < 0 || xField >= field.xFields || yField >= field.yFields)
            {
                return;
            }


            if (field.bombBoard[xField, yField].visible == true) 
            {
                return;
            }
            else if (field.bombBoard[xField, yField].isFlag==true)
            {
                            field.flagNumber--;
                        field.bombBoard[xField, yField].isFlag = false;
            }
            else if (field.bombBoard[xField, yField].isFlag == false)
            {
                field.flagNumber++;
                field.bombBoard[xField, yField].isFlag = true;
            }

            return;
        }


    }
}
