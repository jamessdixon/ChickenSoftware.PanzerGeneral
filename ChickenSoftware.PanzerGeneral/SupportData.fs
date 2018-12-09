module ChickenSoftware.PanzerGeneral.SupportData

open System.IO
open FSharp.Data
open System.Reflection

let getJson (assembly:Assembly) (fileName:string) =
    let stream = assembly.GetManifestResourceStream(fileName);
    let reader = new StreamReader(stream)
    reader.ReadToEnd()

type TileContext = JsonProvider<"Data//Scenario_Tile.json">
type ScenarioUnitContext = JsonProvider<"Data//Scenario_Unit.json">
type TileNameContext = JsonProvider<"Data//TileName.json">
type NationContext = JsonProvider<"Data//Nation.json">
type EquipmentContext = JsonProvider<"Data//Equipment.json">
type MovementCostContext = JsonProvider<"Data//MovementCost.json">

//Data
let getTileData (scenarioId:int) (assembly:Assembly) =
    let json = getJson assembly "scenariotile"
    let scenarioTile = TileContext.Parse(json)
    scenarioTile.Dataroot.ScenarioTile
    |> Array.filter(fun st -> st.ScenarioId = scenarioId)

let getTileNameData (assembly:Assembly) =
    let json = getJson assembly "tilename"
    let tileName = TileNameContext.Parse(json)
    tileName.Dataroot.TileName

let getNationData (assembly:Assembly) =
    let json = getJson assembly "nation"
    let nations = NationContext.Parse(json)
    nations.Dataroot.Nation

let getUnitData (scenarioId: int) (assembly:Assembly) =
    let json = getJson assembly "scenariounit"
    let scenarioUnit = ScenarioUnitContext.Parse(json)
    scenarioUnit.Dataroot.ScenarioUnit
    |> Array.filter(fun su -> su.ScenarioId = scenarioId)

let getEquipmentData (assembly:Assembly) =
    let json = getJson assembly "equipment"
    let equipments = EquipmentContext.Parse(json)
    equipments.Dataroot.Equipment

let getScenarioUnitData (assembly:Assembly) = 
    let json = getJson assembly "scenariounit"
    let scenarioUnit = ScenarioUnitContext.Parse(json)
    scenarioUnit.Dataroot.ScenarioUnit
    
let getMovementCostData (assembly:Assembly) = 
    let json = getJson assembly "movementcost"
    let movementCost = MovementCostContext.Parse(json)
    movementCost.Dataroot.MovementCost

//Datum
let getEquipmentDatum equipmentId assembly =
    let equipmentData = getEquipmentData assembly
    equipmentData |> Array.tryFind(fun ed -> ed.EquipmentId = equipmentId)

let getScenarioUnitDatum scenarioId assembly =
    let scenarioUnitData = getScenarioUnitData assembly
    scenarioUnitData |> Array.tryFind(fun ed -> ed.ScenarioId = scenarioId)