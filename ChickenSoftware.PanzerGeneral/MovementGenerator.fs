
module ChickenSoftware.PanzerGeneral.MovementGenerator

open System.IO
open FSharp.Data
open System.Reflection

type MovementCostContext = JsonProvider<"Data//MovementCost.json">

let getJson (assembly:Assembly) (fileName:string) =
    let stream = assembly.GetManifestResourceStream(fileName);
    let reader = new StreamReader(stream)
    reader.ReadToEnd()

let getMovementCostData (assembly:Assembly) = 
    let json = getJson assembly "movementcost"
    let movementCost = MovementCostContext.Parse(json)
    movementCost.Dataroot.MovementCost
