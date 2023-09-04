using System.Globalization;
using UnitsNet;

namespace UseCase14
{
    public static class UnitsNetExtensions
    {
        public static string ToLocalizedString(this Length length, CultureInfo culture)
        {
            switch (culture.Name)
            {
                case "en-US": return $"{length.ToUnit(UnitsNet.Units.LengthUnit.Inch)}";
                case "fr-FR": return $"{length.ToUnit(UnitsNet.Units.LengthUnit.Meter)}";
                case "uk-UA": return $"{length.ToUnit(UnitsNet.Units.LengthUnit.Meter).Value.ToString("n", culture)} м";

                default:
                    return length.ToString();
            }
        }

        public static string ToLocalizedString(this Volume volume, CultureInfo culture)
        {
            switch (culture.Name)
            {
                case "en-US": return $"{volume.ToUnit(UnitsNet.Units.VolumeUnit.UsOunce)}";
                case "fr-FR": return $"{volume.ToUnit(UnitsNet.Units.VolumeUnit.Liter)}";
                case "uk-UA": return $"{volume.ToUnit(UnitsNet.Units.VolumeUnit.Liter).Value.ToString("n", culture)} л";

                default:
                    return volume.ToString();
            }
        }

        public static string ToLocalizedString(this Mass mass, CultureInfo culture)
        {
            switch (culture.Name)
            {
                case "en-US": return $"{mass.ToUnit(UnitsNet.Units.MassUnit.Pound)}";
                case "fr-FR": return $"{mass.ToUnit(UnitsNet.Units.MassUnit.Kilogram)}";
                case "uk-UA": return $"{mass.ToUnit(UnitsNet.Units.MassUnit.Kilogram).Value.ToString("n", culture)} кг";

                default:
                    return mass.ToString();
            }
        }
    }
}
