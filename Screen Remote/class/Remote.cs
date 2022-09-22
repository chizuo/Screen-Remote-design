using System;

namespace ScreenRemote
{
    public class Remote
    {
        private SignalHandler signal;

        public Remote(SignalHandler screen)
        {
            signal = screen;
        }

        public void Command(string command)
        {
            this.signal(command);
        }
    }
}
