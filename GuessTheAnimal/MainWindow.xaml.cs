using GuessTheAnimal.Data;
using GuessTheAnimal.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace GuessTheAnimal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum Step { Colour, Sound, Has }

        private const string colourQuestion = "Is the animal {0} ?";           // eg. 'gray',     'yellow'
        private const string soundQuestion  = "Does the animal {0} ?";         // eg. 'trumpet',  'roar'
        private const string hasQuestion    = "Does the animal have a {0} ?";  // eg. 'trunk',    'mane'
        private const string nameStatement  = "You choose the {0}";            // eg. 'Elephant', 'Lion'

        private AnimalsViewModel animalsViewModel;
        private Root root;

        private Step step;
        private List<string> options;
        private int optionIndex;
        private string colour;
        private string sound;
        private string has;

        public MainWindow()
        {
            InitializeComponent();
            //AnimalsDataSource.Test();

            options = new List<string>();

            try
            {
                root = AnimalsDataSource.Deserialize();
            }
            catch //(Exception e)
            {
                //MessageBox.Show("Error: " + e.Message + "\r\n  Application must close!", "An error occurred while loading the animals from the file");
                //Close();
                root = new Root();
                root.animals = new ObservableCollection<AnimalViewModel>();
                root.animals.Add(new AnimalViewModel("Elephant", "gray", "trumpet", "trunk"));
                root.animals.Add(new AnimalViewModel("Lion", "yellow", "roar", "mane"));
                root.animals.Add(new AnimalViewModel("Canary", "yellow", "cheep", "beak"));
                root.animals.Add(new AnimalViewModel("Rhinoceros", "gray", "bellow", "horn"));
                AnimalsDataSource.Serialize(root);
            }
            animalsViewModel = new AnimalsViewModel();
            animalsViewModel.Animals = root.animals;
            animalsViewModel.AnimalsViewSource.Source = animalsViewModel.Animals;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listBoxAnimals.DataContext = animalsViewModel;

            btnPlay.Visibility           = Visibility.Visible;
            wrapPanelQuestion.Visibility = Visibility.Hidden;
        }

        private void SetColours()
        {
            step = Step.Colour;
            options.Clear();
            optionIndex = 0;
            foreach (AnimalViewModel animal in animalsViewModel.Animals)
            {
                if (options.IndexOf(animal.Colour) == -1)
                    options.Add(animal.Colour);
            }
        }

        private void SetSounds(string colour)
        {
            step = Step.Sound;
            options.Clear();
            optionIndex = 0;
            foreach (AnimalViewModel animal in animalsViewModel.Animals)
            {
                if (options.IndexOf(animal.Sound) == -1 &&
                    animal.Colour == colour)
                {
                    options.Add(animal.Sound);
                }
            }
        }

        private void SetHas(string colour, string sound)
        {
            step = Step.Has;
            options.Clear();
            optionIndex = 0;
            foreach (AnimalViewModel animal in animalsViewModel.Animals)
            {
                if (options.IndexOf(animal.Has) == -1 &&
                    animal.Colour == colour &&
                    animal.Sound  == sound)
                {
                    options.Add(animal.Has);
                }
            }
        }

        private string GetName(string colour, string sound, string has)
        {
            //step = Step.Name;
            foreach (AnimalViewModel animal in animalsViewModel.Animals)
            {
                if (animal.Colour == colour &&
                    animal.Sound  == sound  &&
                    animal.Has    == has      )
                {
                    return animal.Name;
                }
            }
            return "<ERROR>";
        }

        private void ShowQuestion(string question, string value)
        {
            textBlockQuestion.Text = string.Format(question, value);
        }

        private void ShowAnswer(string colour, string sound, string has)
        {
            //textBlockQuestion.Text = string.Format(nameStatement, GetName(colour, sound, has));
            textBlockPlay.Text = string.Format(nameStatement, GetName(colour, sound, has));
            //btnPlay.Content = "Play Again?";
            stackPanelPlay.Visibility    = Visibility.Visible;
            wrapPanelQuestion.Visibility = Visibility.Hidden;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            stackPanelPlay.Visibility    = Visibility.Hidden;
            wrapPanelQuestion.Visibility = Visibility.Visible;
            SetColours();
            ShowQuestion(colourQuestion, options[optionIndex]);
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            switch (step)
            {
                case Step.Colour:
                    colour = options[optionIndex];
                    SetSounds(colour);
                    sound = options[0];

                    if (options.Count == 1)
                    {
                        SetHas(colour, sound);
                        has = options[0];
                        if (options.Count == 1)
                        {
                            ShowAnswer(colour, sound, has);
                        }
                        else
                            ShowQuestion(hasQuestion, has);
                    }
                    else
                        ShowQuestion(soundQuestion, sound);
                    break;

                case Step.Sound:
                    sound = options[optionIndex];
                    SetHas(colour, sound);
                    has = options[0];
                    if (options.Count == 1)
                    {
                        ShowAnswer(colour, sound, has);
                    }
                    else
                        ShowQuestion(hasQuestion, has);
                    break;

                case Step.Has:
                    has = options[optionIndex];
                    ShowAnswer(colour, sound, has);
                    break;

                //case Step.Name:
                //    //btnPlay.Content = "Play Again?";
                //    stackPanelPlay.Visibility    = Visibility.Visible;
                //    wrapPanelQuestion.Visibility = Visibility.Hidden;
                //    break;
            }
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            switch (step)
            {
                case Step.Colour:
                    optionIndex++;
                    if (optionIndex >= options.Count)
                        optionIndex = 0;
                    ShowQuestion(colourQuestion, options[optionIndex]);
                    break;

                case Step.Sound:
                    optionIndex++;
                    if (optionIndex >= options.Count)
                        optionIndex = 0;
                    ShowQuestion(soundQuestion, options[optionIndex]);
                    break;

                case Step.Has:
                    optionIndex++;
                    if (optionIndex >= options.Count)
                        optionIndex = 0;
                    ShowQuestion(hasQuestion, options[optionIndex]);
                    break;

                //case Step.Name:
                //    optionIndex++;
                //    if (optionIndex >= options.Count)
                //        optionIndex = 0;
                //    ShowQuestion(nameQuestion, options[optionIndex]);
                //    break;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SetupAnimal setupAnimal = new SetupAnimal();
            if (setupAnimal.ShowDialog() == true)
            {
            }
        }
    }
}
