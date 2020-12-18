using Godot;
using System;

namespace Models
{
    public class Teammate
    {
        public int Id { get; set; }
        public Texture BigTexture { get; set; }
        public Texture SmallTexture { get; set; }
        public string Name { get; set; }
        public bool IsAddedToTeam { get; set; }

        public Teammate(int id, string bigTexturePath, string smallTexturePath, string name, bool isAddedToTeam = false)
        {
            Id = id;
            BigTexture = (Texture)GD.Load(bigTexturePath);
            SmallTexture = (Texture)GD.Load(smallTexturePath);
            Name = name;
            IsAddedToTeam = isAddedToTeam;
        }
    }
}