using Controls;
using Models;
using System;

namespace Utils
{
    public class SelectedValueEventArgs : EventArgs
    {
        public int SelectedValue { get; set; }
    }

    public class TeammateControlClickedEventArgs : EventArgs
    {
        public TeammateControl SelectedValue { get; set; }
    }

    public class GameDataEventArgs : EventArgs
    {
        public GameData GameData { get; set; }
    }

    public class GameConfigEventArgs : EventArgs
    {
        public string EncodedConfig { get; set; }
        public string Nickname { get; set; }
    }
}