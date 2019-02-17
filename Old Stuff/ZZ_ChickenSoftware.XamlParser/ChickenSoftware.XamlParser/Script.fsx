
#r "System.Xml.Linq.dll"
#r "Newtonsoft.Json"

open System.IO
open System.Xml
open System.Xml.Linq
open Newtonsoft.Json

let convert xmlPath jsonPath =
    let xmlString = File.ReadAllText(xmlPath)
    let xDocument = XDocument.Parse(xmlString)
    let document = new XmlDocument()
    document.LoadXml(xDocument.ToString())
    let json = JsonConvert.SerializeXmlNode(document)
    File.WriteAllText(jsonPath,json)

let xmlPath = "//Users//jamesdixon//GitHub//ChickenSoftware.XamlParser//ChickenSoftware.XamlParser//XmlLookupData//Scenario_Unit.xml"
let jsonPath = "//Users//jamesdixon//GitHub//ChickenSoftware.XamlParser//ChickenSoftware.XamlParser//JsonLookupData//Scenario_Unit.json"
convert xmlPath jsonPath

//let xmlFolder = "//Users//jamesdixon//GitHub//ChickenSoftware.XamlParser//ChickenSoftware.XamlParser//XmlLookupData"
//let jsonFolder = "//Users//jamesdixon//GitHub//ChickenSoftware.XamlParser//ChickenSoftware.XamlParser//JsonLookupData"
//let info = DirectoryInfo(xmlFolder)
//info.GetFiles()
//|> Array.filter(fun f -> f.Extension = ".xml") 
//|> Array.map(fun f -> f.FullName, jsonFolder + "//" + f.Name.Replace(".xml", ".json"))
//|> Array.iter(fun (x,j) -> convert x j)







