using System;

namespace ScreenRemote
{
    public class UN75 : TU7000
    {
        public UN75(long upc, string name) : base(upc, name) { }

        public override void Menu(string command)
        {
            if (command.Equals("Menu"))
                Console.WriteLine("Menu for {0} ", this.model);
        }

        public override void Settings(string command)
        {
            if (command.Equals("settings"))
                Console.WriteLine("Settings for {0} ", this.model);
        }
    }
}