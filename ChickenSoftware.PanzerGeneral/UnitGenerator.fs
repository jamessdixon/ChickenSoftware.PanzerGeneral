
module ChickenSoftware.PanzerGeneral.UnitGenerator

open Unit
open System.IO
open FSharp.Data
open System.Reflection
open EquipmentGenerator

type ScenarioUnitContext = JsonProvider<"Data//Scenario_Unit.json">

let getJson (assembly:Assembly) (fileName:string) =
    let stream = assembly.GetManifestResourceStream(fileName);
    let reader = new StreamReader(stream)
    reader.ReadToEnd()

let getScenarioUnitData (assembly:Assembly) = 
    let json = getJson assembly "scenariounit"
    let scenarioUnit = ScenarioUnitContext.Parse(json)
    scenarioUnit.Dataroot.ScenarioUnit

let getScenarioUnits scenarioId assembly =
    let scenarioUnitData = getScenarioUnitData assembly
    scenarioUnitData |> Array.tryFind(fun ed -> ed.ScenarioId = scenarioId)
    
