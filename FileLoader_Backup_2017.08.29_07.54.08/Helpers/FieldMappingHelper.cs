using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileLoader.Helpers
{
    
    [XmlType("FieldSetsMapping")]
    public class FieldSetMapping
    {
        public FieldSetMapping()
        {
            FieldSets = new List<FieldSet>();
        }

        [XmlElement("FieldSet")]
        public List<FieldSet> FieldSets { get; set; }

    }
    

    public class FieldSet
    {
        public FieldSet()
        {
            Mappings = new List<Mapping>();
        }
        [XmlElement("RangeName")]
        public string RangeName { get; set; }


        [XmlElement("ExcelColumnName")]
        public string ExcelColumnName { get; set; }

        [XmlElement("SharePointListField")]
        public string SharePointListField { get; set; }

        [XmlArray("Mappings")]
        [XmlArrayItem("Map")]
        public List<Mapping> Mappings { get; set; }
    }

    [XmlType("Map")]
    public class Mapping
    {
        [XmlElement("RangeName")]
        public string RangeName { get; set; }

        [XmlElement("ExcelColumnName")]
        public string ExcelColumnName { get; set; }

        [XmlElement("SharePointListField")]
        public string SharePointListField { get; set; }
      
    }
}
