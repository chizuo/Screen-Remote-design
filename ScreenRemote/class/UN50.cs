namespace ScreenRemote
{
    public class UN50 : TU7000
    {
        protected bool dolby = false;
        protected bool stereo = true;
        protected string tizen = "Tizen v50.1.1";
        public UN50(long upc, string name) : base(upc, name) { }
        public override void Menu(string command)
        {
            if (command.Equals("menu") && this.power == true)
                this.DisplayScreen("Smart Menu powered by " + tizen);
        }

        public override void Settings(string command)
        {
            if (command.Equals("settings") && this.power == true)
            {
                bool valid = true;
                this.DisplaySettings();
                do
                {
                    Console.Write("\nSet Audio to [1]Dolby [2]THX [3]Stereo : ");
                    Int32.TryParse(Console.ReadLine(), out int sound);
                    if (sound < 1 || sound > 3)
                    {
                        valid = false;
                        Console.WriteLine("invalid selection");
                    }
                    else
                    {
                        switch (sound)
                        {
                            case 1:
                                this.dolby = true;
                                this.stereo = false;
                                break;
                            default:
                                this.dolby = false;
                                this.stereo = true;
                                break;
                        }
                        valid = true;
                    }
                } while (!valid);
                DisplaySettings();
            }
        }

        public override void ModelInfo(string command)
        {
            if (command.Equals("info"))
            {
                string sound;

                if (dolby) sound = "Dolby Digital";
                else sound = "Stereo";
                Console.WriteLine("Sound: {0}", sound);
                Console.WriteLine("****************************\n");
                Console.WriteLine("Press enter to exit the info screen...");
                Console.ReadLine();
            }
        }

        public void DisplaySettings()
        {
            string bar = "\n***** Sound Settings for UN55 *****";
            string bottom = "***********************************";
            Console.WriteLine(bar);
            Console.Write("* [{0}] Dolby", this.dolby ? "*" : "_");
            Console.Write("".PadRight(bar.Length - 13));
            Console.WriteLine("*");
            Console.Write("* [{0}] Stereo", this.stereo ? "*" : "_");
            Console.Write("".PadRight(bar.Length - 14));
            Console.WriteLine("*");
            Console.WriteLine(bottom);
        }
    }
}