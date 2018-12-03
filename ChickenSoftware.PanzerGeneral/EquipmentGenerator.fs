module ChickenSoftware.PanzerGeneral.EquipmentGenerator

open System.IO
open FSharp.Data
open System.Reflection

type EquipmentContext = JsonProvider<"Data//Equipment.json">

let getJson (assembly:Assembly) (fileName:string) =
    let stream = assembly.GetManifestResourceStream(fileName);
    let reader = new StreamReader(stream)
    reader.ReadToEnd()

let getEquipmentData (assembly:Assembly) = 
    let json = getJson assembly "equipment"
    let equipment = EquipmentContext.Parse(json)
    equipment.Dataroot.Equipment

let getEquipmentDatum equipmentId assembly =
    let equipmentData = getEquipmentData assembly
    equipmentData |> Array.tryFind(fun ed -> ed.EquipmentId = equipmentId)

