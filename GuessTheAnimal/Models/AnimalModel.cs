using System.Xml.Serialization;

namespace GuessTheAnimal.Models
{
    [XmlRoot("Root")]
    public class AnimalModel
    {
        public string Name   { get; set; } = "";
        public string Colour { get; set; } = "";  // change to enum?
        public string Sound  { get; set; } = "";  // change to enum?
        public string Has    { get; set; } = "";

        public AnimalModel() { }

        public AnimalModel(string name, string colour, string sound, string has)
        {
            Name   = name;
            Colour = colour;
            Sound  = sound;
            Has    = has;
        }
    }
}
