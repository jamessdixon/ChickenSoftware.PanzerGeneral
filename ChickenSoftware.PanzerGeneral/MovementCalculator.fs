module ChickenSoftware.PanzerGeneral.MovementCalculator

open Unit
open Tile
open Terrain
open Movement
open UnitMapper
open SupportData

let getMovmentCost (movementTypeId:int) (tarrainConditionId:int)
    (terrainTypeId:int) (mcs: MovementCostContext.MovementCost array) =
    mcs |> Array.tryFind(fun mc -> mc.MovementTypeId = movementTypeId &&
                                    mc.TerrainConditionId = tarrainConditionId &&
                                    mc.TerrainTypeId = terrainTypeId)

let getTileMovementPoints (unit:Unit) (baseTile:BaseTile) (landCondition: LandCondition)  
        (mcs: MovementCostContext.MovementCost array)  =
    let movementType = getUnitMovementType unit
    let movementTypeId = getUnitMovementTypeId movementType
    let tarrainConditionId = getLandConditionId landCondition
    let terrainTypeId = getTerrainTypeId baseTile.Terrain
    let movementCost = getMovmentCost movementTypeId tarrainConditionId terrainTypeId mcs
    match movementCost.IsSome with
    | true -> movementCost.Value.MovementPoints
    | false -> 0

let getTilesMovementPoints (unit:Unit) (tiles: Tile array) (landCondition: LandCondition)=
    tiles
    |> Array.map(fun t -> t, getBaseTile t)
    |> Array.map(fun (t,bt) -> t, getTileMovementPoints unit bt landCondition)

let canUnitEnterTile (unit:Unit) (baseTile:BaseTile) (landCondition: LandCondition) 
        (mcs: MovementCostContext.MovementCost array)  =
    let unitMovementPoints = getUnitMovementPoints unit
    let tileMovementPoints = getTileMovementPoints unit baseTile landCondition mcs
    let pointDifference = unitMovementPoints - tileMovementPoints
    pointDifference >= 0
