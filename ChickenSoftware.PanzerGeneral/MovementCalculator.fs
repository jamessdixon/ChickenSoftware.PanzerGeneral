module ChickenSoftware.PanzerGeneral.MovementCalculator

open Unit
open Tile
open Terrain
open UnitMapper
open MovementGenerator

let getLandConditionMultiplier (landCondition: LandCondition) (unit:Unit)=
    match landCondition with
    | LandCondition.Dry -> 1
    | LandCondition.Frozen -> 2
    | LandCondition.Muddy -> 2

let getTileMovementPoints (mcs: MovementCostContext.MovementCost array) (unit:Unit) =
    let mc = mcs |> Array.head
    //mc.MovementTypeId
    //mc.TerrainConditionId
    //mc.TerrainTypeId


let IsTargetTileReachable (unit:Unit) =
    let unitMovementPoints = getUnitMovementPoints unit
    let tileMovementPoints = 10
    let pointDifference = unitMovementPoints - tileMovementPoints
    pointDifference >= 0

let possibleMoves (unit:Unit) (tile:BaseTile) (board:BaseTile array)  =
    ()
    //let baseMovementPoints = IsTargetTileReachable unit
    //Tile Points -> kind of terrain + kind of equipment + condition
    //If another unit of same type on tile

    //let baseTile = getBaseTileFromTile tile
    //let movementPoints = getMovementPoints unit
    //let getAdjacentTiles = baseTile.
