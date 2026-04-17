using QuantityMeasurementAppBusinessLayer.Interfaces;
using QuantityMeasurementAppModel.Entities;
using QuantityMeasurementAppModel.Enums;
using QuantityMeasurementAppRepositoryLayer.Interfaces;

namespace QuantityMeasurementAppBusinessLayer.Services
{
    public class QuantityMeasurementService : IQuantityMeasurementService
    {
        private readonly IQuantityMeasurementRepository _repository;

        public QuantityMeasurementService(IQuantityMeasurementRepository repository)
        {
            _repository = repository;
        }

        public string Compare(double value1, string unit1, double value2, string unit2, string quantityType)
        {
            ValidateTwoValueRequest(value1, unit1, value2, unit2, quantityType);

            string normalizedQuantityType = NormalizeQuantityType(quantityType);
            string displayUnit1 = FormatUnitName(unit1, normalizedQuantityType);
            string displayUnit2 = FormatUnitName(unit2, normalizedQuantityType);

            double baseValue1 = ConvertToBaseUnit(value1, unit1, normalizedQuantityType);
            double baseValue2 = ConvertToBaseUnit(value2, unit2, normalizedQuantityType);

            string outputText;
            if (baseValue1 > baseValue2)
                outputText = $"{value1} {displayUnit1} is greater than {value2} {displayUnit2}";
            else if (baseValue1 < baseValue2)
                outputText = $"{value2} {displayUnit2} is greater than {value1} {displayUnit1}";
            else
                outputText = "Both values are equal";

            _repository.SaveMeasurementRecord(new MeasurementRecord
            {
                Timestamp = DateTime.UtcNow,
                Operation = OperationType.Compare,
                Input1Value = value1, Input1Unit = unit1, Input1Type = quantityType,
                Input2Value = value2, Input2Unit = unit2, Input2Type = quantityType,
                OutputText = outputText,
                SuccessFlag = true
            });

            return outputText;
        }

        public string Convert(double value, string fromUnit, string toUnit, string quantityType)
        {
            ValidateConvertRequest(value, fromUnit, toUnit, quantityType);

            string normalizedQuantityType = NormalizeQuantityType(quantityType);
            string displayFromUnit = FormatUnitName(fromUnit, normalizedQuantityType);
            string displayToUnit = FormatUnitName(toUnit, normalizedQuantityType);

            double baseValue = ConvertToBaseUnit(value, fromUnit, normalizedQuantityType);
            double convertedValue = ConvertFromBaseUnit(baseValue, toUnit, normalizedQuantityType);
            string outputText = $"{value} {displayFromUnit} = {Math.Round(convertedValue, 4)} {displayToUnit}";

            _repository.SaveMeasurementRecord(new MeasurementRecord
            {
                Timestamp = DateTime.UtcNow,
                Operation = OperationType.Convert,
                OriginalValue = value, OriginalUnit = fromUnit, OriginalType = quantityType,
                DesiredUnit = toUnit,
                OutputValue = Math.Round(convertedValue, 4), OutputUnit = toUnit,
                OutputText = outputText,
                SuccessFlag = true
            });

            return outputText;
        }

        public string Add(double value1, string unit1, double value2, string unit2, string quantityType)
        {
            ValidateTwoValueRequest(value1, unit1, value2, unit2, quantityType);

            string normalizedQuantityType = NormalizeQuantityType(quantityType);

            if (normalizedQuantityType == "temperature")
                throw new Exception("Add operation is not supported for Temperature.");

            string displayUnit1 = FormatUnitName(unit1, normalizedQuantityType);
            string displayUnit2 = FormatUnitName(unit2, normalizedQuantityType);

            double baseValue1 = ConvertToBaseUnit(value1, unit1, normalizedQuantityType);
            double baseValue2 = ConvertToBaseUnit(value2, unit2, normalizedQuantityType);
            double sumBase = baseValue1 + baseValue2;
            double finalValue = ConvertFromBaseUnit(sumBase, unit1, normalizedQuantityType);
            string outputText = $"{value1} {displayUnit1} + {value2} {displayUnit2} = {Math.Round(finalValue, 4)} {displayUnit1}";

            _repository.SaveMeasurementRecord(new MeasurementRecord
            {
                Timestamp = DateTime.UtcNow,
                Operation = OperationType.Add,
                Input1Value = value1, Input1Unit = unit1, Input1Type = quantityType,
                Input2Value = value2, Input2Unit = unit2, Input2Type = quantityType,
                OutputValue = Math.Round(finalValue, 4), OutputUnit = unit1,
                OutputText = outputText,
                SuccessFlag = true
            });

            return outputText;
        }

        public string Subtract(double value1, string unit1, double value2, string unit2, string quantityType)
        {
            ValidateTwoValueRequest(value1, unit1, value2, unit2, quantityType);

            string normalizedQuantityType = NormalizeQuantityType(quantityType);

            if (normalizedQuantityType == "temperature")
                throw new Exception("Subtract operation is not supported for Temperature.");

            string displayUnit1 = FormatUnitName(unit1, normalizedQuantityType);
            string displayUnit2 = FormatUnitName(unit2, normalizedQuantityType);

            double baseValue1 = ConvertToBaseUnit(value1, unit1, normalizedQuantityType);
            double baseValue2 = ConvertToBaseUnit(value2, unit2, normalizedQuantityType);
            double differenceBase = baseValue1 - baseValue2;
            double finalValue = ConvertFromBaseUnit(differenceBase, unit1, normalizedQuantityType);
            string outputText = $"{value1} {displayUnit1} - {value2} {displayUnit2} = {Math.Round(finalValue, 4)} {displayUnit1}";

            _repository.SaveMeasurementRecord(new MeasurementRecord
            {
                Timestamp = DateTime.UtcNow,
                Operation = OperationType.Subtract,
                Input1Value = value1, Input1Unit = unit1, Input1Type = quantityType,
                Input2Value = value2, Input2Unit = unit2, Input2Type = quantityType,
                OutputValue = Math.Round(finalValue, 4), OutputUnit = unit1,
                OutputText = outputText,
                SuccessFlag = true
            });

            return outputText;
        }

        public string Divide(double value1, string unit1, double value2, string unit2, string quantityType)
        {
            ValidateTwoValueRequest(value1, unit1, value2, unit2, quantityType);

            string normalizedQuantityType = NormalizeQuantityType(quantityType);

            if (normalizedQuantityType == "temperature")
                throw new Exception("Divide operation is not supported for Temperature.");

            string displayUnit1 = FormatUnitName(unit1, normalizedQuantityType);
            string displayUnit2 = FormatUnitName(unit2, normalizedQuantityType);

            double baseValue1 = ConvertToBaseUnit(value1, unit1, normalizedQuantityType);
            double baseValue2 = ConvertToBaseUnit(value2, unit2, normalizedQuantityType);

            if (baseValue2 == 0)
                throw new Exception("Cannot divide by zero.");

            double result = baseValue1 / baseValue2;
            string outputText = $"{value1} {displayUnit1} / {value2} {displayUnit2} = {Math.Round(result, 4)}";

            _repository.SaveMeasurementRecord(new MeasurementRecord
            {
                Timestamp = DateTime.UtcNow,
                Operation = OperationType.Divide,
                Input1Value = value1, Input1Unit = unit1, Input1Type = quantityType,
                Input2Value = value2, Input2Unit = unit2, Input2Type = quantityType,
                OutputValue = Math.Round(result, 4),
                OutputText = outputText,
                SuccessFlag = true
            });

            return outputText;
        }

        public List<MeasurementRecord> GetHistory()
        {
            return _repository.GetAllRecords();
        }

        public int GetCount()
        {
            return _repository.GetRecordCount();
        }

        public List<string> GetOperationTypes()
        {
            return new List<string> { "Convert", "Add", "Subtract", "Divide", "Compare" };
        }

        public List<string> GetMeasurementTypes()
        {
            return new List<string> { "Length", "Weight", "Volume", "Temperature" };
        }

        private void ValidateConvertRequest(double value, string fromUnit, string toUnit, string quantityType)
        {
            if (string.IsNullOrWhiteSpace(fromUnit))
            {
                throw new Exception("FromUnit is required.");
            }

            if (string.IsNullOrWhiteSpace(toUnit))
            {
                throw new Exception("ToUnit is required.");
            }

            if (string.IsNullOrWhiteSpace(quantityType))
            {
                throw new Exception("QuantityType is required.");
            }

            string normalizedQuantityType = NormalizeQuantityType(quantityType);

            ValidateUnitForQuantityType(fromUnit, normalizedQuantityType);
            ValidateUnitForQuantityType(toUnit, normalizedQuantityType);
        }

        private void ValidateTwoValueRequest(double value1, string unit1, double value2, string unit2, string quantityType)
        {
            if (string.IsNullOrWhiteSpace(unit1))
            {
                throw new Exception("Unit1 is required.");
            }

            if (string.IsNullOrWhiteSpace(unit2))
            {
                throw new Exception("Unit2 is required.");
            }

            if (string.IsNullOrWhiteSpace(quantityType))
            {
                throw new Exception("QuantityType is required.");
            }

            string normalizedQuantityType = NormalizeQuantityType(quantityType);

            ValidateUnitForQuantityType(unit1, normalizedQuantityType);
            ValidateUnitForQuantityType(unit2, normalizedQuantityType);
        }

        private string NormalizeQuantityType(string quantityType)
        {
            return quantityType.Trim().ToLower();
        }

        private void ValidateUnitForQuantityType(string unit, string quantityType)
        {
            string normalizedUnit = NormalizeUnit(unit);

            var allowedUnits = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "length", new List<string> { "feet", "foot", "inch", "yard" } },
                { "weight", new List<string> { "milligram", "gram", "kilogram" } },
                { "volume", new List<string> { "milliliter", "liter" } },
                { "temperature", new List<string> { "celsius", "fahrenheit", "kelvin" } }
            };

            if (!allowedUnits.ContainsKey(quantityType))
            {
                throw new Exception($"Invalid quantity type '{quantityType}'.");
            }

            if (!allowedUnits[quantityType].Contains(normalizedUnit))
            {
                throw new Exception($"Invalid unit '{unit}' for quantity type '{quantityType}'.");
            }
        }

        private double ConvertToBaseUnit(double value, string unit, string quantityType)
        {
            string normalizedUnit = NormalizeUnit(unit);

            if (quantityType == "temperature")
            {
                return ConvertTemperatureToCelsius(value, normalizedUnit);
            }

            Dictionary<string, double> factors = GetUnitFactors(quantityType);

            if (!factors.ContainsKey(normalizedUnit))
            {
                throw new Exception($"Invalid unit '{unit}' for quantity type '{quantityType}'.");
            }

            return value * factors[normalizedUnit];
        }

        private double ConvertFromBaseUnit(double value, string unit, string quantityType)
        {
            string normalizedUnit = NormalizeUnit(unit);

            if (quantityType == "temperature")
            {
                return ConvertCelsiusToTarget(value, normalizedUnit);
            }

            Dictionary<string, double> factors = GetUnitFactors(quantityType);

            if (!factors.ContainsKey(normalizedUnit))
            {
                throw new Exception($"Invalid unit '{unit}' for quantity type '{quantityType}'.");
            }

            return value / factors[normalizedUnit];
        }

        private Dictionary<string, double> GetUnitFactors(string quantityType)
        {
            switch (quantityType)
            {
                case "length":
                    return new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "feet", 12 },
                        { "foot", 12 },
                        { "inch", 1 },
                        { "yard", 36 }
                    };

                case "weight":
                    return new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "milligram", 0.001 },
                        { "gram", 1 },
                        { "kilogram", 1000 }
                    };

                case "volume":
                    return new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "milliliter", 0.001 },
                        { "liter", 1 }
                    };

                default:
                    throw new Exception("Invalid quantity type.");
            }
        }

        private double ConvertTemperatureToCelsius(double value, string unit)
        {
            switch (unit)
            {
                case "celsius":
                    return value;

                case "fahrenheit":
                    return (value - 32) * 5 / 9;

                case "kelvin":
                    return value - 273.15;

                default:
                    throw new Exception("Invalid temperature unit.");
            }
        }

        private double ConvertCelsiusToTarget(double value, string unit)
        {
            switch (unit)
            {
                case "celsius":
                    return value;

                case "fahrenheit":
                    return (value * 9 / 5) + 32;

                case "kelvin":
                    return value + 273.15;

                default:
                    throw new Exception("Invalid temperature unit.");
            }
        }

        private string NormalizeUnit(string unit)
        {
            return unit.Trim().ToLower();
        }

        private string FormatUnitName(string unit, string quantityType)
        {
            string normalizedUnit = NormalizeUnit(unit);

            if (quantityType == "length")
            {
                return normalizedUnit switch
                {
                    "feet" => "Feet",
                    "foot" => "Foot",
                    "inch" => "Inch",
                    "yard" => "Yard",
                    _ => unit
                };
            }

            if (quantityType == "weight")
            {
                return normalizedUnit switch
                {
                    "milligram" => "Milligram",
                    "gram" => "Gram",
                    "kilogram" => "Kilogram",
                    _ => unit
                };
            }

            if (quantityType == "volume")
            {
                return normalizedUnit switch
                {
                    "milliliter" => "Milliliter",
                    "liter" => "Liter",
                    _ => unit
                };
            }

            if (quantityType == "temperature")
            {
                return normalizedUnit switch
                {
                    "celsius" => "Celsius",
                    "fahrenheit" => "Fahrenheit",
                    "kelvin" => "Kelvin",
                    _ => unit
                };
            }

            return unit;
        }
    }
}