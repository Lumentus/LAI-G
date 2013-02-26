using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLSGAI
{
    class KI
    {
        private String pathToDB;
        private String name;
        private String gameName;

        public String Name
        {
            get { return name; }
        }

        public KI(String pathToDB)
        {
            this.pathToDB = pathToDB;
            this.name = "AI";
            this.gameName = "";
        }

        /**
         * Analyses a given message and returns in the second parameter a message from the AI that should be put out
         * the return value does on the otherhand tell the main program, whether the AI wants to start a game or not(if it should call startGame method
         **/
        public Boolean analyseMessage(String msg, ref String ret)
        {
            ret = msg;
            return false;
        }

        private void startGame()
        {

        }

        private void stopGame()
        {
        }

        private void playGame()
        {
        }
    }
}
