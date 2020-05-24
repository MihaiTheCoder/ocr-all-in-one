using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ocr.Wrapper.ToAlto
{
    /**
     * <p>Java class for altoType complex type.
     * 
     * <p>The following schema fragment specifies the expected content contained within this class.
     * 
     * <pre>
     * <complexType name="altoType">
     *   <complexContent>
     *     <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
     *       <sequence>
     *         <element name="Description" type="{http://www.loc.gov/standards/alto/ns-v4#}DescriptionType" minOccurs="0"/>
     *         <element name="Styles" type="{http://www.loc.gov/standards/alto/ns-v4#}StylesType" minOccurs="0"/>
     *         <element name="Tags" type="{http://www.loc.gov/standards/alto/ns-v4#}TagsType" minOccurs="0"/>
     *         <element name="Layout" type="{http://www.loc.gov/standards/alto/ns-v4#}LayoutType"/>
     *       </sequence>
     *       <attribute name="SCHEMAVERSION" type="{http://www.w3.org/2001/XMLSchema}string" />
     *     </restriction>
     *   </complexContent>
     * </complexType>
     * </pre>
     * 
     * 
     */

    [XmlRoot(ElementName = "alto", Namespace = "http://www.loc.gov/standards/alto/ns-v3#")]
    public class AltoType
    {
        [XmlElement("Description")]
        public DescriptionType description;
        //@XmlElement(name = "Styles")
        //public StylesType styles;
        //@XmlElement(name = "Tags")
        //public TagsType tags;
        [XmlElement("Layout")]
        public LayoutType layout;
        [XmlAttribute("SCHEMAVERSION")]
        public string schemaversion;

        /**
         * Gets the value of the description property.
         * 
         * @return
         *     possible object is
         *     {@link DescriptionType }
         *     
         */
        public DescriptionType getDescription()
        {
            return description;
        }

        /**
         * Sets the value of the description property.
         * 
         * @param value
         *     allowed object is
         *     {@link DescriptionType }
         *     
         */
        public void setDescription(DescriptionType value)
        {
            this.description = value;
        }

        ///**
        // * Gets the value of the styles property.
        // * 
        // * @return
        // *     possible object is
        // *     {@link StylesType }
        // *     
        // */
        //public StylesType getStyles()
        //{
        //    return styles;
        //}

        ///**
        // * Sets the value of the styles property.
        // * 
        // * @param value
        // *     allowed object is
        // *     {@link StylesType }
        // *     
        // */
        //public void setStyles(StylesType value)
        //{
        //    this.styles = value;
        //}

        ///**
        // * Gets the value of the tags property.
        // * 
        // * @return
        // *     possible object is
        // *     {@link TagsType }
        // *     
        // */
        //public TagsType getTags()
        //{
        //    return tags;
        //}

        ///**
        // * Sets the value of the tags property.
        // * 
        // * @param value
        // *     allowed object is
        // *     {@link TagsType }
        // *     
        // */
        //public void setTags(TagsType value)
        //{
        //    this.tags = value;
        //}

        ///**
        // * Gets the value of the layout property.
        // * 
        // * @return
        // *     possible object is
        // *     {@link LayoutType }
        // *     
        // */
        public LayoutType getLayout()
        {
            return layout;
        }

        ///**
        // * Sets the value of the layout property.
        // * 
        // * @param value
        // *     allowed object is
        // *     {@link LayoutType }
        // *     
        // */
        public void setLayout(LayoutType value)
        {
            this.layout = value;
        }

        ///**
        // * Gets the value of the schemaversion property.
        // * 
        // * @return
        // *     possible object is
        // *     {@link string }
        // *     
        // */
        //public string getSCHEMAVERSION()
        //{
        //    return schemaversion;
        //}

        ///**
        // * Sets the value of the schemaversion property.
        // * 
        // * @param value
        // *     allowed object is
        // *     {@link string }
        // *     
        // */
        //public void setSCHEMAVERSION(string value)
        //{
        //    this.schemaversion = value;
        //}

    }
}
