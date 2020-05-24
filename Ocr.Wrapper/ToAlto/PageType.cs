namespace Ocr.Wrapper.ToAlto
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /**
     * One page of a book or journal.
     * 
     * <p>Java class for PageType complex type.
     * 
     * <p>The following schema fragment specifies the expected content contained within this class.
     * 
     * <pre>
     * <complexType name="PageType">
     *   <complexContent>
     *     <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
     *       <sequence>
     *         <element name="TopMargin" type="{http://www.loc.gov/standards/alto/ns-v4#}PageSpaceType" minOccurs="0"/>
     *         <element name="LeftMargin" type="{http://www.loc.gov/standards/alto/ns-v4#}PageSpaceType" minOccurs="0"/>
     *         <element name="RightMargin" type="{http://www.loc.gov/standards/alto/ns-v4#}PageSpaceType" minOccurs="0"/>
     *         <element name="BottomMargin" type="{http://www.loc.gov/standards/alto/ns-v4#}PageSpaceType" minOccurs="0"/>
     *         <element name="PrintSpace" type="{http://www.loc.gov/standards/alto/ns-v4#}PageSpaceType" minOccurs="0"/>
     *       </sequence>
     *       <attribute name="ID" use="required" type="{http://www.loc.gov/standards/alto/ns-v4#}PageID" />
     *       <attribute name="PAGECLASS" type="{http://www.w3.org/2001/XMLSchema}string" />
     *       <attribute name="STYLEREFS" type="{http://www.w3.org/2001/XMLSchema}IDREFS" />
     *       <attribute name="PROCESSINGREFS" type="{http://www.w3.org/2001/XMLSchema}IDREFS" />
     *       <attribute name="HEIGHT" type="{http://www.w3.org/2001/XMLSchema}float" />
     *       <attribute name="WIDTH" type="{http://www.w3.org/2001/XMLSchema}float" />
     *       <attribute name="PHYSICAL_IMG_NR" use="required" type="{http://www.w3.org/2001/XMLSchema}float" />
     *       <attribute name="PRINTED_IMG_NR" type="{http://www.w3.org/2001/XMLSchema}string" />
     *       <attribute name="QUALITY" type="{http://www.loc.gov/standards/alto/ns-v4#}QualityType" />
     *       <attribute name="QUALITY_DETAIL" type="{http://www.loc.gov/standards/alto/ns-v4#}QualityDetailType" />
     *       <attribute name="POSITION" type="{http://www.loc.gov/standards/alto/ns-v4#}PositionType" />
     *       <attribute name="PROCESSING" type="{http://www.w3.org/2001/XMLSchema}IDREF" />
     *       <attribute name="ACCURACY" type="{http://www.w3.org/2001/XMLSchema}float" />
     *       <attribute name="PC" type="{http://www.loc.gov/standards/alto/ns-v4#}PCType" />
     *     </restriction>
     *   </complexContent>
     * </complexType>
     * </pre>
     * 
     * 
     */

    [XmlType("PageType")]
    public class PageType
    {

        [XmlElement("TopMargin")]
        public PageSpaceType topMargin;
        [XmlElement("LeftMargin")]
        public PageSpaceType leftMargin;
        [XmlElement("RightMargin")]
        public PageSpaceType rightMargin;
        [XmlElement("BottomMargin")]
        public PageSpaceType bottomMargin;
        [XmlElement("PrintSpace")]
        public PageSpaceType printSpace;

        [XmlAttribute("ID")]
        public string id;
        [XmlAttribute("PAGECLASS")]
        public string pageclass;
        [XmlAttribute("STYLEREFS")]
        public string stylerefs;
        [XmlAttribute("PROCESSINGREFS")]
        public string processingrefs;
        [XmlAttribute("HEIGHT")]
        public float height;
        [XmlAttribute("WIDTH")]
        public float width;
        [XmlAttribute("PHYSICAL_IMG_NR")]
        public float physicalimgnr;
        [XmlAttribute("PRINTED_IMG_NR")]
        public string printedimgnr;
        [XmlAttribute("QUALITY")]
        public QualityType quality;
        [XmlAttribute("QUALITY_DETAIL")]
        public string qualitydetail;
        [XmlAttribute("POSITION")]
        public PositionType position;
        [XmlAttribute("PROCESSING")]
        public string processing;
        [XmlAttribute("ACCURACY")]
        public float accuracy;
        [XmlAttribute("PC")]
        public float pc;

        /**
         * Gets the value of the topMargin property.
         * 
         * @return
         *     possible object is
         *     {@link PageSpaceType }
         *     
         */
        public PageSpaceType getTopMargin()
        {
            return topMargin;
        }

        /**
         * Sets the value of the topMargin property.
         * 
         * @param value
         *     allowed object is
         *     {@link PageSpaceType }
         *     
         */
        public void setTopMargin(PageSpaceType value)
        {
            this.topMargin = value;
        }

        /**
         * Gets the value of the leftMargin property.
         * 
         * @return
         *     possible object is
         *     {@link PageSpaceType }
         *     
         */
        public PageSpaceType getLeftMargin()
        {
            return leftMargin;
        }

        /**
         * Sets the value of the leftMargin property.
         * 
         * @param value
         *     allowed object is
         *     {@link PageSpaceType }
         *     
         */
        public void setLeftMargin(PageSpaceType value)
        {
            this.leftMargin = value;
        }

        /**
         * Gets the value of the rightMargin property.
         * 
         * @return
         *     possible object is
         *     {@link PageSpaceType }
         *     
         */
        public PageSpaceType getRightMargin()
        {
            return rightMargin;
        }

        /**
         * Sets the value of the rightMargin property.
         * 
         * @param value
         *     allowed object is
         *     {@link PageSpaceType }
         *     
         */
        public void setRightMargin(PageSpaceType value)
        {
            this.rightMargin = value;
        }

        /**
         * Gets the value of the bottomMargin property.
         * 
         * @return
         *     possible object is
         *     {@link PageSpaceType }
         *     
         */
        public PageSpaceType getBottomMargin()
        {
            return bottomMargin;
        }

        /**
         * Sets the value of the bottomMargin property.
         * 
         * @param value
         *     allowed object is
         *     {@link PageSpaceType }
         *     
         */
        public void setBottomMargin(PageSpaceType value)
        {
            this.bottomMargin = value;
        }

        /**
         * Gets the value of the printSpace property.
         * 
         * @return
         *     possible object is
         *     {@link PageSpaceType }
         *     
         */
        public PageSpaceType getPrintSpace()
        {
            return printSpace;
        }

        /**
         * Sets the value of the printSpace property.
         * 
         * @param value
         *     allowed object is
         *     {@link PageSpaceType }
         *     
         */
        public void setPrintSpace(PageSpaceType value)
        {
            this.printSpace = value;
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
         * Gets the value of the pageclass property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getPAGECLASS()
        {
            return pageclass;
        }

        /**
         * Sets the value of the pageclass property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setPAGECLASS(string value)
        {
            this.pageclass = value;
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
         * Gets the value of the physicalimgnr property.
         * 
         */
        public float getPHYSICALIMGNR()
        {
            return physicalimgnr;
        }

        /**
         * Sets the value of the physicalimgnr property.
         * 
         */
        public void setPHYSICALIMGNR(float value)
        {
            this.physicalimgnr = value;
        }

        /**
         * Gets the value of the printedimgnr property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getPRINTEDIMGNR()
        {
            return printedimgnr;
        }

        /**
         * Sets the value of the printedimgnr property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setPRINTEDIMGNR(string value)
        {
            this.printedimgnr = value;
        }

        /**
         * Gets the value of the quality property.
         * 
         * @return
         *     possible object is
         *     {@link QualityType }
         *     
         */
        public QualityType getQUALITY()
        {
            return quality;
        }

        /**
         * Sets the value of the quality property.
         * 
         * @param value
         *     allowed object is
         *     {@link QualityType }
         *     
         */
        public void setQUALITY(QualityType value)
        {
            this.quality = value;
        }

        /**
         * Gets the value of the qualitydetail property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getQUALITYDETAIL()
        {
            return qualitydetail;
        }

        /**
         * Sets the value of the qualitydetail property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setQUALITYDETAIL(string value)
        {
            this.qualitydetail = value;
        }

        /**
         * Gets the value of the position property.
         * 
         * @return
         *     possible object is
         *     {@link PositionType }
         *     
         */
        public PositionType getPOSITION()
        {
            return position;
        }

        /**
         * Sets the value of the position property.
         * 
         * @param value
         *     allowed object is
         *     {@link PositionType }
         *     
         */
        public void setPOSITION(PositionType value)
        {
            this.position = value;
        }

        /**
         * Gets the value of the processing property.
         * 
         * @return
         *     possible object is
         *     {@link object }
         *     
         */
        public string getPROCESSING()
        {
            return processing;
        }

        /**
         * Sets the value of the processing property.
         * 
         * @param value
         *     allowed object is
         *     {@link object }
         *     
         */
        public void setPROCESSING(string value)
        {
            this.processing = value;
        }

        /**
         * Gets the value of the accuracy property.
         * 
         * @return
         *     possible object is
         *     {@link float }
         *     
         */
        public float getACCURACY()
        {
            return accuracy;
        }

        /**
         * Sets the value of the accuracy property.
         * 
         * @param value
         *     allowed object is
         *     {@link float }
         *     
         */
        public void setACCURACY(float value)
        {
            this.accuracy = value;
        }

        /**
         * Gets the value of the pc property.
         * 
         * @return
         *     possible object is
         *     {@link float }
         *     
         */
        public float getPC()
        {
            return pc;
        }

        /**
         * Sets the value of the pc property.
         * 
         * @param value
         *     allowed object is
         *     {@link float }
         *     
         */
        public void setPC(float value)
        {
            this.pc = value;
        }
    }
}