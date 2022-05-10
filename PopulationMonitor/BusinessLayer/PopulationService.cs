using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace BusinessLayer
{
    public class PopulationService : IPopulationService
    {
        private readonly IPopulationDataService _populationDataService;

        public PopulationService(IPopulationDataService populationDataService)
        {
            _populationDataService = populationDataService;
        }
        public List<PopulationData> GetPopulationData()
        {
            var populationList = _populationDataService.GetPopulationData();
            return populationList;
        }

        public List<PopulationData> GetPopulationDataWithSearch(int start, int length, string sortColumn, string sortColumnDirection, string searchValue)
        {
            var populationData = GetPopulationData();

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                populationData = populationData. AsQueryable().OrderBy(sortColumn + " " + sortColumnDirection).ToList();
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                populationData = populationData.Where(m => m.Country.Contains(searchValue) || m.TotalFemale.ToString().Contains(searchValue)
                || m.TotalMale.ToString().Contains(searchValue) || m.Year.ToString().Contains(searchValue)).ToList();
            }

            return populationData;
        }

        public PopulationData GetPopulationDataById(int id)
        {
            return _populationDataService.GetPopulationData().FirstOrDefault(a => a.PopulationId == id);
        }

        public bool InsertPopulationData(PopulationData record)
        {
            var populationList = GetPopulationData();
            record.PopulationId = populationList.Max(a => a.PopulationId) + 1;
            populationList.Add(record);
            return _populationDataService.SaveToCsv(populationList);
        }

        public bool UpdatePopulationData(PopulationData record)
        {
            var populationList = GetPopulationData();
            var recordToEdit = populationList.FirstOrDefault(a => a.PopulationId == record.PopulationId);
            if(recordToEdit != null)
            {
                recordToEdit.Country = record.Country;
                recordToEdit.Year = record.Year;
                recordToEdit.TotalMale = record.TotalMale;
                recordToEdit.TotalFemale = record.TotalFemale;
                return _populationDataService.SaveToCsv(populationList);
            }
            return false;
        }

        public bool DeletePopulationData(int id)
        {
            var populationList = GetPopulationData();
            var recordToDelete = populationList.FirstOrDefault(a => a.PopulationId == id);
            populationList.Remove(recordToDelete);
            return _populationDataService.SaveToCsv(populationList);
        }

    }
}
