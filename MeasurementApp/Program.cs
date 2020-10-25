using MeasurementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeasurementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (Measurement itemMeasure in FilListOfMeasures())
            {
                Console.WriteLine($"All Measuren-> Time :{itemMeasure.measurementTime} Type :{itemMeasure.measurementType} Value :{itemMeasure.measurementValue}");
            }

            Console.WriteLine("----------------------------------");           
            
            //Start mapping and write to console
            foreach (Measurement itemMeasure in MapMeasure(FilListOfMeasures(), new DateTime(2017, 1, 3, 10, 0, 0), 5))
            {
                Console.WriteLine($"Time :{itemMeasure.measurementTime} Type :{itemMeasure.measurementType} Value :{itemMeasure.measurementValue}");
            }


        }

        /// <summary>
        /// Map all measure record and return measure-list found   
        /// </summary>
        /// <param name="measureList">Measure list to map</param>
        /// <param name="startDateGrid">date of the grid or begins the mapping </param>
        /// <param name="dateIntervalMinute">measured values interval in minute (startdate and startdate+intervalle) </param>
        public static List<Measurement> MapMeasure(List<Measurement> measureList, DateTime startDateGrid, int dateIntervalMinute)
        {
            List<Measurement> measureReturned = new List<Measurement>();
            DateTime dateGridReset = startDateGrid;
            bool isLoop = true;


            //for each Measure-Typ contained in Measuren-List
            foreach (var itemTyp in ExtractListMeasureTyp(measureList))
            {
                do
                {
                    //Retriev all Measuren of mesure-type 
                    var measureAllValueOfTyp = ExtractMeasureTyp(measureList, itemTyp);

                    //retrieve last measure record between the dateGrid-From and dateGrid-To 
                    var measureFund = measureAllValueOfTyp.LastOrDefault(a => a.measurementTime > startDateGrid &&
                                                           a.measurementTime <= startDateGrid.AddMinutes(dateIntervalMinute));
                    if (measureFund != null)
                    {
                        //set Measure and add to measuren-list
                        Measurement recordFund = new Measurement
                        {
                            measurementTime = startDateGrid.AddMinutes(dateIntervalMinute),
                            measurementValue = measureFund.measurementValue,
                            measurementType = measureFund.measurementType
                        };
                        measureReturned.Add(recordFund);

                        //set next dategrid-from 
                        startDateGrid = startDateGrid.AddMinutes(dateIntervalMinute);

                        //Set flag continue mapping for next measure-type
                        isLoop = true;
                    }
                    else
                    {
                        //Reset start datefrid for next Measure Type
                        startDateGrid = dateGridReset;
                       
                        //Set flag to stopt mapping
                        isLoop = false;
                    }
                } while (isLoop);
                

            }
            
            return measureReturned;

        }


        /// <summary>
        /// Extract and return the measure-value list of the selected measure-type
        /// </summary>
        /// <param name="measureList">Measure-list</param>
        /// <param name="measureType">Measure-type to select</param>
        /// <returns>Measure list of Type selected</returns>
        private static IOrderedEnumerable<Measurement> ExtractMeasureTyp(List<Measurement> measureList, MeasureType measureType)
        {
            if (measureList.Any())
            {
                //select measuren record of measure-type filter and equal or to less than the dateGrid
                return (IOrderedEnumerable<Measurement>)measureList.Where(a => a.measurementType == measureType)
                    .OrderBy(b => b.measurementTime);
            }

            return null;

        }


        /// <summary>
        /// Extract and return Measure-Typ list contained in Measure-list
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
        

        


        /// <summary>
        /// Filled sample measure
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
