namespace ScreenRemote
{
    /* Unified remote for the TU7000 series television */
    interface TM124A
    {
        void Power(string command);
        void Mute(string command);
        void Source(string command);
        void Volume(string command);
        void Channel(string command);
        void Last(string command);
        void Info(string command);
    }
}
