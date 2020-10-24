using System;
using System.Collections.Generic;
using System.Linq;
using MeasurementApp.Models;

namespace MeasurementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Measure list Filled
            Measurement[] measureSample = FilledListOfMeasures();


            var listTyp = ExtractListMeasureTyp(measureSample);

            foreach (var itemTyp in listTyp)
            {
                //Extract mesure typ TEMP
                foreach (Measurement itemMeasure in ExtractMeasureTyp(measureSample, itemTyp))
                {
                    Console.WriteLine($"{itemMeasure.measurementTime} Type {itemMeasure.measurementType}");
                }

            }
        }



        /// <summary>
        /// Extract and return Measure list of Type selected
        /// </summary>
        /// <param name="measureList">Complete Measure list</param>
        /// <param name="measureType">Measure type to select</param>
        /// <returns>Measure list of Type selected</returns>
        private static IOrderedEnumerable<Measurement> ExtractMeasureTyp(Measurement[] measureList, MeasureType measureType)
        {
            if (measureList.Any())
            {
                //select measure of type filter and sort to measure time
                return measureList.Where(a => a.measurementType == measureType)
                    .OrderBy(b => b.measurementTime);
            }

            return null;

        }
        

        /// <summary>
        /// Extract and return MeasureTyp list contained in Measurelist
        /// </summary>
        /// <param name="measureList">measure list with measuretyp</param>
        /// <returns>Measure-typ list</returns>
        private static List<MeasureType> ExtractListMeasureTyp(Measurement[] measureList)
        {
            if (measureList.Any())
            {
                
                List<MeasureType> valReturned = new List<MeasureType>();

                foreach (var result in measureList.GroupBy(p => p.measurementType, (g) => new { TypeMeasure = g.measurementType}).ToList())
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

        

    }
}
