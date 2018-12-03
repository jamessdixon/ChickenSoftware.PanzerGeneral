
#r "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/packages/FSharp.Data.3.0.0/lib/net45/FSharp.Data.dll"

#load "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/ChickenSoftware.PanzerGeneral/Unit.fsx"
#load "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/ChickenSoftware.PanzerGeneral/Nation.fsx"
#load "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/ChickenSoftware.PanzerGeneral/Terrain.fsx"
#load "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/ChickenSoftware.PanzerGeneral/Tile.fsx"


open Tile
open Unit
open Nation
open System
open Terrain
open System.IO
open FSharp.Data
open System.Reflection

type TileContext = JsonProvider<"Data//Scenario_Tile.json">
type UnitContext = JsonProvider<"Data//Scenario_Unit.json">
type EquipmentContext = JsonProvider<"Data//Equipment.json">
type TileNameContext = JsonProvider<"Data//TileName.json">
type NationContext = JsonProvider<"Data//Nation.json">

let getJson (assembly:Assembly) (fileName:string) =
    let stream = assembly.GetManifestResourceStream(fileName);
    let reader = new StreamReader(stream)
    reader.ReadToEnd()

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
    let unit = UnitContext.Parse(json)
    unit.Dataroot.ScenarioUnit
    |> Array.filter(fun su -> su.ScenarioId = scenarioId)

let getEquipmentData (assembly:Assembly) = 
    let json = getJson assembly "equipment"
    let equipment = EquipmentContext.Parse(json)
    equipment.Dataroot.Equipment

let getImprovments (roadInd: int) (riverInd: int) =
    match roadInd, riverInd with
    | 1, 1 -> Some Road, Some River
    | 1, 0 -> Some Road, None
    | 0, 1 -> None, Some River
    | _, _ -> None, None

let getLand baseTerrain roadInd riverInd =
    let improvements = getImprovments roadInd riverInd
    match baseTerrain.Id with 
    | 2 -> Land.Rough (baseTerrain, improvements)
    | 3 -> Land.Mountain baseTerrain
    | 4 -> Land.City baseTerrain
    | 5 -> Land.Clear (baseTerrain,improvements)
    | 6 -> Land.Forest baseTerrain
    | 7 -> Land.Swamp baseTerrain
    | 8 -> Land.Airfield baseTerrain
    | 9 -> Land.Fortificaiton (baseTerrain,improvements)
    | 10 -> Land.Bocage (baseTerrain,improvements)
    | 11 -> Land.Desert baseTerrain
    | 12 -> Land.RoughDesert baseTerrain
    | 13 -> Land.Escarpment baseTerrain
    | _ -> Land.Clear (baseTerrain, improvements)

let getBaseTerrain (terrainId: int) (landCondition:LandCondition) (roadInd: int) (riverInd: int)  =
    let baseTerrain = {Id = terrainId; Condition = landCondition}
    match terrainId with 
    | 0 -> Terrain.Sea baseTerrain
    | 1 -> Terrain.Port baseTerrain
    | _ -> Terrain.Land (getLand baseTerrain roadInd riverInd)

let getBaseNation (nation: NationContext.Nation option) =
    match nation with
    | Some n -> 
        match n.NationId with 
        | 3 -> Some (Nation.Axis Bulgaria)
        | 7 -> Some (Nation.Allied France)
        | 8 -> Some (Nation.Axis German)
        | 9 -> Some (Nation.Allied Greece)
        | 10 -> Some (Nation.Allied UnitedStates)
        | 11 -> Some (Nation.Axis Hungary)
        | 13 -> Some (Nation.Axis Italy)
        | 15 -> Some (Nation.Allied Norway)
        | 16 -> Some (Nation.Allied Poland)
        | 18 -> Some (Nation.Axis Romania)
        | 20 -> Some (Nation.Allied SovietUnion)
        | 23 -> Some (Nation.Allied GreatBritian)
        | 24 -> Some (Nation.Allied Yougaslovia)
        | _ -> Some (Nation.Allied OtherAllied)
    | None -> None

let getTerrain (scenarioTile: TileContext.ScenarioTile) (landCondition: LandCondition) = 
    getBaseTerrain scenarioTile.TerrainId landCondition scenarioTile.RoadConnectivityInd scenarioTile.RoadConnectivityInd

let getTileName (scenarioTile: TileContext.ScenarioTile) (tileNameData: TileNameContext.TileName array) = 
        let tileNameDatum = 
            tileNameData |> Array.tryFind(fun tn -> tn.TileNameId = scenarioTile.TileNameId)
        match tileNameDatum with 
        | Some tn -> tn.TileDescription.Value
        | None -> String.Empty

let getNation (scenarioTile: TileContext.ScenarioTile) (nationData: NationContext.Nation array) =
        match scenarioTile.NationId with
        | 0 -> None
        | _ -> nationData
               |> Array.tryFind(fun nd -> nd.NationId = scenarioTile.NationId)
               |> getBaseNation

let getEarthUnit = 
    None

let getSkyUnit =
    None

let getBaseTile (scenarioTile: TileContext.ScenarioTile) (landCondition: LandCondition) 
            (tileNameData: TileNameContext.TileName array) (nationData: NationContext.Nation array) =
    let terrain = getTerrain scenarioTile landCondition
    let tileName = getTileName scenarioTile tileNameData
    let nation = getNation scenarioTile nationData
    let earthUnit = getEarthUnit
    let skyUnit = getSkyUnit
    {Terrain = terrain;Name=tileName;Nation=nation;EarthUnit=earthUnit;SkyUnit=skyUnit}

let getTile (scenarioTile: TileContext.ScenarioTile) (landCondition: LandCondition) 
            (tileNameData: TileNameContext.TileName array) (nationData: NationContext.Nation array) =
    let baseTile = getBaseTile scenarioTile landCondition tileNameData nationData
    match scenarioTile.VictoryTileInd with 
    | true -> Victory {BaseTile=baseTile; VictoryPoints = 0}
    | false -> Regular baseTile

let createBoard (scenrioId:int) (landCondition: LandCondition) =
    let assembly = IntrospectionExtensions.GetTypeInfo(typeof<App>).Assembly
    let scenarioTiles = getTileData scenrioId assembly
    let tileNames = getTileNameData assembly 
    let nations = getNationData assembly
    let nations = getNationData assembly
    let unitData = getUnitData scenrioId assembly
    let equipmentData = getEquipmentData assembly
    scenarioTiles
    |> Array.map(fun st -> getTile st landCondition tileNames nations)




