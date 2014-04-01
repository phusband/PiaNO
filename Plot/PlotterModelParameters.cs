//
//  Copyright © 2014 Parrish Husband (parrish.husband@gmail.com)
//  The MIT License (MIT) - See LICENSE.txt for further details.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PiaNO.Plot
{
    public class PlotterModelParameters : PiaFile
    {
        public string ModelPathName { get; set; }
        public string ModelBaseName { get; set; }
        public string DriverPathName { get; set; }
        public string DriverVersion { get; set; }
        public string DriverTagline { get; set; }
        public short TookitVersion { get; set; }
        public short DriverType { get; set; }
        public string CanonicalFamilyName { get; set; }
        public bool ShowCustomFirst { get; set; }
        public bool TrueTypeAsText { get; set; }
        public string CanonicalModelName { get; set; }
        public string LocalizedFamilyName { get; set; }
        public string LocalizedModelName { get; set; }
        public bool FileOnly { get; set; }
        public long ModelAbilities { get; set; }
        public string UDMDescription { get; set; }
        public string DeviceName { get; set; }
        public string DriverName { get; set; }
        public string Shortname { get; set; }
        public string FriendlyName { get; set; }
        public short DmDriverVersion { get; set; }
        public bool DefaultSystemConfig { get; set; }
        public string Platform { get; set; }
        public string Locale { get; set; } // Check for hex

        // Mod
        // > Media
        //  > Size
        // > Description
        // Del
        // > Media
        // Udm
        // > Calibration
        // > Media
        // Hidden
        // > Media
    }
}
