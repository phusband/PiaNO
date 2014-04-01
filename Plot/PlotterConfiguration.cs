//
//  Copyright © 2014 Parrish Husband (parrish.husband@gmail.com)
//  The MIT License (MIT) - See LICENSE.txt for further details.
//

namespace PiaNO.Plot
{
    public class PlotterConfiguration : PiaFile
    {
        #region Properties

        public string ModelPath
        {
            get { return _getMetaString("user_defined_model_pathname_str"); }
            set { _setMetaString("user_defined_model_pathname_str", value); }
        }

        public string ModelBase
        {
            get { return _getMetaString("user_defined_model_basename_str"); }
            set { _setMetaString("user_defined_model_basename_str", value); }
        }

        public string DriverPath
        {
            get { return _getMetaString("driver_pathname_str"); }
            set { _setMetaString("driver_pathname_str", value); }
        }

        public string DriverVersion
        {
            get { return _getMetaString("driver_version_str"); }
            set { _setMetaString("driver_version_str", value); }
        }

        public string DriverTagline
        {
            get { return _getMetaString("driver_tag_line_str"); }
            set { _setMetaString("driver_tag_line_str", value); }
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
            get { return _getMetaString("canonical_family_name_str"); }
            set { _setMetaString("canonical_family_name_str", value); }
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
            get { return _getMetaString("canonical_model_name_str"); }
            set { _setMetaString("canonical_model_name_str", value); }
        }

        public string LocalizedFamily
        {
            get { return _getMetaString("localized_family_name_str"); }
            set { _setMetaString("localized_family_name_str", value); }
        }

        public string LocalizedModel
        {
            get { return _getMetaString("localized_model_name_str"); }
            set { _setMetaString("localized_model_name_str", value); }
        }

        public bool PlotToFile
        {
            get { return bool.Parse(_getMetaString("file_only")); }
            set { _setMetaString("file_only", value.ToString().ToUpper()); }
        }

        #endregion

        #region Constructors

        public PlotterConfiguration() 
            : base() { }

        public PlotterConfiguration(string fileName)
            : base(fileName) { }

        #endregion

        #region Methods

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

        #endregion
    }
}
