using GuessTheAnimal.ViewModels;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace GuessTheAnimal.Data
{
    public class Root
    {
        [XmlArray("Animals"), XmlArrayItem(typeof(AnimalViewModel), ElementName = "Animal")]
        public ObservableCollection<AnimalViewModel> animals { get; set; }
    }

    public class AnimalsDataSource
    {
        private static readonly string fileName = "Animals.xml";

        public static void Test()
        {
            Root root = Deserialize();
            root.animals.Add(new AnimalViewModel("Canary", "yellow", "cheep", "beak"));
            Serialize(root);
        }

        public static void Serialize(Root root)
        {
            var serializer = new XmlSerializer(root.GetType());
            var settings = new XmlWriterSettings { Indent = true };
            using (var writer = XmlWriter.Create(fileName, settings))
            {
                serializer.Serialize(writer, root);
            }
        }

        public static Root Deserialize()
        {
            Root root = new Root();
            root.animals = new ObservableCollection<AnimalViewModel>();
            var serializer = new XmlSerializer(typeof(Root));
            using (XmlReader reader = XmlReader.Create(fileName))
            {
                root = (Root)serializer.Deserialize(reader);
            }
            return root;
        }
    }
}
