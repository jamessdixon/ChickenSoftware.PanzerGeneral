module ChickenSoftware.PanzerGeneral.Movement

open Unit
open Tile
open Board
open UnitMapper

let possibleMoves (unit:Unit) (tile:Tile) (board:Tile array) =
    let baseMovementPoints = getAllowedMovementPoints unit
    ()
    //Tile Points -> kind of terrain + kind of equipment + condition
    //If another unit of same type on tile

    //let baseTile = getBaseTileFromTile tile
    //let movementPoints = getMovementPoints unit
    //let getAdjacentTiles = baseTile.
