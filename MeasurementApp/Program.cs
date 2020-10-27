using MeasurementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeasurementApp
{
    //-Jeder Messwerttyp wird getrennt gesampled
    //-Aus einem Intervall von 5 Minuten wird nur der letzte Wert eines Typs ausgewählt
    //-Liegt der Wert genau auf dem Raster, zählt er zum aktuellen Intervall
    //-Die Eingabewerte sind zeitlich nicht sortiert
    //-Das Ergebnis des Samplings muss nach Zeit aufsteigend sortiert sein

    class Program
    {

        static void Main()
        {
            //sample start date
            DateTime startOfSampling = new DateTime(2017, 1, 3, 10, 0, 0);

            Console.WriteLine("All measure for mapping------------------------------------");
            //Show all measure record
            foreach (Measurement itemMeasure in FilListOfMeasures())
            {
                Console.WriteLine($"Time :{itemMeasure.measurementTime} Type :{itemMeasure.measurementType} Value :{itemMeasure.measurementValue}");
            }
            
            //Version mapping for measure-typ selected
            Console.WriteLine("V1- list for Type selected----------------------------------");
            foreach (Measurement itemMeasure in Map(MeasureType.TEMP, FilListOfMeasures(), startOfSampling))
            {
                Console.WriteLine($"Time :{itemMeasure.measurementTime} Type :{itemMeasure.measurementType} Value :{itemMeasure.measurementValue}");
            }
            
            //Version mapping for all measure-typ (auto)
            Console.WriteLine("V2- For all Type contained in measure-list-----------------");
            foreach (Measurement itemMeasure in MapAllMeasureType(FilListOfMeasures(), startOfSampling))
            {
                Console.WriteLine($"Time :{itemMeasure.measurementTime} Type :{itemMeasure.measurementType} Value :{itemMeasure.measurementValue}");
            }

        }


        /// <summary>
        /// Map all measure record of measure-Type selected
        /// </summary>
        /// <param name="measurementType">measure typ filter</param>
        /// <param name="measureList">measure list to map</param>
        /// <param name="startOfSampling">date of the grid or begins the mapping</param>
        /// <returns>measure-list for measure-Type selected</returns>
        public static List<Measurement> Map(MeasureType measurementType, List<Measurement> measureList, DateTime startOfSampling)
        {
            //Intervalle in Minute between each check
            const int intervalInMinute = 5;
            bool isLoop = true;
            List<Measurement> measureReturned = new List<Measurement>();

            do
            {
                //Retrieve all measure of mesure-type 
                var measureAllValueOfTyp = ExtractMeasureTyp(measureList, measurementType);

                //retrieve last measure record  for measure type and between the dateGrid-From and dateGrid-To 
                var measureFund = measureAllValueOfTyp.LastOrDefault(a => a.measurementTime > startOfSampling &&
                                                       a.measurementTime <= startOfSampling.AddMinutes(intervalInMinute));
                if (measureFund != null)
                {
                    //set measure and add to measure-list returned
                    Measurement recordFund = new Measurement
                    {
                        measurementTime = startOfSampling.AddMinutes(intervalInMinute),
                        measurementValue = measureFund.measurementValue,
                        measurementType = measureFund.measurementType
                    };
                    measureReturned.Add(recordFund);

                    //set next dategrid-from 
                    startOfSampling = startOfSampling.AddMinutes(intervalInMinute);
                }
                else
                {
                    //Set flag to stopt mapping
                    isLoop = false;
                }
            } while (isLoop);


            return measureReturned;
        }



        /// <summary>
        /// Extract and return the measure-typ list of the selected measure-type
        /// </summary>
        /// <param name="measureList">Measure-list</param>
        /// <param name="measureType">Measure-type to select</param>
        /// <returns>Measure list of Type selected</returns>
        private static IOrderedEnumerable<Measurement> ExtractMeasureTyp(List<Measurement> measureList, MeasureType measureType)
        {
            if (measureList.Any())
            {
                //select measure record of measure-type filter and equal or to less than the dateGrid
                return (IOrderedEnumerable<Measurement>)measureList.Where(a => a.measurementType == measureType)
                    .OrderBy(b => b.measurementTime);
            }
            return null;
        }


        #region get the list of measures-typ automatically (Opional)
        
        /// <summary>
        /// Map all measure record of all measure-Type
        /// </summary>
        /// <param name="measureList">Measure list to map</param>
        /// <param name="startOfSampling">date of the grid or begins the mapping</param>
        /// <returns>measure list for all measure-typ contained in the measure-list</returns>
        public static List<Measurement> MapAllMeasureType(List<Measurement> measureList, DateTime startOfSampling)
        {
            List<Measurement> measureReturned = new List<Measurement>();

            //for each measure-Typ contained in measuren-List
            foreach (var itemTyp in ExtractListMeasureTyp(measureList))
            {
                foreach (var itemMeasure in Map(itemTyp,measureList,startOfSampling))
                {
                    measureReturned.Add(itemMeasure);
                }
            }

            return measureReturned;
        }
        
        /// <summary>
        /// return single measure-Typ list contained in the measure-list
        /// </summary>
        /// <param name="measureList">Measure-list</param>
        /// <returns>single Measure-typ list</returns>
        private static List<MeasureType> ExtractListMeasureTyp(List<Measurement> measureList)
        {
            if (measureList.Any())
            {
                List<MeasureType> valReturned = new List<MeasureType>();

                foreach (var result in measureList.GroupBy(p => p.measurementType, (g) => new { TypeMeasure = g.measurementType }).ToList())
                {
                    valReturned.Add(result.Key);
                }
                return valReturned.ToList();
            }
            return null;
        }

        #endregion

        

        /// <summary>
        /// Filled measure sample
        /// </summary>
        /// <returns></returns>
        private static List<Measurement> FilListOfMeasures()
        {
            List<Measurement> listReturned = new List<Measurement>
            {
                new Measurement
                {
                    measurementTime = new DateTime(2017, 1, 3, 10, 4, 45),
                    measurementValue = 35.79,
                    measurementType = MeasureType.TEMP
                },
                new Measurement()
                {
                    measurementTime = new DateTime(2017, 1, 3, 10, 1, 18),
                    measurementValue = 98.78,
                    measurementType = MeasureType.SPO2
                },
                new Measurement()
                {
                    measurementTime = new DateTime(2017, 1, 3, 10, 9, 7),
                    measurementValue = 35.01,
                    measurementType = MeasureType.TEMP
                },
              new Measurement()
                {
                    measurementTime = new DateTime(2017, 1, 3, 10, 3, 34),
                    measurementValue = 96.49,
                    measurementType = MeasureType.SPO2
                },
                new Measurement()
                {
                    measurementTime = new DateTime(2017, 1, 3, 10, 2, 1),
                    measurementValue = 35.82,
                    measurementType = MeasureType.TEMP
                },
                new Measurement()
                {
                    measurementTime = new DateTime(2017, 1, 3, 10, 5, 0),
                    measurementValue = 97.17,
                    measurementType = MeasureType.SPO2
                },
                new Measurement()
                {
                    measurementTime = new DateTime(2017, 1, 3, 10, 5, 1),
                    measurementValue = 95.08,
                    measurementType = MeasureType.SPO2
                }
            };

            return listReturned;

        }




    }
}
