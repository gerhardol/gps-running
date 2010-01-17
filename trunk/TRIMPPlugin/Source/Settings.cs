using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using ZoneFiveSoftware.Common.Data.Measurement;
using SportTracksTRIMPPlugin.Properties;

namespace SportTracksTRIMPPlugin.Source
{
    class Settings
    {
        private static readonly String prefsPath;

        private static Size windowSize;
        public static Size WindowSize
        {
            get { return windowSize; }
            set
            {
                windowSize = value;
                save();
            }
        }

        private static bool useMaxHR;
        public static bool UseMaxHR
        {
            get { return useMaxHR; }
            set
            {
                useMaxHR = value;
                save();
            }
        }

        private static int startZone;
        public static int StartZone
        {
            get { return startZone; }
            set
            {
                startZone = value;
                save();
            }
        }

        private static IList<double> factors;
        public static IList<double> Factors
        {
            get { return factors; }
            set
            {
                factors = value;
                save();
            }
        }

        public static void reset()
        {
            windowSize = new Size(800, 600);
            startZone = 50;
            factors = new double[] { 1, 1.1, 1.2, 2.2, 4.5 };
            useMaxHR = true;
        }
        
        static Settings()
        {
            prefsPath = Environment.GetEnvironmentVariable("APPDATA") + "/TRIMPPlugin/preferences.xml";
            if (!load())
            {
                Directory.CreateDirectory(Environment.GetEnvironmentVariable("APPDATA") + "/TRIMPPlugin/");
                reset();
                save();
            }
        }

        private static bool load()
        {
            if (!File.Exists(prefsPath)) return false;
            XmlDocument document = new XmlDocument();
            XmlReader reader = new XmlTextReader(prefsPath);
            try
            {               
                document.Load(reader);
                XmlNode elm = document.ChildNodes[0]["view"];
                windowSize = new Size(int.Parse(elm.Attributes["viewWidth"].Value),
                                                    int.Parse(elm.Attributes["viewHeight"].Value));
                startZone = int.Parse(elm.Attributes["startZone"].Value);
                factors = parseFactors(elm.Attributes["factors"].Value);
                useMaxHR = bool.Parse(elm.Attributes["useMaxHR"].Value);
            }
            catch (Exception)
            {
                reader.Close();
                return false;
            }
            reader.Close();
            return true;
        }

        private static IList<double> parseFactors(String str)
        {
            IList<double> list = new List<double>();
            string[] strings = str.Split(new char[] { ' ' });
            foreach (string factor in strings)
            {
                if (!factor.Equals(""))
                {
                    list.Add(parseDouble(factor));
                }
            }
            return list;
        }

        public static double parseDouble(string p)
        {
            //if (!p.Contains(".")) p += ".0";
            double d = double.Parse(p, System.Globalization.NumberStyles.Any);
            return d;
        }

        private static bool saving = false;

        public static void save()
        {
            if (saving) return;
            saving = true;
            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement("trimp");
            document.AppendChild(root);

            XmlElement resultSetupElm = document.CreateElement("view");
            root.AppendChild(resultSetupElm);
            resultSetupElm.SetAttribute("viewWidth", windowSize.Width.ToString());
            resultSetupElm.SetAttribute("viewHeight", windowSize.Height.ToString());
            resultSetupElm.SetAttribute("startZone", startZone.ToString());
            resultSetupElm.SetAttribute("useMaxHR", useMaxHR.ToString(NumberFormatInfo.InvariantInfo));
            String str = "";
            foreach (double factor in factors)
            {
                str += factor + " ";
            }
            resultSetupElm.SetAttribute("factors", str);

            //StringWriter xmlString = new StringWriter();
            //XmlTextWriter writer2 = new XmlTextWriter(xmlString);
            XmlTextWriter writer = new XmlTextWriter(prefsPath, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 3;
            writer.IndentChar = ' ';
            document.WriteContentTo(writer);
            //document.WriteContentTo(writer2);
            writer.Close();
            if (PropertyChanged != null)
            {
                PropertyChanged(null, null);
            }
            saving = false;
        }

        public static double convertFrom(double p, Length.Units metric)
        {
            switch (metric)
            {
                case Length.Units.Kilometer: return p / 1000;
                case Length.Units.Mile: return p / (1.609344 * 1000);
                case Length.Units.Foot: return p * 3.2808399;
                case Length.Units.Inch: return p * 39.370079;
                case Length.Units.Centimeter: return p * 100;
                case Length.Units.Yard: return p * 1.0936133;
            }
            return p;
        }

        public static double convertFromDistance(double p)
        {
            return convertFrom(p, Plugin.GetApplication().SystemPreferences.DistanceUnits);
        }

        public static double convertFromElevation(double p)
        {
            return convertFrom(p, Plugin.GetApplication().SystemPreferences.ElevationUnits);
        }
        
        public static string present(double p)
        {
            return String.Format("{0:0.000}", p);
        }

        public static string present(double p, int n)
        {
            if (n < 1) return ((int)Math.Round(p)).ToString();
            string s = "0";
            for (int i = 0; i < n - 1; i++)
                s += "0";
            return String.Format("{0:0." + s + "}", p);
        }

        #region INotifyPropertyChanged Members

        public static event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion

        public static String translateUnit(Length.Units unit)
        {
            switch (unit)
            {
                case Length.Units.Centimeter: return Resources.UnitCentimeter;
                case Length.Units.Foot: return Resources.UnitFoot;
                case Length.Units.Inch: return Resources.UnitInch;
                case Length.Units.Kilometer: return Resources.UnitKilometer;
                case Length.Units.Meter: return Resources.UnitMeter;
                case Length.Units.Mile: return Resources.UnitMile;
                case Length.Units.Yard: return Resources.UnitYard;
            }
            return null;
        }

        public static String DistanceUnit
        {
            get
            {
                return translateUnit(Plugin.GetApplication().SystemPreferences.DistanceUnits);
            }
        }

        public static String ElevationUnit
        {
            get
            {
                return translateUnit(Plugin.GetApplication().SystemPreferences.ElevationUnits);
            }
        }

        public static String translateUnitShort(Length.Units unit)
        {
            switch (unit)
            {
                case Length.Units.Centimeter: return Resources.UnitCentimeterShort;
                case Length.Units.Foot: return Resources.UnitFootShort;
                case Length.Units.Inch: return Resources.UnitInchShort;
                case Length.Units.Kilometer: return Resources.UnitKilometerShort;
                case Length.Units.Meter: return Resources.UnitMeterShort;
                case Length.Units.Mile: return Resources.UnitMileShort;
                case Length.Units.Yard: return Resources.UnitYardShort;
            }
            return null;
        }

        public static String DistanceUnitShort
        {
            get
            {
                return translateUnitShort(Plugin.GetApplication().SystemPreferences.DistanceUnits);
            }
        }

        public static String ElevationUnitShort
        {
            get
            {
                return translateUnitShort(Plugin.GetApplication().SystemPreferences.ElevationUnits);
            }
        }
    }
}
