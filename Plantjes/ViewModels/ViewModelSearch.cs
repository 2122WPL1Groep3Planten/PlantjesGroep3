﻿using MvvmHelpers.Commands;
using Plantjes.Dao;
using Plantjes.Models.Classes;
using Plantjes.Models.Db;
using Plantjes.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Plantjes.ViewModels.HelpClasses;

namespace Plantjes.ViewModels
{
    // Written by Warre
    // Export written bij Ian Dumalin on 4/5
    public class ViewModelSearch : ViewModelBase
    {
        private readonly IEnumerable<PlantItem> emptyPlants = new List<PlantItem>() { new PlantItem(), new PlantItem(true) };

        private readonly ISearchService searchService;

        private IEnumerable<Plant> plants;
        private IEnumerable<string> _cmbHabitus;
        private IEnumerable<string> _cmbBezonning;
        private IEnumerable<string> _cmbHabitat;
        private IEnumerable<string> _cmbGrondsoort;

        public ViewModelSearch(ISearchService searchService)
        {
            this.searchService = searchService;

            SearchCommand = new Command<object>(new Action<object>(Search));
            ExportCommand = new RelayCommand(ExportCSV);

            _cmbHabitus = searchService.GetList<FenoHabitu>().Select(f => f.Naam).Prepend(string.Empty);
            _cmbBezonning = searchService.GetList<AbioBezonning>().Select(a => a.Naam).Prepend(string.Empty);
            _cmbHabitat = searchService.GetList<AbioHabitat>().Select(a => a.Afkorting).Prepend(string.Empty);
            _cmbGrondsoort = searchService.GetList<AbioGrondsoort>().Select(a => a.Grondsoort).Prepend(string.Empty);
        }

        private void Search(object parameters)
        {
            List<object> items = parameters as List<object>;

            int socIndex = 65;
            string selectedSoc = string.Empty;
            foreach (bool? check in items.GetRange(4, 5))
            {
                if (check ?? false)
                {
                    selectedSoc = ((char)socIndex).ToString();
                    break;
                }
                socIndex++;
            }

            plants = searchService.GetListPlants(
                items[0] as string,
                items[1] as string,
                items[2] as string,
                items[3] as string,
                selectedSoc,
                items[^1] as string);
            OnPropertyChanged("Plants");
        }

        private void ExportCSV()
        {
            CSVHelper.ExportPlantsToCSV(plants);
        }

        public Command SearchCommand { get; set; }
        public RelayCommand ExportCommand { get; set; }

        public IEnumerable<PlantItem> Plants
        {
            get { return plants == null ? Enumerable.Empty<PlantItem>() : plants.Count() == 0 ? emptyPlants : plants.Select(p => new PlantItem(p)); }
        }

        public IEnumerable<string> CmbHabitus
        {
            get => _cmbHabitus;
        }

        public IEnumerable<string> CmbBezonning
        {
            get => _cmbBezonning;
        }

        public IEnumerable<string> CmbHabitat
        {
            get => _cmbHabitat;
        }

        public IEnumerable<string> CmbGrondsoort
        {
            get => _cmbGrondsoort;
        }
    }
}
