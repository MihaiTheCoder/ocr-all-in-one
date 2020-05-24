using System.Xml.Serialization;

namespace Ocr.Wrapper.ToAlto
{
    /**
 * <p>Java class for SUBS_TYPEType.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * <p>
 * <pre>
 * <simpleType name="SUBS_TYPEType">
 *   <restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *     <enumeration value="HypPart1"/>
 *     <enumeration value="HypPart2"/>
 *     <enumeration value="Abbreviation"/>
 *   </restriction>
 * </simpleType>
 * </pre>
 * 
 */
    [XmlType("SUBS_TYPEType")]
    public enum SUBSTYPEType
    {
        HypPart1,
        HypPart2,
        Abbreviation,
    }
}