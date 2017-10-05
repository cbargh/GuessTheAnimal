using GuessTheAnimal.Models;

namespace GuessTheAnimal.ViewModels
{
    public class AnimalViewModel : ViewModelBase
    {
        private AnimalModel animalModel = null;

        public AnimalViewModel()
        {
            animalModel = new AnimalModel();
        }

        public AnimalViewModel(string name, string colour, string sound, string has)
        {
            animalModel = new AnimalModel(name, colour, sound, has);
        }

        public string Name
        {
            get { return animalModel.Name; }
            set
            {
                if (value != animalModel.Name)
                {
                    animalModel.Name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Colour
        {
            get { return animalModel.Colour; }
            set
            {
                if (value != animalModel.Colour)
                {
                    animalModel.Colour = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Sound
        {
            get { return animalModel.Sound; }
            set
            {
                if (value != animalModel.Sound)
                {
                    animalModel.Sound = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Has
        {
            get { return animalModel.Has; }
            set
            {
                if (value != animalModel.Has)
                {
                    animalModel.Has = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
