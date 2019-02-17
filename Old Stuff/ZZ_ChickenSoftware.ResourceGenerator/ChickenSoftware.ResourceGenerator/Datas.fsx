
open System

let getLastDirectory (filePath:string) =
    let lastSlashPosition = filePath.LastIndexOf(char 47)
    let totalLength = filePath.Length
    let tokenLength = totalLength - lastSlashPosition - 1
    filePath.Substring(lastSlashPosition+1,tokenLength)    

let getResources path =
    let directoryInfo = System.IO.DirectoryInfo(path)
    let directory = getLastDirectory path
    let files = directoryInfo.GetFiles()
    files
    |> Array.map(fun f -> directory + "\\" + f.Name)

let getLogicalNames path =
    let directoryInfo = System.IO.DirectoryInfo(path)
    let files = directoryInfo.GetFiles()
    files
    |> Array.map(fun f -> f.Name)
    |> Array.map(fun n -> n.ToLowerInvariant())
    |> Array.map(fun n -> n.Replace(".json",String.Empty))
    |> Array.map(fun n -> n.Replace("_",String.Empty))

let createBlock (resource:string) (logicalName:string)=
    "<EmbeddedResource Include=\"" + resource + "\"><LogicalName>" + logicalName + "</LogicalName></EmbeddedResource>"

let getOutput path =
    let resources = getResources path
    let logicalNames = getLogicalNames path
    resources
    |> Array.zip logicalNames
    |> Array.map(fun (r,ln) -> createBlock ln r)
    |> Array.map(fun s -> "    " + s)

let directoryName = "Data"
let inputPath = "//Users//jamesdixon//Desktop//" + directoryName
let output = getOutput inputPath

let fileName = directoryName + ".txt"
let outputPath = "//Users//jamesdixon//Desktop//" + fileName
System.IO.File.WriteAllLines(outputPath, output)


 
