using System.Xml.Serialization;

namespace Ocr.Wrapper.ToAlto
{
    /**
     * <p>Java class for PositionType.
     * 
     * <p>The following schema fragment specifies the expected content contained within this class.
     * <p>
     * <pre>
     * <simpleType name="PositionType">
     *   <restriction base="{http://www.w3.org/2001/XMLSchema}string">
     *     <enumeration value="Left"/>
     *     <enumeration value="Right"/>
     *     <enumeration value="Foldout"/>
     *     <enumeration value="Single"/>
     *     <enumeration value="Cover"/>
     *   </restriction>
     * </simpleType>
     * </pre>
     * 
     */
    [XmlType(TypeName = "PositionType")]
    public enum PositionType
    {
        Left,
        Right,
        Foldout,
        Single,
        Cover,
    }
}