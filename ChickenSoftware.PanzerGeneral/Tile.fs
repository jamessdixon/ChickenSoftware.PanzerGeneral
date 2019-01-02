
module ChickenSoftware.PanzerGeneral.Tile

open Unit
open Nation
open Terrain

type BaseTile = {
    Id: int;
    ColumnNumber: int;
    RowNumber: int;
    Terrain: Terrain; 
    Name: string; 
    Nation: Nation option; 
    EarthUnit: Unit option; 
    SkyUnit: Unit option;
    IsDeployTile: bool;
    IsSupplyTile: bool}

type VictoryTile = {BaseTile:BaseTile; VictoryPoints: int}

type Tile =
| Regular of BaseTile
| Victory of VictoryTile

let getBaseTile (tile:Tile) =
    match tile with
    | Tile.Regular bt -> bt
    | Tile.Victory vt -> vt.BaseTile

let getTileId (tile:Tile) =
    (getBaseTile tile).Id

let getTerrainMovementCost (terrain: Terrain) =
        match terrain with 
        | Terrain.Land l ->
            match l with 
            | Land.Airfield bt -> 1
            | Land.Bocage bt -> 1
            | Land.City bt -> 1
            | Land.Clear bt -> 1
            | Land.Desert bt -> 1
            | Land.Escarpment bt -> 1
            | Land.Forest bt -> 1
            | Land.Fortificaiton bt -> 1
            | Land.Mountain bt -> 1
            | Land.Rough bt -> 1
            | Land.RoughDesert bt -> 1
            | Land.Swamp bt -> 1
        | Terrain.Sea _ -> 1
        | Terrain.Port _ -> 1

let getUnweightedDistance (sourceTile:Tile) (targetTile:Tile) =
    let sourceBaseTile = getBaseTile sourceTile
    let targetBaseTile = getBaseTile targetTile
    let columnDistance = abs(targetBaseTile.ColumnNumber - sourceBaseTile.ColumnNumber)
    let rowDistance = abs(targetBaseTile.RowNumber - sourceBaseTile.RowNumber)
    columnDistance + rowDistance

let getWeightedDistance (unit:Unit) (sourceTile:Tile) (targetTile:Tile) =
    0




