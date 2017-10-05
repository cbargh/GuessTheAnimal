using System.Collections.ObjectModel;
using System.Windows.Data;

namespace GuessTheAnimal.ViewModels
{
    public class AnimalsViewModel
    {
        public AnimalsViewModel()
        {
            Animals           = new ObservableCollection<AnimalViewModel>();
            AnimalsViewSource = new CollectionViewSource();
            AnimalsViewSource.Source = Animals;
        }

        public ObservableCollection<AnimalViewModel> Animals { get; set; }

        public CollectionViewSource AnimalsViewSource { get; }
    }
}
