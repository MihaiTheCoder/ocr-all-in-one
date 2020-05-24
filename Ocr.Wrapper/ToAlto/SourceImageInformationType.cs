namespace Ocr.Wrapper.ToAlto
{


    using System.Collections.Generic;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /**
     * Information to identify the image file from which the OCR text was created.
     * 
     * <p>Java class for sourceImageInformationType complex type.
     * 
     * <p>The following schema fragment specifies the expected content contained within this class.
     * 
     * <pre>
     * <complexType name="sourceImageInformationType">
     *   <complexContent>
     *     <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
     *       <sequence>
     *         <element name="fileName" type="{http://www.loc.gov/standards/alto/ns-v4#}fileNameType" minOccurs="0"/>
     *         <element name="fileIdentifier" type="{http://www.loc.gov/standards/alto/ns-v4#}fileIdentifierType" maxOccurs="unbounded" minOccurs="0"/>
     *         <element name="documentIdentifier" type="{http://www.loc.gov/standards/alto/ns-v4#}documentIdentifierType" maxOccurs="unbounded" minOccurs="0"/>
     *       </sequence>
     *     </restriction>
     *   </complexContent>
     * </complexType>
     * </pre>
     * 
     * 
     */

    [XmlType("sourceImageInformationType")]
    public class SourceImageInformationType
    {
        public string fileName;
        //public List<FileIdentifierType> fileIdentifier;
        //public List<DocumentIdentifierType> documentIdentifier;

        /**
         * Gets the value of the fileName property.
         * 
         * @return
         *     possible object is
         *     {@link string }
         *     
         */
        public string getFileName()
        {
            return fileName;
        }

        /**
         * Sets the value of the fileName property.
         * 
         * @param value
         *     allowed object is
         *     {@link string }
         *     
         */
        public void setFileName(string value)
        {
            this.fileName = value;
        }

        /**
         * Gets the value of the fileIdentifier property.
         * 
         * <p>
         * This accessor method returns a reference to the live list,
         * not a snapshot. Therefore any modification you make to the
         * returned list will be present inside the JAXB object.
         * This is why there is not a <CODE>set</CODE> method for the fileIdentifier property.
         * 
         * <p>
         * For example, to add a new item, do as follows:
         * <pre>
         *    getFileIdentifier().add(newItem);
         * </pre>
         * 
         * 
         * <p>
         * Objects of the following type(s) are allowed in the list
         * {@link FileIdentifierType }
         * 
         * 
         */
        //public List<FileIdentifierType> getFileIdentifier()
        //{
        //    if (fileIdentifier == null)
        //    {
        //        fileIdentifier = new List<FileIdentifierType>();
        //    }
        //    return this.fileIdentifier;
        //}

        /**
         * Gets the value of the documentIdentifier property.
         * 
         * <p>
         * This accessor method returns a reference to the live list,
         * not a snapshot. Therefore any modification you make to the
         * returned list will be present inside the JAXB object.
         * This is why there is not a <CODE>set</CODE> method for the documentIdentifier property.
         * 
         * <p>
         * For example, to add a new item, do as follows:
         * <pre>
         *    getDocumentIdentifier().add(newItem);
         * </pre>
         * 
         * 
         * <p>
         * Objects of the following type(s) are allowed in the list
         * {@link DocumentIdentifierType }
         * 
         * 
         */
        //public List<DocumentIdentifierType> getDocumentIdentifier()
        //{
        //    if (documentIdentifier == null)
        //    {
        //        documentIdentifier = new List<DocumentIdentifierType>();
        //    }
        //    return this.documentIdentifier;
        //}
    }
}
