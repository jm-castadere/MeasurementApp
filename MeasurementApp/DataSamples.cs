using MeasurementApp.Models;
using System;
using System.Collections.Generic;

namespace MeasurementApp
{
    public class DataSamples
    {

        //sample start date
        public static DateTime startOfSampling = new DateTime(2017, 1, 3, 10, 0, 0);
        
        /// <summary>
        /// Filled measure sample
        /// </summary>
        /// <returns>Die Eingabewerte sind zeitlich nicht sortiert</returns>
        public static List<Measurement> GetListOfMeasures()
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
