
module ChickenSoftware.PanzerGeneral.UnitGenerator

open Unit
open System.IO
open FSharp.Data
open System.Reflection
open EquipmentGenerator



let getJson (assembly:Assembly) (fileName:string) =
    let stream = assembly.GetManifestResourceStream(fileName);
    let reader = new StreamReader(stream)
    reader.ReadToEnd()



    
