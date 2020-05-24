using System.Xml.Serialization;

namespace Ocr.Wrapper.ToAlto
{
    /**
 * <p>Java class for QualityType.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * <p>
 * <pre>
 * <simpleType name="QualityType">
 *   <restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *     <enumeration value="OK"/>
 *     <enumeration value="Missing"/>
 *     <enumeration value="Missing in original"/>
 *     <enumeration value="Damaged"/>
 *     <enumeration value="Retained"/>
 *     <enumeration value="Target"/>
 *     <enumeration value="As in original"/>
 *   </restriction>
 * </simpleType>
 * </pre>
 * 
 */
    [XmlType("QualityType")]
    public enum QualityType
    {

        OK,
        Missing,
        [XmlEnum("Missing in original")]
        Missing_In_Original,
        Damaged,
        Retained,
        Target,
        [XmlEnum("As in original")]
        AS_IN_ORIGINAL,
    }
}
