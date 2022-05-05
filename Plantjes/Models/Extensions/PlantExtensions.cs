﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plantjes.Models.Db;

namespace Plantjes.Models.Extensions
{
    //Written by Ian Dumalin on 05/05
    internal static class PlantExtensions
    {
        public static string PlantToString(this Plant plant)
        {
            string type = plant.Type.FirstToUpper();
            string familie = plant.Familie.FirstToUpper();
            string geslacht = plant.Geslacht.FirstToUpper();
            string soort = plant.Soort;
            soort =
                soort == "__" ? string.Empty : plant.Soort.FirstToUpper();
            string variant = plant.Variant.FirstToUpper();
            plant.NederlandsNaam = plant.NederlandsNaam.FirstToUpper();
            return $"{plant.Type};{plant.Familie};{plant.Geslacht};{plant.Soort};" +
                   $"{plant.Variant};{plant.NederlandsNaam}";
        }
    }
}