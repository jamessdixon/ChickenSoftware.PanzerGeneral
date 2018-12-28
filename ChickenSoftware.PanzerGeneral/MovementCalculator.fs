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

let getAdjacentTile (baseTile: BaseTile) (board: Tile array) (rowChange:int) (columnChange:int) =
    board 
    |> Array.map(fun t -> getBaseTile t)
    |> Array.tryFind(fun t -> t.RowNumber = baseTile.RowNumber + rowChange 
                              && t.ColumnNumber = baseTile.ColumnNumber + columnChange)

let getAdjacentTiles (baseTile: BaseTile) getCurrentAdjacentTile = 
        match baseTile.ColumnNumber % 2 with 
        | 0 ->
            let north = getCurrentAdjacentTile -1 0
            let south = getCurrentAdjacentTile 1 0
            let northEast = getCurrentAdjacentTile -1 1
            let southEast = getCurrentAdjacentTile 0 1
            let northWest = getCurrentAdjacentTile -1 -1
            let southWest = getCurrentAdjacentTile 0 -1
            [|north;south;northEast;southEast;northWest;southWest|]
        | _ -> 
            let north = getCurrentAdjacentTile -1 0
            let south = getCurrentAdjacentTile 1 0
            let northEast = getCurrentAdjacentTile 0 1
            let southEast = getCurrentAdjacentTile 1 1
            let northWest = getCurrentAdjacentTile 0 -1
            let southWest = getCurrentAdjacentTile 1 -1
            [|north;south;northEast;southEast;northWest;southWest|]

let getMovableTiles (unit:Unit) (targetTile:Tile) (landCondition: LandCondition) (board: Tile array) =
    let baseTile = getBaseTile targetTile
    let getCurrentAdjacentTile = getAdjacentTile baseTile board 
    let adjacentTiles = getAdjacentTiles baseTile getCurrentAdjacentTile
    adjacentTiles
    |> Array.filter(fun t -> t.IsSome)
    |> Array.map(fun t -> t.Value)
    |> Array.filter(fun t -> t.EarthUnit.IsNone)
    //TODO start here
    //|> Array.filter(fun t -> t.Terrain = Terrain.Land)


