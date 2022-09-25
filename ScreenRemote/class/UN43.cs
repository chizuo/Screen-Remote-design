namespace ScreenRemote
{
    public class UN43 : TU7000
    {
        protected bool stereo = true;
        protected bool mono = false;
        protected string tizen = "Tizen v43.1.1";
        public UN43(long upc, string name) : base(upc, name) { }

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
                    Console.Write("\nSet Audio to [1]Stereo [2]Mono : ");
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
                                this.mono = false;
                                this.stereo = true;
                                break;
                            default:
                                this.mono = true;
                                this.stereo = false;
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

                if (stereo) sound = "Stereo";
                else sound = "Mono";
                Console.WriteLine("Sound: {0}", sound);
                Console.WriteLine("****************************\n");
                Console.WriteLine("Press enter to exit the info screen...");
                Console.ReadLine();
            }
        }

        public void DisplaySettings()
        {
            string bar = "\n***** Sound Settings for UN43 *****";
            string bottom = "***********************************";
            Console.WriteLine(bar);
            Console.Write("* [{0}] Stereo", this.stereo ? "*" : "_");
            Console.Write("".PadRight(bar.Length - 14));
            Console.WriteLine("*");
            Console.Write("* [{0}] Mono", this.mono ? "*" : "_");
            Console.Write("".PadRight(bar.Length - 12));
            Console.WriteLine("*");
            Console.WriteLine(bottom);
        }
    }
}