using Godot;
using System;

namespace Models
{
    public class Teammate
    {
        public int Id { get; set; }
        public Texture Texture { get; set; }
        public string Name { get; set; }
        public bool IsAddedToTeam { get; set; }

        public Teammate(int id, string texturePath, string name, bool isAddedToTeam = false)
        {
            Id = id;
            Texture = (Texture)GD.Load(texturePath);
            Name = name;
            IsAddedToTeam = isAddedToTeam;
        }
    }
}