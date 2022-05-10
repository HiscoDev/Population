using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface IPopulationDataService
    {
        List<PopulationData> GetPopulationData();

        bool SaveToCsv<T>(List<T> populationData);
    }
}
