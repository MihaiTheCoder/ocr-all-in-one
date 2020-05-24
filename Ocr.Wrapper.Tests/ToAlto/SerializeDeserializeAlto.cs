using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocr.Wrapper.ToAlto;

namespace Ocr.Wrapper.Tests.ToAlto
{

    [TestClass]
    public class SerializeDeserializeAlto
    {
        [TestMethod]
        public void DeserializeDescription()
        {
            var tesseractVer = "tesseract v4.0.0.20190314";
            var d =
$@"<?xml version=""1.0"" encoding=""UTF-8""?>
<alto xmlns=""http://www.loc.gov/standards/alto/ns-v3#"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.loc.gov/standards/alto/ns-v3# http://www.loc.gov/alto/v3/alto-3-0.xsd"">
    <Description>
	    <MeasurementUnit>pixel</MeasurementUnit>
	    <sourceImageInformation>
		    <fileName>			</fileName>
	    </sourceImageInformation>
	    <OCRProcessing ID=""OCR_0"">
		    <ocrProcessingStep>
			    <processingSoftware>
				    <softwareName>{tesseractVer}</softwareName>
			    </processingSoftware>
		    </ocrProcessingStep>
	    </OCRProcessing>
    </Description>
</alto>";
            var res = Deserialize<AltoType>(d);
            Assert.AreEqual(MeasurementUnitType.pixel, res.description.measurementUnit);
            Assert.AreEqual(
                tesseractVer, 
                res.description
                   .ocrProcessing[0]
                   .ocrProcessingStep
                   .processingSoftware
                   .softwareName);
        }

        [TestMethod]
        public void SerializeDescription()
        {
            var test_guid = Guid.NewGuid().ToString("N");
            var alto = new AltoType();
            var desc = new DescriptionType();
            desc.measurementUnit = MeasurementUnitType.pixel;
            desc.sourceImageInformation = new SourceImageInformationType();
            desc.sourceImageInformation.fileName = "";
            var p = desc.getOCRProcessing();
            p.Add(new DescriptionType.OCRProcessing()
            {
                id = test_guid,
                ocrProcessingStep = new ProcessingStepType()
                {
                    processingSoftware = new ProcessingSoftwareType()
                    {
                        softwareName = "tesseract v4.0.0.20190314"
                    }
                }
            });
            alto.setDescription(desc);

            var xml = Serialize(alto);
            var _ = Deserialize<AltoType>(xml);
            Assert.IsTrue(xml.Contains(test_guid));
        }

        [TestMethod]
        public void SerializeLayout() {
            var alto = new AltoType();
            alto.layout = new LayoutType()
            {
                page = new System.Collections.Generic.List<PageType>()
                {
                    new PageType()
                    {
                         printSpace = new PageSpaceType()
                         {
                              textBlockOrIllustrationOrGraphicalElement = new System.Collections.Generic.List<TextBlockType>()
                              {
                                new TextBlockType()
                                {
                                     height=1,
                                     vpos=2,
                                     textLine = new System.Collections.Generic.List<TextBlockType.TextLine>()
                                     {
                                         new TextBlockType.TextLine()
                                         {
                                             stringAndSP = new System.Collections.Generic.List<object>()
                                             {
                                                 new stringType()
                                                 {
                                                     content = "some content",
                                                     wc = 0.9f
                                                 }
                                             }
                                         }
                                     }
                                }
                              }
                         }
                    }
                }
            };
            var xml = Serialize(alto);
        }

        [TestMethod]
        public void DeserialiazeLayout() { 
        var xml = 
$@"<?xml version=""1.0"" encoding=""UTF-8""?>
<alto xmlns=""http://www.loc.gov/standards/alto/ns-v3#"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.loc.gov/standards/alto/ns-v3# http://www.loc.gov/alto/v3/alto-3-0.xsd"">
    <Layout>
	    <Page WIDTH=""4032"" HEIGHT=""3024"" PHYSICAL_IMG_NR=""0"" ID=""page_0"">
		    <PrintSpace HPOS=""0"" VPOS=""0"" WIDTH=""4032"" HEIGHT=""3024"">
			    <TextBlock ID=""block_0"" HPOS=""3401"" VPOS=""661"" WIDTH=""190"" HEIGHT=""282"">
				    <TextLine ID=""line_0"" HPOS=""3494"" VPOS=""695"" WIDTH=""97"" HEIGHT=""243"">
					    <String ID=""string_0"" HPOS=""3494"" VPOS=""695"" WIDTH=""97"" HEIGHT=""162"" WC=""0.6"" CONTENT=""(ereorg""/><SP WIDTH=""-95"" VPOS=""695"" HPOS=""3591""/>
					    <String ID=""string_1"" HPOS=""3496"" VPOS=""872"" WIDTH=""64"" HEIGHT=""66"" WC=""0.0"" CONTENT=""[00)""/>
				    </TextLine>
			    </TextBlock>
		    </PrintSpace>
	    </Page>
    </Layout>
</alto>";
            var res = Deserialize<AltoType>(xml);
            var tb = res.layout.page[0].printSpace.textBlockOrIllustrationOrGraphicalElement[0] as TextBlockType;
            Assert.AreEqual(tb.id, "block_0");
            Assert.AreEqual(tb.hpos, 3401);
            Assert.AreEqual(tb.vpos, 661);
        }
        [TestMethod]
        public void DeserializeTesseractAltoXml()
        {
            var xml =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<alto xmlns=""http://www.loc.gov/standards/alto/ns-v3#"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.loc.gov/standards/alto/ns-v3# http://www.loc.gov/alto/v3/alto-3-0.xsd"">
	<Description>
		<MeasurementUnit>pixel</MeasurementUnit>
		<sourceImageInformation>
			<fileName>			</fileName>
		</sourceImageInformation>
		<OCRProcessing ID=""OCR_0"">
			<ocrProcessingStep>
				<processingSoftware>
					<softwareName>tesseract v4.0.0.20190314</softwareName>
				</processingSoftware>
			</ocrProcessingStep>
		</OCRProcessing>
	</Description>
	<Layout>
		<Page WIDTH=""4032"" HEIGHT=""3024"" PHYSICAL_IMG_NR=""0"" ID=""page_0"">
			<PrintSpace HPOS=""0"" VPOS=""0"" WIDTH=""4032"" HEIGHT=""3024"">
				<TextBlock ID=""block_0"" HPOS=""3401"" VPOS=""661"" WIDTH=""190"" HEIGHT=""282"">
					<TextLine ID=""line_0"" HPOS=""3494"" VPOS=""695"" WIDTH=""97"" HEIGHT=""243"">
						<String ID=""string_0"" HPOS=""3494"" VPOS=""695"" WIDTH=""97"" HEIGHT=""162"" WC=""0.6"" CONTENT=""(ereorg""/><SP WIDTH=""-95"" VPOS=""695"" HPOS=""3591""/>
						<String ID=""string_1"" HPOS=""3496"" VPOS=""872"" WIDTH=""64"" HEIGHT=""66"" WC=""0.0"" CONTENT=""[00)""/>
					</TextLine>
					<TextLine ID=""line_1"" HPOS=""3401"" VPOS=""661"" WIDTH=""96"" HEIGHT=""282"">
						<String ID=""string_2"" HPOS=""3401"" VPOS=""661"" WIDTH=""96"" HEIGHT=""108"" WC=""0.39"" CONTENT=""ad""/><SP WIDTH=""-47"" VPOS=""661"" HPOS=""3497""/>
						<String ID=""string_3"" HPOS=""3450"" VPOS=""775"" WIDTH=""49"" HEIGHT=""55"" WC=""0.69"" CONTENT=""@P""/><SP WIDTH=""-51"" VPOS=""775"" HPOS=""3499""/>
						<String ID=""string_4"" HPOS=""3448"" VPOS=""844"" WIDTH=""39"" HEIGHT=""99"" WC=""0.45"" CONTENT=""[BIOL""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_1"" HPOS=""3443"" VPOS=""1081"" WIDTH=""98"" HEIGHT=""216"">
					<TextLine ID=""line_2"" HPOS=""3499"" VPOS=""1091"" WIDTH=""42"" HEIGHT=""206"">
						<String ID=""string_5"" HPOS=""3499"" VPOS=""1091"" WIDTH=""42"" HEIGHT=""134"" WC=""0.0"" CONTENT=""B.juUd""/><SP WIDTH=""-42"" VPOS=""1091"" HPOS=""3541""/>
						<String ID=""string_6"" HPOS=""3499"" VPOS=""1250"" WIDTH=""34"" HEIGHT=""47"" WC=""0.84"" CONTENT=""ep""/>
					</TextLine>
					<TextLine ID=""line_3"" HPOS=""3443"" VPOS=""1081"" WIDTH=""38"" HEIGHT=""214"">
						<String ID=""string_7"" HPOS=""3443"" VPOS=""1081"" WIDTH=""38"" HEIGHT=""214"" WC=""0.0"" CONTENT=""BunRUWES""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_2"" HPOS=""3274"" VPOS=""1116"" WIDTH=""121"" HEIGHT=""178"">
					<TextLine ID=""line_4"" HPOS=""3330"" VPOS=""1170"" WIDTH=""65"" HEIGHT=""123"">
						<String ID=""string_8"" HPOS=""3330"" VPOS=""1170"" WIDTH=""65"" HEIGHT=""123"" WC=""0.0"" CONTENT=""ezjooe""/>
					</TextLine>
					<TextLine ID=""line_5"" HPOS=""3274"" VPOS=""1116"" WIDTH=""38"" HEIGHT=""178"">
						<String ID=""string_9"" HPOS=""3284"" VPOS=""1116"" WIDTH=""28"" HEIGHT=""97"" WC=""0.53"" CONTENT=""‘e189""/><SP WIDTH=""-38"" VPOS=""1116"" HPOS=""3312""/>
						<String ID=""string_10"" HPOS=""3274"" VPOS=""1238"" WIDTH=""35"" HEIGHT=""56"" WC=""0.59"" CONTENT=""UIP""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_3"" HPOS=""1162"" VPOS=""1092"" WIDTH=""193"" HEIGHT=""229"">
					<TextLine ID=""line_6"" HPOS=""1323"" VPOS=""1193"" WIDTH=""32"" HEIGHT=""23"">
						<String ID=""string_11"" HPOS=""1323"" VPOS=""1193"" WIDTH=""32"" HEIGHT=""23"" WC=""0.70"" CONTENT=""v""/>
					</TextLine>
					<TextLine ID=""line_7"" HPOS=""1267"" VPOS=""1159"" WIDTH=""34"" HEIGHT=""87"">
						<String ID=""string_12"" HPOS=""1267"" VPOS=""1159"" WIDTH=""34"" HEIGHT=""87"" WC=""0.55"" CONTENT=""—|8|-""/>
					</TextLine>
					<TextLine ID=""line_8"" HPOS=""1209"" VPOS=""1093"" WIDTH=""44"" HEIGHT=""228"">
						<String ID=""string_13"" HPOS=""1209"" VPOS=""1093"" WIDTH=""42"" HEIGHT=""132"" WC=""0.2"" CONTENT=""(‘W&#39;A&#39;L""/><SP WIDTH=""-39"" VPOS=""1093"" HPOS=""1251""/>
						<String ID=""string_14"" HPOS=""1212"" VPOS=""1235"" WIDTH=""41"" HEIGHT=""86"" WC=""0.8"" CONTENT=""B42))""/>
					</TextLine>
					<TextLine ID=""line_9"" HPOS=""1162"" VPOS=""1092"" WIDTH=""41"" HEIGHT=""228"">
						<String ID=""string_15"" HPOS=""1162"" VPOS=""1092"" WIDTH=""34"" HEIGHT=""107"" WC=""0.0"" CONTENT=""JeyUN""/><SP WIDTH=""-33"" VPOS=""1092"" HPOS=""1196""/>
						<String ID=""string_16"" HPOS=""1163"" VPOS=""1211"" WIDTH=""40"" HEIGHT=""109"" WC=""0.30"" CONTENT=""[Need""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_4"" HPOS=""1246"" VPOS=""407"" WIDTH=""39"" HEIGHT=""222"">
					<TextLine ID=""line_10"" HPOS=""1246"" VPOS=""407"" WIDTH=""39"" HEIGHT=""222"">
						<String ID=""string_17"" HPOS=""1246"" VPOS=""407"" WIDTH=""35"" HEIGHT=""99"" WC=""0.30"" CONTENT=""-198\-""/><SP WIDTH=""-32"" VPOS=""407"" HPOS=""1281""/>
						<String ID=""string_18"" HPOS=""1249"" VPOS=""548"" WIDTH=""36"" HEIGHT=""81"" WC=""0.56"" CONTENT=""WAL""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_5"" HPOS=""1186"" VPOS=""396"" WIDTH=""38"" HEIGHT=""126"">
					<TextLine ID=""line_11"" HPOS=""1186"" VPOS=""396"" WIDTH=""38"" HEIGHT=""126"">
						<String ID=""string_19"" HPOS=""1186"" VPOS=""396"" WIDTH=""38"" HEIGHT=""126"" WC=""0.40"" CONTENT=""Wier""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_6"" HPOS=""1141"" VPOS=""365"" WIDTH=""68"" HEIGHT=""549"">
					<TextLine ID=""line_12"" HPOS=""1141"" VPOS=""365"" WIDTH=""68"" HEIGHT=""549"">
						<String ID=""string_20"" HPOS=""1141"" VPOS=""365"" WIDTH=""36"" HEIGHT=""183"" WC=""0.0"" CONTENT=""BOJBO|eA""/><SP WIDTH=""-21"" VPOS=""365"" HPOS=""1177""/>
						<String ID=""string_21"" HPOS=""1156"" VPOS=""750"" WIDTH=""53"" HEIGHT=""164"" WC=""0.0"" CONTENT=""Bolieoje/A""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_7"" HPOS=""1123"" VPOS=""934"" WIDTH=""9"" HEIGHT=""16"">
					<TextLine ID=""line_13"" HPOS=""1123"" VPOS=""934"" WIDTH=""9"" HEIGHT=""16"">
						<String ID=""string_22"" HPOS=""1123"" VPOS=""934"" WIDTH=""9"" HEIGHT=""16"" WC=""0.26"" CONTENT=""cy""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_8"" HPOS=""1106"" VPOS=""843"" WIDTH=""16"" HEIGHT=""178"">
					<TextLine ID=""line_14"" HPOS=""1106"" VPOS=""843"" WIDTH=""16"" HEIGHT=""178"">
						<String ID=""string_23"" HPOS=""1106"" VPOS=""843"" WIDTH=""16"" HEIGHT=""178"" WC=""0.27"" CONTENT=""Goa""/><SP WIDTH=""-24"" VPOS=""843"" HPOS=""1122""/>
						<String ID=""string_24"" HPOS=""1098"" VPOS=""901"" WIDTH=""28"" HEIGHT=""20"" WC=""0.23"" CONTENT=""v""/><SP WIDTH=""-20"" VPOS=""901"" HPOS=""1126""/>
						<String ID=""string_25"" HPOS=""1106"" VPOS=""923"" WIDTH=""16"" HEIGHT=""61"" WC=""0.67"" CONTENT=""LIV""/><SP WIDTH=""-24"" VPOS=""923"" HPOS=""1122""/>
						<String ID=""string_26"" HPOS=""1098"" VPOS=""993"" WIDTH=""28"" HEIGHT=""28"" WC=""0.17"" CONTENT=""PY""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_9"" HPOS=""810"" VPOS=""465"" WIDTH=""270"" HEIGHT=""561"">
					<TextLine ID=""line_15"" HPOS=""1040"" VPOS=""654"" WIDTH=""40"" HEIGHT=""372"">
						<String ID=""string_27"" HPOS=""1040"" VPOS=""654"" WIDTH=""33"" HEIGHT=""143"" WC=""0.50"" CONTENT=""eA""/><SP WIDTH=""-31"" VPOS=""654"" HPOS=""1073""/>
						<String ID=""string_28"" HPOS=""1042"" VPOS=""815"" WIDTH=""33"" HEIGHT=""48"" WC=""0.91"" CONTENT=""ap""/><SP WIDTH=""-27"" VPOS=""815"" HPOS=""1075""/>
						<String ID=""string_29"" HPOS=""1048"" VPOS=""880"" WIDTH=""32"" HEIGHT=""146"" WC=""0.43"" CONTENT=""ESaupy""/>
					</TextLine>
					<TextLine ID=""line_16"" HPOS=""955"" VPOS=""465"" WIDTH=""65"" HEIGHT=""428"">
						<String ID=""string_30"" HPOS=""955"" VPOS=""465"" WIDTH=""55"" HEIGHT=""166"" WC=""0.12"" CONTENT=""Ssninvi""/><SP WIDTH=""-44"" VPOS=""465"" HPOS=""1010""/>
						<String ID=""string_31"" HPOS=""966"" VPOS=""658"" WIDTH=""54"" HEIGHT=""235"" WC=""0.0"" CONTENT=""NYva1S0uVv""/>
					</TextLine>
					<TextLine ID=""line_17"" HPOS=""870"" VPOS=""483"" WIDTH=""68"" HEIGHT=""438"">
						<String ID=""string_32"" HPOS=""870"" VPOS=""483"" WIDTH=""51"" HEIGHT=""71"" WC=""0.87"" CONTENT=""6Z""/><SP WIDTH=""-33"" VPOS=""483"" HPOS=""921""/>
						<String ID=""string_33"" HPOS=""888"" VPOS=""644"" WIDTH=""44"" HEIGHT=""108"" WC=""0.0"" CONTENT=""eve""/><SP WIDTH=""-41"" VPOS=""644"" HPOS=""932""/>
						<String ID=""string_34"" HPOS=""891"" VPOS=""766"" WIDTH=""47"" HEIGHT=""154"" WC=""0.0"" CONTENT=""NAH""/>
					</TextLine>
					<TextLine ID=""line_18"" HPOS=""810"" VPOS=""560"" WIDTH=""44"" HEIGHT=""191"">
						<String ID=""string_35"" HPOS=""810"" VPOS=""560"" WIDTH=""35"" HEIGHT=""36"" WC=""0.62"" CONTENT=""yO""/><SP WIDTH=""-31"" VPOS=""560"" HPOS=""845""/>
						<String ID=""string_36"" HPOS=""814"" VPOS=""618"" WIDTH=""40"" HEIGHT=""133"" WC=""0.43"" CONTENT=""YLNOWS""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_10"" HPOS=""763"" VPOS=""578"" WIDTH=""17"" HEIGHT=""193"">
					<TextLine ID=""line_19"" HPOS=""763"" VPOS=""578"" WIDTH=""17"" HEIGHT=""193"">
						<String ID=""string_37"" HPOS=""763"" VPOS=""578"" WIDTH=""17"" HEIGHT=""193"" WC=""0.0"" CONTENT=""crepe""/><SP WIDTH=""-21"" VPOS=""578"" HPOS=""780""/>
						<String ID=""string_38"" HPOS=""759"" VPOS=""648"" WIDTH=""29"" HEIGHT=""40"" WC=""0.0"" CONTENT=""ser""/><SP WIDTH=""-25"" VPOS=""648"" HPOS=""788""/>
						<String ID=""string_39"" HPOS=""763"" VPOS=""687"" WIDTH=""17"" HEIGHT=""84"" WC=""0.0"" CONTENT=""eeneees""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_11"" HPOS=""864"" VPOS=""302"" WIDTH=""130"" HEIGHT=""161"">
					<TextLine ID=""line_20"" HPOS=""953"" VPOS=""302"" WIDTH=""41"" HEIGHT=""154"">
						<String ID=""string_40"" HPOS=""953"" VPOS=""302"" WIDTH=""41"" HEIGHT=""154"" WC=""0.22"" CONTENT=""MOTs""/>
					</TextLine>
					<TextLine ID=""line_21"" HPOS=""864"" VPOS=""393"" WIDTH=""51"" HEIGHT=""70"">
						<String ID=""string_41"" HPOS=""864"" VPOS=""393"" WIDTH=""51"" HEIGHT=""70"" WC=""0.63"" CONTENT=""of""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_12"" HPOS=""774"" VPOS=""778"" WIDTH=""10"" HEIGHT=""92"">
					<TextLine ID=""line_22"" HPOS=""774"" VPOS=""778"" WIDTH=""10"" HEIGHT=""92"">
						<String ID=""string_42"" HPOS=""774"" VPOS=""778"" WIDTH=""10"" HEIGHT=""92"" WC=""0.0"" CONTENT=""wepeeene""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_13"" HPOS=""653"" VPOS=""764"" WIDTH=""142"" HEIGHT=""320"">
					<TextLine ID=""line_23"" HPOS=""762"" VPOS=""889"" WIDTH=""33"" HEIGHT=""192"">
						<String ID=""string_43"" HPOS=""780"" VPOS=""889"" WIDTH=""8"" HEIGHT=""55"" WC=""0.29"" CONTENT=""“""/><SP WIDTH=""-26"" VPOS=""889"" HPOS=""788""/>
						<String ID=""string_44"" HPOS=""762"" VPOS=""961"" WIDTH=""33"" HEIGHT=""120"" WC=""0.9"" CONTENT=""pound""/>
					</TextLine>
					<TextLine ID=""line_24"" HPOS=""703"" VPOS=""925"" WIDTH=""40"" HEIGHT=""157"">
						<String ID=""string_45"" HPOS=""729"" VPOS=""925"" WIDTH=""6"" HEIGHT=""17"" WC=""0.0"" CONTENT=""&quot;""/><SP WIDTH=""-32"" VPOS=""925"" HPOS=""735""/>
						<String ID=""string_46"" HPOS=""703"" VPOS=""960"" WIDTH=""40"" HEIGHT=""122"" WC=""0.29"" CONTENT=""jnjuog""/>
					</TextLine>
					<TextLine ID=""line_25"" HPOS=""653"" VPOS=""764"" WIDTH=""81"" HEIGHT=""320"">
						<String ID=""string_47"" HPOS=""653"" VPOS=""764"" WIDTH=""81"" HEIGHT=""145"" WC=""0.21"" CONTENT=""“anew""/><SP WIDTH=""-81"" VPOS=""764"" HPOS=""734""/>
						<String ID=""string_48"" HPOS=""653"" VPOS=""945"" WIDTH=""40"" HEIGHT=""139"" WC=""0.54"" CONTENT=""injepnr""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_14"" HPOS=""576"" VPOS=""482"" WIDTH=""63"" HEIGHT=""601"">
					<TextLine ID=""line_26"" HPOS=""576"" VPOS=""482"" WIDTH=""63"" HEIGHT=""601"">
						<String ID=""string_49"" HPOS=""576"" VPOS=""482"" WIDTH=""38"" HEIGHT=""120"" WC=""0.19"" CONTENT=""seme""/><SP WIDTH=""-42"" VPOS=""482"" HPOS=""614""/>
						<String ID=""string_50"" HPOS=""572"" VPOS=""626"" WIDTH=""74"" HEIGHT=""128"" WC=""0.11"" CONTENT=""neg""/><SP WIDTH=""-43"" VPOS=""626"" HPOS=""646""/>
						<String ID=""string_51"" HPOS=""603"" VPOS=""966"" WIDTH=""36"" HEIGHT=""117"" WC=""0.20"" CONTENT=""INIPES""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_15"" HPOS=""459"" VPOS=""499"" WIDTH=""130"" HEIGHT=""585"">
					<TextLine ID=""line_27"" HPOS=""526"" VPOS=""549"" WIDTH=""63"" HEIGHT=""535"">
						<String ID=""string_52"" HPOS=""551"" VPOS=""549"" WIDTH=""7"" HEIGHT=""5"" WC=""0.9"" CONTENT=""pgp""/><SP WIDTH=""-32"" VPOS=""549"" HPOS=""558""/>
						<String ID=""string_53"" HPOS=""526"" VPOS=""600"" WIDTH=""46"" HEIGHT=""180"" WC=""0.0"" CONTENT=""get”""/><SP WIDTH=""-19"" VPOS=""600"" HPOS=""572""/>
						<String ID=""string_54"" HPOS=""553"" VPOS=""934"" WIDTH=""36"" HEIGHT=""150"" WC=""0.17"" CONTENT=""mat""/>
					</TextLine>
					<TextLine ID=""line_28"" HPOS=""459"" VPOS=""499"" WIDTH=""106"" HEIGHT=""585"">
						<String ID=""string_55"" HPOS=""459"" VPOS=""499"" WIDTH=""106"" HEIGHT=""169"" WC=""0.13"" CONTENT=""Poet""/><SP WIDTH=""-78"" VPOS=""499"" HPOS=""565""/>
						<String ID=""string_56"" HPOS=""487"" VPOS=""721"" WIDTH=""51"" HEIGHT=""363"" WC=""0.0"" CONTENT="" up/&#39;woo&#39;Bes&#39;pso&#39;uN""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_16"" HPOS=""425"" VPOS=""522"" WIDTH=""60"" HEIGHT=""386"">
					<TextLine ID=""line_29"" HPOS=""425"" VPOS=""522"" WIDTH=""60"" HEIGHT=""386"">
						<String ID=""string_57"" HPOS=""425"" VPOS=""522"" WIDTH=""39"" HEIGHT=""119"" WC=""0.34"" CONTENT=""(gojpyin{""/><SP WIDTH=""-25"" VPOS=""522"" HPOS=""464""/>
						<String ID=""string_58"" HPOS=""439"" VPOS=""651"" WIDTH=""26"" HEIGHT=""87"" WC=""0.48"" CONTENT=""BLO}""/><SP WIDTH=""-26"" VPOS=""651"" HPOS=""465""/>
						<String ID=""string_59"" HPOS=""439"" VPOS=""751"" WIDTH=""46"" HEIGHT=""157"" WC=""0.0"" CONTENT=""‘gsjuinuep)""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_17"" HPOS=""406"" VPOS=""737"" WIDTH=""22"" HEIGHT=""214"">
					<TextLine ID=""line_30"" HPOS=""406"" VPOS=""737"" WIDTH=""22"" HEIGHT=""214"">
						<String ID=""string_60"" HPOS=""406"" VPOS=""737"" WIDTH=""7"" HEIGHT=""152"" WC=""0.7"" CONTENT=""SEROTEC""/><SP WIDTH=""4"" VPOS=""737"" HPOS=""413""/>
						<String ID=""string_61"" HPOS=""417"" VPOS=""898"" WIDTH=""11"" HEIGHT=""53"" WC=""0.9"" CONTENT=""ne""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_18"" HPOS=""342"" VPOS=""826"" WIDTH=""47"" HEIGHT=""261"">
					<TextLine ID=""line_31"" HPOS=""342"" VPOS=""826"" WIDTH=""47"" HEIGHT=""261"">
						<String ID=""string_62"" HPOS=""360"" VPOS=""826"" WIDTH=""6"" HEIGHT=""6"" WC=""0.45"" CONTENT=""»""/><SP WIDTH=""-24"" VPOS=""826"" HPOS=""366""/>
						<String ID=""string_63"" HPOS=""342"" VPOS=""858"" WIDTH=""47"" HEIGHT=""229"" WC=""0.2"" CONTENT=""yoyesedwuno""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_19"" HPOS=""745"" VPOS=""347"" WIDTH=""22"" HEIGHT=""211"">
					<TextLine ID=""line_32"" HPOS=""745"" VPOS=""347"" WIDTH=""22"" HEIGHT=""211"">
						<String ID=""string_64"" HPOS=""745"" VPOS=""347"" WIDTH=""11"" HEIGHT=""45"" WC=""0.22"" CONTENT=""vets""/><SP WIDTH=""-7"" VPOS=""347"" HPOS=""756""/>
						<String ID=""string_65"" HPOS=""749"" VPOS=""399"" WIDTH=""11"" HEIGHT=""44"" WC=""0.15"" CONTENT=""cane""/><SP WIDTH=""-7"" VPOS=""399"" HPOS=""760""/>
						<String ID=""string_66"" HPOS=""753"" VPOS=""451"" WIDTH=""10"" HEIGHT=""44"" WC=""0.0"" CONTENT=""reee""/><SP WIDTH=""-6"" VPOS=""451"" HPOS=""763""/>
						<String ID=""string_67"" HPOS=""757"" VPOS=""502"" WIDTH=""10"" HEIGHT=""56"" WC=""0.0"" CONTENT=""Eee?""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_20"" HPOS=""615"" VPOS=""758"" WIDTH=""12"" HEIGHT=""67"">
					<TextLine ID=""line_33"" HPOS=""615"" VPOS=""758"" WIDTH=""12"" HEIGHT=""67"">
						<String ID=""string_68"" HPOS=""615"" VPOS=""758"" WIDTH=""12"" HEIGHT=""67"" WC=""0.0"" CONTENT=""seneee""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_21"" HPOS=""567"" VPOS=""786"" WIDTH=""14"" HEIGHT=""141"">
					<TextLine ID=""line_34"" HPOS=""567"" VPOS=""786"" WIDTH=""14"" HEIGHT=""141"">
						<String ID=""string_69"" HPOS=""567"" VPOS=""786"" WIDTH=""14"" HEIGHT=""141"" WC=""0.0"" CONTENT=""pepeeeenenet""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_22"" HPOS=""423"" VPOS=""971"" WIDTH=""14"" HEIGHT=""113"">
					<TextLine ID=""line_35"" HPOS=""423"" VPOS=""971"" WIDTH=""14"" HEIGHT=""113"">
						<String ID=""string_70"" HPOS=""423"" VPOS=""971"" WIDTH=""11"" HEIGHT=""77"" WC=""0.0"" CONTENT=""pounseg""/><SP WIDTH=""-18"" VPOS=""971"" HPOS=""434""/>
						<String ID=""string_71"" HPOS=""416"" VPOS=""1051"" WIDTH=""28"" HEIGHT=""33"" WC=""0.0"" CONTENT=""eee""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_23"" HPOS=""391"" VPOS=""524"" WIDTH=""20"" HEIGHT=""205"">
					<TextLine ID=""line_36"" HPOS=""391"" VPOS=""524"" WIDTH=""20"" HEIGHT=""205"">
						<String ID=""string_72"" HPOS=""391"" VPOS=""524"" WIDTH=""17"" HEIGHT=""156"" WC=""0.0"" CONTENT=""aRraeey""/><SP WIDTH=""-21"" VPOS=""524"" HPOS=""408""/>
						<String ID=""string_73"" HPOS=""387"" VPOS=""605"" WIDTH=""30"" HEIGHT=""25"" WC=""0.20"" CONTENT=""eo""/><SP WIDTH=""-26"" VPOS=""605"" HPOS=""417""/>
						<String ID=""string_74"" HPOS=""391"" VPOS=""630"" WIDTH=""17"" HEIGHT=""62"" WC=""0.18"" CONTENT=""eee""/><SP WIDTH=""-6"" VPOS=""630"" HPOS=""408""/>
						<String ID=""string_75"" HPOS=""402"" VPOS=""707"" WIDTH=""9"" HEIGHT=""22"" WC=""0.24"" CONTENT=""se""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_24"" HPOS=""368"" VPOS=""342"" WIDTH=""29"" HEIGHT=""175"">
					<TextLine ID=""line_37"" HPOS=""384"" VPOS=""459"" WIDTH=""13"" HEIGHT=""58"">
						<String ID=""string_76"" HPOS=""384"" VPOS=""459"" WIDTH=""13"" HEIGHT=""58"" WC=""0.0"" CONTENT=""aeeee""/>
					</TextLine>
					<TextLine ID=""line_38"" HPOS=""368"" VPOS=""342"" WIDTH=""26"" HEIGHT=""149"">
						<String ID=""string_77"" HPOS=""368"" VPOS=""342"" WIDTH=""21"" HEIGHT=""110"" WC=""0.0"" CONTENT=""aupeeret®""/><SP WIDTH=""-4"" VPOS=""342"" HPOS=""389""/>
						<String ID=""string_78"" HPOS=""385"" VPOS=""485"" WIDTH=""9"" HEIGHT=""6"" WC=""0.0"" CONTENT=""f""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_25"" HPOS=""273"" VPOS=""247"" WIDTH=""92"" HEIGHT=""572"">
					<TextLine ID=""line_39"" HPOS=""273"" VPOS=""247"" WIDTH=""92"" HEIGHT=""572"">
						<String ID=""string_79"" HPOS=""273"" VPOS=""260"" WIDTH=""35"" HEIGHT=""13"" WC=""0.50"" CONTENT=""|""/><SP WIDTH=""-39"" VPOS=""260"" HPOS=""308""/>
						<String ID=""string_80"" HPOS=""269"" VPOS=""306"" WIDTH=""102"" HEIGHT=""98"" WC=""0.40"" CONTENT=""yaa""/><SP WIDTH=""-102"" VPOS=""306"" HPOS=""371""/>
						<String ID=""string_81"" HPOS=""269"" VPOS=""421"" WIDTH=""102"" HEIGHT=""200"" WC=""0.0"" CONTENT=""ncaa""/><SP WIDTH=""-102"" VPOS=""421"" HPOS=""371""/>
						<String ID=""string_82"" HPOS=""269"" VPOS=""634"" WIDTH=""102"" HEIGHT=""185"" WC=""0.0"" CONTENT=""TIVoeTs""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_26"" HPOS=""93"" VPOS=""388"" WIDTH=""108"" HEIGHT=""341"">
					<TextLine ID=""line_40"" HPOS=""93"" VPOS=""388"" WIDTH=""108"" HEIGHT=""341"">
						<String ID=""string_83"" HPOS=""93"" VPOS=""388"" WIDTH=""108"" HEIGHT=""341"" WC=""0.0"" CONTENT=""¢z6g900""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_27"" HPOS=""115"" VPOS=""802"" WIDTH=""99"" HEIGHT=""510"">
					<TextLine ID=""line_41"" HPOS=""152"" VPOS=""819"" WIDTH=""62"" HEIGHT=""383"">
						<String ID=""string_84"" HPOS=""161"" VPOS=""819"" WIDTH=""28"" HEIGHT=""48"" WC=""0.36"" CONTENT=""4u""/><SP WIDTH=""-37"" VPOS=""819"" HPOS=""189""/>
						<String ID=""string_85"" HPOS=""152"" VPOS=""887"" WIDTH=""50"" HEIGHT=""112"" WC=""0.48"" CONTENT=""MAX""/><SP WIDTH=""-38"" VPOS=""887"" HPOS=""202""/>
						<String ID=""string_86"" HPOS=""164"" VPOS=""1014"" WIDTH=""45"" HEIGHT=""70"" WC=""0.76"" CONTENT=""H@""/><SP WIDTH=""-30"" VPOS=""1014"" HPOS=""209""/>
						<String ID=""string_87"" HPOS=""179"" VPOS=""1103"" WIDTH=""35"" HEIGHT=""99"" WC=""0.0"" CONTENT=""#HeS""/>
					</TextLine>
					<TextLine ID=""line_42"" HPOS=""115"" VPOS=""802"" WIDTH=""39"" HEIGHT=""510"">
						<String ID=""string_88"" HPOS=""111"" VPOS=""733"" WIDTH=""77"" HEIGHT=""284"" WC=""0.22"" CONTENT=""oS""/><SP WIDTH=""-77"" VPOS=""733"" HPOS=""188""/>
						<String ID=""string_89"" HPOS=""111"" VPOS=""1080"" WIDTH=""77"" HEIGHT=""149"" WC=""0.51"" CONTENT=""eee""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_28"" HPOS=""728"" VPOS=""1174"" WIDTH=""173"" HEIGHT=""685"">
					<TextLine ID=""line_43"" HPOS=""864"" VPOS=""1197"" WIDTH=""37"" HEIGHT=""185"">
						<String ID=""string_90"" HPOS=""864"" VPOS=""1197"" WIDTH=""34"" HEIGHT=""142"" WC=""0.0"" CONTENT=""B02&quot;""/><SP WIDTH=""-49"" VPOS=""1197"" HPOS=""898""/>
						<String ID=""string_91"" HPOS=""849"" VPOS=""1291"" WIDTH=""56"" HEIGHT=""28"" WC=""0.19"" CONTENT=""TT""/><SP WIDTH=""-38"" VPOS=""1291"" HPOS=""905""/>
						<String ID=""string_92"" HPOS=""867"" VPOS=""1362"" WIDTH=""34"" HEIGHT=""20"" WC=""0.0"" CONTENT=""&quot;80""/>
					</TextLine>
					<TextLine ID=""line_44"" HPOS=""794"" VPOS=""1197"" WIDTH=""68"" HEIGHT=""662"">
						<String ID=""string_93"" HPOS=""838"" VPOS=""1197"" WIDTH=""10"" HEIGHT=""197"" WC=""0.24"" CONTENT=""yee""/><SP WIDTH=""-8"" VPOS=""1197"" HPOS=""848""/>
						<String ID=""string_94"" HPOS=""840"" VPOS=""1401"" WIDTH=""8"" HEIGHT=""29"" WC=""0.24"" CONTENT=""ae""/><SP WIDTH=""-54"" VPOS=""1401"" HPOS=""848""/>
						<String ID=""string_95"" HPOS=""794"" VPOS=""1465"" WIDTH=""62"" HEIGHT=""99"" WC=""0.48"" CONTENT=""(nue""/><SP WIDTH=""-61"" VPOS=""1465"" HPOS=""856""/>
						<String ID=""string_96"" HPOS=""795"" VPOS=""1567"" WIDTH=""61"" HEIGHT=""89"" WC=""0.21"" CONTENT=""aL""/><SP WIDTH=""-34"" VPOS=""1567"" HPOS=""856""/>
						<String ID=""string_97"" HPOS=""822"" VPOS=""1661"" WIDTH=""40"" HEIGHT=""96"" WC=""0.19"" CONTENT=""GOR)""/><SP WIDTH=""-40"" VPOS=""1661"" HPOS=""862""/>
						<String ID=""string_98"" HPOS=""822"" VPOS=""1772"" WIDTH=""32"" HEIGHT=""87"" WC=""0.47"" CONTENT=""eed""/>
					</TextLine>
					<TextLine ID=""line_45"" HPOS=""728"" VPOS=""1174"" WIDTH=""71"" HEIGHT=""325"">
						<String ID=""string_99"" HPOS=""728"" VPOS=""1174"" WIDTH=""69"" HEIGHT=""189"" WC=""0.24"" CONTENT=""SZESUOUTH""/><SP WIDTH=""-6"" VPOS=""1174"" HPOS=""797""/>
						<String ID=""string_100"" HPOS=""791"" VPOS=""1471"" WIDTH=""8"" HEIGHT=""64"" WC=""0.35"" CONTENT=""oe""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_29"" HPOS=""3567"" VPOS=""1417"" WIDTH=""125"" HEIGHT=""881"">
					<TextLine ID=""line_46"" HPOS=""3657"" VPOS=""1594"" WIDTH=""29"" HEIGHT=""413"">
						<String ID=""string_101"" HPOS=""3659"" VPOS=""1594"" WIDTH=""27"" HEIGHT=""84"" WC=""0.0"" CONTENT=""Hera""/><SP WIDTH=""-12"" VPOS=""1594"" HPOS=""3686""/>
						<String ID=""string_102"" HPOS=""3674"" VPOS=""1689"" WIDTH=""8"" HEIGHT=""232"" WC=""0.0"" CONTENT=""ygeieieieeiens""/><SP WIDTH=""-10"" VPOS=""1689"" HPOS=""3682""/>
						<String ID=""string_103"" HPOS=""3672"" VPOS=""1940"" WIDTH=""8"" HEIGHT=""103"" WC=""0.22"" CONTENT=""ee""/>
					</TextLine>
					<TextLine ID=""line_47"" HPOS=""3567"" VPOS=""1417"" WIDTH=""125"" HEIGHT=""881"">
						<String ID=""string_104"" HPOS=""3571"" VPOS=""1417"" WIDTH=""64"" HEIGHT=""32"" WC=""0.34"" CONTENT=""BI""/><SP WIDTH=""-68"" VPOS=""1417"" HPOS=""3635""/>
						<String ID=""string_105"" HPOS=""3567"" VPOS=""1454"" WIDTH=""125"" HEIGHT=""319"" WC=""0.0"" CONTENT=""enswowaiwagoa""/><SP WIDTH=""-98"" VPOS=""1454"" HPOS=""3692""/>
						<String ID=""string_106"" HPOS=""3594"" VPOS=""1800"" WIDTH=""33"" HEIGHT=""33"" WC=""0.0"" CONTENT=""UL""/><SP WIDTH=""-35"" VPOS=""1800"" HPOS=""3627""/>
						<String ID=""string_107"" HPOS=""3592"" VPOS=""1845"" WIDTH=""35"" HEIGHT=""154"" WC=""0.0"" CONTENT=""JBNOe)9""/><SP WIDTH=""-27"" VPOS=""1845"" HPOS=""3627""/>
						<String ID=""string_108"" HPOS=""3600"" VPOS=""2009"" WIDTH=""25"" HEIGHT=""58"" WC=""0.48"" CONTENT=""B-s""/><SP WIDTH=""-36"" VPOS=""2009"" HPOS=""3625""/>
						<String ID=""string_109"" HPOS=""3589"" VPOS=""2080"" WIDTH=""43"" HEIGHT=""218"" WC=""0.0"" CONTENT=""BOJE|pedxy""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_30"" HPOS=""3478"" VPOS=""1583"" WIDTH=""99"" HEIGHT=""716"">
					<TextLine ID=""line_48"" HPOS=""3533"" VPOS=""1583"" WIDTH=""44"" HEIGHT=""716"">
						<String ID=""string_110"" HPOS=""3534"" VPOS=""1583"" WIDTH=""43"" HEIGHT=""52"" WC=""0.24"" CONTENT=""OS&#39;s""/><SP WIDTH=""-33"" VPOS=""1583"" HPOS=""3577""/>
						<String ID=""string_111"" HPOS=""3544"" VPOS=""2262"" WIDTH=""25"" HEIGHT=""37"" WC=""0.47"" CONTENT=""iu""/>
					</TextLine>
					<TextLine ID=""line_49"" HPOS=""3478"" VPOS=""1665"" WIDTH=""95"" HEIGHT=""619"">
						<String ID=""string_112"" HPOS=""3514"" VPOS=""1665"" WIDTH=""8"" HEIGHT=""28"" WC=""0.5"" CONTENT=""BON""/><SP WIDTH=""-8"" VPOS=""1665"" HPOS=""3522""/>
						<String ID=""string_113"" HPOS=""3514"" VPOS=""1701"" WIDTH=""7"" HEIGHT=""42"" WC=""0.0"" CONTENT=""CUAA""/><SP WIDTH=""-7"" VPOS=""1701"" HPOS=""3521""/>
						<String ID=""string_114"" HPOS=""3514"" VPOS=""1751"" WIDTH=""7"" HEIGHT=""41"" WC=""0.0"" CONTENT=""eb""/><SP WIDTH=""-8"" VPOS=""1751"" HPOS=""3521""/>
						<String ID=""string_115"" HPOS=""3513"" VPOS=""1800"" WIDTH=""7"" HEIGHT=""77"" WC=""0.0"" CONTENT=""Ceae""/><SP WIDTH=""-15"" VPOS=""1800"" HPOS=""3520""/>
						<String ID=""string_116"" HPOS=""3505"" VPOS=""1845"" WIDTH=""28"" HEIGHT=""36"" WC=""0.0"" CONTENT=""een""/><SP WIDTH=""-45"" VPOS=""1845"" HPOS=""3533""/>
						<String ID=""string_117"" HPOS=""3488"" VPOS=""1886"" WIDTH=""85"" HEIGHT=""194"" WC=""0.0"" CONTENT=""iadaidn""/><SP WIDTH=""-88"" VPOS=""1886"" HPOS=""3573""/>
						<String ID=""string_118"" HPOS=""3485"" VPOS=""2095"" WIDTH=""34"" HEIGHT=""46"" WC=""0.78"" CONTENT=""ap""/><SP WIDTH=""-41"" VPOS=""2095"" HPOS=""3519""/>
						<String ID=""string_119"" HPOS=""3478"" VPOS=""2157"" WIDTH=""68"" HEIGHT=""127"" WC=""0.0"" CONTENT=""iNoO;IW""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_31"" HPOS=""3317"" VPOS=""1395"" WIDTH=""158"" HEIGHT=""1334"">
					<TextLine ID=""line_50"" HPOS=""3407"" VPOS=""1627"" WIDTH=""68"" HEIGHT=""491"">
						<String ID=""string_120"" HPOS=""3407"" VPOS=""1627"" WIDTH=""68"" HEIGHT=""254"" WC=""0.9"" CONTENT=""me""/><SP WIDTH=""-16"" VPOS=""1627"" HPOS=""3475""/>
						<String ID=""string_121"" HPOS=""3459"" VPOS=""1889"" WIDTH=""8"" HEIGHT=""114"" WC=""0.0"" CONTENT=""nieeeeess""/><SP WIDTH=""-27"" VPOS=""1889"" HPOS=""3467""/>
						<String ID=""string_122"" HPOS=""3440"" VPOS=""2012"" WIDTH=""26"" HEIGHT=""106"" WC=""0.22"" CONTENT=""Hea""/>
					</TextLine>
					<TextLine ID=""line_51"" HPOS=""3339"" VPOS=""1395"" WIDTH=""130"" HEIGHT=""904"">
						<String ID=""string_123"" HPOS=""3354"" VPOS=""1395"" WIDTH=""115"" HEIGHT=""339"" WC=""0.0"" CONTENT=""seninto®""/><SP WIDTH=""-130"" VPOS=""1395"" HPOS=""3469""/>
						<String ID=""string_124"" HPOS=""3339"" VPOS=""1735"" WIDTH=""92"" HEIGHT=""187"" WC=""0.0"" CONTENT=""aieiNeep|""/><SP WIDTH=""-50"" VPOS=""1735"" HPOS=""3431""/>
						<String ID=""string_125"" HPOS=""3381"" VPOS=""1939"" WIDTH=""32"" HEIGHT=""46"" WC=""0.80"" CONTENT=""ep""/><SP WIDTH=""-38"" VPOS=""1939"" HPOS=""3413""/>
						<String ID=""string_126"" HPOS=""3375"" VPOS=""2000"" WIDTH=""59"" HEIGHT=""299"" WC=""0.5"" CONTENT=""voyeo/inujeing""/>
					</TextLine>
					<TextLine ID=""line_52"" HPOS=""3317"" VPOS=""1964"" WIDTH=""75"" HEIGHT=""765"">
						<String ID=""string_127"" HPOS=""3332"" VPOS=""1964"" WIDTH=""60"" HEIGHT=""52"" WC=""0.23"" CONTENT=""(seat""/><SP WIDTH=""-44"" VPOS=""1964"" HPOS=""3392""/>
						<String ID=""string_128"" HPOS=""3348"" VPOS=""2030"" WIDTH=""10"" HEIGHT=""267"" WC=""0.0"" CONTENT=""edeGttestsvTifearieat""/><SP WIDTH=""-41"" VPOS=""2030"" HPOS=""3358""/>
						<String ID=""string_129"" HPOS=""3317"" VPOS=""2517"" WIDTH=""53"" HEIGHT=""212"" WC=""0.18"" CONTENT=""Peta:""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_32"" HPOS=""3269"" VPOS=""1598"" WIDTH=""45"" HEIGHT=""902"">
					<TextLine ID=""line_53"" HPOS=""3269"" VPOS=""1598"" WIDTH=""45"" HEIGHT=""902"">
						<String ID=""string_130"" HPOS=""3301"" VPOS=""1413"" WIDTH=""8"" HEIGHT=""152"" WC=""0.0"" CONTENT=""eciuare""/><SP WIDTH=""-8"" VPOS=""1413"" HPOS=""3309""/>
						<String ID=""string_131"" HPOS=""3301"" VPOS=""1574"" WIDTH=""7"" HEIGHT=""90"" WC=""0.0"" CONTENT=""sities""/><SP WIDTH=""-8"" VPOS=""1574"" HPOS=""3308""/>
						<String ID=""string_132"" HPOS=""3300"" VPOS=""1672"" WIDTH=""7"" HEIGHT=""202"" WC=""0.0"" CONTENT=""eiiceaaitette""/><SP WIDTH=""-37"" VPOS=""1672"" HPOS=""3307""/>
						<String ID=""string_133"" HPOS=""3270"" VPOS=""1882"" WIDTH=""44"" HEIGHT=""246"" WC=""0.0"" CONTENT=""“animeBejep""/><SP WIDTH=""-45"" VPOS=""1882"" HPOS=""3314""/>
						<String ID=""string_134"" HPOS=""3269"" VPOS=""2153"" WIDTH=""34"" HEIGHT=""147"" WC=""0.33"" CONTENT=""ejowny""/><SP WIDTH=""-32"" VPOS=""2153"" HPOS=""3303""/>
						<String ID=""string_135"" HPOS=""3271"" VPOS=""2463"" WIDTH=""32"" HEIGHT=""37"" WC=""0.45"" CONTENT=""Fe""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_33"" HPOS=""3156"" VPOS=""1861"" WIDTH=""106"" HEIGHT=""878"">
					<TextLine ID=""line_54"" HPOS=""3215"" VPOS=""1861"" WIDTH=""47"" HEIGHT=""439"">
						<String ID=""string_136"" HPOS=""3219"" VPOS=""1861"" WIDTH=""43"" HEIGHT=""173"" WC=""0.0"" CONTENT=""el\pedxe""/><SP WIDTH=""-44"" VPOS=""1861"" HPOS=""3262""/>
						<String ID=""string_137"" HPOS=""3218"" VPOS=""2060"" WIDTH=""39"" HEIGHT=""125"" WC=""0.0"" CONTENT=""puinud""/><SP WIDTH=""-42"" VPOS=""2060"" HPOS=""3257""/>
						<String ID=""string_138"" HPOS=""3215"" VPOS=""2211"" WIDTH=""34"" HEIGHT=""89"" WC=""0.25"" CONTENT=""ejyeq""/>
					</TextLine>
					<TextLine ID=""line_55"" HPOS=""3195"" VPOS=""2110"" WIDTH=""59"" HEIGHT=""590"">
						<String ID=""string_139"" HPOS=""3217"" VPOS=""2110"" WIDTH=""6"" HEIGHT=""3"" WC=""0.28"" CONTENT=""(Al""/><SP WIDTH=""-26"" VPOS=""2110"" HPOS=""3223""/>
						<String ID=""string_140"" HPOS=""3197"" VPOS=""2330"" WIDTH=""36"" HEIGHT=""91"" WC=""0.56"" CONTENT=""$6200""/><SP WIDTH=""-38"" VPOS=""2330"" HPOS=""3233""/>
						<String ID=""string_141"" HPOS=""3195"" VPOS=""2442"" WIDTH=""59"" HEIGHT=""258"" WC=""0.33"" CONTENT=""HEL""/><SP WIDTH=""-70"" VPOS=""2442"" HPOS=""3254""/>
						<String ID=""string_142"" HPOS=""3184"" VPOS=""2591"" WIDTH=""74"" HEIGHT=""38"" WC=""0.33"" CONTENT=""RY""/><SP WIDTH=""-74"" VPOS=""2591"" HPOS=""3258""/>
						<String ID=""string_143"" HPOS=""3184"" VPOS=""2652"" WIDTH=""74"" HEIGHT=""38"" WC=""0.62"" CONTENT=""WA""/>
					</TextLine>
					<TextLine ID=""line_56"" HPOS=""3156"" VPOS=""2236"" WIDTH=""87"" HEIGHT=""503"">
						<String ID=""string_144"" HPOS=""3166"" VPOS=""2192"" WIDTH=""4"" HEIGHT=""57"" WC=""0.0"" CONTENT=""—__""/><SP WIDTH=""-14"" VPOS=""2192"" HPOS=""3170""/>
						<String ID=""string_145"" HPOS=""3156"" VPOS=""2336"" WIDTH=""38"" HEIGHT=""323"" WC=""0.3"" CONTENT=""edad""/><SP WIDTH=""-50"" VPOS=""2336"" HPOS=""3194""/>
						<String ID=""string_146"" HPOS=""3144"" VPOS=""2617"" WIDTH=""56"" HEIGHT=""43"" WC=""0.20"" CONTENT=""2a""/><SP WIDTH=""-32"" VPOS=""2617"" HPOS=""3200""/>
						<String ID=""string_147"" HPOS=""3168"" VPOS=""2685"" WIDTH=""75"" HEIGHT=""54"" WC=""0.41"" CONTENT=""a""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_34"" HPOS=""3087"" VPOS=""2098"" WIDTH=""40"" HEIGHT=""509"">
					<TextLine ID=""line_57"" HPOS=""3087"" VPOS=""2098"" WIDTH=""40"" HEIGHT=""509"">
						<String ID=""string_148"" HPOS=""3081"" VPOS=""2127"" WIDTH=""50"" HEIGHT=""72"" WC=""0.0"" CONTENT=""dese""/><SP WIDTH=""-50"" VPOS=""2127"" HPOS=""3131""/>
						<String ID=""string_149"" HPOS=""3081"" VPOS=""2198"" WIDTH=""50"" HEIGHT=""54"" WC=""0.0"" CONTENT=""IU}""/><SP WIDTH=""-43"" VPOS=""2198"" HPOS=""3131""/>
						<String ID=""string_150"" HPOS=""3088"" VPOS=""2265"" WIDTH=""29"" HEIGHT=""29"" WC=""0.0"" CONTENT=""b]""/><SP WIDTH=""-30"" VPOS=""2265"" HPOS=""3117""/>
						<String ID=""string_151"" HPOS=""3087"" VPOS=""2308"" WIDTH=""31"" HEIGHT=""108"" WC=""0.46"" CONTENT=""W/L,""/><SP WIDTH=""-31"" VPOS=""2308"" HPOS=""3118""/>
						<String ID=""string_152"" HPOS=""3087"" VPOS=""2432"" WIDTH=""34"" HEIGHT=""126"" WC=""0.0"" CONTENT=""[daits""/><SP WIDTH=""-29"" VPOS=""2432"" HPOS=""3121""/>
						<String ID=""string_153"" HPOS=""3092"" VPOS=""2571"" WIDTH=""23"" HEIGHT=""36"" WC=""0.25"" CONTENT=""BD""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_35"" HPOS=""3566"" VPOS=""1405"" WIDTH=""13"" HEIGHT=""583"">
					<TextLine ID=""line_58"" HPOS=""3572"" VPOS=""1405"" WIDTH=""7"" HEIGHT=""4"">
						<String ID=""string_154"" HPOS=""3572"" VPOS=""1405"" WIDTH=""7"" HEIGHT=""4"" WC=""0.0"" CONTENT="".""/>
					</TextLine>
					<TextLine ID=""line_59"" HPOS=""3566"" VPOS=""1676"" WIDTH=""9"" HEIGHT=""312"">
						<String ID=""string_155"" HPOS=""3569"" VPOS=""1676"" WIDTH=""6"" HEIGHT=""78"" WC=""0.0"" CONTENT=""weraaar""/><SP WIDTH=""-7"" VPOS=""1676"" HPOS=""3575""/>
						<String ID=""string_156"" HPOS=""3568"" VPOS=""1764"" WIDTH=""7"" HEIGHT=""40"" WC=""0.7"" CONTENT=""PASE""/><SP WIDTH=""-9"" VPOS=""1764"" HPOS=""3575""/>
						<String ID=""string_157"" HPOS=""3566"" VPOS=""1812"" WIDTH=""7"" HEIGHT=""152"" WC=""0.7"" CONTENT=""CREPE""/><SP WIDTH=""-15"" VPOS=""1812"" HPOS=""3573""/>
						<String ID=""string_158"" HPOS=""3558"" VPOS=""1867"" WIDTH=""28"" HEIGHT=""40"" WC=""0.0"" CONTENT=""eRe""/><SP WIDTH=""-28"" VPOS=""1867"" HPOS=""3586""/>
						<String ID=""string_159"" HPOS=""3558"" VPOS=""1918"" WIDTH=""28"" HEIGHT=""24"" WC=""0.0"" CONTENT=""Ea""/><SP WIDTH=""-28"" VPOS=""1918"" HPOS=""3586""/>
						<String ID=""string_160"" HPOS=""3558"" VPOS=""1942"" WIDTH=""28"" HEIGHT=""26"" WC=""0.0"" CONTENT=""EE""/><SP WIDTH=""-20"" VPOS=""1942"" HPOS=""3586""/>
						<String ID=""string_161"" HPOS=""3566"" VPOS=""1984"" WIDTH=""6"" HEIGHT=""4"" WC=""0.0"" CONTENT=""EY""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_36"" HPOS=""3463"" VPOS=""1467"" WIDTH=""8"" HEIGHT=""91"">
					<TextLine ID=""line_60"" HPOS=""3463"" VPOS=""1467"" WIDTH=""8"" HEIGHT=""91"">
						<String ID=""string_162"" HPOS=""3463"" VPOS=""1467"" WIDTH=""8"" HEIGHT=""91"" WC=""0.0"" CONTENT=""skestane""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_37"" HPOS=""3128"" VPOS=""1783"" WIDTH=""34"" HEIGHT=""242"">
					<TextLine ID=""line_61"" HPOS=""3128"" VPOS=""1783"" WIDTH=""34"" HEIGHT=""242"">
						<String ID=""string_163"" HPOS=""3130"" VPOS=""1783"" WIDTH=""32"" HEIGHT=""66"" WC=""0.9"" CONTENT=""allZ""/><SP WIDTH=""-33"" VPOS=""1783"" HPOS=""3162""/>
						<String ID=""string_164"" HPOS=""3129"" VPOS=""1862"" WIDTH=""32"" HEIGHT=""20"" WC=""0.0"" CONTENT=""L""/><SP WIDTH=""-30"" VPOS=""1862"" HPOS=""3161""/>
						<String ID=""string_165"" HPOS=""3131"" VPOS=""1893"" WIDTH=""30"" HEIGHT=""29"" WC=""0.67"" CONTENT=""Bl""/><SP WIDTH=""-33"" VPOS=""1893"" HPOS=""3161""/>
						<String ID=""string_166"" HPOS=""3128"" VPOS=""1936"" WIDTH=""33"" HEIGHT=""89"" WC=""0.46"" CONTENT=""BRIA""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_38"" HPOS=""2257"" VPOS=""2610"" WIDTH=""903"" HEIGHT=""23"">
					<TextLine ID=""line_62"" HPOS=""2257"" VPOS=""2610"" WIDTH=""903"" HEIGHT=""23"">
						<String ID=""string_167"" HPOS=""2257"" VPOS=""2610"" WIDTH=""903"" HEIGHT=""23"" WC=""0.95"" CONTENT="" ""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_39"" HPOS=""1365"" VPOS=""1670"" WIDTH=""292"" HEIGHT=""846"">
					<TextLine ID=""line_63"" HPOS=""1623"" VPOS=""1977"" WIDTH=""34"" HEIGHT=""539"">
						<String ID=""string_168"" HPOS=""1624"" VPOS=""1977"" WIDTH=""32"" HEIGHT=""81"" WC=""0.49"" CONTENT=""TS0""/><SP WIDTH=""-32"" VPOS=""1977"" HPOS=""1656""/>
						<String ID=""string_169"" HPOS=""1624"" VPOS=""2070"" WIDTH=""32"" HEIGHT=""49"" WC=""0.92"" CONTENT=""LS""/><SP WIDTH=""-33"" VPOS=""2070"" HPOS=""1656""/>
						<String ID=""string_170"" HPOS=""1623"" VPOS=""2136"" WIDTH=""33"" HEIGHT=""84"" WC=""0.22"" CONTENT=""OC""/><SP WIDTH=""-33"" VPOS=""2136"" HPOS=""1656""/>
						<String ID=""string_171"" HPOS=""1623"" VPOS=""2232"" WIDTH=""33"" HEIGHT=""150"" WC=""0.2"" CONTENT=""UNDAd""/><SP WIDTH=""-48"" VPOS=""2232"" HPOS=""1656""/>
						<String ID=""string_172"" HPOS=""1608"" VPOS=""2395"" WIDTH=""53"" HEIGHT=""64"" WC=""0.0"" CONTENT=""CV""/><SP WIDTH=""-36"" VPOS=""2395"" HPOS=""1661""/>
						<String ID=""string_173"" HPOS=""1625"" VPOS=""2458"" WIDTH=""32"" HEIGHT=""58"" WC=""0.0"" CONTENT=""TA""/>
					</TextLine>
					<TextLine ID=""line_64"" HPOS=""1567"" VPOS=""1998"" WIDTH=""34"" HEIGHT=""516"">
						<String ID=""string_174"" HPOS=""1568"" VPOS=""1998"" WIDTH=""32"" HEIGHT=""91"" WC=""0.49"" CONTENT=""TSO""/><SP WIDTH=""-33"" VPOS=""1998"" HPOS=""1600""/>
						<String ID=""string_175"" HPOS=""1567"" VPOS=""2101"" WIDTH=""33"" HEIGHT=""50"" WC=""0.92"" CONTENT=""LS""/><SP WIDTH=""-33"" VPOS=""2101"" HPOS=""1600""/>
						<String ID=""string_176"" HPOS=""1567"" VPOS=""2168"" WIDTH=""34"" HEIGHT=""83"" WC=""0.26"" CONTENT=""W9t""/><SP WIDTH=""-34"" VPOS=""2168"" HPOS=""1601""/>
						<String ID=""string_177"" HPOS=""1567"" VPOS=""2264"" WIDTH=""33"" HEIGHT=""115"" WC=""0.0"" CONTENT=""WAAd""/><SP WIDTH=""-32"" VPOS=""2264"" HPOS=""1600""/>
						<String ID=""string_178"" HPOS=""1568"" VPOS=""2391"" WIDTH=""32"" HEIGHT=""123"" WC=""0.62"" CONTENT=""AVIA""/>
					</TextLine>
					<TextLine ID=""line_65"" HPOS=""1509"" VPOS=""1914"" WIDTH=""35"" HEIGHT=""600"">
						<String ID=""string_179"" HPOS=""1510"" VPOS=""1914"" WIDTH=""33"" HEIGHT=""113"" WC=""0.16"" CONTENT=""‘TSC""/><SP WIDTH=""-48"" VPOS=""1914"" HPOS=""1543""/>
						<String ID=""string_180"" HPOS=""1495"" VPOS=""1998"" WIDTH=""53"" HEIGHT=""27"" WC=""0.32"" CONTENT=""0""/><SP WIDTH=""-39"" VPOS=""1998"" HPOS=""1548""/>
						<String ID=""string_181"" HPOS=""1509"" VPOS=""2037"" WIDTH=""34"" HEIGHT=""146"" WC=""0.17"" CONTENT=""ANIC""/><SP WIDTH=""-33"" VPOS=""2037"" HPOS=""1543""/>
						<String ID=""string_182"" HPOS=""1510"" VPOS=""2196"" WIDTH=""33"" HEIGHT=""187"" WC=""0.1"" CONTENT=""ADMANG""/><SP WIDTH=""-32"" VPOS=""2196"" HPOS=""1543""/>
						<String ID=""string_183"" HPOS=""1511"" VPOS=""2396"" WIDTH=""33"" HEIGHT=""118"" WC=""0.40"" CONTENT=""TIA""/>
					</TextLine>
					<TextLine ID=""line_66"" HPOS=""1456"" VPOS=""2374"" WIDTH=""40"" HEIGHT=""139"">
						<String ID=""string_184"" HPOS=""1456"" VPOS=""2374"" WIDTH=""40"" HEIGHT=""139"" WC=""0.52"" CONTENT=""@XVE""/>
					</TextLine>
					<TextLine ID=""line_67"" HPOS=""1365"" VPOS=""1670"" WIDTH=""76"" HEIGHT=""839"">
						<String ID=""string_185"" HPOS=""1401"" VPOS=""1670"" WIDTH=""33"" HEIGHT=""83"" WC=""0.17"" CONTENT=""Lad""/><SP WIDTH=""-47"" VPOS=""1670"" HPOS=""1434""/>
						<String ID=""string_186"" HPOS=""1387"" VPOS=""1753"" WIDTH=""58"" HEIGHT=""20"" WC=""0.17"" CONTENT=""|""/><SP WIDTH=""-43"" VPOS=""1753"" HPOS=""1445""/>
						<String ID=""string_187"" HPOS=""1402"" VPOS=""1805"" WIDTH=""39"" HEIGHT=""12"" WC=""0.83"" CONTENT="")""/><SP WIDTH=""-40"" VPOS=""1805"" HPOS=""1441""/>
						<String ID=""string_188"" HPOS=""1401"" VPOS=""1822"" WIDTH=""40"" HEIGHT=""80"" WC=""0.37"" CONTENT=""TSO""/><SP WIDTH=""-39"" VPOS=""1822"" HPOS=""1441""/>
						<String ID=""string_189"" HPOS=""1402"" VPOS=""1917"" WIDTH=""32"" HEIGHT=""81"" WC=""0.35"" CONTENT=""LT""/><SP WIDTH=""-69"" VPOS=""1917"" HPOS=""1434""/>
						<String ID=""string_190"" HPOS=""1365"" VPOS=""2015"" WIDTH=""70"" HEIGHT=""158"" WC=""0.32"" CONTENT=""yoga""/><SP WIDTH=""-69"" VPOS=""2015"" HPOS=""1435""/>
						<String ID=""string_191"" HPOS=""1366"" VPOS=""2184"" WIDTH=""70"" HEIGHT=""206"" WC=""0.19"" CONTENT=""SONS""/><SP WIDTH=""-32"" VPOS=""2184"" HPOS=""1436""/>
						<String ID=""string_192"" HPOS=""1404"" VPOS=""2401"" WIDTH=""33"" HEIGHT=""106"" WC=""0.0"" CONTENT=""TY""/><SP WIDTH=""-47"" VPOS=""2401"" HPOS=""1437""/>
						<String ID=""string_193"" HPOS=""1390"" VPOS=""2477"" WIDTH=""51"" HEIGHT=""27"" WC=""0.0"" CONTENT=""¢""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_40"" HPOS=""1329"" VPOS=""2179"" WIDTH=""30"" HEIGHT=""11"">
					<TextLine ID=""line_68"" HPOS=""1329"" VPOS=""2179"" WIDTH=""30"" HEIGHT=""11"">
						<String ID=""string_194"" HPOS=""1317"" VPOS=""2078"" WIDTH=""52"" HEIGHT=""112"" WC=""0.25"" CONTENT=""ee""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_41"" HPOS=""1510"" VPOS=""1691"" WIDTH=""147"" HEIGHT=""62"">
					<TextLine ID=""line_69"" HPOS=""1622"" VPOS=""1693"" WIDTH=""35"" HEIGHT=""50"">
						<String ID=""string_195"" HPOS=""1622"" VPOS=""1693"" WIDTH=""35"" HEIGHT=""50"" WC=""0.74"" CONTENT=""LS""/>
					</TextLine>
					<TextLine ID=""line_70"" HPOS=""1567"" VPOS=""1691"" WIDTH=""33"" HEIGHT=""49"">
						<String ID=""string_196"" HPOS=""1567"" VPOS=""1691"" WIDTH=""33"" HEIGHT=""49"" WC=""0.72"" CONTENT=""LS""/>
					</TextLine>
					<TextLine ID=""line_71"" HPOS=""1510"" VPOS=""1691"" WIDTH=""33"" HEIGHT=""62"">
						<String ID=""string_197"" HPOS=""1510"" VPOS=""1691"" WIDTH=""33"" HEIGHT=""62"" WC=""0.65"" CONTENT=""no""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_42"" HPOS=""1134"" VPOS=""1964"" WIDTH=""149"" HEIGHT=""725"">
					<TextLine ID=""line_72"" HPOS=""1229"" VPOS=""2025"" WIDTH=""54"" HEIGHT=""664"">
						<String ID=""string_198"" HPOS=""1229"" VPOS=""2025"" WIDTH=""34"" HEIGHT=""177"" WC=""0.0"" CONTENT=""Joolues""/><SP WIDTH=""-26"" VPOS=""2025"" HPOS=""1263""/>
						<String ID=""string_199"" HPOS=""1237"" VPOS=""2225"" WIDTH=""26"" HEIGHT=""23"" WC=""0.51"" CONTENT=""B@""/><SP WIDTH=""-26"" VPOS=""2225"" HPOS=""1263""/>
						<String ID=""string_200"" HPOS=""1237"" VPOS=""2273"" WIDTH=""26"" HEIGHT=""66"" WC=""0.87"" CONTENT=""nes""/><SP WIDTH=""-10"" VPOS=""2273"" HPOS=""1263""/>
						<String ID=""string_201"" HPOS=""1253"" VPOS=""2636"" WIDTH=""30"" HEIGHT=""53"" WC=""0.57"" CONTENT=""yo""/>
					</TextLine>
					<TextLine ID=""line_73"" HPOS=""1182"" VPOS=""1964"" WIDTH=""50"" HEIGHT=""677"">
						<String ID=""string_202"" HPOS=""1182"" VPOS=""1964"" WIDTH=""42"" HEIGHT=""200"" WC=""0.70"" CONTENT=""Jojesnpoid""/><SP WIDTH=""-39"" VPOS=""1964"" HPOS=""1224""/>
						<String ID=""string_203"" HPOS=""1185"" VPOS=""2189"" WIDTH=""32"" HEIGHT=""206"" WC=""0.0"" CONTENT=""vejuunueq""/><SP WIDTH=""11"" VPOS=""2189"" HPOS=""1217""/>
						<String ID=""string_204"" HPOS=""1228"" VPOS=""2637"" WIDTH=""4"" HEIGHT=""4"" WC=""0.66"" CONTENT=""‘""/>
					</TextLine>
					<TextLine ID=""line_74"" HPOS=""1134"" VPOS=""2003"" WIDTH=""45"" HEIGHT=""493"">
						<String ID=""string_205"" HPOS=""1134"" VPOS=""2003"" WIDTH=""43"" HEIGHT=""169"" WC=""0.7"" CONTENT=""vadvyd""/><SP WIDTH=""-64"" VPOS=""2003"" HPOS=""1177""/>
						<String ID=""string_206"" HPOS=""1113"" VPOS=""2175"" WIDTH=""65"" HEIGHT=""140"" WC=""0.0"" CONTENT=""1491""/><SP WIDTH=""-40"" VPOS=""2175"" HPOS=""1178""/>
						<String ID=""string_207"" HPOS=""1138"" VPOS=""2344"" WIDTH=""41"" HEIGHT=""152"" WC=""0.43"" CONTENT=""LINNd""/><SP WIDTH=""-70"" VPOS=""2344"" HPOS=""1179""/>
						<String ID=""string_208"" HPOS=""1109"" VPOS=""2465"" WIDTH=""80"" HEIGHT=""28"" WC=""0.43"" CONTENT=""_""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_43"" HPOS=""1135"" VPOS=""2228"" WIDTH=""47"" HEIGHT=""488"">
					<TextLine ID=""line_75"" HPOS=""1135"" VPOS=""2228"" WIDTH=""47"" HEIGHT=""488"">
						<String ID=""string_209"" HPOS=""1135"" VPOS=""2251"" WIDTH=""3"" HEIGHT=""50"" WC=""0.21"" CONTENT=""EU""/><SP WIDTH=""-1"" VPOS=""2251"" HPOS=""1138""/>
						<String ID=""string_210"" HPOS=""1137"" VPOS=""2328"" WIDTH=""4"" HEIGHT=""154"" WC=""0.2"" CONTENT=""LN""/><SP WIDTH=""-2"" VPOS=""2328"" HPOS=""1141""/>
						<String ID=""string_211"" HPOS=""1139"" VPOS=""2531"" WIDTH=""43"" HEIGHT=""185"" WC=""0.7"" CONTENT=""licen""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_44"" HPOS=""1097"" VPOS=""2523"" WIDTH=""29"" HEIGHT=""177"">
					<TextLine ID=""line_76"" HPOS=""1097"" VPOS=""2523"" WIDTH=""29"" HEIGHT=""177"">
						<String ID=""string_212"" HPOS=""1097"" VPOS=""2523"" WIDTH=""27"" HEIGHT=""85"" WC=""0.90"" CONTENT=""VAL""/><SP WIDTH=""-25"" VPOS=""2523"" HPOS=""1124""/>
						<String ID=""string_213"" HPOS=""1099"" VPOS=""2629"" WIDTH=""27"" HEIGHT=""71"" WC=""0.35"" CONTENT=""BOD""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_45"" HPOS=""1181"" VPOS=""2595"" WIDTH=""964"" HEIGHT=""18"">
					<TextLine ID=""line_77"" HPOS=""1181"" VPOS=""2595"" WIDTH=""964"" HEIGHT=""18"">
						<String ID=""string_214"" HPOS=""1181"" VPOS=""2595"" WIDTH=""964"" HEIGHT=""18"" WC=""0.95"" CONTENT="" ""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_46"" HPOS=""1209"" VPOS=""1415"" WIDTH=""35"" HEIGHT=""329"">
					<TextLine ID=""line_78"" HPOS=""1209"" VPOS=""1415"" WIDTH=""35"" HEIGHT=""329"">
						<String ID=""string_215"" HPOS=""1209"" VPOS=""1415"" WIDTH=""35"" HEIGHT=""194"" WC=""0.29"" CONTENT=""eoyeueg""/><SP WIDTH=""-42"" VPOS=""1415"" HPOS=""1244""/>
						<String ID=""string_216"" HPOS=""1202"" VPOS=""1622"" WIDTH=""59"" HEIGHT=""30"" WC=""0.49"" CONTENT=""|""/><SP WIDTH=""-51"" VPOS=""1622"" HPOS=""1261""/>
						<String ID=""string_217"" HPOS=""1210"" VPOS=""1659"" WIDTH=""34"" HEIGHT=""85"" WC=""0.46"" CONTENT=""win""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_47"" HPOS=""210"" VPOS=""1389"" WIDTH=""775"" HEIGHT=""1300"">
					<TextLine ID=""line_79"" HPOS=""925"" VPOS=""1438"" WIDTH=""60"" HEIGHT=""249"">
						<String ID=""string_218"" HPOS=""925"" VPOS=""1438"" WIDTH=""31"" HEIGHT=""85"" WC=""0.51"" CONTENT=""(jnzBo""/><SP WIDTH=""-27"" VPOS=""1438"" HPOS=""956""/>
						<String ID=""string_219"" HPOS=""929"" VPOS=""1532"" WIDTH=""24"" HEIGHT=""60"" WC=""0.0"" CONTENT=""eso""/><SP WIDTH=""-24"" VPOS=""1532"" HPOS=""953""/>
						<String ID=""string_220"" HPOS=""929"" VPOS=""1588"" WIDTH=""56"" HEIGHT=""99"" WC=""0.17"" CONTENT=""pn).""/>
					</TextLine>
					<TextLine ID=""line_80"" HPOS=""875"" VPOS=""1389"" WIDTH=""30"" HEIGHT=""424"">
						<String ID=""string_221"" HPOS=""891"" VPOS=""1389"" WIDTH=""7"" HEIGHT=""5"" WC=""0.59"" CONTENT="":""/><SP WIDTH=""-23"" VPOS=""1389"" HPOS=""898""/>
						<String ID=""string_222"" HPOS=""875"" VPOS=""1465"" WIDTH=""25"" HEIGHT=""72"" WC=""0.0"" CONTENT=""JPW""/><SP WIDTH=""-22"" VPOS=""1465"" HPOS=""900""/>
						<String ID=""string_223"" HPOS=""878"" VPOS=""1549"" WIDTH=""24"" HEIGHT=""21"" WC=""0.10"" CONTENT=""B""/><SP WIDTH=""-24"" VPOS=""1549"" HPOS=""902""/>
						<String ID=""string_224"" HPOS=""878"" VPOS=""1581"" WIDTH=""26"" HEIGHT=""125"" WC=""0.0"" CONTENT=""@4OSU!""/><SP WIDTH=""-25"" VPOS=""1581"" HPOS=""904""/>
						<String ID=""string_225"" HPOS=""879"" VPOS=""1730"" WIDTH=""26"" HEIGHT=""83"" WC=""0.16"" CONTENT=""Z|AB""/>
					</TextLine>
					<TextLine ID=""line_81"" HPOS=""865"" VPOS=""1426"" WIDTH=""44"" HEIGHT=""1202"">
						<String ID=""string_226"" HPOS=""865"" VPOS=""1426"" WIDTH=""35"" HEIGHT=""76"" WC=""0.57"" CONTENT=""WHY""/><SP WIDTH=""-28"" VPOS=""1426"" HPOS=""900""/>
						<String ID=""string_227"" HPOS=""872"" VPOS=""1620"" WIDTH=""37"" HEIGHT=""19"" WC=""0.43"" CONTENT=""i""/><SP WIDTH=""-38"" VPOS=""1620"" HPOS=""909""/>
						<String ID=""string_228"" HPOS=""871"" VPOS=""1710"" WIDTH=""33"" HEIGHT=""9"" WC=""0.10"" CONTENT=""aa""/><SP WIDTH=""-32"" VPOS=""1710"" HPOS=""904""/>
						<String ID=""string_229"" HPOS=""872"" VPOS=""1752"" WIDTH=""32"" HEIGHT=""28"" WC=""0.0"" CONTENT=""AW""/><SP WIDTH=""-31"" VPOS=""1752"" HPOS=""904""/>
						<String ID=""string_230"" HPOS=""873"" VPOS=""1817"" WIDTH=""31"" HEIGHT=""42"" WC=""0.0"" CONTENT=""NN""/><SP WIDTH=""-35"" VPOS=""1817"" HPOS=""904""/>
						<String ID=""string_231"" HPOS=""869"" VPOS=""2356"" WIDTH=""28"" HEIGHT=""54"" WC=""0.67"" CONTENT=""OOS""/><SP WIDTH=""-27"" VPOS=""2356"" HPOS=""897""/>
						<String ID=""string_232"" HPOS=""870"" VPOS=""2426"" WIDTH=""27"" HEIGHT=""49"" WC=""0.69"" CONTENT=""LBS""/><SP WIDTH=""-26"" VPOS=""2426"" HPOS=""897""/>
						<String ID=""string_233"" HPOS=""871"" VPOS=""2481"" WIDTH=""27"" HEIGHT=""77"" WC=""0.29"" CONTENT=""OELO""/><SP WIDTH=""-25"" VPOS=""2481"" HPOS=""898""/>
						<String ID=""string_234"" HPOS=""873"" VPOS=""2572"" WIDTH=""25"" HEIGHT=""56"" WC=""0.0"" CONTENT=""-1AL""/>
					</TextLine>
					<TextLine ID=""line_82"" HPOS=""823"" VPOS=""2007"" WIDTH=""38"" HEIGHT=""682"">
						<String ID=""string_235"" HPOS=""823"" VPOS=""2007"" WIDTH=""26"" HEIGHT=""39"" WC=""0.31"" CONTENT=""1/2""/><SP WIDTH=""-19"" VPOS=""2007"" HPOS=""849""/>
						<String ID=""string_236"" HPOS=""830"" VPOS=""2065"" WIDTH=""20"" HEIGHT=""28"" WC=""0.60"" CONTENT=""uu""/><SP WIDTH=""-23"" VPOS=""2065"" HPOS=""850""/>
						<String ID=""string_237"" HPOS=""827"" VPOS=""2105"" WIDTH=""34"" HEIGHT=""184"" WC=""0.0"" CONTENT=""Jojo)NoL""/><SP WIDTH=""-42"" VPOS=""2105"" HPOS=""861""/>
						<String ID=""string_238"" HPOS=""819"" VPOS=""2243"" WIDTH=""48"" HEIGHT=""44"" WC=""0.0"" CONTENT=""By""/><SP WIDTH=""-35"" VPOS=""2243"" HPOS=""867""/>
						<String ID=""string_239"" HPOS=""832"" VPOS=""2302"" WIDTH=""24"" HEIGHT=""39"" WC=""0.51"" CONTENT=""ws""/><SP WIDTH=""-26"" VPOS=""2302"" HPOS=""856""/>
						<String ID=""string_240"" HPOS=""830"" VPOS=""2355"" WIDTH=""28"" HEIGHT=""76"" WC=""0.0"" CONTENT=""‘pReay""/><SP WIDTH=""-21"" VPOS=""2355"" HPOS=""858""/>
						<String ID=""string_241"" HPOS=""837"" VPOS=""2443"" WIDTH=""21"" HEIGHT=""68"" WC=""0.53"" CONTENT=""“UAW""/><SP WIDTH=""-25"" VPOS=""2443"" HPOS=""858""/>
						<String ID=""string_242"" HPOS=""833"" VPOS=""2526"" WIDTH=""28"" HEIGHT=""75"" WC=""0.0"" CONTENT=""&quot;pely""/><SP WIDTH=""-25"" VPOS=""2526"" HPOS=""861""/>
						<String ID=""string_243"" HPOS=""836"" VPOS=""2614"" WIDTH=""25"" HEIGHT=""57"" WC=""0.0"" CONTENT=""&quot;PNe""/><SP WIDTH=""-10"" VPOS=""2614"" HPOS=""861""/>
						<String ID=""string_244"" HPOS=""851"" VPOS=""2682"" WIDTH=""3"" HEIGHT=""7"" WC=""0.52"" CONTENT=""-""/>
					</TextLine>
					<TextLine ID=""line_83"" HPOS=""777"" VPOS=""1948"" WIDTH=""39"" HEIGHT=""678"">
						<String ID=""string_245"" HPOS=""777"" VPOS=""1948"" WIDTH=""28"" HEIGHT=""63"" WC=""0.0"" CONTENT=""OLS""/><SP WIDTH=""-26"" VPOS=""1948"" HPOS=""805""/>
						<String ID=""string_246"" HPOS=""779"" VPOS=""2021"" WIDTH=""30"" HEIGHT=""136"" WC=""0.0"" CONTENT=""L69&#39;0EZ0""/><SP WIDTH=""-27"" VPOS=""2021"" HPOS=""809""/>
						<String ID=""string_247"" HPOS=""782"" VPOS=""2170"" WIDTH=""27"" HEIGHT=""87"" WC=""0.0"" CONTENT=""HAOW""/><SP WIDTH=""-24"" VPOS=""2170"" HPOS=""809""/>
						<String ID=""string_248"" HPOS=""785"" VPOS=""2271"" WIDTH=""27"" HEIGHT=""69"" WC=""0.41"" CONTENT=""‘GLE""/><SP WIDTH=""-25"" VPOS=""2271"" HPOS=""812""/>
						<String ID=""string_249"" HPOS=""787"" VPOS=""2344"" WIDTH=""27"" HEIGHT=""141"" WC=""0.0"" CONTENT=""OLEVESZ0""/><SP WIDTH=""-26"" VPOS=""2344"" HPOS=""814""/>
						<String ID=""string_250"" HPOS=""788"" VPOS=""2498"" WIDTH=""28"" HEIGHT=""128"" WC=""0.0"" CONTENT=""/XBa/1OL""/>
					</TextLine>
					<TextLine ID=""line_84"" HPOS=""738"" VPOS=""2091"" WIDTH=""39"" HEIGHT=""597"">
						<String ID=""string_251"" HPOS=""738"" VPOS=""2091"" WIDTH=""27"" HEIGHT=""55"" WC=""0.82"" CONTENT=""OOZ""/><SP WIDTH=""-20"" VPOS=""2091"" HPOS=""765""/>
						<String ID=""string_252"" HPOS=""745"" VPOS=""2160"" WIDTH=""21"" HEIGHT=""35"" WC=""0.76"" CONTENT=""WU""/><SP WIDTH=""-24"" VPOS=""2160"" HPOS=""766""/>
						<String ID=""string_253"" HPOS=""742"" VPOS=""2209"" WIDTH=""27"" HEIGHT=""121"" WC=""0.43"" CONTENT=""UOHURS""/><SP WIDTH=""-26"" VPOS=""2209"" HPOS=""769""/>
						<String ID=""string_254"" HPOS=""743"" VPOS=""2342"" WIDTH=""28"" HEIGHT=""49"" WC=""0.48"" CONTENT=""WBS""/><SP WIDTH=""-25"" VPOS=""2342"" HPOS=""771""/>
						<String ID=""string_255"" HPOS=""746"" VPOS=""2404"" WIDTH=""31"" HEIGHT=""75"" WC=""0.25"" CONTENT=""‘Siog""/><SP WIDTH=""-31"" VPOS=""2404"" HPOS=""777""/>
						<String ID=""string_256"" HPOS=""746"" VPOS=""2493"" WIDTH=""27"" HEIGHT=""77"" WC=""0.3"" CONTENT=""&quot;WOO""/><SP WIDTH=""-24"" VPOS=""2493"" HPOS=""773""/>
						<String ID=""string_257"" HPOS=""749"" VPOS=""2584"" WIDTH=""27"" HEIGHT=""82"" WC=""0.39"" CONTENT=""VOU!""/><SP WIDTH=""-13"" VPOS=""2584"" HPOS=""776""/>
						<String ID=""string_258"" HPOS=""763"" VPOS=""2680"" WIDTH=""5"" HEIGHT=""8"" WC=""0.48"" CONTENT=""-""/>
					</TextLine>
					<TextLine ID=""line_85"" HPOS=""707"" VPOS=""2317"" WIDTH=""30"" HEIGHT=""303"">
						<String ID=""string_259"" HPOS=""707"" VPOS=""2317"" WIDTH=""26"" HEIGHT=""61"" WC=""0.73"" CONTENT=""pre""/><SP WIDTH=""-25"" VPOS=""2317"" HPOS=""733""/>
						<String ID=""string_260"" HPOS=""708"" VPOS=""2382"" WIDTH=""28"" HEIGHT=""139"" WC=""0.0"" CONTENT=""§LO&#39;ssZ0""/><SP WIDTH=""-26"" VPOS=""2382"" HPOS=""736""/>
						<String ID=""string_261"" HPOS=""710"" VPOS=""2534"" WIDTH=""27"" HEIGHT=""86"" WC=""0.0"" CONTENT=""‘NAO""/>
					</TextLine>
					<TextLine ID=""line_86"" HPOS=""659"" VPOS=""2023"" WIDTH=""40"" HEIGHT=""596"">
						<String ID=""string_262"" HPOS=""659"" VPOS=""2023"" WIDTH=""28"" HEIGHT=""91"" WC=""0.30"" CONTENT=""00&#39;S’""/><SP WIDTH=""-24"" VPOS=""2023"" HPOS=""687""/>
						<String ID=""string_263"" HPOS=""663"" VPOS=""2123"" WIDTH=""25"" HEIGHT=""9"" WC=""0.16"" CONTENT=""LL""/><SP WIDTH=""-25"" VPOS=""2123"" HPOS=""688""/>
						<String ID=""string_264"" HPOS=""663"" VPOS=""2155"" WIDTH=""26"" HEIGHT=""42"" WC=""0.0"" CONTENT=""P/L""/><SP WIDTH=""-25"" VPOS=""2155"" HPOS=""689""/>
						<String ID=""string_265"" HPOS=""664"" VPOS=""2203"" WIDTH=""27"" HEIGHT=""36"" WC=""0.0"" CONTENT=""20""/><SP WIDTH=""-25"" VPOS=""2203"" HPOS=""691""/>
						<String ID=""string_266"" HPOS=""666"" VPOS=""2252"" WIDTH=""27"" HEIGHT=""51"" WC=""0.5"" CONTENT=""(69&#39;""/><SP WIDTH=""-38"" VPOS=""2252"" HPOS=""693""/>
						<String ID=""string_267"" HPOS=""655"" VPOS=""2307"" WIDTH=""48"" HEIGHT=""44"" WC=""0.12"" CONTENT=""98""/><SP WIDTH=""-35"" VPOS=""2307"" HPOS=""703""/>
						<String ID=""string_268"" HPOS=""668"" VPOS=""2359"" WIDTH=""25"" HEIGHT=""28"" WC=""0.0"" CONTENT=""LL""/><SP WIDTH=""-25"" VPOS=""2359"" HPOS=""693""/>
						<String ID=""string_269"" HPOS=""668"" VPOS=""2391"" WIDTH=""28"" HEIGHT=""83"" WC=""0.0"" CONTENT=""P/1L20""/><SP WIDTH=""-25"" VPOS=""2391"" HPOS=""696""/>
						<String ID=""string_270"" HPOS=""671"" VPOS=""2486"" WIDTH=""27"" HEIGHT=""77"" WC=""0.0"" CONTENT=""‘XVa/""/><SP WIDTH=""-26"" VPOS=""2486"" HPOS=""698""/>
						<String ID=""string_271"" HPOS=""672"" VPOS=""2567"" WIDTH=""27"" HEIGHT=""52"" WC=""0.45"" CONTENT=""TOL""/>
					</TextLine>
					<TextLine ID=""line_87"" HPOS=""615"" VPOS=""1987"" WIDTH=""44"" HEIGHT=""696"">
						<String ID=""string_272"" HPOS=""615"" VPOS=""1987"" WIDTH=""26"" HEIGHT=""17"" WC=""0.41"" CONTENT=""2""/><SP WIDTH=""-18"" VPOS=""1987"" HPOS=""641""/>
						<String ID=""string_273"" HPOS=""623"" VPOS=""2018"" WIDTH=""20"" HEIGHT=""35"" WC=""0.41"" CONTENT=""au""/><SP WIDTH=""-25"" VPOS=""2018"" HPOS=""643""/>
						<String ID=""string_274"" HPOS=""618"" VPOS=""2066"" WIDTH=""35"" HEIGHT=""174"" WC=""0.0"" CONTENT=""jnnxeidwog""/><SP WIDTH=""-31"" VPOS=""2066"" HPOS=""653""/>
						<String ID=""string_275"" HPOS=""622"" VPOS=""2264"" WIDTH=""28"" HEIGHT=""48"" WC=""0.29"" CONTENT=""sg""/><SP WIDTH=""-25"" VPOS=""2264"" HPOS=""650""/>
						<String ID=""string_276"" HPOS=""625"" VPOS=""2326"" WIDTH=""32"" HEIGHT=""120"" WC=""0.0"" CONTENT=""‘eufelys""/><SP WIDTH=""-28"" VPOS=""2326"" HPOS=""657""/>
						<String ID=""string_277"" HPOS=""629"" VPOS=""2458"" WIDTH=""27"" HEIGHT=""125"" WC=""0.9"" CONTENT=""eUNWOS""/><SP WIDTH=""-26"" VPOS=""2458"" HPOS=""656""/>
						<String ID=""string_278"" HPOS=""630"" VPOS=""2597"" WIDTH=""29"" HEIGHT=""65"" WC=""0.2"" CONTENT=""“AHI""/><SP WIDTH=""-14"" VPOS=""2597"" HPOS=""659""/>
						<String ID=""string_279"" HPOS=""645"" VPOS=""2676"" WIDTH=""5"" HEIGHT=""7"" WC=""0.0"" CONTENT=""-""/>
					</TextLine>
					<TextLine ID=""line_88"" HPOS=""590"" VPOS=""2443"" WIDTH=""29"" HEIGHT=""240"">
						<String ID=""string_280"" HPOS=""590"" VPOS=""2443"" WIDTH=""27"" HEIGHT=""79"" WC=""0.15"" CONTENT=""‘nuon|""/><SP WIDTH=""-26"" VPOS=""2443"" HPOS=""617""/>
						<String ID=""string_281"" HPOS=""591"" VPOS=""2534"" WIDTH=""27"" HEIGHT=""36"" WC=""0.73"" CONTENT=""ep""/><SP WIDTH=""-24"" VPOS=""2534"" HPOS=""618""/>
						<String ID=""string_282"" HPOS=""594"" VPOS=""2582"" WIDTH=""25"" HEIGHT=""101"" WC=""0.23"" CONTENT=""eyoUNd""/>
					</TextLine>
					<TextLine ID=""line_89"" HPOS=""544"" VPOS=""2244"" WIDTH=""41"" HEIGHT=""438"">
						<String ID=""string_283"" HPOS=""544"" VPOS=""2244"" WIDTH=""28"" HEIGHT=""72"" WC=""0.71"" CONTENT=""NOU""/><SP WIDTH=""-27"" VPOS=""2244"" HPOS=""572""/>
						<String ID=""string_284"" HPOS=""545"" VPOS=""2330"" WIDTH=""29"" HEIGHT=""121"" WC=""0.0"" CONTENT=""OaP&#39;69)""/><SP WIDTH=""-24"" VPOS=""2330"" HPOS=""574""/>
						<String ID=""string_285"" HPOS=""550"" VPOS=""2466"" WIDTH=""28"" HEIGHT=""96"" WC=""0.0"" CONTENT=""1ISOS""/><SP WIDTH=""-25"" VPOS=""2466"" HPOS=""578""/>
						<String ID=""string_286"" HPOS=""553"" VPOS=""2575"" WIDTH=""32"" HEIGHT=""107"" WC=""0.18"" CONTENT=""[eydeO""/>
					</TextLine>
					<TextLine ID=""line_90"" HPOS=""503"" VPOS=""2152"" WIDTH=""37"" HEIGHT=""528"">
						<String ID=""string_287"" HPOS=""503"" VPOS=""2152"" WIDTH=""27"" HEIGHT=""117"" WC=""0.45"" CONTENT=""BapelO""/><SP WIDTH=""-27"" VPOS=""2152"" HPOS=""530""/>
						<String ID=""string_288"" HPOS=""503"" VPOS=""2284"" WIDTH=""29"" HEIGHT=""66"" WC=""0.77"" CONTENT=""‘ONS""/><SP WIDTH=""-14"" VPOS=""2284"" HPOS=""532""/>
						<String ID=""string_289"" HPOS=""518"" VPOS=""2364"" WIDTH=""5"" HEIGHT=""8"" WC=""0.49"" CONTENT=""»""/><SP WIDTH=""-18"" VPOS=""2364"" HPOS=""523""/>
						<String ID=""string_290"" HPOS=""505"" VPOS=""2385"" WIDTH=""29"" HEIGHT=""93"" WC=""0.41"" CONTENT=""HNVA""/><SP WIDTH=""-25"" VPOS=""2385"" HPOS=""534""/>
						<String ID=""string_291"" HPOS=""509"" VPOS=""2491"" WIDTH=""28"" HEIGHT=""70"" WC=""0.63"" CONTENT=""dLO""/><SP WIDTH=""-23"" VPOS=""2491"" HPOS=""537""/>
						<String ID=""string_292"" HPOS=""514"" VPOS=""2574"" WIDTH=""26"" HEIGHT=""106"" WC=""0.27"" CONTENT=""‘Bourg""/>
					</TextLine>
					<TextLine ID=""line_91"" HPOS=""458"" VPOS=""1990"" WIDTH=""41"" HEIGHT=""689"">
						<String ID=""string_293"" HPOS=""458"" VPOS=""1990"" WIDTH=""27"" HEIGHT=""89"" WC=""0.24"" CONTENT=""£00U""/><SP WIDTH=""-25"" VPOS=""1990"" HPOS=""485""/>
						<String ID=""string_294"" HPOS=""460"" VPOS=""2093"" WIDTH=""27"" HEIGHT=""75"" WC=""0.40"" CONTENT=""BIG""/><SP WIDTH=""-25"" VPOS=""2093"" HPOS=""487""/>
						<String ID=""string_295"" HPOS=""462"" VPOS=""2183"" WIDTH=""27"" HEIGHT=""78"" WC=""0.40"" CONTENT=""ZO00""/><SP WIDTH=""-26"" VPOS=""2183"" HPOS=""489""/>
						<String ID=""string_296"" HPOS=""463"" VPOS=""2273"" WIDTH=""27"" HEIGHT=""78"" WC=""0.34"" CONTENT=""O18?""/><SP WIDTH=""-27"" VPOS=""2273"" HPOS=""490""/>
						<String ID=""string_297"" HPOS=""463"" VPOS=""2362"" WIDTH=""30"" HEIGHT=""94"" WC=""0.69"" CONTENT=""AdLO""/><SP WIDTH=""-24"" VPOS=""2362"" HPOS=""493""/>
						<String ID=""string_298"" HPOS=""469"" VPOS=""2469"" WIDTH=""27"" HEIGHT=""88"" WC=""0.82"" CONTENT=""96OH""/><SP WIDTH=""-25"" VPOS=""2469"" HPOS=""496""/>
						<String ID=""string_299"" HPOS=""471"" VPOS=""2572"" WIDTH=""28"" HEIGHT=""107"" WC=""0.0"" CONTENT=""“IMU""/>
					</TextLine>
					<TextLine ID=""line_92"" HPOS=""417"" VPOS=""2007"" WIDTH=""41"" HEIGHT=""670"">
						<String ID=""string_300"" HPOS=""417"" VPOS=""2007"" WIDTH=""34"" HEIGHT=""140"" WC=""0.76"" CONTENT=""snueboy""/><SP WIDTH=""-32"" VPOS=""2007"" HPOS=""451""/>
						<String ID=""string_301"" HPOS=""419"" VPOS=""2163"" WIDTH=""34"" HEIGHT=""70"" WC=""0.66"" CONTENT=""‘by""/><SP WIDTH=""-40"" VPOS=""2163"" HPOS=""453""/>
						<String ID=""string_302"" HPOS=""413"" VPOS=""2211"" WIDTH=""49"" HEIGHT=""25"" WC=""0.81"" CONTENT=""-""/><SP WIDTH=""-42"" VPOS=""2211"" HPOS=""462""/>
						<String ID=""string_303"" HPOS=""420"" VPOS=""2247"" WIDTH=""34"" HEIGHT=""58"" WC=""0.67"" CONTENT=""[No""/><SP WIDTH=""-31"" VPOS=""2247"" HPOS=""454""/>
						<String ID=""string_304"" HPOS=""423"" VPOS=""2318"" WIDTH=""26"" HEIGHT=""68"" WC=""0.40"" CONTENT=""eieg""/><SP WIDTH=""-24"" VPOS=""2318"" HPOS=""449""/>
						<String ID=""string_305"" HPOS=""425"" VPOS=""2399"" WIDTH=""33"" HEIGHT=""75"" WC=""0.25"" CONTENT=""dio""/><SP WIDTH=""-31"" VPOS=""2399"" HPOS=""458""/>
						<String ID=""string_306"" HPOS=""427"" VPOS=""2487"" WIDTH=""27"" HEIGHT=""72"" WC=""0.57"" CONTENT=""dua""/><SP WIDTH=""-22"" VPOS=""2487"" HPOS=""454""/>
						<String ID=""string_307"" HPOS=""432"" VPOS=""2572"" WIDTH=""26"" HEIGHT=""105"" WC=""0.5"" CONTENT=""‘BoUurE""/>
					</TextLine>
					<TextLine ID=""line_93"" HPOS=""376"" VPOS=""1992"" WIDTH=""41"" HEIGHT=""685"">
						<String ID=""string_308"" HPOS=""376"" VPOS=""1992"" WIDTH=""27"" HEIGHT=""78"" WC=""0.47"" CONTENT=""00S0""/><SP WIDTH=""-25"" VPOS=""1992"" HPOS=""403""/>
						<String ID=""string_309"" HPOS=""378"" VPOS=""2083"" WIDTH=""27"" HEIGHT=""76"" WC=""0.73"" CONTENT=""£189""/><SP WIDTH=""-26"" VPOS=""2083"" HPOS=""405""/>
						<String ID=""string_310"" HPOS=""379"" VPOS=""2172"" WIDTH=""27"" HEIGHT=""81"" WC=""0.28"" CONTENT=""OPA""/><SP WIDTH=""-27"" VPOS=""2172"" HPOS=""406""/>
						<String ID=""string_311"" HPOS=""379"" VPOS=""2266"" WIDTH=""29"" HEIGHT=""81"" WC=""0.50"" CONTENT=""SOS0""/><SP WIDTH=""-27"" VPOS=""2266"" HPOS=""408""/>
						<String ID=""string_312"" HPOS=""381"" VPOS=""2360"" WIDTH=""31"" HEIGHT=""94"" WC=""0.21"" CONTENT=""FAHA""/><SP WIDTH=""-25"" VPOS=""2360"" HPOS=""412""/>
						<String ID=""string_313"" HPOS=""387"" VPOS=""2467"" WIDTH=""28"" HEIGHT=""88"" WC=""0.12"" CONTENT=""690H""/><SP WIDTH=""-27"" VPOS=""2467"" HPOS=""415""/>
						<String ID=""string_314"" HPOS=""388"" VPOS=""2569"" WIDTH=""29"" HEIGHT=""108"" WC=""0.0"" CONTENT=""“IMUOD""/>
					</TextLine>
					<TextLine ID=""line_94"" HPOS=""334"" VPOS=""1975"" WIDTH=""43"" HEIGHT=""701"">
						<String ID=""string_315"" HPOS=""334"" VPOS=""1975"" WIDTH=""34"" HEIGHT=""164"" WC=""0.7"" CONTENT=""ySeunong""/><SP WIDTH=""-29"" VPOS=""1975"" HPOS=""368""/>
						<String ID=""string_316"" HPOS=""339"" VPOS=""2144"" WIDTH=""26"" HEIGHT=""17"" WC=""0.54"" CONTENT=""&#39;g""/><SP WIDTH=""-26"" VPOS=""2144"" HPOS=""365""/>
						<String ID=""string_317"" HPOS=""339"" VPOS=""2175"" WIDTH=""27"" HEIGHT=""77"" WC=""0.13"" CONTENT=""OOS""/><SP WIDTH=""-26"" VPOS=""2175"" HPOS=""366""/>
						<String ID=""string_318"" HPOS=""340"" VPOS=""2266"" WIDTH=""30"" HEIGHT=""46"" WC=""0.57"" CONTENT=""‘ee""/><SP WIDTH=""-23"" VPOS=""2266"" HPOS=""370""/>
						<String ID=""string_319"" HPOS=""347"" VPOS=""2326"" WIDTH=""20"" HEIGHT=""37"" WC=""0.56"" CONTENT=""Wu""/><SP WIDTH=""-25"" VPOS=""2326"" HPOS=""367""/>
						<String ID=""string_320"" HPOS=""342"" VPOS=""2376"" WIDTH=""35"" HEIGHT=""117"" WC=""0.34"" CONTENT=""jzodse)""/><SP WIDTH=""-29"" VPOS=""2376"" HPOS=""377""/>
						<String ID=""string_321"" HPOS=""348"" VPOS=""2508"" WIDTH=""26"" HEIGHT=""50"" WC=""0.69"" CONTENT=""“AS""/><SP WIDTH=""-25"" VPOS=""2508"" HPOS=""374""/>
						<String ID=""string_322"" HPOS=""349"" VPOS=""2572"" WIDTH=""28"" HEIGHT=""104"" WC=""0.0"" CONTENT=""{/NIPeS""/>
					</TextLine>
					<TextLine ID=""line_95"" HPOS=""303"" VPOS=""2383"" WIDTH=""34"" HEIGHT=""292"">
						<String ID=""string_323"" HPOS=""303"" VPOS=""2383"" WIDTH=""31"" HEIGHT=""204"" WC=""0.40"" CONTENT=""€9691680H""/><SP WIDTH=""-25"" VPOS=""2383"" HPOS=""334""/>
						<String ID=""string_324"" HPOS=""309"" VPOS=""2594"" WIDTH=""26"" HEIGHT=""36"" WC=""0.20"" CONTENT=""“a""/><SP WIDTH=""-25"" VPOS=""2594"" HPOS=""335""/>
						<String ID=""string_325"" HPOS=""310"" VPOS=""2634"" WIDTH=""26"" HEIGHT=""15"" WC=""0.2"" CONTENT=""1""/><SP WIDTH=""-26"" VPOS=""2634"" HPOS=""336""/>
						<String ID=""string_326"" HPOS=""310"" VPOS=""2653"" WIDTH=""27"" HEIGHT=""22"" WC=""0.2"" CONTENT=""O""/>
					</TextLine>
					<TextLine ID=""line_96"" HPOS=""257"" VPOS=""2128"" WIDTH=""41"" HEIGHT=""545"">
						<String ID=""string_327"" HPOS=""257"" VPOS=""2128"" WIDTH=""28"" HEIGHT=""57"" WC=""0.34"" CONTENT=""966""/><SP WIDTH=""-27"" VPOS=""2128"" HPOS=""285""/>
						<String ID=""string_328"" HPOS=""258"" VPOS=""2193"" WIDTH=""30"" HEIGHT=""170"" WC=""0.0"" CONTENT=""L/EeL6/Orl""/><SP WIDTH=""-24"" VPOS=""2193"" HPOS=""288""/>
						<String ID=""string_329"" HPOS=""264"" VPOS=""2374"" WIDTH=""34"" HEIGHT=""274"" WC=""0.0"" CONTENT=""sus""/><SP WIDTH=""-38"" VPOS=""2374"" HPOS=""298""/>
						<String ID=""string_330"" HPOS=""260"" VPOS=""2443"" WIDTH=""42"" HEIGHT=""60"" WC=""0.0"" CONTENT=""Woo""/><SP WIDTH=""-42"" VPOS=""2443"" HPOS=""302""/>
						<String ID=""string_331"" HPOS=""260"" VPOS=""2506"" WIDTH=""42"" HEIGHT=""53"" WC=""0.0"" CONTENT=""Ba""/><SP WIDTH=""-38"" VPOS=""2506"" HPOS=""302""/>
						<String ID=""string_332"" HPOS=""264"" VPOS=""2576"" WIDTH=""34"" HEIGHT=""97"" WC=""0.8"" CONTENT=""p4o“N""/>
					</TextLine>
					<TextLine ID=""line_97"" HPOS=""210"" VPOS=""2158"" WIDTH=""46"" HEIGHT=""513"">
						<String ID=""string_333"" HPOS=""210"" VPOS=""2158"" WIDTH=""35"" HEIGHT=""83"" WC=""0.58"" CONTENT=""HS""/><SP WIDTH=""-32"" VPOS=""2158"" HPOS=""245""/>
						<String ID=""string_334"" HPOS=""213"" VPOS=""2257"" WIDTH=""38"" HEIGHT=""266"" WC=""0.7"" CONTENT=""OU&#39;SNNINGS""/><SP WIDTH=""-23"" VPOS=""2257"" HPOS=""251""/>
						<String ID=""string_335"" HPOS=""228"" VPOS=""2537"" WIDTH=""28"" HEIGHT=""134"" WC=""0.0"" CONTENT=""“02nd""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_48"" HPOS=""771"" VPOS=""1661"" WIDTH=""33"" HEIGHT=""197"">
					<TextLine ID=""line_98"" HPOS=""771"" VPOS=""1661"" WIDTH=""33"" HEIGHT=""197"">
						<String ID=""string_336"" HPOS=""737"" VPOS=""1591"" WIDTH=""67"" HEIGHT=""267"" WC=""0.0"" CONTENT=""vPROARLAN""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_49"" HPOS=""535"" VPOS=""1338"" WIDTH=""89"" HEIGHT=""404"">
					<TextLine ID=""line_99"" HPOS=""535"" VPOS=""1338"" WIDTH=""89"" HEIGHT=""404"">
						<String ID=""string_337"" HPOS=""535"" VPOS=""1338"" WIDTH=""89"" HEIGHT=""404"" WC=""0.38"" CONTENT=""VYuNLova""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_50"" HPOS=""402"" VPOS=""1336"" WIDTH=""48"" HEIGHT=""298"">
					<TextLine ID=""line_100"" HPOS=""402"" VPOS=""1336"" WIDTH=""48"" HEIGHT=""298"">
						<String ID=""string_338"" HPOS=""410"" VPOS=""1336"" WIDTH=""24"" HEIGHT=""82"" WC=""0.0"" CONTENT=""njned""/><SP WIDTH=""-20"" VPOS=""1336"" HPOS=""434""/>
						<String ID=""string_339"" HPOS=""414"" VPOS=""1428"" WIDTH=""21"" HEIGHT=""27"" WC=""0.95"" CONTENT=""ap""/><SP WIDTH=""-33"" VPOS=""1428"" HPOS=""435""/>
						<String ID=""string_340"" HPOS=""402"" VPOS=""1461"" WIDTH=""42"" HEIGHT=""96"" WC=""0.3"" CONTENT=""supinoud""/><SP WIDTH=""-30"" VPOS=""1461"" HPOS=""444""/>
						<String ID=""string_341"" HPOS=""414"" VPOS=""1568"" WIDTH=""36"" HEIGHT=""66"" WC=""0.19"" CONTENT=""jnwyad""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_51"" HPOS=""337"" VPOS=""1480"" WIDTH=""81"" HEIGHT=""210"">
					<TextLine ID=""line_101"" HPOS=""337"" VPOS=""1480"" WIDTH=""81"" HEIGHT=""210"">
						<String ID=""string_342"" HPOS=""349"" VPOS=""1480"" WIDTH=""64"" HEIGHT=""47"" WC=""0.44"" CONTENT=""ir""/><SP WIDTH=""-76"" VPOS=""1480"" HPOS=""413""/>
						<String ID=""string_343"" HPOS=""337"" VPOS=""1534"" WIDTH=""81"" HEIGHT=""156"" WC=""0.0"" CONTENT=""syulipe""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_52"" HPOS=""231"" VPOS=""2691"" WIDTH=""679"" HEIGHT=""22"">
					<TextLine ID=""line_102"" HPOS=""231"" VPOS=""2691"" WIDTH=""679"" HEIGHT=""22"">
						<String ID=""string_344"" HPOS=""231"" VPOS=""2691"" WIDTH=""679"" HEIGHT=""22"" WC=""0.95"" CONTENT="" ""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_53"" HPOS=""231"" VPOS=""2689"" WIDTH=""605"" HEIGHT=""21"">
					<TextLine ID=""line_103"" HPOS=""231"" VPOS=""2689"" WIDTH=""605"" HEIGHT=""21"">
						<String ID=""string_345"" HPOS=""231"" VPOS=""2689"" WIDTH=""605"" HEIGHT=""21"" WC=""0.95"" CONTENT="" ""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_54"" HPOS=""0"" VPOS=""0"" WIDTH=""4032"" HEIGHT=""3024"">
					<TextLine ID=""line_104"" HPOS=""0"" VPOS=""0"" WIDTH=""4032"" HEIGHT=""3024"">
						<String ID=""string_346"" HPOS=""0"" VPOS=""0"" WIDTH=""4032"" HEIGHT=""3024"" WC=""0.95"" CONTENT="" ""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_55"" HPOS=""2785"" VPOS=""2743"" WIDTH=""977"" HEIGHT=""23"">
					<TextLine ID=""line_105"" HPOS=""2785"" VPOS=""2743"" WIDTH=""977"" HEIGHT=""23"">
						<String ID=""string_347"" HPOS=""2785"" VPOS=""2743"" WIDTH=""977"" HEIGHT=""23"" WC=""0.95"" CONTENT="" ""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_56"" HPOS=""1434"" VPOS=""2725"" WIDTH=""1151"" HEIGHT=""25"">
					<TextLine ID=""line_106"" HPOS=""1434"" VPOS=""2725"" WIDTH=""1151"" HEIGHT=""25"">
						<String ID=""string_348"" HPOS=""1434"" VPOS=""2725"" WIDTH=""1151"" HEIGHT=""25"" WC=""0.95"" CONTENT="" ""/>
					</TextLine>
				</TextBlock>
				<TextBlock ID=""block_57"" HPOS=""918"" VPOS=""2711"" WIDTH=""1667"" HEIGHT=""41"">
					<TextLine ID=""line_107"" HPOS=""918"" VPOS=""2711"" WIDTH=""1667"" HEIGHT=""41"">
						<String ID=""string_349"" HPOS=""918"" VPOS=""2711"" WIDTH=""1667"" HEIGHT=""41"" WC=""0.95"" CONTENT="" ""/>
					</TextLine>
				</TextBlock>
			</PrintSpace>
		</Page>
	</Layout>
</alto>";
			var res = Deserialize<AltoType>(xml);
			Assert.IsTrue(true);
        }

        public string Serialize<T>(T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        public T Deserialize<T>(string toDeSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (StringReader reader = new StringReader(toDeSerialize))
            {
                var result = (T)xmlSerializer.Deserialize(reader);
                return result;
            }
        }
    }
}