﻿using Plantjes.Models.Classes;
using Plantjes.Models.Db;
using Plantjes.Models.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Plantjes.ViewModels
{
    //Written by Renzo
    public class ViewModelPlantDetail : ViewModelBase
    {
        private List<StackPanel> _eigenschappen;

        public ViewModelPlantDetail(Plant plant)
        {
            Plant = plant;
            _eigenschappen = new List<StackPanel>();
            _eigenschappen.Add(new PlantEigenschap<Fenotype, FenotypeMulti>(plant.Fenotypes, plant.FenotypeMultis));
            _eigenschappen.Add(new PlantEigenschap<Abiotiek, AbiotiekMulti>(plant.Abiotieks, plant.AbiotiekMultis));
            _eigenschappen.Add(new PlantEigenschap<Commensalisme, CommensalismeMulti>(plant.Commensalismes, plant.CommensalismeMultis));
            _eigenschappen.Add(new PlantEigenschap<BeheerMaand, object>(plant.BeheerMaands));
            _eigenschappen.Add(new PlantEigenschap<ExtraEigenschap, object>(plant.ExtraEigenschaps));
        }

        public string PlantNaam
        {
            get => Plant.GetPlantName();
        }

        public Plant Plant { get; private set; }
        
        public BitmapImage PlantImage { get => Plant.GetPlantImage(); }

        public List<StackPanel> Eigenschappen { get => _eigenschappen; }
    }
}
