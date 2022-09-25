namespace ScreenRemote
{
    public class UN58 : TU7000
    {
        protected bool dts = false;
        protected bool thx = false;
        protected bool stereo = true;
        protected string tizen = "Tizen v58.1.1";

        public UN58(long upc, string name) : base(upc, name) { }

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
                    Console.Write("\nSet Audio to [1]DTS [2]THX [3]Stereo : ");
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
                                this.dts = true;
                                this.thx = false;
                                this.stereo = false;
                                break;
                            case 2:
                                this.dts = false;
                                this.thx = true;
                                this.stereo = false;
                                break;
                            default:
                                this.dts = false;
                                this.thx = false;
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

                if (dts) sound = "Dolby DTS";
                else if (thx) sound = "THX Spatial Audio";
                else sound = "Stereo";
                Console.WriteLine("Sound: {0}", sound);
                Console.WriteLine("****************************\n");
                Console.WriteLine("Press enter to exit the info screen...");
                Console.ReadLine();
            }
        }

        public void DisplaySettings()
        {
            string bar = "\n***** Sound Settings for UN58 *****";
            string bottom = "***********************************";
            Console.WriteLine(bar);
            Console.Write("* [{0}] DTS", this.dts ? "*" : "_");
            Console.Write("".PadRight(bar.Length - 11));
            Console.WriteLine("*");
            Console.Write("* [{0}] THX", this.thx ? "*" : "_");
            Console.Write("".PadRight(bar.Length - 11));
            Console.WriteLine("*");
            Console.Write("* [{0}] Stereo", this.stereo ? "*" : "_");
            Console.Write("".PadRight(bar.Length - 14));
            Console.WriteLine("*");
            Console.WriteLine(bottom);
        }
    }
}