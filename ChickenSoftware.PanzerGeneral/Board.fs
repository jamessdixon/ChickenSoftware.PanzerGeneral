
module ChickenSoftware.PanzerGeneral.Board

open Tile
open Unit
open Nation
open System
open Terrain
open Xamarin
open System.IO
open UnitMapper
open SupportData
open FSharp.Data
open System.Reflection

let getBaseNation (nation: NationContext.Nation option) =
    match nation with
    | Some n -> Some (getNation n.NationId)
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

let isAirUnit (unit:Unit) =
    match unit with 
    | Unit.Combat c -> 
        match c with 
        | Combat.Air ac -> true
        | Combat.Land lc -> false
        | Combat.Naval nc -> false
    | Unit.Transport t -> 
        match t with 
        | Transport.Air at -> true
        | Transport.Land lt -> false
        | Transport.Naval nt -> false

let getUnits (tileId:int) (scenarioUnitData: ScenarioUnitContext.ScenarioUnit array) (equipmentData: EquipmentContext.Equipment array) = 
    let units = 
        scenarioUnitData 
        |> Array.where(fun su -> su.StartingScenarioTileId = tileId)
        |> Array.map(fun su -> getUnit su.ScenarioUnitId su equipmentData)
        |> Array.filter(fun u -> u.IsSome)
        |> Array.map(fun u -> u.Value, isAirUnit u.Value)
    let landUnit = units |> Array.tryFind(fun x -> snd x = false)
    let airUnit =  units |> Array.tryFind(fun x -> snd x = true)
    let landUnit' = 
        match landUnit with 
        | Some lu -> Some (fst lu)
        | None -> None
    let airUnit' = 
        match airUnit with 
        | Some au -> Some (fst au)
        | None -> None
    landUnit', airUnit'

let createBaseTile (scenarioTile: TileContext.ScenarioTile) (landCondition: LandCondition) 
            (tileNameData: TileNameContext.TileName array) (nationData: NationContext.Nation array) 
            (scenarioUnitData: ScenarioUnitContext.ScenarioUnit array) (equipmentData: EquipmentContext.Equipment array) =
    let id = scenarioTile.ScenarioTileId
    let columnNumber = scenarioTile.ColumnNumber
    let rowNumber = scenarioTile.RowNumber
    let terrain = getTerrain scenarioTile landCondition
    let tileName = getTileName scenarioTile tileNameData
    let nation = getNation scenarioTile nationData
    let units = getUnits id scenarioUnitData equipmentData
    let isDeployTile = scenarioTile.DeployTileInd
    let isSupplyTile = scenarioTile.SupplyTileInd
    {Id=id; ColumnNumber=columnNumber; RowNumber=rowNumber;
    Terrain = terrain;Name=tileName;Nation=nation;
    EarthUnit=fst units;SkyUnit=snd units;
    IsDeployTile=isDeployTile; IsSupplyTile = isSupplyTile}

let createTile (scenarioTile: TileContext.ScenarioTile) (landCondition: LandCondition) 
            (tileNameData: TileNameContext.TileName array) (nationData: NationContext.Nation array) 
            (scenarioUnitData: ScenarioUnitContext.ScenarioUnit array) (equipmentData: EquipmentContext.Equipment array)=
    let baseTile = createBaseTile scenarioTile landCondition tileNameData nationData scenarioUnitData equipmentData
    match scenarioTile.VictoryTileInd with 
    | true -> Victory {BaseTile=baseTile; VictoryPoints = 0}
    | false -> Regular baseTile

let createBoard (assembly: Assembly) (scenrioId:int) (landCondition: LandCondition) =
    let scenarioTiles = getTileData scenrioId assembly
    let tileNames = getTileNameData assembly 
    let nations = getNationData assembly
    let scenarioUnits = getUnitData scenrioId assembly
    let equipments = getEquipmentData assembly
    scenarioTiles
    |> Array.map(fun st -> createTile st landCondition tileNames nations scenarioUnits equipments)
