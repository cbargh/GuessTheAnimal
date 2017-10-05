using GuessTheAnimal.Data;
using GuessTheAnimal.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace GuessTheAnimal
{
    /// <summary>
    /// Interaction logic for SetupAnimal.xaml
    /// </summary>
    public partial class SetupAnimal : Window
    {
        private AnimalViewModel animalViewModel;

        public SetupAnimal()
        {
            animalViewModel = new AnimalViewModel();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxName.DataContext   = animalViewModel;
            textBoxColour.DataContext = animalViewModel;
            textBoxSound.DataContext  = animalViewModel;
            textBoxHas.DataContext    = animalViewModel;
        }

        private void Clear()
        {
            animalViewModel.Name = "";
            animalViewModel.Colour = "";
            animalViewModel.Sound = "";
            animalViewModel.Has = "";
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (animalViewModel.Name != "" &&
                animalViewModel.Colour != "" &&
                animalViewModel.Sound != "" &&
                animalViewModel.Has != "")
            {
                try
                {
                    Root root = AnimalsDataSource.Deserialize();
                    root.animals.Add(animalViewModel);
                    AnimalsDataSource.Serialize(root);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "An error occurred while loading the animals from the file");
                }
                Clear();
            }
            else
                MessageBox.Show("Please make sure all questions have been filled in", "Cannot save - missing question(s)");
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }
    }
}
