﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VisitRoskilde.Interfaces;

namespace VisitRoskilde.Weather
{
    class Weather: ISave, ILoad
    {
        // Serialize the Weather Object
        public bool SaveData()
        {
            throw new NotImplementedException();
        }

        // Deserialize the Weather Object
        public bool LoadData()
        {
            throw new NotImplementedException();
        }
    }
}