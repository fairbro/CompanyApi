using System.Xml.Serialization;

[XmlRoot("Data")]
public record CompanyXmlModel
{
    [XmlElement("id")]
    public required int Id { get; set; }
    [XmlElement("name")]
    public required string Name { get; set; }
    [XmlElement("description")]
    public required string Description { get; set; }
}
