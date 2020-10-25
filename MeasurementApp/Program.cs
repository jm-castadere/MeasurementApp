using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using MeasurementApp.Models;

namespace MeasurementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Start mapping

            ////for each Measurement record
            foreach (Measurement itemMeasure in MapMeasure(FilListOfMeasures(), new DateTime(2017, 1, 3, 10, 0, 0), 5))
            {
                Console.WriteLine($"Time :{itemMeasure.measurementTime} Type :{itemMeasure.measurementType} Value :{itemMeasure.measurementValue}");
            }


        }

        /// <summary>
        /// Map all measuren record and return measuren-list found   
        /// </summary>
        /// <param name="measureList">Measure list to map</param>
        /// <param name="startDateGrid">date of the grid or begins the mapping </param>
        /// <param name="dateIntervalMinute">measured values interval in minute (startdate+ intervalle) </param>
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
                    //Retriev all Measuren of mesure type and in time interval
                    var measureAllValueOfTyp = ExtractMeasureTyp(measureList, itemTyp);

                    //retrieve last measure record between the dates Grid From and To
                    var measureFund = measureAllValueOfTyp.LastOrDefault(a => a.measurementTime > startDateGrid &&
                                                                              a.measurementTime <=
                                                                              startDateGrid.AddMinutes(
                                                                                  dateIntervalMinute));
                    if (measureFund != null)
                    {
                        Measurement recordFund = new Measurement
                        {
                            measurementTime = startDateGrid.AddMinutes(dateIntervalMinute),
                            measurementValue = measureFund.measurementValue,
                            measurementType = measureFund.measurementType
                        };

                        measureReturned.Add(recordFund);

                        startDateGrid = startDateGrid.AddMinutes(dateIntervalMinute);

                        isLoop = true;
                    }
                    else
                    {
                        startDateGrid = dateGridReset;
                        isLoop = false;
                    }
                } while (isLoop);
                

            }


            return measureReturned;

          

        }




        /// <summary>
        /// Extract and return the measuren-value list of the selected measure-type
        /// </summary>
        /// <param name="measureList">Measuren-list</param>
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
        /// Extract and return Measure-Typ list contained in Measuren-list
        /// </summary>
        /// <param name="measureList">Measuren-list</param>
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
        /// Creates and returns a list of sample measures 
        /// </summary>
        /// <returns>Measurement list</returns>
        private static Measurement[] FilledListOfMeasures()
        {
            Measurement[] lisMeasurement = {
                new Measurement(){ measurementTime= new DateTime(2017, 1, 3,10,4,45), measurementValue= 35.79,measurementType = MeasureType.TEMP},
                new Measurement(){ measurementTime= new DateTime(2017, 1, 3,10,1,18), measurementValue= 98.78,measurementType = MeasureType.SPO2},
                new Measurement(){ measurementTime= new DateTime(2017, 1, 3,10,9,7), measurementValue= 35.01,measurementType = MeasureType.TEMP},
                new Measurement(){ measurementTime= new DateTime(2017, 1, 3,10,3,34), measurementValue= 96.49,measurementType = MeasureType.SPO2},
                new Measurement(){ measurementTime= new DateTime(2017, 1, 3,10,2,1), measurementValue= 35.82,measurementType = MeasureType.TEMP},
                new Measurement(){ measurementTime= new DateTime(2017, 1, 3,10,5,0), measurementValue= 97.17,measurementType = MeasureType.SPO2},
                new Measurement(){ measurementTime= new DateTime(2017, 1, 3,10,5,1), measurementValue= 95.08,measurementType = MeasureType.SPO2},
            };

            return lisMeasurement;

        }


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
