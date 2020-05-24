namespace Ocr.Wrapper.ToAlto
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /**
     * A block of text.
     * 
     * <p>Java class for TextBlockType complex type.
     * 
     * <p>The following schema fragment specifies the expected content contained within this class.
     * 
     * <pre>
     * <complexType name="TextBlockType">
     *   <complexContent>
     *     <extension base="{http://www.loc.gov/standards/alto/ns-v4#}BlockType">
     *       <sequence minOccurs="0">
     *         <element name="TextLine" maxOccurs="unbounded">
     *           <complexType>
     *             <complexContent>
     *               <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
     *                 <sequence>
     *                   <sequence>
     *                     <element name="Shape" type="{http://www.loc.gov/standards/alto/ns-v4#}ShapeType" minOccurs="0"/>
     *                   </sequence>
     *                   <sequence maxOccurs="unbounded">
     *                     <element name="string" type="{http://www.loc.gov/standards/alto/ns-v4#}stringType"/>
     *                     <element name="SP" type="{http://www.loc.gov/standards/alto/ns-v4#}SPType" minOccurs="0"/>
     *                   </sequence>
     *                   <element name="HYP" minOccurs="0">
     *                     <complexType>
     *                       <complexContent>
     *                         <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
     *                           <attribute name="HEIGHT" type="{http://www.w3.org/2001/XMLSchema}float" />
     *                           <attribute name="WIDTH" type="{http://www.w3.org/2001/XMLSchema}float" />
     *                           <attribute name="HPOS" type="{http://www.w3.org/2001/XMLSchema}float" />
     *                           <attribute name="VPOS" type="{http://www.w3.org/2001/XMLSchema}float" />
     *                           <attribute name="CONTENT" use="required" type="{http://www.w3.org/2001/XMLSchema}string" />
     *                         </restriction>
     *                       </complexContent>
     *                     </complexType>
     *                   </element>
     *                 </sequence>
     *                 <attribute name="ID" type="{http://www.loc.gov/standards/alto/ns-v4#}TextLineID" />
     *                 <attribute name="STYLEREFS" type="{http://www.w3.org/2001/XMLSchema}IDREFS" />
     *                 <attribute name="TAGREFS" type="{http://www.w3.org/2001/XMLSchema}IDREFS" />
     *                 <attribute name="PROCESSINGREFS" type="{http://www.w3.org/2001/XMLSchema}IDREFS" />
     *                 <attribute name="HEIGHT" type="{http://www.w3.org/2001/XMLSchema}float" />
     *                 <attribute name="WIDTH" type="{http://www.w3.org/2001/XMLSchema}float" />
     *                 <attribute name="HPOS" type="{http://www.w3.org/2001/XMLSchema}float" />
     *                 <attribute name="VPOS" type="{http://www.w3.org/2001/XMLSchema}float" />
     *                 <attribute name="BASELINE" type="{http://www.w3.org/2001/XMLSchema}float" />
     *                 <attribute name="LANG" type="{http://www.w3.org/2001/XMLSchema}language" />
     *                 <attribute name="CS" type="{http://www.w3.org/2001/XMLSchema}bool" />
     *               </restriction>
     *             </complexContent>
     *           </complexType>
     *         </element>
     *       </sequence>
     *       <attribute name="language" type="{http://www.w3.org/2001/XMLSchema}language" />
     *       <attribute name="LANG" type="{http://www.w3.org/2001/XMLSchema}language" />
     *     </extension>
     *   </complexContent>
     * </complexType>
     * </pre>
     * 
     * 
     */

    [XmlType("TextBlock")]
    public class TextBlockType : BlockType
    {

        [XmlElement("TextLine")]
        public List<TextBlockType.TextLine> textLine;

        [XmlAttribute("language")]
        public string language;
        [XmlAttribute("LANG")]
        public string lang;

        /**
         * Gets the value of the textLine property.
         * 
         * <p>
         * This accessor method returns a reference to the live list,
         * not a snapshot. Therefore any modification you make to the
         * returned list will be present inside the JAXB object.
         * This is why there is not a <CODE>set</CODE> method for the textLine property.
         * 
         * <p>
         * For example, to add a new item, do as follows:
         * <pre>
         *    getTextLine().add(newItem);
         * </pre>
         * 
         * 
         * <p>
         * objects of the following type(s) are allowed in the list
         * {@link TextBlockType.TextLine }
         * 
         * 
         */
        public List<TextBlockType.TextLine> getTextLine()
        {
            if (textLine == null)
            {
                textLine = new List<TextBlockType.TextLine>();
            }
            return this.textLine;
        }

        /**
         * Gets the value of the language property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getLanguage()
        {
            return language;
        }

        /**
         * Sets the value of the language property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setLanguage(string value)
        {
            this.language = value;
        }

        /**
         * Gets the value of the lang property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getLANG()
        {
            return lang;
        }

        /**
         * Sets the value of the lang property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setLANG(string value)
        {
            this.lang = value;
        }


        /**
         * <p>Java class for anonymous complex type.
         * 
         * <p>The following schema fragment specifies the expected content contained within this class.
         * 
         * <pre>
         * <complexType>
         *   <complexContent>
         *     <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
         *       <sequence>
         *         <sequence>
         *           <element name="Shape" type="{http://www.loc.gov/standards/alto/ns-v4#}ShapeType" minOccurs="0"/>
         *         </sequence>
         *         <sequence maxOccurs="unbounded">
         *           <element name="string" type="{http://www.loc.gov/standards/alto/ns-v4#}stringType"/>
         *           <element name="SP" type="{http://www.loc.gov/standards/alto/ns-v4#}SPType" minOccurs="0"/>
         *         </sequence>
         *         <element name="HYP" minOccurs="0">
         *           <complexType>
         *             <complexContent>
         *               <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
         *                 <attribute name="HEIGHT" type="{http://www.w3.org/2001/XMLSchema}float" />
         *                 <attribute name="WIDTH" type="{http://www.w3.org/2001/XMLSchema}float" />
         *                 <attribute name="HPOS" type="{http://www.w3.org/2001/XMLSchema}float" />
         *                 <attribute name="VPOS" type="{http://www.w3.org/2001/XMLSchema}float" />
         *                 <attribute name="CONTENT" use="required" type="{http://www.w3.org/2001/XMLSchema}string" />
         *               </restriction>
         *             </complexContent>
         *           </complexType>
         *         </element>
         *       </sequence>
         *       <attribute name="ID" type="{http://www.loc.gov/standards/alto/ns-v4#}TextLineID" />
         *       <attribute name="STYLEREFS" type="{http://www.w3.org/2001/XMLSchema}IDREFS" />
         *       <attribute name="TAGREFS" type="{http://www.w3.org/2001/XMLSchema}IDREFS" />
         *       <attribute name="PROCESSINGREFS" type="{http://www.w3.org/2001/XMLSchema}IDREFS" />
         *       <attribute name="HEIGHT" type="{http://www.w3.org/2001/XMLSchema}float" />
         *       <attribute name="WIDTH" type="{http://www.w3.org/2001/XMLSchema}float" />
         *       <attribute name="HPOS" type="{http://www.w3.org/2001/XMLSchema}float" />
         *       <attribute name="VPOS" type="{http://www.w3.org/2001/XMLSchema}float" />
         *       <attribute name="BASELINE" type="{http://www.w3.org/2001/XMLSchema}float" />
         *       <attribute name="LANG" type="{http://www.w3.org/2001/XMLSchema}language" />
         *       <attribute name="CS" type="{http://www.w3.org/2001/XMLSchema}bool" />
         *     </restriction>
         *   </complexContent>
         * </complexType>
         * </pre>
         * 
         * 
         */

        [XmlType(AnonymousType = true)]
        public class TextLine
        {

            [XmlElement("Shape")]
            public ShapeType shape;

            [XmlArray]
            [XmlArrayItem("string", typeof(stringType))]
            [XmlArrayItem("SP", typeof(SPType))]
            public List<object> stringAndSP;
            [XmlElement("HYP")]
            public TextBlockType.TextLine.HYP hyp;
            [XmlAttribute("ID")]
            public string id;
            [XmlAttribute("STYLEREFS")]
            public string stylerefs;
            [XmlAttribute("TAGREFS")]
            public string tagrefs;
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
            [XmlAttribute("BASELINE")]
            public float baseline;
            [XmlAttribute("LANG")]
            public string lang;
            [XmlAttribute("CS")]
            public bool cs;

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
             * Gets the value of the stringAndSP property.
             * 
             * <p>
             * This accessor method returns a reference to the live list,
             * not a snapshot. Therefore any modification you make to the
             * returned list will be present inside the JAXB object.
             * This is why there is not a <CODE>set</CODE> method for the stringAndSP property.
             * 
             * <p>
             * For example, to add a new item, do as follows:
             * <pre>
             *    getstringAndSP().add(newItem);
             * </pre>
             * 
             * 
             * <p>
             * objects of the following type(s) are allowed in the list
             * {@link stringType }
             * {@link SPType }
             * 
             * 
             */
            public List<object> getstringAndSP()
            {
                if (stringAndSP == null)
                {
                    stringAndSP = new List<object>();
                }
                return this.stringAndSP;
            }

            /**
             * Gets the value of the hyp property.
             * 
             * @return
             *     possible object is
             *     {@link TextBlockType.TextLine.HYP }
             *     
             */
            public TextBlockType.TextLine.HYP getHYP()
            {
                return hyp;
            }

            /**
             * Sets the value of the hyp property.
             * 
             * @param value
             *     allowed object is
             *     {@link TextBlockType.TextLine.HYP }
             *     
             */
            public void setHYP(TextBlockType.TextLine.HYP value)
            {
                this.hyp = value;
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
                return this.stylerefs;
            }

            /**
             * Gets the value of the tagrefs property.
             * 
             * <p>
             * This accessor method returns a reference to the live list,
             * not a snapshot. Therefore any modification you make to the
             * returned list will be present inside the JAXB object.
             * This is why there is not a <CODE>set</CODE> method for the tagrefs property.
             * 
             * <p>
             * For example, to add a new item, do as follows:
             * <pre>
             *    getTAGREFS().add(newItem);
             * </pre>
             * 
             * 
             * <p>
             * objects of the following type(s) are allowed in the list
             * {@link object }
             * 
             * 
             */
            public string getTAGREFS()
            {
                if (tagrefs == null)
                {
                    tagrefs = "";
                }
                return this.tagrefs;
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

            /**
             * Gets the value of the baseline property.
             * 
             * @return
             *     possible object is
             *     {@link float }
             *     
             */
            public float getBASELINE()
            {
                return baseline;
            }

            /**
             * Sets the value of the baseline property.
             * 
             * @param value
             *     allowed object is
             *     {@link float }
             *     
             */
            public void setBASELINE(float value)
            {
                this.baseline = value;
            }

            /**
             * Gets the value of the lang property.
             * 
             * @return
             *     possible object is
             *     {@link string }
             *     
             */
            public string getLANG()
            {
                return lang;
            }

            /**
             * Sets the value of the lang property.
             * 
             * @param value
             *     allowed object is
             *     {@link string }
             *     
             */
            public void setLANG(string value)
            {
                this.lang = value;
            }

            /**
             * Gets the value of the cs property.
             * 
             * @return
             *     possible object is
             *     {@link bool }
             *     
             */
            public bool isCS()
            {
                return cs;
            }

            /**
             * Sets the value of the cs property.
             * 
             * @param value
             *     allowed object is
             *     {@link bool }
             *     
             */
            public void setCS(bool value)
            {
                this.cs = value;
            }


            /**
             * <p>Java class for anonymous complex type.
             * 
             * <p>The following schema fragment specifies the expected content contained within this class.
             * 
             * <pre>
             * <complexType>
             *   <complexContent>
             *     <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
             *       <attribute name="HEIGHT" type="{http://www.w3.org/2001/XMLSchema}float" />
             *       <attribute name="WIDTH" type="{http://www.w3.org/2001/XMLSchema}float" />
             *       <attribute name="HPOS" type="{http://www.w3.org/2001/XMLSchema}float" />
             *       <attribute name="VPOS" type="{http://www.w3.org/2001/XMLSchema}float" />
             *       <attribute name="CONTENT" use="required" type="{http://www.w3.org/2001/XMLSchema}string" />
             *     </restriction>
             *   </complexContent>
             * </complexType>
             * </pre>
             * 
             * 
             */

            [XmlType(AnonymousType = true)]
            public class HYP
            {

                [XmlAttribute("HEIGHT")]
                public float height;
                [XmlAttribute("WIDTH")]
                public float width;
                [XmlAttribute("HPOS")]
                public float hpos;
                [XmlAttribute("VPOS")]
                public float vpos;
                [XmlAttribute("CONTENT")]
                public string content;

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

                /**
                 * Gets the value of the content property.
                 * 
                 * @return
                 *     possible object is
                 *     {@link string }
                 *     
                 */
                public string getCONTENT()
                {
                    return content;
                }

                /**
                 * Sets the value of the content property.
                 * 
                 * @param value
                 *     allowed object is
                 *     {@link string }
                 *     
                 */
                public void setCONTENT(string value)
                {
                    this.content = value;
                }

            }
        }
    }
}
