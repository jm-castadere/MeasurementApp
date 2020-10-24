using System;
using System.Collections.Generic;
using System.Text;

namespace MeasurementApp.Models
{
    class Measurement
    {
        public DateTime measurementTime { get; set; }
        public Double measurementValue { get; set; }
        public MeasureType measurementType { get; set; }
        
    }
}
