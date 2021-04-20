using System;
using System.Collections.Generic;
using System.Text;

namespace ToaxSnake
{
    public class Circle
    {
        //X est la position sur l'axe des absisses
        public int X { get; set; } //get = lire / set = ecrire

        //Y est la position sur l'axe des ordonnées
        public int Y { get; set; }

        //Constructeur avec valeur par defaut
        public Circle()
        {
            X = 0;
            Y = 0;
        }

        //Constructeur = fabrique l'objet a partir de x et de y
        public Circle(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
