namespace Ocr.Wrapper.ToAlto
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /**
     * Description of the processing step.
     * 
     * <p>Java class for processingStepType complex type.
     * 
     * <p>The following schema fragment specifies the expected content contained within this class.
     * 
     * <pre>
     * <complexType name="processingStepType">
     *   <complexContent>
     *     <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
     *       <sequence>
     *         <element name="processingCategory" type="{http://www.loc.gov/standards/alto/ns-v4#}processingCategoryType" minOccurs="0"/>
     *         <element name="processingDateTime" type="{http://www.loc.gov/standards/alto/ns-v4#}dateTimeType" minOccurs="0"/>
     *         <element name="processingAgency" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *         <element name="processingStepDescription" type="{http://www.w3.org/2001/XMLSchema}string" maxOccurs="unbounded" minOccurs="0"/>
     *         <element name="processingStepSettings" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *         <element name="processingSoftware" type="{http://www.loc.gov/standards/alto/ns-v4#}processingSoftwareType" minOccurs="0"/>
     *       </sequence>
     *     </restriction>
     *   </complexContent>
     * </complexType>
     * </pre>
     * 
     * 
     */


    //@XmlSeeAlso({
    //    org.mycore.xml.alto.v4.DescriptionType.Processing.class
    //})
    [XmlType("processingStepType")]
    public class ProcessingStepType
    {

        [XmlArray]
        public List<string> processingCategory;
        public string processingDateTime;
        public string processingAgency;
        public List<string> processingStepDescription;
        public string processingStepSettings;
        public ProcessingSoftwareType processingSoftware;

        /**
         * Gets the value of the processingCategory property.
         * 
         * <p>
         * This accessor method returns a reference to the live list,
         * not a snapshot. Therefore any modification you make to the
         * returned list will be present inside the JAXB object.
         * This is why there is not a <CODE>set</CODE> method for the processingCategory property.
         * 
         * <p>
         * For example, to add a new item, do as follows:
         * <pre>
         *    getProcessingCategory().add(newItem);
         * </pre>
         * 
         * 
         * <p>
         * Objects of the following type(s) are allowed in the list
         * {@link string }
         * 
         * 
         */
        public List<string> getProcessingCategory()
        {
            if (processingCategory == null)
            {
                processingCategory = new List<string>();
            }
            return this.processingCategory;
        }

        /**
         * Gets the value of the processingDateTime property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getProcessingDateTime()
        {
            return processingDateTime;
        }

        /**
         * Sets the value of the processingDateTime property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setProcessingDateTime(string value)
        {
            this.processingDateTime = value;
        }

        /**
         * Gets the value of the processingAgency property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getProcessingAgency()
        {
            return processingAgency;
        }

        /**
         * Sets the value of the processingAgency property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setProcessingAgency(string value)
        {
            this.processingAgency = value;
        }

        /**
         * Gets the value of the processingStepDescription property.
         * 
         * <p>
         * This accessor method returns a reference to the live list,
         * not a snapshot. Therefore any modification you make to the
         * returned list will be present inside the JAXB object.
         * This is why there is not a <CODE>set</CODE> method for the processingStepDescription property.
         * 
         * <p>
         * For example, to add a new item, do as follows:
         * <pre>
         *    getProcessingStepDescription().add(newItem);
         * </pre>
         * 
         * 
         * <p>
         * Objects of the following type(s) are allowed in the list
         * {@link string }
         * 
         * 
         */
        public List<string> getProcessingStepDescription()
        {
            if (processingStepDescription == null)
            {
                processingStepDescription = new List<string>();
            }
            return this.processingStepDescription;
        }

        /**
         * Gets the value of the processingStepSettings property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getProcessingStepSettings()
        {
            return processingStepSettings;
        }

        /**
         * Sets the value of the processingStepSettings property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setProcessingStepSettings(string value)
        {
            this.processingStepSettings = value;
        }

        /**
         * Gets the value of the processingSoftware property.
         * 
         * @return
         *     possible object is
         *     {@link ProcessingSoftwareType }
         *     
         */
        public ProcessingSoftwareType getProcessingSoftware()
        {
            return processingSoftware;
        }

        /**
         * Sets the value of the processingSoftware property.
         * 
         * @param value
         *     allowed object is
         *     {@link ProcessingSoftwareType }
         *     
         */
        public void setProcessingSoftware(ProcessingSoftwareType value)
        {
            this.processingSoftware = value;
        }
    }
}