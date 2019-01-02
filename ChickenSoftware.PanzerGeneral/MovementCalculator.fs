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

let getAdjacentTile (board: Tile array) (tile:Tile) (rowChange:int) (columnChange:int) =
    let baseTile = getBaseTile tile
    let found = 
        board 
        |> Array.map(fun t -> t, getBaseTile t)
        |> Array.tryFind(fun (t,bt) -> bt.RowNumber = baseTile.RowNumber + rowChange && bt.ColumnNumber = baseTile.ColumnNumber + columnChange)
    match found with 
    | Some (t,bt) -> Some t
    | Option.None -> Option.None

let getAdjacentTiles (board: Tile array) (tile: Tile) = 
    let baseTile = getBaseTile tile
    match baseTile.ColumnNumber % 2 with 
    | 0 ->
        let north = getAdjacentTile board tile -1 0
        let south = getAdjacentTile board tile 1 0
        let northEast = getAdjacentTile board tile -1 1
        let southEast = getAdjacentTile board tile 0 1
        let northWest = getAdjacentTile board tile -1 -1
        let southWest = getAdjacentTile board tile 0 -1
        [|north;south;northEast;southEast;northWest;southWest|]
    | _ -> 
        let north = getAdjacentTile board tile -1 0
        let south = getAdjacentTile board tile 1 0
        let northEast = getAdjacentTile board tile 0 1
        let southEast = getAdjacentTile board tile 1 1
        let northWest = getAdjacentTile board tile 0 -1
        let southWest = getAdjacentTile board tile 1 -1
        [|north;south;northEast;southEast;northWest;southWest|]

let rec getExtendedAdjacentTiles (accumulator: Tile option array) (board: Tile array) (tile: Tile) (currentDistance:int) (maxDistance: int) =
    let adjacentTiles = getAdjacentTiles board tile
    match currentDistance < maxDistance with 
    | true -> 
            let updatedDistance = currentDistance + 1
            adjacentTiles 
            |> Array.filter(fun t -> t.IsSome)
            |> Array.map(fun t -> t.Value)
            |> Array.map(fun t -> getExtendedAdjacentTiles accumulator board t updatedDistance maxDistance)
            |> Array.reduce(fun acc t -> Array.append acc t)
    | false  ->  
        Array.append accumulator adjacentTiles
        |> Array.distinct

let getMovableTiles (board: Tile array) (landCondition: LandCondition) (tile:Tile) (unit:Unit)  =
    let baseTile = getBaseTile tile
    let maximumDistance = (getUnitMovementPoints unit) - 1
    let accumulator = Array.zeroCreate<Tile option> 0
    let adjacentTiles = getExtendedAdjacentTiles accumulator board tile 0 maximumDistance
    adjacentTiles
    |> Array.filter(fun t -> t.IsSome)
    |> Array.map(fun t -> t.Value)
    |> Array.map(fun t -> t, getBaseTile t)
    |> Array.filter(fun (t,bt) -> bt.EarthUnit.IsNone)
    |> Array.filter(fun (t,bt) -> canLandUnitsEnter(bt.Terrain))
    |> Array.map(fun (t,bt) -> t)
