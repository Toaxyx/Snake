using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ToaxSnake
{
    public class Input
    {
        //Dictionnaire de touche de clavier (key) et booleen (value)
        private static Hashtable keyTable = new Hashtable();

        /*
         * Si touche = entrée (key)
         * Si tableau[entrée] == null (donc n'existe pas dans le tableau ou n'est pas définie)
         *  alors retourne faux
         * Sinon on retourne la valeur de tableau[entrée]
         */
        public static bool KeyPressed(Keys key)
        {
            if(keyTable[key] == null)
            {
                return false;
            }
            return (bool) keyTable[key];
        }

        /*
         * Si touche = entrée (key)
         * Tableau[entrée] = vrai
         */
        public static void ChangeState(Keys key, bool state)
        {
            keyTable[key] = state;
        }
    }
}
