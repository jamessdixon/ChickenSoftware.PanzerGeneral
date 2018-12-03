module ChickenSoftware.PanzerGeneral.Movement

open Tile
open Board
open UnitMapper

let possibleMoves (unit:Unit) (tile:Tile) (board:Tile array) =
    //Movement Points
    //Fuel Remaining
    //Tile Points -> kind of terrain + kind of equipment + condition
    //If another unit of same type on tile

    //let baseTile = getBaseTileFromTile tile
    //let movementPoints = getMovementPoints unit
    //let getAdjacentTiles = baseTile.
