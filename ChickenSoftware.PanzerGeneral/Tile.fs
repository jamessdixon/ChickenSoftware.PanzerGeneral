
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

let getBaseTileFromTile (tile:Tile) =
    match tile with
    | Tile.Regular bt -> bt
    | Tile.Victory vt -> vt.BaseTile

