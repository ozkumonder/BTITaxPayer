using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BTITaxPayerService.Model
{
    [XmlRoot(ElementName = "Alias")]
    public class Alias
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "CreationTime")]
        public DateTime CreationTime { get; set; }
        [XmlElement(ElementName = "DeletionTime")]
        public DateTime DeletionTime { get; set; }
    }
    [XmlRoot(ElementName = "Document")]
    public class Document
    {
        [XmlElement(ElementName = "Alias")]
        public List<Alias> Alias { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }
    [XmlRoot(ElementName = "Documents")]
    public class Documents
    {
        [XmlElement(ElementName = "Document")]
        public List<Document> Document { get; set; }
    }
    [XmlRoot(ElementName = "User")]
    public class User
    {
        [XmlElement(ElementName = "Identifier")]
        public string Identifier { get; set; }
        [XmlElement(ElementName = "Title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "Type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "FirstCreationTime")]
        public DateTime FirstCreationTime { get; set; }
        [XmlElement(ElementName = "AccountType")]
        public string AccountType { get; set; }
        [XmlElement(ElementName = "Documents")]
        public Documents Documents { get; set; }
    }
    [XmlRoot(ElementName = "UserList")]
    public class UserList
    {
        [XmlElement(ElementName = "User")]
        public List<User> User { get; set; }

        public string ListType { get; set; }
    }
}