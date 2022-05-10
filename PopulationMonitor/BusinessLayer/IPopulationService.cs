using System;
using System.Collections.Generic;
using DataAccessLayer;

namespace BusinessLayer
{
    public interface IPopulationService
    {
        List<PopulationData> GetPopulationData();

        List<PopulationData> GetPopulationDataWithSearch(int start, int length, string sortColumn, string sortColumnDirection, string searchValue);

        PopulationData GetPopulationDataById(int id);

        bool InsertPopulationData(PopulationData record);

        bool UpdatePopulationData(PopulationData record);

        bool DeletePopulationData(int id);
    }
}
