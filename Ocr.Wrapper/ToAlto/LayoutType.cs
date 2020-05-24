namespace Ocr.Wrapper.ToAlto
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /**
     * <p>Java class for LayoutType complex type.
     * 
     * <p>The following schema fragment specifies the expected content contained within this class.
     * 
     * <pre>
     * <complexType name="LayoutType">
     *   <complexContent>
     *     <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
     *       <sequence>
     *         <element name="Page" type="{http://www.loc.gov/standards/alto/ns-v4#}PageType" maxOccurs="unbounded"/>
     *       </sequence>
     *       <attribute name="STYLEREFS" type="{http://www.w3.org/2001/XMLSchema}IDREFS" />
     *     </restriction>
     *   </complexContent>
     * </complexType>
     * </pre>
     * 
     * 
     */

    [XmlType("LayoutType")]
    public class LayoutType
    {
        [XmlElement("Page")]
        public List<PageType> page;

        [XmlAttribute("STYLEREFS")]
        public string stylerefs;

        /**
         * Gets the value of the page property.
         * 
         * <p>
         * This accessor method returns a reference to the live list,
         * not a snapshot. Therefore any modification you make to the
         * returned list will be present inside the JAXB object.
         * This is why there is not a <CODE>set</CODE> method for the page property.
         * 
         * <p>
         * For example, to add a new item, do as follows:
         * <pre>
         *    getPage().add(newItem);
         * </pre>
         * 
         * 
         * <p>
         * Objects of the following type(s) are allowed in the list
         * {@link PageType }
         * 
         * 
         */
        public List<PageType> getPage()
        {
            if (page == null)
            {
                page = new List<PageType>();
            }
            return this.page;
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
         * Objects of the following type(s) are allowed in the list
         * {@link Object }
         * 
         * 
         */
        public string getSTYLEREFS()
        {
            if (stylerefs == null)
                stylerefs = "";
            return this.stylerefs;
        }
    }
}