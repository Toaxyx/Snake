using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToaxSnake
{
    public partial class Form1 : Form
    {
        private List<Circle> snake;
        private Circle food;

        public Form1()
        {
            InitializeComponent();

            this.snake = new List<Circle>();
            this.food = new Circle(16,16);

            new Parameters();

            gameTimer.Interval = 1000 / Parameters.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            StartGame();
        }

        public void UpdateScreen(object sender, EventArgs e)
        {
            if (Parameters.GameOver == true)
            {
                if(Input.KeyPressed(Keys.Enter) == true)
                {
                    StartGame();
                }
            }
            else
            {
                //gérer la direction opposée au serpent
                if (Input.KeyPressed(Keys.Right) && Parameters.Direction != Direction.Left)
                {
                    Parameters.Direction = Direction.Right;
                }
                else if(Input.KeyPressed(Keys.Left) && Parameters.Direction != Direction.Right)
                {
                    Parameters.Direction = Direction.Left;
                }
                else if (Input.KeyPressed(Keys.Up) && Parameters.Direction != Direction.Down)
                {
                    Parameters.Direction = Direction.Up;
                }
                else if (Input.KeyPressed(Keys.Down) && Parameters.Direction != Direction.Up)
                {
                    Parameters.Direction = Direction.Down;
                }

                MovePlayer();
            }

            //enleve ce qui existe et le redessine
            pbCanvas.Invalidate();
        }

        private void StartGame()
        {
            lblGameOver.Visible = false; //cache le "Game Over"
            new Parameters(); // réinitialise les paramètres du jeu

            this.snake.Clear();
            Circle head = new Circle(15, 15);
            snake.Add(head);

            lblScoreValue.Text = "";
            GenerateFood();
        }

        private void MovePlayer()
        {
            for(int i = snake.Count - 1; i >= 0; i--)
            {
                //si la tete
                if(i == 0)
                {
                    switch(Parameters.Direction)
                    {
                        case Direction.Right:
                            snake[i].X++;
                            break;
                        case Direction.Left:
                            snake[i].X--;
                            break;
                        case Direction.Up:
                            snake[i].Y--;
                            break;
                        case Direction.Down:
                            snake[i].Y++;
                            break;
                    }


                    //récupérer la taille maximale du canvas
                    int maxXposition = pbCanvas.Size.Width / Parameters.Width; //valeur max de l'absisse
                    int maxYposition = pbCanvas.Size.Height / Parameters.Height; //valeur max de l'ordonnée

                    //détecter les collision 

                    // avec le corps
                    for(int j = 1; j < snake.Count; j++)
                    {
                        if(snake[i].X == snake[j].X 
                            && snake[i].Y == snake[j].Y)
                        {
                            GameOver();
                        }
                    }

                    //avec le bord de l'ecran
                    if(snake[i].X < 0 || snake[i].Y < 0
                        || snake[i].X >= maxXposition || snake[i].Y >= maxYposition)
                    {
                        GameOver();
                    }

                    //avec la nourriture
                    if(snake[0].X == food.X && snake[0].Y == food.Y)
                    {
                        Eat();
                    }
                }
                //si c'est le corps
                else
                {
                    snake[i].X = snake[i - 1].X;
                    snake[i].Y = snake[i - 1].Y;
                }
            }
        }

        #region TOUCHE CLAVIER
        //fonction form_keyup
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        //fonction form_keydown
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        #endregion
        //fonction eat
        private void Eat()
        {
            //rajoute un élément au serpent
            Circle lastPart = new Circle();
            lastPart.X = snake[snake.Count - 1].X;
            lastPart.Y = snake[snake.Count - 1].Y;
            snake.Add(lastPart);

            //gérer le score
            //Parameters.Score = Parameters.Score + Parameters.Point;
            Parameters.Score += Parameters.Point;
            lblScoreValue.Text = Parameters.Score.ToString();

            //Regénérer un élément nourriture
            GenerateFood();
        }

        //fonction die
        private void GameOver()
        {
            Parameters.GameOver = true;
        }

        //fonction picturebox_paint
        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if(!Parameters.GameOver) // pas game over
            {
                for(int i = 0; i < snake.Count; i++)
                {
                    Brush snakeColour;
                    if(i == 0)
                    {
                        snakeColour = Brushes.Blue;
                    }
                    else
                    {
                        snakeColour = Brushes.BlueViolet;
                    }

                    //Dessiner le serpent
                    // dessine un rectangle (point de depart x, y, largeur, hauteur)
                    // remplis le d'une couleur
                    // rabote les angles (pour faire un rond / ellipse)
                    canvas.FillEllipse(snakeColour, 
                        new Rectangle(snake[i].X * Parameters.Width,
                                      snake[i].Y * Parameters.Height,
                                      Parameters.Width, Parameters.Height));



                    //Dessiner la nourriture
                    canvas.FillEllipse(Brushes.Red,
                        new Rectangle(food.X * Parameters.Width,
                                      food.Y * Parameters.Height,
                                      Parameters.Width, Parameters.Height));
                }
            }
            else
            {
                lblGameOver.Visible = true;
                lblGameOver.Text = "Tu as perdu ! \nAppuis sur ENTRER pour recommencer !";
            }
        }

        //fonction generate food
        public void GenerateFood()
        {
            int maxXposition = pbCanvas.Size.Width / Parameters.Width; //valeur max de l'absisse
            int maxYposition = pbCanvas.Size.Height / Parameters.Height; //valeur max de l'ordonnée

            Random rand = new Random();
            var x = rand.Next(0, maxXposition);
            var y = rand.Next(0, maxYposition);

            food = new Circle(x, y);
        }

    }
}
