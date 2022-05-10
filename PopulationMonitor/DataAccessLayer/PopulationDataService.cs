using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace DataAccessLayer
{
    public class PopulationDataService : IPopulationDataService
    {
        public static string FilePath = "../DataAccessLayer/PopulationSampleDataSet.csv";
        public static PopulationData DataFromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            PopulationData populationValue = new PopulationData();
            populationValue.PopulationId = int.TryParse(values[0], out int populationID) ? populationID : 0;
            populationValue.Country = values[1];
            populationValue.Year = int.TryParse(values[2], out int year) ? year: 1900;
            populationValue.TotalMale = decimal.TryParse(values[3], out decimal totalMale) ? totalMale : 0;
            populationValue.TotalFemale = decimal.TryParse(values[4], out decimal totalFemale) ? totalFemale : 0;
            populationValue.PopulationSummary = (populationValue.TotalMale + populationValue.TotalFemale) / 2;
            return populationValue;
        }
        public List<PopulationData> GetPopulationData()
        {
            var populationRecords = File.ReadAllLines(FilePath)
                               .Skip(1)
                               .Select(v => DataFromCsv(v))
                               .ToList();

            return populationRecords;
        }

        public bool SaveToCsv<T>(List<T> populationData)
        {
            var lines = new List<string>();
            IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(typeof(T)).OfType<PropertyDescriptor>();
            var header = string.Join(",", props.ToList().Select(x => x.Name));
            lines.Add(header);
            var valueLines = populationData.Select(row => string.Join(",", header.Split(',').Select(a => row.GetType().GetProperty(a).GetValue(row, null))));
            lines.AddRange(valueLines);
            System.IO.File.WriteAllLines(FilePath, lines.ToArray());
            return true;
        }


    }
}
