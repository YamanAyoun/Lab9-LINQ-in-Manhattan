using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LINQ_in_Manhattan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Filter();

        }
        public class RootObject
        {
            public string type { get; set; }
            public Feature[] features { get; set; }
        }

        public class Feature
        {
            public string type { get; set; }
            public Geometry geometry { get; set; }
            public Properties properties { get; set; }
        }

        public class Geometry
        {
            public string type { get; set; }
            public float[] coordinates { get; set; }
        }

        public class Properties
        {
            public string zip { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string address { get; set; }
            public string borough { get; set; }
            public string neighborhood { get; set; }
            public string county { get; set; }
        }

        public static void Filter()
        {
            
            using (StreamReader reader = File.OpenText("../../../data.json"))

            {   
                JObject dataFile = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                
                var result = JsonConvert.DeserializeObject<RootObject>(dataFile.ToString());

                
                var neighborhoods = from n in result.features
                                    select n.properties.neighborhood;
                Console.WriteLine($"All neighborhoods by using LINQ query statements: {neighborhoods.Count()}.");
                
                var filteredNeighborhoods = from n in neighborhoods
                                            where (n != "")
                                            select n;
                Console.WriteLine($"Filtered neighborhoods by using LINQ query statements: {filteredNeighborhoods.Count()}.");
               
                
            }
        }

        
    }
    }