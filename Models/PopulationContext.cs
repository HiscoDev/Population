using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace PopulationAPI_nh.Models
{
    public class PopulationContext : DbContext
    {
        public PopulationContext(DbContextOptions<PopulationContext> options)
            :base(options)
        {
            Database.EnsureCreated();
            if (Populations.ToList<Population>().Count == 0)
            {
                try
                {
                    //Seed the db from csv ONLY ON FIRST RUN OR IF DB IS EMPTY
                    //If there is no csv file, or the csv doesn't match the model,
                    //error logging will catch and write the error to the console.

                    Assembly assembly = Assembly.GetExecutingAssembly();
                    string resourceName = "PopulationAPI_nh.Data.PopulationSampleDataSet.csv";

                    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            CsvReader csvReader = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);
                            var population = csvReader.GetRecords<Population>().ToArray();
                            Populations.AddRange(population);
                            SaveChanges();                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }                
            }
        }
        public DbSet<Population> Populations { get; set; }
    }
}
