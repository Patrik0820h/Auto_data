using System;
using System.IO;
using Xunit;

namespace Auto_data.Tests
{
    public class CsvServiceTests
    {
        [Fact]
        public void Read_ShouldReadCsvCorrectly()
        {
            string filePath = Path.GetTempFileName();

            string csvContent =
                "Id,Márka,Típus,Üzemanyag,Teljesítmény_LE,Vételár_EUR,Gyártási_év,Átlagos_CO2_g_km\n" +
                "1,BMW,320d,Dízel,190,25000,2020,120\n";

            File.WriteAllText(filePath, csvContent);

            try
            {
                var result = CsvService.Read(filePath);

                Assert.Single(result);

                var (auto, marka, tipus) = result[0];

                Assert.Equal(1, auto.Id);
                Assert.Equal("BMW", marka.Márka);
                Assert.Equal("320d", tipus.Típus);
                Assert.Equal("Dízel", auto.Üzemanyag);
                Assert.Equal(190, auto.Teljesítmény_LE);
                Assert.Equal(25000, auto.Vételár_EUR);
                Assert.Equal(2020, auto.Gyártási_év);
                Assert.Equal(120, auto.Átlagos_CO2_g_km);
            }
            finally
            {
                File.Delete(filePath);
            }
        }
    }
}
