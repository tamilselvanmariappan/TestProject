using CsvHelper;
using CsvHelper.Configuration;
using Data.Test;
using Data.Test.Models;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace GeneralKnowledge.Test.App.Tests
{
    /// <summary>
    /// CSV processing test
    /// </summary>
    /// 
    public sealed class AssetMap : ClassMap<Asset>
    {
        public AssetMap()
        {
            Map(m => m.AssetId).Name("asset id");
            Map(m => m.FileName).Name("file_name");
            Map(m => m.CreatedBy).Name("created_by");
            Map(m => m.Email).Name("email");
            Map(m => m.Description).Name("description");
            References<CountryMap>(m => m.Country);
            References<MimeTypeMap>(m => m.MimeType);
        }
    }

    public sealed class CountryMap : ClassMap<Country>
    {
        public CountryMap()
        {
            Map(m => m.CountryName).Name("country");
        }
    }

    public sealed class MimeTypeMap : ClassMap<MimeType>
    {
        public MimeTypeMap()
        {
            Map(m => m.Type).Name("mime_type");
        }
    }

    public class CsvProcessingTest : ITest
    {
        public void Run()
        {
            // TODO
            // Create a domain model via POCO classes to store the data available in the CSV file below
            // Objects to be present in the domain model: Asset, Country and Mime type
            // Process the file in the most robust way possible
            // The use of 3rd party plugins is permitted
            Console.WriteLine("\n\nCsvProcessingTest: Processing the data and insert into TestDB in local SQL server");
            var csvFile = Resources.AssetImport;
            using (TextReader reader = new StringReader(csvFile))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<AssetMap>();
                var records = csv.GetRecords<Asset>();
                DataRepository repo = new DataRepository();
                repo.SaveAssetDataFromCSV(records.ToList());
            }
            Console.WriteLine("\nCsvProcessingTest: Date import completed");
        }
    }

}
