using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementAppBusinessLayer.Interfaces;
using QuantityMeasurementAppModel.DTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace QuantityMeasurementAppAPI.Controllers
{
    /// <summary>
    /// APIs for quantity measurement operations such as add, subtract, compare, divide, convert, and history.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class QuantityController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;

        public QuantityController(IQuantityMeasurementService service)
        {
            _service = service;
        }

        /// <summary>
        /// Add two quantities.
        /// </summary>
        /// <param name="request">Input values, units, and quantity type.</param>
        /// <returns>Returns the sum of the two quantities.</returns>
        [HttpPost("add")]
        [SwaggerOperation(Summary = "Add two quantities", Description = "Adds two values with units and returns the result.")]
        public IActionResult Add([FromBody] AddRequestDto request)
        {
            try
            {
                var result = _service.Add(request.Value1, request.Unit1, request.Value2, request.Unit2, request.QuantityType);
                return Ok(new { success = true, result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Subtract one quantity from another.
        /// </summary>
        /// <param name="request">Input values, units, and quantity type.</param>
        /// <returns>Returns the subtraction result.</returns>
        [HttpPost("subtract")]
        [SwaggerOperation(Summary = "Subtract quantities", Description = "Subtracts the second quantity from the first quantity.")]
        public IActionResult Subtract([FromBody] SubtractRequestDto request)
        {
            try
            {
                var result = _service.Subtract(request.Value1, request.Unit1, request.Value2, request.Unit2, request.QuantityType);
                return Ok(new { success = true, result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Compare two quantities.
        /// </summary>
        /// <param name="request">Input values, units, and quantity type.</param>
        /// <returns>Returns which quantity is greater or if both are equal.</returns>
        [HttpPost("compare")]
        [SwaggerOperation(Summary = "Compare quantities", Description = "Compares two quantities and tells which one is greater or if both are equal.")]
        public IActionResult Compare([FromBody] CompareRequestDto request)
        {
            try
            {
                var result = _service.Compare(request.Value1, request.Unit1, request.Value2, request.Unit2, request.QuantityType);
                return Ok(new { success = true, result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Divide one quantity by another.
        /// </summary>
        /// <param name="request">Input values, units, and quantity type.</param>
        /// <returns>Returns the division result.</returns>
        [HttpPost("divide")]
        [SwaggerOperation(Summary = "Divide quantities", Description = "Divides the first quantity by the second quantity.")]
        public IActionResult Divide([FromBody] DivideRequestDto request)
        {
            try
            {
                var result = _service.Divide(request.Value1, request.Unit1, request.Value2, request.Unit2, request.QuantityType);
                return Ok(new { success = true, result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Convert a quantity from one unit to another.
        /// </summary>
        /// <param name="request">Input value, source unit, target unit, and quantity type.</param>
        /// <returns>Returns the converted value.</returns>
        [HttpPost("convert")]
        [SwaggerOperation(Summary = "Convert quantity", Description = "Converts a quantity from one unit to another unit.")]
        public IActionResult Convert([FromBody] ConvertRequestDto request)
        {
            try
            {
                var result = _service.Convert(request.Value, request.FromUnit, request.ToUnit, request.QuantityType);
                return Ok(new { success = true, result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Calculate a quantity operation based on the requested operation type.
        /// </summary>
        /// <param name="request">Input values, units, quantity type, and desired operation.</param>
        /// <returns>Returns the calculation result for add, subtract, divide, compare, or convert.</returns>
        [HttpPost("calculate")]
        [SwaggerOperation(Summary = "Calculate quantity", Description = "Performs a requested quantity operation such as add, subtract, compare, divide, or convert.")]
        public IActionResult Calculate([FromBody] CalculationRequestDto request)
        {
            try
            {
                string operation = request.Operation?.Trim().ToLower() ?? string.Empty;
                string result;

                switch (operation)
                {
                    case "add":
                        result = _service.Add(request.Value1, request.Unit1, request.Value2, request.Unit2, request.QuantityType);
                        break;

                    case "subtract":
                        result = _service.Subtract(request.Value1, request.Unit1, request.Value2, request.Unit2, request.QuantityType);
                        break;

                    case "compare":
                        result = _service.Compare(request.Value1, request.Unit1, request.Value2, request.Unit2, request.QuantityType);
                        break;

                    case "divide":
                        result = _service.Divide(request.Value1, request.Unit1, request.Value2, request.Unit2, request.QuantityType);
                        break;

                    case "convert":
                        result = _service.Convert(request.Value, request.FromUnit, request.ToUnit, request.QuantityType);
                        break;

                    default:
                        return BadRequest(new { success = false, message = "Unsupported operation type." });
                }

                return Ok(new { success = true, result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get complete history of measurement operations.
        /// </summary>
        /// <returns>Returns all stored quantity measurement records.</returns>
        [HttpGet("history")]
        [SwaggerOperation(Summary = "Get history", Description = "Returns all stored quantity measurement operation records.")]
        public IActionResult GetHistory()
        {
            try
            {
                var result = _service.GetHistory();
                return Ok(new { success = true, result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get total count of stored operations.
        /// </summary>
        /// <returns>Returns total number of saved operations.</returns>
        [HttpGet("count")]
        [SwaggerOperation(Summary = "Get count", Description = "Returns total number of saved quantity measurement operations.")]
        public IActionResult GetCount()
        {
            try
            {
                var result = _service.GetCount();
                return Ok(new { success = true, result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get all supported operation types.
        /// </summary>
        /// <returns>Returns the list of supported operation types.</returns>
        [HttpGet("operationtype")]
        [SwaggerOperation(Summary = "Get operation types", Description = "Returns the list of supported operation types.")]
        public IActionResult GetOperationType()
        {
            try
            {
                var result = _service.GetOperationTypes();
                return Ok(new { success = true, result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get all supported measurement types.
        /// </summary>
        /// <returns>Returns the list of supported measurement categories.</returns>
        [HttpGet("measurementtype")]
        [SwaggerOperation(Summary = "Get measurement types", Description = "Returns the list of supported measurement categories.")]
        public IActionResult GetMeasurementType()
        {
            try
            {
                var result = _service.GetMeasurementTypes();
                return Ok(new { success = true, result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}