using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrequencyInputBox.Model
{
    public enum UnitType
    {
        Hz,
        kHz,
        MHz,
        GHz,
        THz,
        unknown
    }

    public class Frequency
    {
        public double value;
        public UnitType unit;

        public double Hz
        {
            get
            {
                switch (unit)
                {
                    case UnitType.Hz: return value;
                    case UnitType.kHz: return value * 1000;
                    case UnitType.MHz: return value * 1_000_000;
                    case UnitType.GHz: return value * 1_000_000_000;
                    case UnitType.THz: return value * 1_000_000_000_000;
                    default: return value;
                }
            }
        }

        internal static UnitType ConvertStringToUnitType(string value)
        {
            switch (value)
            {
                case "Hz": 
                case "Гц":
                case "Г":
                case "H":
                case "г":
                case "h": return UnitType.Hz;
                case "kHz":
                case "кГц":
                case "К":
                case "K":
                case "к":
                case "k": return UnitType.kHz;
                default: return UnitType.unknown;
            }
        }
    }
}

