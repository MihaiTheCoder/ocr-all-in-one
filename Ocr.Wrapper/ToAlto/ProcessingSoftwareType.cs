using System.Xml.Serialization;

namespace Ocr.Wrapper.ToAlto
{
    /**
     * Information about a software application. Where applicable, the preferred method for determining this information is by selecting Help -- About.
     * 
     * <p>Java class for processingSoftwareType complex type.
     * 
     * <p>The following schema fragment specifies the expected content contained within this class.
     * 
     * <pre>
     * <complexType name="processingSoftwareType">
     *   <complexContent>
     *     <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
     *       <sequence>
     *         <element name="softwareCreator" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *         <element name="softwareName" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *         <element name="softwareVersion" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *         <element name="applicationDescription" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *       </sequence>
     *     </restriction>
     *   </complexContent>
     * </complexType>
     * </pre>
     * 
     * 
     */

    [XmlType("processingSoftwareType")]
    public class ProcessingSoftwareType
    {

        public string softwareCreator;
        public string softwareName;
        public string softwareVersion;
        public string applicationDescription;

        /**
         * Gets the value of the softwareCreator property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getSoftwareCreator()
        {
            return softwareCreator;
        }

        /**
         * Sets the value of the softwareCreator property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setSoftwareCreator(string value)
        {
            this.softwareCreator = value;
        }

        /**
         * Gets the value of the softwareName property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getSoftwareName()
        {
            return softwareName;
        }

        /**
         * Sets the value of the softwareName property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setSoftwareName(string value)
        {
            this.softwareName = value;
        }

        /**
         * Gets the value of the softwareVersion property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getSoftwareVersion()
        {
            return softwareVersion;
        }

        /**
         * Sets the value of the softwareVersion property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setSoftwareVersion(string value)
        {
            this.softwareVersion = value;
        }

        /**
         * Gets the value of the applicationDescription property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getApplicationDescription()
        {
            return applicationDescription;
        }

        /**
         * Sets the value of the applicationDescription property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setApplicationDescription(string value)
        {
            this.applicationDescription = value;
        }

    }
}