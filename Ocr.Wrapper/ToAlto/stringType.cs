using Ocr.Wrapper.ToAlto;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ocr.Wrapper.ToAlto
{
    using System.Collections.Generic;
    /**
     * A sequence of chars. strings are separated by white spaces or hyphenation chars.
     * 
     * <p>Java class for stringType complex type.
     * 
     * <p>The following schema fragment specifies the expected content contained within this class.
     * 
     * <pre>
     * <complexType name="stringType">
     *   <complexContent>
     *     <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
     *       <sequence minOccurs="0">
     *         <element name="Shape" type="{http://www.loc.gov/standards/alto/ns-v4#}ShapeType" minOccurs="0"/>
     *         <element name="ALTERNATIVE" type="{http://www.loc.gov/standards/alto/ns-v4#}ALTERNATIVEType" maxOccurs="unbounded" minOccurs="0"/>
     *         <element name="Glyph" type="{http://www.loc.gov/standards/alto/ns-v4#}GlyphType" maxOccurs="unbounded" minOccurs="0"/>
     *       </sequence>
     *       <attribute name="ID" type="{http://www.loc.gov/standards/alto/ns-v4#}stringTypeID" />
     *       <attribute name="STYLEREFS" type="{http://www.w3.org/2001/XMLSchema}IDREFS" />
     *       <attribute name="TAGREFS" type="{http://www.w3.org/2001/XMLSchema}IDREFS" />
     *       <attribute name="PROCESSINGREFS" type="{http://www.w3.org/2001/XMLSchema}IDREFS" />
     *       <attribute name="HEIGHT" type="{http://www.w3.org/2001/XMLSchema}float" />
     *       <attribute name="WIDTH" type="{http://www.w3.org/2001/XMLSchema}float" />
     *       <attribute name="HPOS" type="{http://www.w3.org/2001/XMLSchema}float" />
     *       <attribute name="VPOS" type="{http://www.w3.org/2001/XMLSchema}float" />
     *       <attribute name="CONTENT" use="required" type="{http://www.loc.gov/standards/alto/ns-v4#}CONTENTType" />
     *       <attribute name="STYLE" type="{http://www.loc.gov/standards/alto/ns-v4#}fontStylesType" />
     *       <attribute name="SUBS_TYPE" type="{http://www.loc.gov/standards/alto/ns-v4#}SUBS_TYPEType" />
     *       <attribute name="SUBS_CONTENT" type="{http://www.w3.org/2001/XMLSchema}string" />
     *       <attribute name="WC" type="{http://www.loc.gov/standards/alto/ns-v4#}WCType" />
     *       <attribute name="CC" type="{http://www.w3.org/2001/XMLSchema}string" />
     *       <attribute name="CS" type="{http://www.w3.org/2001/XMLSchema}bool" />
     *       <attribute name="LANG" type="{http://www.w3.org/2001/XMLSchema}language" />
     *     </restriction>
     *   </complexContent>
     * </complexType>
     * </pre>
     * 
     * 
     */

    [XmlType("stringType")]
public class stringType
{
    [XmlElement("Shape")]
    public ShapeType shape;
    [XmlElement("ALTERNATIVE")]
    public List<ALTERNATIVEType> alternative;
    [XmlElement("Glyph")]
    public List<GlyphType> glyph;
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
    [XmlAttribute("CONTENT")]
    public string content;
    [XmlAttribute("STYLE")]
    public string style;
    [XmlAttribute("SUBS_TYPE")]
    public SUBSTYPEType substype;
    [XmlAttribute("SUBS_CONTENT")]
    public string subscontent;
    [XmlAttribute("WC")]
    public float wc;
    [XmlAttribute("CC")]
    public string cc;
    [XmlAttribute("CS")]
    public bool cs;
    [XmlAttribute("LANG")]
    public string lang;

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
     * Gets the value of the alternative property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the alternative property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getALTERNATIVE().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * objects of the following type(s) are allowed in the list
     * {@link ALTERNATIVEType }
     * 
     * 
     */
    public List<ALTERNATIVEType> getALTERNATIVE()
    {
        if (alternative == null)
        {
            alternative = new List<ALTERNATIVEType>();
        }
        return this.alternative;
    }

    /**
     * Gets the value of the glyph property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the glyph property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getGlyph().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * objects of the following type(s) are allowed in the list
     * {@link GlyphType }
     * 
     * 
     */
    public List<GlyphType> getGlyph()
    {
        if (glyph == null)
        {
            glyph = new List<GlyphType>();
        }
        return this.glyph;
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

    /**
     * Gets the value of the style property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the style property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getSTYLE().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * objects of the following type(s) are allowed in the list
     * {@link string }
     * 
     * 
     */
    public string getSTYLE()
    {
        if (style == null)
        {
                style = "";
        }
        return this.style;
    }

    /**
     * Gets the value of the substype property.
     * 
     * @return
     *     possible object is
     *     {@link SUBSTYPEType }
     *     
     */
    public SUBSTYPEType getSUBSTYPE()
    {
        return substype;
    }

    /**
     * Sets the value of the substype property.
     * 
     * @param value
     *     allowed object is
     *     {@link SUBSTYPEType }
     *     
     */
    public void setSUBSTYPE(SUBSTYPEType value)
    {
        this.substype = value;
    }

    /**
     * Gets the value of the subscontent property.
     * 
     * @return
     *     possible object is
     *     {@link string }
     *     
     */
    public string getSUBSCONTENT()
    {
        return subscontent;
    }

    /**
     * Sets the value of the subscontent property.
     * 
     * @param value
     *     allowed object is
     *     {@link string }
     *     
     */
    public void setSUBSCONTENT(string value)
    {
        this.subscontent = value;
    }

    /**
     * Gets the value of the wc property.
     * 
     * @return
     *     possible object is
     *     {@link float }
     *     
     */
    public float getWC()
    {
        return wc;
    }

    /**
     * Sets the value of the wc property.
     * 
     * @param value
     *     allowed object is
     *     {@link float }
     *     
     */
    public void setWC(float value)
    {
        this.wc = value;
    }

    /**
     * Gets the value of the cc property.
     * 
     * @return
     *     possible object is
     *     {@link string }
     *     
     */
    public string getCC()
    {
        return cc;
    }

    /**
     * Sets the value of the cc property.
     * 
     * @param value
     *     allowed object is
     *     {@link string }
     *     
     */
    public void setCC(string value)
    {
        this.cc = value;
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

}

}