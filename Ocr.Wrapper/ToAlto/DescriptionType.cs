using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ocr.Wrapper.ToAlto
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /**
* <p>Java class for DescriptionType complex type.
* 
* <p>The following schema fragment specifies the expected content contained within this class.
* 
* <pre>
* <complexType name="DescriptionType">
*   <complexContent>
*     <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
*       <sequence>
*         <element name="MeasurementUnit" type="{http://www.loc.gov/standards/alto/ns-v4#}MeasurementUnitType"/>
*         <element name="sourceImageInformation" type="{http://www.loc.gov/standards/alto/ns-v4#}sourceImageInformationType" minOccurs="0"/>
*         <element name="OCRProcessing" maxOccurs="unbounded" minOccurs="0">
*           <complexType>
*             <complexContent>
*               <extension base="{http://www.loc.gov/standards/alto/ns-v4#}ocrProcessingType">
*                 <attribute name="ID" use="required" type="{http://www.w3.org/2001/XMLSchema}ID" />
*               </extension>
*             </complexContent>
*           </complexType>
*         </element>
*         <element name="Processing" maxOccurs="unbounded" minOccurs="0">
*           <complexType>
*             <complexContent>
*               <extension base="{http://www.loc.gov/standards/alto/ns-v4#}processingStepType">
*                 <attribute name="ID" use="required" type="{http://www.w3.org/2001/XMLSchema}ID" />
*               </extension>
*             </complexContent>
*           </complexType>
*         </element>
*       </sequence>
*     </restriction>
*   </complexContent>
* </complexType>
* </pre>
* 
* 
*/


    [XmlType(TypeName = "DescriptionType")]
    public class DescriptionType
    {

        [XmlElement("MeasurementUnit")]
        //@XmlSchemaType(name = "string")
        public MeasurementUnitType measurementUnit;

        public SourceImageInformationType sourceImageInformation;
        [XmlElement("OCRProcessing")]
        public List<DescriptionType.OCRProcessing> ocrProcessing;

        [XmlElement("Processing")]
        public List<DescriptionType.Processing> processing;

        /**
         * Gets the value of the measurementUnit property.
         * 
         * @return
         *     possible object is
         *     {@link MeasurementUnitType }
         *     
         */
        public MeasurementUnitType getMeasurementUnit()
        {
            return measurementUnit;
        }

        /**
         * Sets the value of the measurementUnit property.
         * 
         * @param value
         *     allowed object is
         *     {@link MeasurementUnitType }
         *     
         */
        public void setMeasurementUnit(MeasurementUnitType value)
        {
            this.measurementUnit = value;
        }

        /**
         * Gets the value of the sourceImageInformation property.
         * 
         * @return
         *     possible object is
         *     {@link SourceImageInformationType }
         *     
         */
        public SourceImageInformationType getSourceImageInformation()
        {
            return sourceImageInformation;
        }

        /**
         * Sets the value of the sourceImageInformation property.
         * 
         * @param value
         *     allowed object is
         *     {@link SourceImageInformationType }
         *     
         */
        public void setSourceImageInformation(SourceImageInformationType value)
        {
            this.sourceImageInformation = value;
        }

        /**
         * Gets the value of the ocrProcessing property.
         * 
         * <p>
         * This accessor method returns a reference to the live list,
         * not a snapshot. Therefore any modification you make to the
         * returned list will be present inside the JAXB object.
         * This is why there is not a <CODE>set</CODE> method for the ocrProcessing property.
         * 
         * <p>
         * For example, to add a new item, do as follows:
         * <pre>
         *    getOCRProcessing().add(newItem);
         * </pre>
         * 
         * 
         * <p>
         * Objects of the following type(s) are allowed in the list
         * {@link DescriptionType.OCRProcessing }
         * 
         * 
         */
        public List<DescriptionType.OCRProcessing> getOCRProcessing()
        {
            if (ocrProcessing == null)
            {
                ocrProcessing = new List<DescriptionType.OCRProcessing>();
            }
            return this.ocrProcessing;
        }

        /**
         * Gets the value of the processing property.
         * 
         * <p>
         * This accessor method returns a reference to the live list,
         * not a snapshot. Therefore any modification you make to the
         * returned list will be present inside the JAXB object.
         * This is why there is not a <CODE>set</CODE> method for the processing property.
         * 
         * <p>
         * For example, to add a new item, do as follows:
         * <pre>
         *    getProcessing().add(newItem);
         * </pre>
         * 
         * 
         * <p>
         * Objects of the following type(s) are allowed in the list
         * {@link DescriptionType.Processing }
         * 
         * 
         */
        public List<DescriptionType.Processing> getProcessing()
        {
            if (processing == null)
            {
                processing = new List<DescriptionType.Processing>();
            }
            return this.processing;
        }


        /**
         * <p>Java class for anonymous complex type.
         * 
         * <p>The following schema fragment specifies the expected content contained within this class.
         * 
         * <pre>
         * <complexType>
         *   <complexContent>
         *     <extension base="{http://www.loc.gov/standards/alto/ns-v4#}ocrProcessingType">
         *       <attribute name="ID" use="required" type="{http://www.w3.org/2001/XMLSchema}ID" />
         *     </extension>
         *   </complexContent>
         * </complexType>
         * </pre>
         * 
         * 
         */

        [XmlType(AnonymousType = true)]
        public class OCRProcessing : OcrProcessingType
        {

        //    @XmlAttribute(name = "ID", required = true)
        //    @XmlJavaTypeAdapter(CollapsedStringAdapter.class)
        //@XmlID
        //@XmlSchemaType(name = "ID")
            [XmlAttribute("ID")]
            public String id;

            /**
             * Gets the value of the id property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getID()
            {
                return id;
            }

            /**
             * Sets the value of the id property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setID(String value)
            {
                this.id = value;
            }
        }


        /**
         * <p>Java class for anonymous complex type.
         * 
         * <p>The following schema fragment specifies the expected content contained within this class.
         * 
         * <pre>
         * <complexType>
         *   <complexContent>
         *     <extension base="{http://www.loc.gov/standards/alto/ns-v4#}processingStepType">
         *       <attribute name="ID" use="required" type="{http://www.w3.org/2001/XMLSchema}ID" />
         *     </extension>
         *   </complexContent>
         * </complexType>
         * </pre>
         * 
         * 
         */

        [XmlType(AnonymousType = true)]
        public class Processing : ProcessingStepType
        {

            //@XmlAttribute(name = "ID", required = true)
            //@XmlJavaTypeAdapter(CollapsedStringAdapter.class)
            //@XmlID
            //@XmlSchemaType(name = "ID")
            [XmlAttribute("ID")]
            public String id;

            /**
             * Gets the value of the id property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getID()
            {
                return id;
            }

            /**
             * Sets the value of the id property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setID(String value)
            {
                this.id = value;
            }
        }
    }
}
