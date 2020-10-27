using System.Collections.Generic;
using NUnit.Framework;

namespace NUnitTestMeasure
{
    public class TestMeasure
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestMeasureTypeTemp()
        {
            //measure refence for first Measure-typ TEMP
            double measureValueRef = 35.79; 

            //Retrieve first measure for type TEMP 
            List<MeasurementApp.Models.Measurement> valMeasure = MeasurementApp.Program.Map(MeasurementApp.Models.MeasureType.TEMP,
                MeasurementApp.DataSamples.GetListOfMeasures(), MeasurementApp.DataSamples.startOfSampling);
            
            Assert.AreEqual(valMeasure[0].measurementValue,measureValueRef);
        }


        [Test]
        public void TestMeasureTypeSpo2()
        {
            ////measure refence for fisrt Measure-typ SPO2
            double measureValueRef = 97.17; 

            //Retrieve first measure for type SPO2 
            List<MeasurementApp.Models.Measurement> valMeasure = MeasurementApp.Program.Map(MeasurementApp.Models.MeasureType.SPO2,
                MeasurementApp.DataSamples.GetListOfMeasures(), MeasurementApp.DataSamples.startOfSampling);
            
            
            Assert.AreEqual(valMeasure[0].measurementValue,measureValueRef);
        }

    }
}