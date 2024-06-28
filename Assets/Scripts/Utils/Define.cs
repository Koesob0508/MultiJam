namespace MultiJam
{
    public class Define
    {
        public enum Player
        {
            Leader,
            Follower
        }
        public enum Scene
        {
            Unknown,
            Title,
            Lobby,
            Main
        }

        public enum MouseEvent
        {
            Press,
            Click,
        }

        public enum UIEvent
        {
            Click,
            Drag,
        }
    }
}