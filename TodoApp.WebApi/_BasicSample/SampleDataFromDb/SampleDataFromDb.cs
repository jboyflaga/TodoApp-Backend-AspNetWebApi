namespace TodoApp.WebApi.Entities;

using System.Text.Json.Serialization;

public class SampleDataFromDb
{
    public int Id { get; set; }
    public string? StringData { get; set; }
    public DateTime DateData { get; set; }
    public Guid GuidData { get; set; }
    public double DoubleData { get; set; }
    public decimal DecimalData { get; set; }
    public SampleEnum EnumData { get; set; }

    public enum SampleEnum
    {
        SampleEnum_Zero,
        SampleEnum_One,
        SampleEnum_Two,
    }
}