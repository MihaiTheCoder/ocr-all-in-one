using System.Xml.Serialization;

namespace Ocr.Wrapper.ToAlto
{
    /**
 * A white space.
 * 
 * <p>Java class for SPType complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * <complexType name="SPType">
 *   <complexContent>
 *     <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       <attribute name="ID" type="{http://www.loc.gov/standards/alto/ns-v4#}SPTypeID" />
 *       <attribute name="HEIGHT" type="{http://www.w3.org/2001/XMLSchema}float" />
 *       <attribute name="WIDTH" type="{http://www.w3.org/2001/XMLSchema}float" />
 *       <attribute name="HPOS" type="{http://www.w3.org/2001/XMLSchema}float" />
 *       <attribute name="VPOS" type="{http://www.w3.org/2001/XMLSchema}float" />
 *     </restriction>
 *   </complexContent>
 * </complexType>
 * </pre>
 * 
 * 
 */

    [XmlType("SPType")]
    public class SPType
    {

        [XmlAttribute("ID")]
        public string id;
        [XmlAttribute("HEIGHT")]
        public float height;
        [XmlAttribute("WIDTH")]
        public float width;
        [XmlAttribute("HPOS")]
        public float hpos;
        [XmlAttribute("VPOS")]
        public float vpos;

        /**
         * Gets the value of the id property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getID()
        {
            return id;
        }

        /**
         * Sets the value of the id property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setID(string value)
        {
            this.id = value;
        }

        /**
         * Gets the value of the height property.
         * 
         * @return
         *     possible object is
         *     {@link float }
         *     
         */
        public float getHEIGHT()
        {
            return height;
        }

        /**
         * Sets the value of the height property.
         * 
         * @param value
         *     allowed object is
         *     {@link float }
         *     
         */
        public void setHEIGHT(float value)
        {
            this.height = value;
        }

        /**
         * Gets the value of the width property.
         * 
         * @return
         *     possible object is
         *     {@link float }
         *     
         */
        public float getWIDTH()
        {
            return width;
        }

        /**
         * Sets the value of the width property.
         * 
         * @param value
         *     allowed object is
         *     {@link float }
         *     
         */
        public void setWIDTH(float value)
        {
            this.width = value;
        }

        /**
         * Gets the value of the hpos property.
         * 
         * @return
         *     possible object is
         *     {@link float }
         *     
         */
        public float getHPOS()
        {
            return hpos;
        }

        /**
         * Sets the value of the hpos property.
         * 
         * @param value
         *     allowed object is
         *     {@link float }
         *     
         */
        public void setHPOS(float value)
        {
            this.hpos = value;
        }

        /**
         * Gets the value of the vpos property.
         * 
         * @return
         *     possible object is
         *     {@link float }
         *     
         */
        public float getVPOS()
        {
            return vpos;
        }

        /**
         * Sets the value of the vpos property.
         * 
         * @param value
         *     allowed object is
         *     {@link float }
         *     
         */
        public void setVPOS(float value)
        {
            this.vpos = value;
        }
    }
}