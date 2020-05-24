using System.Xml.Serialization;

namespace Ocr.Wrapper.ToAlto
{
    /**
     * <p>Java class for MeasurementUnitType.
     * 
     * <p>The following schema fragment specifies the expected content contained within this class.
     * <p>
     * <pre>
     * <simpleType name="MeasurementUnitType">
     *   <restriction base="{http://www.w3.org/2001/XMLSchema}string">
     *     <enumeration value="pixel"/>
     *     <enumeration value="mm10"/>
     *     <enumeration value="inch1200"/>
     *   </restriction>
     * </simpleType>
     * </pre>
     * 
     */
    [XmlType("MeasurementUnitType")]
    public enum MeasurementUnitType
    {
        pixel,
        mm10,
        inch1200
    }
}
