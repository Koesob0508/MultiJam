using System;

namespace MultiJam
{
    public interface ICreature
    {
        public int Health { get; set; }
        public int Attack { get; set; }
        public event Action OnEnterField;
        public event Action OnExitField;
    }
}