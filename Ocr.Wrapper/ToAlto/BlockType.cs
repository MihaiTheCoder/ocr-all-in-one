namespace Ocr.Wrapper.ToAlto
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /**
* Base type for any kind of block on the page.
* 
* <p>Java class for BlockType complex type.
* 
* <p>The following schema fragment specifies the expected content contained within this class.
* 
* <pre>
* <complexType name="BlockType">
*   <complexContent>
*     <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
*       <sequence minOccurs="0">
*         <element name="Shape" type="{http://www.loc.gov/standards/alto/ns-v4#}ShapeType"/>
*       </sequence>
*       <attGroup ref="{http://www.w3.org/1999/xlink}simpleLink"/>
*       <attribute name="ID" use="required" type="{http://www.loc.gov/standards/alto/ns-v4#}BlockTypeID" />
*       <attribute name="STYLEREFS" type="{http://www.w3.org/2001/XMLSchema}IDREFS" />
*       <attribute name="TAGREFS" type="{http://www.w3.org/2001/XMLSchema}IDREFS" />
*       <attribute name="PROCESSINGREFS" type="{http://www.w3.org/2001/XMLSchema}IDREFS" />
*       <attribute name="HEIGHT" type="{http://www.w3.org/2001/XMLSchema}float" />
*       <attribute name="WIDTH" type="{http://www.w3.org/2001/XMLSchema}float" />
*       <attribute name="HPOS" type="{http://www.w3.org/2001/XMLSchema}float" />
*       <attribute name="VPOS" type="{http://www.w3.org/2001/XMLSchema}float" />
*       <attribute name="ROTATION" type="{http://www.w3.org/2001/XMLSchema}float" />
*       <attribute name="IDNEXT" type="{http://www.w3.org/2001/XMLSchema}IDREF" />
*       <attribute name="CS" type="{http://www.w3.org/2001/XMLSchema}boolean" />
*     </restriction>
*   </complexContent>
* </complexType>
* </pre>
* 
* 
*/

    [XmlType("BlockType")]
    public class BlockType
    {

        [XmlElement("Shape")]
        public ShapeType shape;
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
        [XmlAttribute("ROTATION")]
        public float rotation;
        [XmlAttribute("IDNEXT")]
        public string idnext;
        [XmlAttribute("CS")]
        public bool cs;
        [XmlAttribute("type", Namespace = "http://www.w3.org/1999/xlink")]
        public string type;
        [XmlAttribute("href", Namespace = "http://www.w3.org/1999/xlink")]
        public string href;
        [XmlAttribute("role", Namespace = "http://www.w3.org/1999/xlink")]
        public string role;
        [XmlAttribute("arcrole", Namespace = "http://www.w3.org/1999/xlink")]
        public string arcrole;
        [XmlAttribute("title", Namespace = "http://www.w3.org/1999/xlink")]
        public string title;
        [XmlAttribute("show", Namespace = "http://www.w3.org/1999/xlink")]
        public string show;
        [XmlAttribute("actuate", Namespace = "http://www.w3.org/1999/xlink")]
        public string actuate;

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
         * Gets the value of the rotation property.
         * 
         * @return
         *     possible object is
         *     {@link float }
         *     
         */
        public float getROTATION()
        {
            return rotation;
        }

        /**
         * Sets the value of the rotation property.
         * 
         * @param value
         *     allowed object is
         *     {@link float }
         *     
         */
        public void setROTATION(float value)
        {
            this.rotation = value;
        }

        /**
         * Gets the value of the idnext property.
         * 
         * @return
         *     possible object is
         *     {@link object }
         *     
         */
        public string getIDNEXT()
        {
            return idnext;
        }

        /**
         * Sets the value of the idnext property.
         * 
         * @param value
         *     allowed object is
         *     {@link object }
         *     
         */
        public void setIDNEXT(string value)
        {
            this.idnext = value;
        }

        /**
         * Gets the value of the cs property.
         * 
         * @return
         *     possible object is
         *     {@link Boolean }
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
         *     {@link Boolean }
         *     
         */
        public void setCS(bool value)
        {
            this.cs = value;
        }

        /**
         * Gets the value of the type property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getType()
        {
            if (type == null)
            {
                return "simple";
            }
            else
            {
                return type;
            }
        }

        /**
         * Sets the value of the type property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setType(string value)
        {
            this.type = value;
        }

        /**
         * Gets the value of the href property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getHref()
        {
            return href;
        }

        /**
         * Sets the value of the href property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setHref(string value)
        {
            this.href = value;
        }

        /**
         * Gets the value of the role property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getRole()
        {
            return role;
        }

        /**
         * Sets the value of the role property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setRole(string value)
        {
            this.role = value;
        }

        /**
         * Gets the value of the arcrole property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getArcrole()
        {
            return arcrole;
        }

        /**
         * Sets the value of the arcrole property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setArcrole(string value)
        {
            this.arcrole = value;
        }

        /**
         * Gets the value of the title property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getTitle()
        {
            return title;
        }

        /**
         * Sets the value of the title property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setTitle(string value)
        {
            this.title = value;
        }

        /**
         * Gets the value of the show property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getShow()
        {
            return show;
        }

        /**
         * Sets the value of the show property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setShow(string value)
        {
            this.show = value;
        }

        /**
         * Gets the value of the actuate property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getActuate()
        {
            return actuate;
        }

        /**
         * Sets the value of the actuate property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setActuate(string value)
        {
            this.actuate = value;
        }
    }
}
