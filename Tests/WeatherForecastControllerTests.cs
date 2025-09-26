using Xunit;

namespace LabPipeLine.Tests
{
    public class WeatherForecastControllerTests
    {
        private readonly WeatherForecastController _controller;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastControllerTests()
        {
            // Use a mock logger for testing
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = loggerFactory.CreateLogger<WeatherForecastController>();
            _controller = new WeatherForecastController(_logger);
        }

        [Fact]
        public void Get_ReturnsFiveWeatherForecasts()
        {
            // Act
            var result = _controller.GetForecast();

            // Assert
            Assert.NotNull(result);
            var forecasts = result.ToArray();
            Assert.Equal(5, forecasts.Length);
            foreach (var forecast in forecasts)
            {
                Assert.InRange(forecast.TemperatureC, -20, 55);
                Assert.False(string.IsNullOrEmpty(forecast.Summary));
                Assert.True(forecast.Date > DateOnly.FromDateTime(DateTime.Now));
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(10)]
        public void GetTime_ReturnsCorrectNumberOfTimeStrings(int days)
        {
            // Act
            var result = _controller.GetTime(days);

            // Assert
            Assert.NotNull(result);
            var times = result.ToArray();
            Assert.Equal(days, times.Length);
            foreach (var timeString in times)
            {
                DateTime parsed;
                Assert.True(DateTime.TryParseExact(timeString, "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out parsed));
            }
        }
    }
}