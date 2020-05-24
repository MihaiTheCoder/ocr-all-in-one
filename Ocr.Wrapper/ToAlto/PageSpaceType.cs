namespace Ocr.Wrapper.ToAlto
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /**
     * A region on a page
     * 
     * <p>Java class for PageSpaceType complex type.
     * 
     * <p>The following schema fragment specifies the expected content contained within this class.
     * 
     * <pre>
     * <complexType name="PageSpaceType">
     *   <complexContent>
     *     <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
     *       <sequence>
     *         <element name="Shape" type="{http://www.loc.gov/standards/alto/ns-v4#}ShapeType" minOccurs="0"/>
     *         <sequence maxOccurs="unbounded" minOccurs="0">
     *           <group ref="{http://www.loc.gov/standards/alto/ns-v4#}BlockGroup"/>
     *         </sequence>
     *       </sequence>
     *       <attribute name="ID" type="{http://www.loc.gov/standards/alto/ns-v4#}PageSpaceTypeID" />
     *       <attribute name="STYLEREFS" type="{http://www.w3.org/2001/XMLSchema}IDREFS" />
     *       <attribute name="PROCESSINGREFS" type="{http://www.w3.org/2001/XMLSchema}IDREFS" />
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

    [XmlType("PageSpaceType")]
    public class PageSpaceType
    {

        [XmlElement("Shape")]
        public ShapeType shape;

        //there is a bug here: for now it will only work with TextBlockType
        //[XmlArray]
        //[XmlArrayItem("TextBlock", typeof(TextBlockType))]
        //[XmlArrayItem("Illustration", typeof(IllustrationType))]
        //[XmlArrayItem("GraphicalElement", typeof(GraphicalElementType))]
        //[XmlArrayItem("ComposedBlock", typeof(ComposedBlockType))]
        [XmlElement("TextBlock")]
        public List<TextBlockType> textBlockOrIllustrationOrGraphicalElement;
        //public List<TextBlockType> textBlockOrIllustrationOrGraphicalElement;

        [XmlAttribute("ID")]
        public string id;
        [XmlAttribute("STYLEREFS")]
        public string stylerefs;
        [XmlAttribute("PROCESSINGREFS")]
        public string processingrefs;
        [XmlAttribute("HEIGHT")]
        public float height;
        [XmlAttribute("WIDTH")]
        public float width;
        [XmlAttribute("HPOS")]
        public float hpos;
        [XmlAttribute("VPOS")]
        public float vpos;

        /**
         * Gets the value of the shape property.
         * 
         * @return
         *     possible object is
         *     {@link ShapeType }
         *     
         */
        public ShapeType getShape()
        {
            return shape;
        }

        /**
         * Sets the value of the shape property.
         * 
         * @param value
         *     allowed object is
         *     {@link ShapeType }
         *     
         */
        public void setShape(ShapeType value)
        {
            this.shape = value;
        }

        /**
         * Gets the value of the textBlockOrIllustrationOrGraphicalElement property.
         * 
         * <p>
         * This accessor method returns a reference to the live list,
         * not a snapshot. Therefore any modification you make to the
         * returned list will be present inside the JAXB object.
         * This is why there is not a <CODE>set</CODE> method for the textBlockOrIllustrationOrGraphicalElement property.
         * 
         * <p>
         * For example, to add a new item, do as follows:
         * <pre>
         *    getTextBlockOrIllustrationOrGraphicalElement().add(newItem);
         * </pre>
         * 
         * 
         * <p>
         * objects of the following type(s) are allowed in the list
         * {@link TextBlockType }
         * {@link IllustrationType }
         * {@link GraphicalElementType }
         * {@link ComposedBlockType }
         * 
         * 
         */
        public List<TextBlockType> getTextBlockOrIllustrationOrGraphicalElement()
        {
            if (textBlockOrIllustrationOrGraphicalElement == null)
            {
                textBlockOrIllustrationOrGraphicalElement = new List<TextBlockType>();
            }
            return this.textBlockOrIllustrationOrGraphicalElement;
        }

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
         * Gets the value of the stylerefs property.
         * 
         * <p>
         * This accessor method returns a reference to the live list,
         * not a snapshot. Therefore any modification you make to the
         * returned list will be present inside the JAXB object.
         * This is why there is not a <CODE>set</CODE> method for the stylerefs property.
         * 
         * <p>
         * For example, to add a new item, do as follows:
         * <pre>
         *    getSTYLEREFS().add(newItem);
         * </pre>
         * 
         * 
         * <p>
         * objects of the following type(s) are allowed in the list
         * {@link object }
         * 
         * 
         */
        public string getSTYLEREFS()
        {
            if (stylerefs == null)
            {
                stylerefs = "";
            }
            return this.stylerefs;
        }

        /**
         * Gets the value of the processingrefs property.
         * 
         * <p>
         * This accessor method returns a reference to the live list,
         * not a snapshot. Therefore any modification you make to the
         * returned list will be present inside the JAXB object.
         * This is why there is not a <CODE>set</CODE> method for the processingrefs property.
         * 
         * <p>
         * For example, to add a new item, do as follows:
         * <pre>
         *    getPROCESSINGREFS().add(newItem);
         * </pre>
         * 
         * 
         * <p>
         * objects of the following type(s) are allowed in the list
         * {@link object }
         * 
         * 
         */
        public string getPROCESSINGREFS()
        {
            if (processingrefs == null)
            {
                processingrefs = "";
            }
            return this.processingrefs;
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