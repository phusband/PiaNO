using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PiaNO.Plot
{
    public class PlotterConfiguration : PiaFile
    {
        public string ModelPath
        {
            get { return _getMetaString("user_defined_model_pathname"); }
            set { _setMetaString("user_defined_model_pathname", value); }
        }

        public string ModelBase
        {
            get { return _getMetaString("user_defined_model_basename"); }
            set { _setMetaString("user_defined_model_basename", value); }
        }

        public string DriverPath
        {
            get { return _getMetaString("driver_pathname"); }
            set { _setMetaString("driver_pathname", value); }
        }

        public string DriverVersion
        {
            get { return _getMetaString("driver_version"); }
            set { _setMetaString("driver_version", value); }
        }

        public string DriverTagline
        {
            get { return _getMetaString("driver_tag_line"); }
            set { _setMetaString("driver_tag_line", value); }
        }

        public int ToolkitVersion
        {
            get { return int.Parse(_getMetaString("toolkit_version")); }
            set { _setMetaString("toolkit_version", value.ToString()); }
        }

        public int DriverType
        {
            get { return int.Parse(_getMetaString("driver_type")); }
            set { _setMetaString("driver_type", value.ToString()); }
        }

        public string CanonicalFamily
        {
            get { return _getMetaString("canonical_family_name"); }
            set { _setMetaString("canonical_family_name", value); }
        }

        public bool ShowCustomFirst
        {
            get { return bool.Parse(_getMetaString("show_custom_first")); }
            set { _setMetaString("show_custom_first", value.ToString().ToUpper()); }
        }

        public bool TruetypeAsText
        {
            get { return bool.Parse(_getMetaString("truetype_as_text")); }
            set { _setMetaString("truetype_as_text", value.ToString().ToUpper()); }
        }

        public string CanonicalModel
        {
            get { return _getMetaString("canonical_model_name"); }
            set { _setMetaString("canonical_model_name", value); }
        }

        public string LocalizedFamily
        {
            get { return _getMetaString("localized_family_name"); }
            set { _setMetaString("localized_family_name", value); }
        }

        public string LocalizedModel
        {
            get { return _getMetaString("localized_model_name"); }
            set { _setMetaString("localized_model_name", value); }
        }

        public bool PlotToFile
        {
            get { return bool.Parse(_getMetaString("file_only")); }
            set { _setMetaString("file_only", value.ToString().ToUpper()); }
        }

        //public Dictionary<string, object

        // Meta
        // Media
        // > Size
        //  > Media Description
        //   > Media Bounds
        // > Destination
        // IO
        // Res Color Mem
        // > Resolution
        // Custom
        // > 0
        // > 1
        // > 2
        // > 3
        // > 4
        // > 5
        // > 6

        private string _getMetaString(string name)
        {
            if (!HasChildNodes)
                return null;

            var metaNode = this["meta"];
            if (metaNode == null)
                return null;

            if (!metaNode.Values.ContainsKey(name))
                return null;

            return metaNode.Values[name];
        }
        private void _setMetaString(string name, string value)
        {
            if (!HasChildNodes)
                return;

            var metaNode = this["meta"];
            if (metaNode == null)
                return;

            metaNode.Values[name] = value;
        }

    }
}
