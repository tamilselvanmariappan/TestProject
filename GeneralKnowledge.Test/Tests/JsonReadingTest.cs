using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneralKnowledge.Test.App.Tests
{
    /// <summary>
    /// Basic data retrieval from JSON test
    /// </summary>
    /// 

    public class DataItem
    {
        public string date { get; set; }
        public string temperature { get; set; }
        public string pH { get; set; }
        public string phosphate { get; set; }
        public string chloride { get; set; }
        public string nitrate { get; set; }
    }

    public class DataItemList
    {
        public List<DataItem> samples { get; set; }
    }

    public class JsonReadingTest : ITest
    {
        public string Name { get { return "JSON Reading Test"; } }

        public void Run()
        {
            var jsonData = Resources.SamplePoints;

            // TODO: 
            // Determine for each parameter stored in the variable below, the average value, lowest and highest number.
            // Example output
            // parameter   LOW AVG MAX
            // temperature   x   y   z
            // pH            x   y   z
            // Chloride      x   y   z
            // Phosphate     x   y   z
            // Nitrate       x   y   z

            PrintOverview(jsonData);
        }

        private void PrintOverview(byte[] data)
        {
            string jsonData = Encoding.UTF8.GetString(data);
            DataItemList dataItemList = JsonConvert.DeserializeObject<DataItemList>(jsonData);
            Console.WriteLine($"\n\n{this.Name}");
            Dictionary<string, List<float>> objList = new Dictionary<string, List<float>>();
            objList.Add("Temprature", dataItemList.samples.Where(x => x.temperature != null).Select(x => float.Parse(x.temperature)).ToList());
            objList.Add("PH", dataItemList.samples.Where(x => x.pH != null).Select(x => float.Parse(x.pH)).ToList());
            objList.Add("Chloride", dataItemList.samples.Where(x => x.chloride != null).Select(x => float.Parse(x.chloride)).ToList());
            objList.Add("Phosphate", dataItemList.samples.Where(x => x.phosphate != null).Select(x => float.Parse(x.phosphate)).ToList());
            objList.Add("Nitrate", dataItemList.samples.Where(x => x.nitrate != null).Select(x => float.Parse(x.nitrate)).ToList());
            Console.WriteLine(String.Format("{0,-12} {1,-12} {2,-12} {3,-12}", "Parameter", "LOW", "AVG", "MAX"));
            foreach (var t in objList)
            {
                Console.WriteLine(String.Format("{0,-12} {1,-12} {2,-12} {3,-12}", t.Key, t.Value.Min(), t.Value.Average(), t.Value.Max()));
            }
        }
    }
}
