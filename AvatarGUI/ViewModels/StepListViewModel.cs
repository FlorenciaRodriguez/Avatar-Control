﻿using AvatarGUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AvatarGUI.ViewModels
{
    class StepListViewModel
    {
        private Scene scene { get; set; }
        public ObservableCollection<StepViewModel> StepList { get; set; } = new ObservableCollection<StepViewModel>();
        private StepsWindow window;
        private SceneViewModel sceneViewModel;

        public StepListViewModel(Scene scene,StepsWindow window, SceneViewModel sceneViewModel)
        {
            this.window = window;
            this.scene = scene;
            this.sceneViewModel = sceneViewModel;
            AvailableEmotions.Add(0, "Neutro");
            AvailableEmotions.Add(1, "Feliz");
            AvailableEmotions.Add(2, "Enojado");
            AvailableEmotions.Add(3, "Triste");
            AvailableEmotions.Add(4, "Llorando");
            AvailableEmotions.Add(5, "Corriendo");
            AvailableEmotions.Add(6, "Gritando");
            AvailableEmotions.Add(7, "Engreido");
            AvailableEmotions.Add(8, "Lo Que Sea");
            AvailableEmotions.Add(9, "Caminata");
            AvailableEmotions.Add(10, "Brazo");
            AvailableEmotions.Add(11, "Miedo");
            AvailableEmotions.Add(12, "Pensativo mentón");
            AvailableEmotions.Add(13, "Asiente");
            AvailableEmotions.Add(14, "Señala con cabeza");
            AvailableEmotions.Add(15, "Oscila");
            AvailableEmotions.Add(16, "Gesto con mano");
            AvailableEmotions.Add(17, "Victoria");
            AvailableEmotions.Add(18, "Desacuerdo");
            AvailableEmotions.Add(19, "Pensativo");
            AvailableEmotions.Add(20, "Avergonzado");
            AvailableEmotions.Add(21, "Avergonzado hombro");
            AvailableEmotions.Add(22, "Indiferente");
            AvailableEmotions.Add(23, "Habla");
            AvailableEmotions.Add(24, "Asustado");
            AvailableEmotions.Add(25, "Berrinche");
            AvailableEmotions.Add(26, "Rezonga");
            AvailableEmotions.Add(27, "Salta");
            AvailableEmotions.Add(28, "Saluda");
            AvailableEmotions.Add(29, "Festeja en silencio");
            foreach (Step step in scene.steps)
            {
                StepViewModel stepvm = new StepViewModel(this);
                stepvm.step = step;
                StepList.Add(stepvm);
            }
            AddStepCommand = new RelayCommand(new Action<object>(AddStep));
            ShowHelpCommand = new RelayCommand(new Action<object>(ShowHelp));
            AvailableAudios = ResourceLoader.Instance.GetAudios(sceneViewModel.AudioFolderName);
        }

        public int GetNumeroEscena {
            get
            {
                return sceneViewModel.getNumeroEscena;
            }
        }

        public string ModoPersonajes {
            get
            {
                return sceneViewModel.Modes[sceneViewModel.CharacterMode];
            }
        }

        public string ModoNarrador
        {
            get
            {
                return sceneViewModel.Modes[sceneViewModel.NarratorMode];
            }
        }


        public List<string> getAudios
        {
            get
            {
                return AvailableAudios;
            }
        }

        public Dictionary<int, string> AvailableEmotions { get; } = new Dictionary<int, string>();

        public List<string> AvailableAudios { get; }

        public Dictionary<int,string> Prefabs
        {
            get {
                Dictionary<int, string> dictionary = new Dictionary<int, string>();
                for (int i = 0; i< scene.prefabs.Count; i++)
                {
                    if (scene.prefabs[i].modelName != Constants.PREFAB_VACIO)
                        dictionary.Add(i,scene.prefabs[i].modelName+" en posicion "+(i+1));
                }
                dictionary.Add(Constants.NARRATOR, "Narrador");
                return dictionary;
                }
        }

        public int CharacterMode
        {
            get { return scene.characterMode; }
        }

        public int NarratorMode
        {
            get { return scene.narratorMode; }
        }

        private ICommand _addStepCommand;
        public ICommand AddStepCommand
        {
            get
            {
                return _addStepCommand;
            }
            set
            {
                _addStepCommand = value;
            }
        }

        private ICommand _showHelpCommand;
        public ICommand ShowHelpCommand
        {
            get
            {
                return _showHelpCommand;
            }
            set
            {
                _showHelpCommand = value;
            }
        }

        public void ShowHelp(object obj)
        {
            MessageBox.Show("Dependiendo de los modos de personajes o narrador que se hayan elegido en la pantalla de edicion de escenas," +
                " en cada paso que se describa en la grilla de esta pantalla se utilizara el audio que se asigne, el texto, o ambos."+ Environment.NewLine +
                "Con el fin de permitir modelos duplicados, la asignacion del autor de cada paso es por posicion, es decir si le asigno al personaje que designe " +
                "como Personaje 1 a un paso, y luego cambio dicho personaje por otro, el nuevo personaje sera el actor del paso en cuestion.");
        }

        public void AddStep(object obj)
        {
            Step step = new Step();
            scene.steps.Add(step);
            StepViewModel stepVm = new StepViewModel(this);
            stepVm.step = step;
            StepList.Add(stepVm);
        }

        public void DeleteStep(StepViewModel viewModel)
        {
            scene.steps.Remove(viewModel.step);
            StepList.Remove(viewModel);
            window.DataGrid_RowChanged();
        }        
    }
}
